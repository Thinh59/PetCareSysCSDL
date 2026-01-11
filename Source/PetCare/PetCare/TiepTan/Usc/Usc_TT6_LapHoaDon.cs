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

        private bool isLoaded = false;

        private decimal tienGiamGia = 0;
        private int diemLoyaltySuDung = 0;

        public Usc_TT6_LapHoaDon()
        {
            InitializeComponent();
            dgv_TT6_HoaDon.AutoGenerateColumns = true;
            dgv_TT6_HoaDon.DataSource = danhSachHDLap;

            LoadThongTinChung(); 
            LoadComboBoxKhachHang();

            isLoaded = true; 
        }

        private void LoadThongTinChung()
        {
            try
            {
             
                string maNV = SessionData.MaNV;
                string tenNV = SessionData.TenHienThi;
                string tenCN = SessionData.TenCN;
                

                txtBox_TT6_NVLap.Text = tenNV;
                txb_TT6_MaCN.Text = tenCN;
                txtBox_TT6_MaHD.Text = serviceDAL.GetNextMaHD();
            }
            catch { }
        }
        private void LoadComboBoxKhachHang()
        {
            try
            {
                cmb_TT6_KH_Ten_SDT.SelectedIndexChanged -= cmb_TT6_KH_Ten_SDT_SelectedIndexChanged;

                DataTable dt = serviceDAL.GetKhachHangChoThanhToan();
                cmb_TT6_KH_Ten_SDT.DataSource = dt;
                cmb_TT6_KH_Ten_SDT.DisplayMember = "DisplayText";
                cmb_TT6_KH_Ten_SDT.ValueMember = "MaKH";

                cmb_TT6_KH_Ten_SDT.SelectedIndex = -1;

                cmb_TT6_KH_Ten_SDT.SelectedIndexChanged += cmb_TT6_KH_Ten_SDT_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message);
            }
        }

        private void cmb_TT6_KH_Ten_SDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoaded || cmb_TT6_KH_Ten_SDT.SelectedIndex == -1) return;

            if (cmb_TT6_KH_Ten_SDT.SelectedValue == null) return;

            string maKH = cmb_TT6_KH_Ten_SDT.SelectedValue.ToString();

            tienGiamGia = 0;
            diemLoyaltySuDung = 0;
            txb_TT6_KM.Text = "0";

            LoadDichVuCuaKhachHang(maKH);
        }

        private void LoadDichVuCuaKhachHang(string maKH)
        {
            try
            {
                DataTable dt = serviceDAL.XemTruocDichVu(maKH);

                if (dt.Rows.Count > 0)
                {
                    txtBox_TT6_MaKHHD.Text = maKH;
                    txtBox_TT6_SDT.Text = dt.Rows[0]["SDT_KH"].ToString();

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

                    txb_TT6_TruocKM.Text = tongCong.ToString("N0");
                    CapNhatTongTienCuoi(); 
                }
                else
                {
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

        private void CapNhatTongTienCuoi()
        {
            decimal tienGoc = 0;
            decimal.TryParse(txb_TT6_TruocKM.Text.Replace(",", "").Replace(".", ""), out tienGoc);

            decimal tienPhaiTra = tienGoc - tienGiamGia;
            if (tienPhaiTra < 0) tienPhaiTra = 0;

            txtBox_TT6_TongTien.Text = tienPhaiTra.ToString("N0");
            txb_TT6_KM.Text = tienGiamGia.ToString("N0");
        }

        private void btn_TT6_ChiTietKM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox_TT6_MaKHHD.Text) || cmb_TT6_KH_Ten_SDT.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và có dịch vụ để thanh toán!");
                return;
            }

            try
            {
                string maKH = cmb_TT6_KH_Ten_SDT.SelectedValue.ToString();

                decimal tienGoc = 0;
                decimal.TryParse(txb_TT6_TruocKM.Text.Replace(",", "").Replace(".", ""), out tienGoc);

                if (tienGoc <= 0)
                {
                    MessageBox.Show("Hóa đơn bằng 0, không cần áp dụng khuyến mãi.");
                    return;
                }

                DataTable dtKM = serviceDAL.GetThongTinKhuyenMaiKH(maKH);

                int diemHienCo = 0;
                string capDo = "Thường";

                if (dtKM.Rows.Count > 0)
                {
                    capDo = dtKM.Rows[0]["CapDo"].ToString();
                    diemHienCo = dtKM.Rows[0]["DiemLoyalty"] != DBNull.Value ? Convert.ToInt32(dtKM.Rows[0]["DiemLoyalty"]) : 0;
                }

                Frm_TT6_ChonKhuyenMai frmKM = new Frm_TT6_ChonKhuyenMai(maKH, diemHienCo, tienGoc, capDo);

                if (frmKM.ShowDialog() == DialogResult.OK)
                {
                    this.tienGiamGia = frmKM.TongTienGiam;      
                    this.diemLoyaltySuDung = frmKM.DiemDaDung;  

                    CapNhatTongTienCuoi();

                    MessageBox.Show($"Đã áp dụng khuyến mãi!\n- Tổng giảm: {this.tienGiamGia:N0} VNĐ\n- Điểm sử dụng: {this.diemLoyaltySuDung}", "Thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở chi tiết khuyến mãi: " + ex.Message);
            }
        }

        private void btn_TT6_XN_Click(object sender, EventArgs e)
        {
            if (danhSachHDLap.Count == 0) return;

            string maKH = txtBox_TT6_MaKHHD.Text;
            string maNV = SessionData.MaNV;
            if (string.IsNullOrEmpty(maNV)) maNV = txtBox_TT6_NVLap.Text.Trim();
            string maCN = SessionData.MaCN;
            DateTime ngayLap = dtp_TT6_NgayLapHD.Value;

            decimal tienGoc = 0; 
            decimal.TryParse(txb_TT6_TruocKM.Text.Replace(",", "").Replace(".", ""), out tienGoc);

            decimal tienThanhToan = 0; 
            decimal.TryParse(txtBox_TT6_TongTien.Text.Replace(",", "").Replace(".", ""), out tienThanhToan);

            if (MessageBox.Show("Xác nhận TẠO HÓA ĐƠN này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string createdMaHD = serviceDAL.LuuHoaDonMoi(maKH, maNV, tienGoc, ngayLap, maCN);

                    if (!string.IsNullOrEmpty(createdMaHD))
                    {
                        string updateSql = "UPDATE HOADON SET TienThanhToan = @TienThucTra WHERE MaHD = @MaHD";
                        serviceDAL.ExecuteNonQuery(updateSql, new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@TienThucTra", tienThanhToan),
                    new System.Data.SqlClient.SqlParameter("@MaHD", createdMaHD)
                });

                        DataTable dtHV = serviceDAL.GetThongTinKhuyenMaiKH(maKH);
                        string capDo = "Thường";
                        if (dtHV.Rows.Count > 0) capDo = dtHV.Rows[0]["CapDo"].ToString();

                        string maKM_Hang = "";
                        decimal tienGiamHang = 0;

                        if (capDo == "VIP") { maKM_Hang = "HVVIP"; tienGiamHang = tienGoc * 0.10m; }
                        else if (capDo == "Thân thiết") { maKM_Hang = "HVThanThiet"; tienGiamHang = tienGoc * 0.05m; }
                        else if (capDo == "Cơ bản") { maKM_Hang = "KM003"; tienGiamHang = tienGoc * 0.03m; } // Bạn nói mã 3% là KM003

                        if (!string.IsNullOrEmpty(maKM_Hang) && tienGiamHang > 0)
                        {
                            serviceDAL.LuuChiTietKhuyenMai(createdMaHD, maKM_Hang, 1, tienGiamHang);
                        }

                        if (diemLoyaltySuDung > 0)
                        {
                            decimal tienGiamTuDiem = diemLoyaltySuDung * 1000;
                            serviceDAL.LuuChiTietKhuyenMai(createdMaHD, "KM004", diemLoyaltySuDung, tienGiamTuDiem);
                        }

                        if (dtHV.Rows.Count > 0)
                        {
                            int diemHienCo = serviceDAL.GetDiemHienTai_HoiVien(maKH);
                            int diemCong = (int)(tienThanhToan / 100000);

                            int diemMoi = (diemHienCo - diemLoyaltySuDung) + diemCong;
                            if (diemMoi < 0) diemMoi = 0;

                            serviceDAL.CapNhatDiem_HoiVien(maKH, diemMoi);
                        }

                        txtBox_TT6_MaHD.Text = createdMaHD;

                        string msg = $"Lập hóa đơn thành công!\nMã HĐ: {createdMaHD}\n\n";
                        if (tienGiamHang > 0) msg += $"- Hạng {capDo}: -{tienGiamHang:N0} VNĐ\n";
                        if (diemLoyaltySuDung > 0) msg += $"- Điểm ({diemLoyaltySuDung}): -{diemLoyaltySuDung * 1000:N0} VNĐ\n";
                        msg += $"\nTổng thu: {tienThanhToan:N0} VNĐ";

                        MessageBox.Show(msg, "Thành công");

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