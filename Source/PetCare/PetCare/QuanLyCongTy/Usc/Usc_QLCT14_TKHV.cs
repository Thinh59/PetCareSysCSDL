using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT14_TKHV : UserControl
    {
        private BindingList<Class_QLCT14_TKHV> danhSach = new BindingList<Class_QLCT14_TKHV>();
        private ServiceDAL service = new ServiceDAL();

        public Usc_QLCT14_TKHV()
        {
            InitializeComponent();
            dgv_QLCT14_TKHV.DataSource = danhSach;
        }

        private void Usc_QLCT14_TKHV_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadStatistics();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = service.ThongKeHoiVien();
                danhSach.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    Class_QLCT14_TKHV hv = new Class_QLCT14_TKHV
                    {
                        MaKH = row["MaKH"].ToString(),
                        HoTen_KH = row["HoTen_KH"].ToString(),
                        SDT_KH = row["SDT_KH"].ToString(),
                        NgaySinh_KH = Convert.ToDateTime(row["NgaySinh_KH"]),
                        GioiTinh_KH = row["GioiTinh_KH"].ToString(),
                        Email_KH = row["Email_KH"].ToString(),
                        Loai_KH = row["Loai_KH"].ToString(),
                        TenDangNhap = row["TenDangNhap"].ToString(),
                        CapDo = row["CapDo"] != DBNull.Value ? row["CapDo"].ToString() : "",
                        DiemLoyalty = row["DiemLoyalty"] != DBNull.Value ? Convert.ToInt32(row["DiemLoyalty"]) : 0,
                        TongChiTieu = row["TongChiTieu"] != DBNull.Value ? Convert.ToInt32(row["TongChiTieu"]) : 0
                    };

                    danhSach.Add(hv);
                }

                dgv_QLCT14_TKHV.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hội viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                int totalMembers = service.GetTongSoLuongHoiVien();

                int avgSpending = service.GetChiTieuTrungBinhHoiVien();
                txb_QLCT14_CTTB.Text = string.Format("{0:n0} VNĐ", avgSpending);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnl_QLCT14_TKKH_Paint(object sender, PaintEventArgs e) { }
        private void txb_QLCT14_SLHV_TextChanged(object sender, EventArgs e) { }
        private void dgv_QLCT14_TKHV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txb_QLCT14_CTTB_TextChanged(object sender, EventArgs e) { }
    }
}