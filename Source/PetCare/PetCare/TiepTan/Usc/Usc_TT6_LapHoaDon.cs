using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PetCare.TiepTan.Class;

namespace PetCare
{
    public partial class Usc_TT6_LapHoaDon : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_TT6_HoaDon> danhSachHDLap = new BindingList<Class_TT6_HoaDon>();

        // Biến cờ để tránh lỗi khi form đang load
        private bool isLoaded = false;

        // Biến lưu thông tin giảm giá
        private decimal tienGiamGia = 0;
        private int diemLoyaltySuDung = 0;

        public Usc_TT6_LapHoaDon()
        {
            InitializeComponent();
            dgv_TT6_HoaDon.AutoGenerateColumns = true;
            dgv_TT6_HoaDon.DataSource = danhSachHDLap;

            // Load dữ liệu khi khởi tạo
            LoadThongTinChung(); // Load NV, CN, MaHD mới
            LoadComboBoxKhachHang();

            isLoaded = true; // Đánh dấu đã load xong
        }

        // --- 1. LOAD THÔNG TIN CHUNG (NV, CN, MaHD Mới) ---
        private void LoadThongTinChung()
        {
            try
            {
                // A. Lấy thông tin Nhân viên và Chi nhánh từ Session
                string maNV = SessionData.MaNV;
                string tenNV = SessionData.TenHienThi;
                string tenCN = SessionData.TenCN;
                //if (string.IsNullOrEmpty(maNV))
                //{
                //    maNV = "NV00002"; // Giả sử NV002 là nhân viên đang trực
                //}
                //if (!string.IsNullOrEmpty(maNV))
                //{
                //    DataTable dtNV = serviceDAL.GetThongTinNhanVienLap(maNV);
                //    if (dtNV.Rows.Count > 0)
                //    {
                //        txtBox_TT6_NVLap.Text = dtNV.Rows[0]["HoTenNV"].ToString(); // Hiển thị tên
                //        txb_TT6_MaCN.Text = dtNV.Rows[0]["TenCN"].ToString();       // Hiển thị tên CN
                //    }
                //}

                txtBox_TT6_NVLap.Text = tenNV;
                txb_TT6_MaCN.Text = tenCN;
                // B. Lấy Mã Hóa Đơn tiếp theo (HD00000023)
                txtBox_TT6_MaHD.Text = serviceDAL.GetNextMaHD();
            }
            catch { /* Bỏ qua lỗi nhỏ UI */ }
        }

