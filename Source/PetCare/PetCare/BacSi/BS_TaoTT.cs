using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class BS_TaoTT : UserControl
    {
        DataConnection dc = new DataConnection();
        decimal tongTienToa = 0;

        public BS_TaoTT()
        {
            InitializeComponent();
            data_BS_TTT_DST.AutoGenerateColumns = false;
            this.Load += BS_TaoTT_Load;
        }

        private void BS_TaoTT_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BS_QLHSKB.CurrentMaLSKB))
            {
                LoadThongTinThuCung(BS_QLHSKB.CurrentMaLSKB);
            }
        }

        private void LoadThongTinThuCung(string maLS)
        {
            try
            {
                SqlParameter[] p = { new SqlParameter("@MaLSKB", maLS) };
                DataTable dt = dc.ExecuteProcedure("sp_GetChiTietHoSo", p);
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    tbox_BS_TTT_MaTC.Text = r["MaThuCung"].ToString();
                    tbox_BS_TTT_MaKH.Text = r["HoTen_KH"].ToString();
                    tbox_TTT_Loai.Text = r["LoaiThuCung"].ToString();
                    tbox_TTT_Tuoi.Text = r["Tuoi"].ToString();
                    tbox_TTT_CN.Text = r["TinhTrangSK"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load thông tin: " + ex.Message); }
        }

        private void tbox_TTT_MaThuoc_TextChanged(object sender, EventArgs e)
        {
        }


        private void lab_TTT_LD_Click(object sender, EventArgs e)
        {

        }

        private void tbox_BS_TTT_MaTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_TTT_MaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_TTT_Loai_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_TTT_Tuoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_TTT_CN_TextChanged(object sender, EventArgs e)
        {

        }

        private void data_BS_TTT_DST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbox_BS_TTT_ThanhTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_TTT_SL_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_TTT_LD_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_BS_ThemTT_Click(object sender, EventArgs e)
        {
            string maThuoc = tbox_TTT_MaThuoc.Text.Trim();
            string slString = tbox_BS_TTT_SL.Text.Trim();
            string lieuDung = tbox_BS_TTT_LD.Text.Trim();

            if (string.IsNullOrEmpty(maThuoc) || string.IsNullOrEmpty(slString))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã thuốc và Số lượng!");
                return;
            }

            try
            {
                SqlParameter[] p = {
                    new SqlParameter("@MaThuoc", maThuoc),
                    new SqlParameter("@MaLSDV", BS_QLHSKB.CurrentMaLSKB)
                };
                DataTable dt = dc.ExecuteProcedure("sp_GetThongTinThuoc", p);

                if (dt.Rows.Count > 0)
                {
                    string tenThuoc = dt.Rows[0]["TenSP"].ToString();
                    decimal gia = Convert.ToDecimal(dt.Rows[0]["GiaBan"]);
                    int sl = int.Parse(slString);

                    decimal thanhTienDong = gia * sl;

                    data_BS_TTT_DST.Rows.Add(maThuoc, tenThuoc, sl, lieuDung);

                    tongTienToa += thanhTienDong;
                    tbox_BS_TTT_ThanhTien.Text = tongTienToa.ToString("N0");

                    tbox_TTT_MaThuoc.Clear();
                    tbox_BS_TTT_SL.Clear();
                    tbox_BS_TTT_LD.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btn_BS_TTT_TaoTT_Click(object sender, EventArgs e)
        {
            if (data_BS_TTT_DST.Rows.Count == 0) return;

            try
            {
                foreach (DataGridViewRow row in data_BS_TTT_DST.Rows)
                {
                    if (row.Cells["MaThu"].Value != null)
                    {
                        string maT = row.Cells["MaThu"].Value.ToString();
                        int sl = Convert.ToInt32(row.Cells["SoLuongThuoc"].Value);

                        SqlParameter[] pGia = { new SqlParameter("@MaThuoc", maT), new SqlParameter("@MaLSDV", BS_QLHSKB.CurrentMaLSKB) };
                        decimal gia = Convert.ToDecimal(dc.ExecuteProcedure("sp_GetThongTinThuoc", pGia).Rows[0]["GiaBan"]);

                        SqlParameter[] p = {
                            new SqlParameter("@MaThuoc", maT),
                            new SqlParameter("@MaLSDV", BS_QLHSKB.CurrentMaLSKB),
                            new SqlParameter("@SL", sl),
                            new SqlParameter("@LieuDung", row.Cells["LieuDung"].Value),
                            new SqlParameter("@ThanhTien", gia * sl) 
                        };
                        dc.ExecuteProcedure("sp_LuuChiTietToa", p);
                    }
                }
                MessageBox.Show("Đã lưu toa thuốc!");
                data_BS_TTT_DST.Rows.Clear();
                tongTienToa = 0;
                tbox_BS_TTT_ThanhTien.Text = "0";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu: " + ex.Message); }
        }

        private void BS_TaoTT_Load_1(object sender, EventArgs e)
        {

        }

        private void btn_BS_HoanTat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BS_QLHSKB.CurrentMaLSKB))
            {
                MessageBox.Show("Chưa có thông tin hồ sơ khám bệnh!");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Xác nhận hoàn tất ca khám và chuyển sang lập hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    SqlParameter[] p = {
                new SqlParameter("@MaLSDV", BS_QLHSKB.CurrentMaLSKB),
                new SqlParameter("@MaNV", SessionData.MaNV) 
            };

                    dc.ExecuteProcedure("sp_BS_HoanTatKham", p);

                    MessageBox.Show("Đã hoàn tất ca khám! Hồ sơ đã chuyển sang Tiếp tân.");

                    BS_QLHSKB.CurrentMaLSKB = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hoàn tất: " + ex.Message);
                }
            }
        }

        private void txb_BS_MaLSDV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
