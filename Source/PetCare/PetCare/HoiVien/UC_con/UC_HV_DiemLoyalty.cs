using PetCare.KhachHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.HoiVien
{
    public partial class UC_HV_DiemLoyalty : UserControl
    {
        ServiceDAL dal = new ServiceDAL();
        public event Action QuayVeTrangChu;

        public UC_HV_DiemLoyalty()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadDiem();
        }

        private void LoadDiem()
        {
            try
            {
                string maKH = SessionData.MaKH;

                int diemHienTai = dal.GetDiemHienTai_HoiVien(maKH);
                textBox_HV_DiemHienTai.Text = diemHienTai.ToString("N0");

                DataTable dtHoaDon = dal.GetLichSuTuHoaDon_Simple(maKH);

                DataTable dtLichSu = new DataTable();
                dtLichSu.Columns.Add("NgayGio", typeof(DateTime));
                dtLichSu.Columns.Add("HoaDon", typeof(string));
                dtLichSu.Columns.Add("MoTa", typeof(string));
                dtLichSu.Columns.Add("DiemCong", typeof(int));
                dtLichSu.Columns.Add("DiemTru", typeof(int));

                dtLichSu.Columns.Add("DiemConLai", typeof(int));

                int soDuChay = diemHienTai;

                if (dtHoaDon != null && dtHoaDon.Rows.Count > 0)
                {
                    foreach (DataRow row in dtHoaDon.Rows)
                    {
                        string trangThai = "";
                        if (dtHoaDon.Columns.Contains("TrangThaiHD"))
                            trangThai = row["TrangThaiHD"].ToString();
                        else if (dtHoaDon.Columns.Contains("TrangThai"))
                            trangThai = row["TrangThai"].ToString();

                        if (!string.IsNullOrEmpty(trangThai) &&
                            !trangThai.Contains("Đã thanh toán") &&
                            !trangThai.Contains("Hoàn tất"))
                            continue;

                        string maHD = row["MaHD"].ToString();

                        DateTime ngayLap = DateTime.Now;
                        if (dtHoaDon.Columns.Contains("NgayLap") && row["NgayLap"] != DBNull.Value)
                            ngayLap = Convert.ToDateTime(row["NgayLap"]);

                        decimal tienThanhToan = 0;
                        if (dtHoaDon.Columns.Contains("TienThanhToan") && row["TienThanhToan"] != DBNull.Value)
                            tienThanhToan = Convert.ToDecimal(row["TienThanhToan"]);
                        else if (dtHoaDon.Columns.Contains("TongTien") && row["TongTien"] != DBNull.Value)
                            tienThanhToan = Convert.ToDecimal(row["TongTien"]);

                        int diemCong = (int)(tienThanhToan / 100000);
                        int diemTru = dal.GetDiemDaDungTrongHoaDon(maHD);

                        if (diemCong == 0 && diemTru == 0) continue;

                        DataRow newRow = dtLichSu.NewRow();
                        newRow["NgayGio"] = ngayLap;
                        newRow["HoaDon"] = maHD;

                        string moTa = "";
                        if (diemTru > 0) moTa = $"Thanh toán (Dùng {diemTru} điểm)";
                        else moTa = $"Tích điểm hóa đơn ({tienThanhToan:N0}đ)";

                        newRow["MoTa"] = moTa;
                        newRow["DiemCong"] = diemCong;
                        newRow["DiemTru"] = diemTru;

                        newRow["DiemConLai"] = soDuChay;

                        soDuChay = soDuChay - diemCong + diemTru;

                        dtLichSu.Rows.Add(newRow);
                    }
                }

                dataGridView_HV_LSDiem.AutoGenerateColumns = false;
                dataGridView_HV_LSDiem.DataSource = dtLichSu;

                if (dataGridView_HV_LSDiem.Columns["NgayGio"] != null) dataGridView_HV_LSDiem.Columns["NgayGio"].DataPropertyName = "NgayGio";
                if (dataGridView_HV_LSDiem.Columns["HoaDon"] != null) dataGridView_HV_LSDiem.Columns["HoaDon"].DataPropertyName = "HoaDon";
                if (dataGridView_HV_LSDiem.Columns["MoTa"] != null) dataGridView_HV_LSDiem.Columns["MoTa"].DataPropertyName = "MoTa";
                if (dataGridView_HV_LSDiem.Columns["DiemCong"] != null) dataGridView_HV_LSDiem.Columns["DiemCong"].DataPropertyName = "DiemCong";
                if (dataGridView_HV_LSDiem.Columns["DiemTru"] != null) dataGridView_HV_LSDiem.Columns["DiemTru"].DataPropertyName = "DiemTru";
                if (dataGridView_HV_LSDiem.Columns["DiemConLai"] != null) dataGridView_HV_LSDiem.Columns["DiemConLai"].DataPropertyName = "DiemConLai";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử điểm: " + ex.Message);
            }
        }

        private void btn_HV_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}