        // --- 2. Load ComboBox Khách Hàng ---
        private void LoadComboBoxKhachHang()
        {
            try
            {
                // Tạm thời ngắt sự kiện để tránh lỗi
                cmb_TT6_KH_Ten_SDT.SelectedIndexChanged -= cmb_TT6_KH_Ten_SDT_SelectedIndexChanged;

                DataTable dt = serviceDAL.GetKhachHangChoThanhToan();
                cmb_TT6_KH_Ten_SDT.DataSource = dt;
                cmb_TT6_KH_Ten_SDT.DisplayMember = "DisplayText";
                cmb_TT6_KH_Ten_SDT.ValueMember = "MaKH";

                cmb_TT6_KH_Ten_SDT.SelectedIndex = -1;

                // Gắn lại sự kiện
                cmb_TT6_KH_Ten_SDT.SelectedIndexChanged += cmb_TT6_KH_Ten_SDT_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message);
            }
        }

        // --- 3. Sự kiện chọn Khách Hàng ---
        private void cmb_TT6_KH_Ten_SDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra isLoaded để tránh lỗi khi form mới mở
            if (!isLoaded || cmb_TT6_KH_Ten_SDT.SelectedIndex == -1) return;

            // Kiểm tra giá trị null
            if (cmb_TT6_KH_Ten_SDT.SelectedValue == null) return;

            string maKH = cmb_TT6_KH_Ten_SDT.SelectedValue.ToString();

            // Reset các thông tin giảm giá cũ khi chọn khách mới
            tienGiamGia = 0;
            diemLoyaltySuDung = 0;
            txb_TT6_KM.Text = "0";

            LoadDichVuCuaKhachHang(maKH);
        }

        // --- 4. Hàm Load Dịch Vụ ---
        private void LoadDichVuCuaKhachHang(string maKH)
        {
            try
            {
                DataTable dt = serviceDAL.XemTruocDichVu(maKH);

                if (dt.Rows.Count > 0)
                {
                    txtBox_TT6_MaKHHD.Text = maKH;
                    txtBox_TT6_SDT.Text = dt.Rows[0]["SDT_KH"].ToString();

                    // Reset lại Mã HD mới nhất (đề phòng có người khác vừa tạo HD)
                    txtBox_TT6_MaHD.Text = serviceDAL.GetNextMaHD();

                    danhSachHDLap.Clear();
                    decimal tongCong = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);
                        tongCong += thanhTien;

                        danhSachHDLap.Add(new Class_TT6_HoaDon
                        {
                            MaLSDV = row["MaLSDV"].ToString(),
                            TenDV = row["TenDV"].ToString(),
                            PhiDV = Convert.ToDecimal(row["PhiDV"]),
                            PhiSanPham = Convert.ToDecimal(row["PhiSanPham"]),
                            ThanhTien = thanhTien
                        });
                    }
                    dgv_TT6_HoaDon.Refresh();

                    // Hiển thị tiền
                    txb_TT6_TruocKM.Text = tongCong.ToString("N0"); // Tiền gốc
                    CapNhatTongTienCuoi(); // Tính lại tổng tiền sau khi trừ KM (nếu có)
                }
                else
                {
                    // Trường hợp này ít xảy ra nếu query combobox đúng
                    danhSachHDLap.Clear();
                    txtBox_TT6_TongTien.Text = "0";
                    txb_TT6_TruocKM.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dịch vụ: " + ex.Message);
            }
        }

        // --- HÀM TÍNH TOÁN TIỀN CUỐI CÙNG ---
        private void CapNhatTongTienCuoi()
        {
            decimal tienGoc = 0;
            decimal.TryParse(txb_TT6_TruocKM.Text.Replace(",", "").Replace(".", ""), out tienGoc);

            decimal tienPhaiTra = tienGoc - tienGiamGia;
            if (tienPhaiTra < 0) tienPhaiTra = 0;

            txtBox_TT6_TongTien.Text = tienPhaiTra.ToString("N0");
            txb_TT6_KM.Text = tienGiamGia.ToString("N0");
        }

        // --- 5. NÚT CHI TIẾT KHUYẾN MÃI (LOGIC MỚI) ---
        private void btn_TT6_ChiTietKM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox_TT6_MaKHHD.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước!");
                return;
            }

            try
            {
                // Lấy thông tin hạng và điểm
                //string maKH = txtBox_TT6_MaKHHD.Text;
                if (cmb_TT6_KH_Ten_SDT.SelectedValue == null || cmb_TT6_KH_Ten_SDT.SelectedIndex == -1)
                {
                    return; // Nếu chưa chọn được Mã KH chuẩn thì dừng lại, không gọi SP để tránh lỗi
                }

                // Lấy Mã KH an toàn
                string maKH = cmb_TT6_KH_Ten_SDT.SelectedValue.ToString();
                DataTable dtKM = serviceDAL.GetThongTinKhuyenMaiKH(maKH);

                if (dtKM.Rows.Count > 0)
                {
                    string capDo = dtKM.Rows[0]["CapDo"].ToString();
                    int diemHienCo = dtKM.Rows[0]["DiemLoyalty"] != DBNull.Value ? Convert.ToInt32(dtKM.Rows[0]["DiemLoyalty"]) : 0;

                    decimal tienGoc = 0;
                    decimal.TryParse(txb_TT6_TruocKM.Text.Replace(",", "").Replace(".", ""), out tienGoc);

                    decimal giamGiaHang = 0;
                    string tenHang = "Thường";

                    if (capDo == "VIP") { giamGiaHang = tienGoc * 0.1m; tenHang = "VIP (10%)"; }
                    else if (capDo == "Thân thiết") { giamGiaHang = tienGoc * 0.05m; tenHang = "Thân thiết (5%)"; }
                    else if (capDo == "Cơ bản") { giamGiaHang = tienGoc * 0.03m; tenHang = "Cơ bản (3%)"; }

                    string msg = $"Khách hàng: {capDo}\n" +
                                 $"Điểm tích lũy: {diemHienCo} điểm (1 điểm = 1.000 VNĐ)\n\n" +
                                 $"Giảm giá hạng thành viên ({tenHang}): {giamGiaHang:N0} VNĐ\n\n" +
                                 $"Bạn có muốn sử dụng điểm Loyalty không?";

                    DialogResult dr = MessageBox.Show(msg, "Áp dụng khuyến mãi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    decimal giamGiaDiem = 0;
                    diemLoyaltySuDung = 0;

                    if (dr == DialogResult.Yes)
                    {
                        decimal tienConLai = tienGoc - giamGiaHang;
                        int diemCanDung = (int)(tienConLai / 1000);

                        if (diemHienCo >= diemCanDung)
                        {
                            diemLoyaltySuDung = diemCanDung;
                        }
                        else
                        {
                            diemLoyaltySuDung = diemHienCo;
                        }
                        giamGiaDiem = diemLoyaltySuDung * 1000;
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        return; 
                    }

                    tienGiamGia = giamGiaHang + giamGiaDiem;

                    CapNhatTongTienCuoi();

                    MessageBox.Show($"Đã áp dụng:\n- Hạng {capDo}: -{giamGiaHang:N0}\n- Điểm ({diemLoyaltySuDung}): -{giamGiaDiem:N0}", "Thành công");
                }
                else
                {
                    MessageBox.Show("Khách hàng này chưa đăng ký hội viên nên không có khuyến mãi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính khuyến mãi: " + ex.Message);
            }
        }

        private void btn_TT6_XN_Click(object sender, EventArgs e)
        {
            if (danhSachHDLap.Count == 0) return;

            string maKH = txtBox_TT6_MaKHHD.Text;
            string maNV = SessionData.MaNV;
            string maCN = SessionData.MaCN;

            if (string.IsNullOrEmpty(maNV)) maNV = txtBox_TT6_NVLap.Text.Trim(); // Fallback nếu chưa đăng nhập

            DateTime ngayLap = dtp_TT6_NgayLapHD.Value;
            decimal tongTien = 0;
            decimal.TryParse(txtBox_TT6_TongTien.Text.Replace(",", "").Replace(".", ""), out tongTien);

            if (MessageBox.Show("Xác nhận TẠO HÓA ĐƠN này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string nextMaHD = serviceDAL.GetNextMaHD();

                    string createdMaHD = serviceDAL.LuuHoaDonMoi(maKH, maNV, tongTien, ngayLap, maCN);

                    if (diemLoyaltySuDung > 0)
                    {
                        serviceDAL.TruDiemLoyalty(maKH, diemLoyaltySuDung);
                    }

                    if (!string.IsNullOrEmpty(createdMaHD))
                    {
                        txtBox_TT6_MaHD.Text = createdMaHD;
                        MessageBox.Show($"Lập hóa đơn thành công!\nMã HĐ: {createdMaHD}", "Thành công");

                        LoadComboBoxKhachHang();
                        btn_TT6_Huy_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void btn_TT6_Huy_Click(object sender, EventArgs e)
        {
            danhSachHDLap.Clear();
            dgv_TT6_HoaDon.Refresh();

            txtBox_TT6_MaKHHD.Clear();
            txtBox_TT6_SDT.Clear();
            txtBox_TT6_TongTien.Clear();
            txb_TT6_TruocKM.Clear();
            txb_TT6_KM.Clear();

            tienGiamGia = 0;
            diemLoyaltySuDung = 0;

            LoadThongTinChung();

            cmb_TT6_KH_Ten_SDT.SelectedIndex = -1;
        }

        private void btn_TT6_ChiTietSP_Click(object sender, EventArgs e)
        {
            if (dgv_TT6_HoaDon.CurrentRow == null) return;
            var item = dgv_TT6_HoaDon.CurrentRow.DataBoundItem as Class_TT6_HoaDon;
            if (item != null)
            {
                try
                {
                    DataTable dt = serviceDAL.GetChiTietVatTu(item.MaLSDV);
                    if (dt.Rows.Count > 0)
                    {
                        Frm_TT6_ChiTiet frm = new Frm_TT6_ChiTiet("Chi tiết - " + item.MaLSDV, dt);
                        frm.ShowDialog();
                    }
                    else MessageBox.Show("Dịch vụ này không dùng thêm thuốc/vật tư.");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btn_TT6_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        private void btn_TT6_TimMaHD_Click(object sender, EventArgs e) { }
        private void txtBox_TT6_MaHD_TextChanged(object sender, EventArgs e) { }
        private void txtBox_TT6_MaKHHD_TextChanged(object sender, EventArgs e) { }
        private void txtBox_TT6_SDT_TextChanged(object sender, EventArgs e) { }
        private void txb_TT6_TruocKM_TextChanged(object sender, EventArgs e) { }
        private void txb_TT6_KM_TextChanged(object sender, EventArgs e) { }
        private void txb_TT6_HinhThuc_TextChanged(object sender, EventArgs e) { }
        private void txtBox_TT6_NVLap_TextChanged(object sender, EventArgs e) { }
        private void txtBox_TT6_TongTien_TextChanged(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void txb_TT6_MaCN_TextChanged(object sender, EventArgs e) { }
        private void dgv_TT6_HoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtBox_TT6_MaKH_TextChanged(object sender, EventArgs e) { }
        private void btn_TT6_Tim_Click(object sender, EventArgs e) { } 
    }
}