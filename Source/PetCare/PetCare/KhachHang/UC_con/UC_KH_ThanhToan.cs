using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang
{
    public partial class UC_KH_ThanhToan : UserControl, IReturnToMainPage
    {
        private string _maLSGD;
        private int _tongTienHienTai = 0;
        private int _tongTienGocTuDB = 0;
        private int _diemDaApDungTuTruoc = 0;

        ServiceDAL dal = new ServiceDAL();

        public event Action QuayVeTrangChu;

        public UC_KH_ThanhToan(string maLSGD, int tongTien)
        {
            InitializeComponent();
            this._maLSGD = maLSGD;
            this._tongTienGocTuDB = tongTien;
            this.Load += (s, e) => LoadChiTietVaTinhTien();
        }

        private string loaiKhachHang = "";

        private void SetVisibility(bool isVisible)
        {
            if (label_CTKM != null) label_CTKM.Visible = isVisible;
            comboBox_CTKM.Visible = isVisible;

            if (lb_KH_DiemLoyalty != null) lb_KH_DiemLoyalty.Visible = isVisible;
            textBox_KH_DiemLoyalty.Visible = isVisible;

            textBox_KH_DiemLoyalty.Enabled = true;

            if (lb_KH_HTTT != null) lb_KH_HTTT.Visible = isVisible;
            comboBox_KH_HTTT.Visible = isVisible;

            if (!isVisible)
            {
                textBox_KH_DiemLoyalty.Text = "0";
                if (comboBox_CTKM.Items.Count > 0) comboBox_CTKM.SelectedIndex = 0;
                comboBox_KH_HTTT.SelectedIndex = -1;
            }
        }

        private void LoadChiTietVaTinhTien()
        {
            try
            {
                dataGridView_KH_CTHD.DataSource = null;
                dataGridView_KH_CTHD.Columns.Clear();

                bool isGioHang = string.IsNullOrEmpty(_maLSGD) || _maLSGD.StartsWith("MH");

                if (isGioHang)
                {
                    SetVisibility(true);
                    _diemDaApDungTuTruoc = 0; 

                    DataTable dt = dal.GetChiTietGioHang(SessionData.MaKH, SessionData.MaCN_DangChon);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataGridView_KH_CTHD.AutoGenerateColumns = false;
                        dataGridView_KH_CTHD.DataSource = dt;
                        EnsureColumnExists(dataGridView_KH_CTHD, "SanPham", "Sản phẩm", "SanPham");
                        EnsureColumnExists(dataGridView_KH_CTHD, "DonGia", "Đơn giá", "DonGia");
                        EnsureColumnExists(dataGridView_KH_CTHD, "SoLuong", "Số lượng", "SoLuong");
                        EnsureColumnExists(dataGridView_KH_CTHD, "ThanhTien", "Thành tiền", "ThanhTien");

                        if (_tongTienGocTuDB > 0) _tongTienHienTai = _tongTienGocTuDB;
                    }
                }
                else
                {
                    SetVisibility(false);
                    _diemDaApDungTuTruoc = dal.GetDiemDaSuDungChoGiaoDich(_maLSGD);

                    if (_diemDaApDungTuTruoc > 0)
                    {
                        if (lb_KH_DiemLoyalty != null) lb_KH_DiemLoyalty.Visible = true;
                        textBox_KH_DiemLoyalty.Visible = true;
                        textBox_KH_DiemLoyalty.Enabled = false; 
                        textBox_KH_DiemLoyalty.Text = _diemDaApDungTuTruoc.ToString();

                        _tongTienHienTai = _tongTienGocTuDB - (_diemDaApDungTuTruoc * 1000);
                    }
                    else
                    {
                        _tongTienHienTai = _tongTienGocTuDB;
                    }

                    DataTable dtFake = new DataTable();
                    dtFake.Columns.Add("SanPham");
                    dtFake.Columns.Add("DonGia");
                    dtFake.Columns.Add("SoLuong");
                    dtFake.Columns.Add("ThanhTien");
                    dtFake.Rows.Add("Thanh toán GD: " + _maLSGD, _tongTienHienTai, 1, _tongTienHienTai);

                    dataGridView_KH_CTHD.AutoGenerateColumns = false;
                    dataGridView_KH_CTHD.DataSource = dtFake;
                    EnsureColumnExists(dataGridView_KH_CTHD, "SanPham", "Nội dung", "SanPham");
                    EnsureColumnExists(dataGridView_KH_CTHD, "ThanhTien", "Thành tiền", "ThanhTien");
                }

                textBox_TienTruocKM.Text = _tongTienGocTuDB.ToString("N0");

                loaiKhachHang = dal.GetLoaiKhachHang(SessionData.MaKH);
                if (textBox_KH_DiemLoyalty.Visible && textBox_KH_DiemLoyalty.Enabled)
                {
                    if (loaiKhachHang != "Hội viên")
                    {
                        textBox_KH_DiemLoyalty.Enabled = false;
                        textBox_KH_DiemLoyalty.Text = "0";
                    }
                }

                LoadKhuyenMai();
                TinhToanTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        private void EnsureColumnExists(DataGridView dgv, string name, string header, string dataProp)
        {
            if (!dgv.Columns.Contains(name))
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = name;
                col.HeaderText = header;
                col.DataPropertyName = dataProp;
                dgv.Columns.Add(col);
            }
            else dgv.Columns[name].DataPropertyName = dataProp;
        }

        private void LoadKhuyenMai()
        {
            try
            {
                DataTable dtKM = dal.GetDanhSachKhuyenMai(SessionData.MaKH);
                string capDo = GetCapDoHoiVien(SessionData.MaKH);
                List<DataRow> rowsToRemove = new List<DataRow>();

                foreach (DataRow row in dtKM.Rows)
                {
                    string loaiKM = row["LoaiKM"].ToString().Trim();
                    if ((loaiKM == "HVVIP" && capDo != "VIP") ||
                        (loaiKM == "HVThanThiet" && capDo != "Thân thiết") ||
                        (loaiKM == "HVCoBan" && capDo != "Cơ bản"))
                    {
                        rowsToRemove.Add(row);
                    }
                }
                foreach (DataRow row in rowsToRemove) dtKM.Rows.Remove(row);

                DataRow dr = dtKM.NewRow();
                dr["MaKM"] = DBNull.Value;
                dr["LoaiKM"] = "None";
                dr["GiaTriGoc"] = 0;
                dr["HienThi"] = "-- Không chọn chương trình khuyến mãi --";
                dtKM.Rows.InsertAt(dr, 0);

                comboBox_CTKM.DataSource = dtKM;
                comboBox_CTKM.DisplayMember = "HienThi";
                comboBox_CTKM.ValueMember = "MaKM";
                comboBox_CTKM.SelectedIndex = 0;

                comboBox_CTKM.SelectedIndexChanged -= ComboBox_CTKM_SelectedIndexChanged;
                comboBox_CTKM.SelectedIndexChanged += ComboBox_CTKM_SelectedIndexChanged;
                textBox_KH_DiemLoyalty.TextChanged -= TextBox_KH_DiemLoyalty_TextChanged;
                textBox_KH_DiemLoyalty.TextChanged += TextBox_KH_DiemLoyalty_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khuyến mãi: " + ex.Message);
            }
        }

        private string GetCapDoHoiVien(string maKH)
        {
            try
            {
                DataTable dt = dal.GetThongTinHoiVien(maKH);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("Cấp độ hiện tại")) return dt.Rows[0]["Cấp độ hiện tại"].ToString();
                    if (dt.Columns.Contains("CapDo")) return dt.Rows[0]["CapDo"].ToString();
                }
            }
            catch { }
            return "Thường";
        }

        private void ComboBox_CTKM_SelectedIndexChanged(object sender, EventArgs e) => TinhToanTongTien();
        private void TextBox_KH_DiemLoyalty_TextChanged(object sender, EventArgs e) => TinhToanTongTien();

        private void TinhToanTongTien()
        {
            double tongSauKM = _tongTienHienTai;

            if (_diemDaApDungTuTruoc > 0 && !textBox_KH_DiemLoyalty.Enabled)
            {
            }
            else
            {
                double tienGiamVoucher = 0;
                if (comboBox_CTKM.Visible && comboBox_CTKM.SelectedItem is DataRowView row)
                {
                    string loai = row["LoaiKM"].ToString().Trim();
                    if (loai != "None")
                    {
                        int giaTri = Convert.ToInt32(row["GiaTriGoc"]);
                        if (loai.StartsWith("HV") || loai.Contains("%") || loai.Contains("Phần trăm"))
                            tienGiamVoucher = (_tongTienHienTai * giaTri / 100.0);
                        else
                            tienGiamVoucher = giaTri;
                    }
                }
                tongSauKM -= tienGiamVoucher;

                if (textBox_KH_DiemLoyalty.Visible && textBox_KH_DiemLoyalty.Enabled
                    && int.TryParse(textBox_KH_DiemLoyalty.Text, out int diem))
                {
                    if (diem < 0) { textBox_KH_DiemLoyalty.Text = "0"; diem = 0; }
                    double tienLoyalty = (diem * 1000);
                    if (tienLoyalty > tongSauKM) tienLoyalty = tongSauKM;
                    tongSauKM -= tienLoyalty;
                }
            }

            textBox_KH_TongTien.Text = (tongSauKM < 0 ? 0 : tongSauKM).ToString("N0");
        }

        private void btn_KH_XacNhanTT_Click(object sender, EventArgs e)
        {
            try
            {
                string hinhThuc = comboBox_KH_HTTT.Text;
                string maCN = SessionData.MaCN_DangChon;

                bool isGioHang = string.IsNullOrEmpty(_maLSGD) || _maLSGD.StartsWith("MH");

                int diemSuDungThucTe = 0;
                if (isGioHang && textBox_KH_DiemLoyalty.Visible)
                    int.TryParse(textBox_KH_DiemLoyalty.Text, out diemSuDungThucTe);
                else if (!isGioHang)
                    diemSuDungThucTe = _diemDaApDungTuTruoc;

                int tienGoc = _tongTienGocTuDB;

                decimal tienThucTra = 0;
                decimal.TryParse(textBox_KH_TongTien.Text.Replace(",", "").Replace(".", ""), out tienThucTra);

                string maHD = "";
                if (isGioHang)
                {
                    maHD = dal.ThanhToanGioHang(SessionData.MaKH, _maLSGD, hinhThuc, tienGoc, maCN);
                }
                else
                {
                    maHD = dal.ThanhToanHoaDon(SessionData.MaKH, _maLSGD, hinhThuc, 0, maCN, null);
                }

                if (!string.IsNullOrEmpty(maHD))
                {
                    string updateSql = "UPDATE HOADON SET TienThanhToan = @TienThuc WHERE MaHD = @MaHD";
                    dal.ExecuteNonQuery(updateSql, new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@TienThuc", tienThucTra),
                new System.Data.SqlClient.SqlParameter("@MaHD", maHD)
            });

                    string loaiKH = dal.GetLoaiKhachHang(SessionData.MaKH);
                    if (loaiKH == "Hội viên")
                    {
                        int diemHienCo = dal.GetDiemHienTai_HoiVien(SessionData.MaKH);
                        int diemCong = (int)(tienThucTra / 100000);
                        int diemMoi = 0;

                        if (isGioHang)
                        {

                            if (diemSuDungThucTe > 0)
                                dal.LuuChiTietKhuyenMai(maHD, "KM004", diemSuDungThucTe, diemSuDungThucTe * 1000);

                            if (comboBox_CTKM.SelectedIndex > 0 && comboBox_CTKM.SelectedValue != DBNull.Value)
                            {
                                string maKM = comboBox_CTKM.SelectedValue.ToString();
                                decimal tienGiam = (decimal)tienGoc - (tienThucTra + (diemSuDungThucTe * 1000));
                                if (tienGiam > 0) dal.LuuChiTietKhuyenMai(maHD, maKM, 1, tienGiam);
                            }


                            diemMoi = (diemHienCo - diemSuDungThucTe) + diemCong;
                        }
                        else
                        {
                            diemMoi = diemHienCo + diemCong;
                        }

                        if (diemMoi < 0) diemMoi = 0;
                        dal.CapNhatDiem_HoiVien(SessionData.MaKH, diemMoi);

                        string msg = $"Thanh toán thành công!\nMã HĐ: {maHD}\n";
                        if (isGioHang) msg += $"- Điểm dùng: {diemSuDungThucTe}\n";
                        msg += $"- Điểm cộng: {diemCong}\n=> Điểm còn lại: {diemMoi}";

                        MessageBox.Show(msg, "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show($"Thanh toán thành công!\nMã HĐ: {maHD}", "Thông báo");
                    }

                    QuayVeTrangChu?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }

        private void btn_KH_QuayLai_Click(object sender, EventArgs e) => QuayVeTrangChu?.Invoke();
        private void label_TienTruocKM_Click(object sender, EventArgs e) { }
        private void label_CTKM_Click(object sender, EventArgs e) { }
        private void lb_KH_DiemLoyalty_Click(object sender, EventArgs e) { }
        private void lb_TongTien_Click(object sender, EventArgs e) { }
        private void lb_KH_HTTT_Click(object sender, EventArgs e) { }
    }
}