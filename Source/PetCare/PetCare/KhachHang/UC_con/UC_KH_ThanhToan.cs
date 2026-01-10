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

        ServiceDAL dal = new ServiceDAL();

        public UC_KH_ThanhToan(string maLSGD, int tongTien)
        {
            InitializeComponent();
            this._maLSGD = maLSGD;
            this._tongTienGocTuDB = tongTien; 

            this.Load += (s, e) => LoadChiTietVaTinhTien();
        }

        private string loaiKhachHang = "";


        private void LoadChiTietVaTinhTien()
        {
            try
            {
                dataGridView_KH_CTHD.DataSource = null;
                dataGridView_KH_CTHD.Columns.Clear();

                if (string.IsNullOrEmpty(_maLSGD) || _maLSGD.StartsWith("MH"))
                {
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
                    _tongTienHienTai = _tongTienGocTuDB;

                    DataTable dtFake = new DataTable();
                    dtFake.Columns.Add("SanPham");
                    dtFake.Columns.Add("DonGia");
                    dtFake.Columns.Add("SoLuong");
                    dtFake.Columns.Add("ThanhTien");

                    string tenHienThi = "Thanh toán GD: " + _maLSGD;
                    dtFake.Rows.Add(tenHienThi, _tongTienHienTai, 1, _tongTienHienTai);

                    dataGridView_KH_CTHD.AutoGenerateColumns = false;
                    dataGridView_KH_CTHD.DataSource = dtFake;

                    EnsureColumnExists(dataGridView_KH_CTHD, "SanPham", "Nội dung", "SanPham");
                    EnsureColumnExists(dataGridView_KH_CTHD, "DonGia", "Đơn giá", "DonGia");
                    EnsureColumnExists(dataGridView_KH_CTHD, "SoLuong", "SL", "SoLuong");
                    EnsureColumnExists(dataGridView_KH_CTHD, "ThanhTien", "Thành tiền", "ThanhTien");
                }

                textBox_TienTruocKM.Text = _tongTienHienTai.ToString("N0");

                loaiKhachHang = dal.GetLoaiKhachHang(SessionData.MaKH);
                if (loaiKhachHang != "Hội viên")
                {
                    textBox_KH_DiemLoyalty.Enabled = false;
                    textBox_KH_DiemLoyalty.Text = "0";
                }

                TinhToanTongTien();
                LoadKhuyenMai();
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
            else
            {
                dgv.Columns[name].DataPropertyName = dataProp; 
            }
        }

        private void LoadKhuyenMai()
        {
            DataTable dtKM = dal.GetDanhSachKhuyenMai(SessionData.MaKH);

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
        private void ComboBox_CTKM_SelectedIndexChanged(object sender, EventArgs e) => TinhToanTongTien();
        private void TextBox_KH_DiemLoyalty_TextChanged(object sender, EventArgs e) => TinhToanTongTien();

        private void TinhToanTongTien()
        {
            double tongSauKM = _tongTienHienTai;
            double tienGiamVoucher = 0;

            if (comboBox_CTKM.SelectedItem is DataRowView row)
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

            if (int.TryParse(textBox_KH_DiemLoyalty.Text, out int diem))
            {
                if (diem < 0) { textBox_KH_DiemLoyalty.Text = "0"; diem = 0; }

                double tienLoyalty = (diem * 1000);
                if (tienLoyalty > tongSauKM) tienLoyalty = tongSauKM;

                tongSauKM -= tienLoyalty;
            }

            textBox_KH_TongTien.Text = (tongSauKM < 0 ? 0 : tongSauKM).ToString("N0");
        }

        private void btn_KH_XacNhanTT_Click(object sender, EventArgs e)
        {
            try
            {
                string hinhThuc = comboBox_KH_HTTT.Text;
                string maCN = SessionData.MaCN_DangChon;
                string maHD = "";


                bool isThanhToanGioHang = _maLSGD.StartsWith("MH");

                if (isThanhToanGioHang)
                {
                    maHD = dal.ThanhToanGioHang(SessionData.MaKH, _maLSGD, hinhThuc, _tongTienHienTai, maCN);
                }
                else
                {
                    int diemDung = 0;
                    int.TryParse(textBox_KH_DiemLoyalty.Text, out diemDung);
                    string maKM = comboBox_CTKM.SelectedValue?.ToString();

                    maHD = dal.ThanhToanHoaDon(SessionData.MaKH, _maLSGD, hinhThuc, diemDung, maCN, maKM);
                }

                MessageBox.Show($"Thanh toán thành công! Mã hóa đơn: {maHD}", "Thông báo");
                QuayVeTrangChu?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
