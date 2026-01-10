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
using System.Diagnostics;

namespace PetCare
{
    public partial class BS_QLHSKB : UserControl
    {
        DataConnection dc = new DataConnection();

        public static string CurrentMaLSKB = "";

        public BS_QLHSKB()
        {
            InitializeComponent();
        }

        private void txb_BS_MaLSDV_Leave(object sender, EventArgs e)
        {
            string maNhap = txb_BS_MaLSDV.Text.Trim();

            if (!string.IsNullOrEmpty(maNhap))
            {
                CurrentMaLSKB = maNhap;

                LoadThongTinCaKham(maNhap);
            }
        }
        private void LoadThongTinCaKham(string maLS)
        {
            try
            {
                string sql = "SELECT MaThuCung FROM LS_DVKHAMBENH WHERE MaLSDVKB = @MaLS";

                SqlParameter[] p = {
                    new SqlParameter("@MaLS", maLS)
                };

                DataTable dt = dc.ExecuteQuery(sql, p);
                if (dt.Rows.Count > 0)
                {
                    tbox_BS_HSKB_MaTC.Text = dt.Rows[0]["MaThuCung"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Mã phiếu khám: " + maLS + "\nVui lòng kiểm tra lại với Lễ tân.");
                    tbox_BS_HSKB_MaTC.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message);
            }
        }

        private void btn_BS_QLHSK_THS_Click(object sender, EventArgs e)
        {
            string maKham = txb_BS_MaLSDV.Text.Trim();

            if (string.IsNullOrEmpty(maKham))
            {
                MessageBox.Show("Vui lòng nhập Mã Lịch Sử Dịch Vụ (MaLSDV) trước!");
                txb_BS_MaLSDV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbox_BS_HSKB_MaTC.Text))
            {
                MessageBox.Show("Chưa có thông tin Thú Cưng. Vui lòng kiểm tra lại Mã LSDV!");
                return;
            }

            try
            {
                CurrentMaLSKB = maKham;
                SqlParameter[] p = {
                    new SqlParameter("@MaLSKB", maKham),            
                    new SqlParameter("@MaNV", tbox_HSKB_BSPT.Text.Trim()), 
                    new SqlParameter("@TrieuChung", tbox_HSKB_TC.Text.Trim()),
                    new SqlParameter("@ChuanDoan", tbox_HSKB_CD.Text.Trim())
                };

                dc.ExecuteProcedure("sp_BacSiCapNhatKham", p);

                MessageBox.Show("Đã lưu kết quả khám thành công!\nVui lòng chuyển sang tab Toa Thuốc.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            tbox_BS_HSKB_MaTC.Clear();
            tbox_BS_HSKB_MaKH.Clear();
            tbox_HSKB_Loai.Clear();
            tbox_HSKB_Tuoi.Clear();
            tbox_HSKB_TC.Clear();
            tbox_HSKB_CD.Clear();
            tbox_HSKB_BSPT.Clear();
        }

        private void dateTime_BS_KSKB_NK_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_HSKB_MaTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_HSKB_MaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_HSKB_Tuoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_HSKB_TC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_HSKB_CD_TextChanged(object sender, EventArgs e)
        {

        }

        private void data_BS_QLHSK_TT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbox_HSKB_BSPT_TextChanged(object sender, EventArgs e)
        {

        }

        private void BS_QLHSKB_Load(object sender, EventArgs e)
        {

        }

        private void txb_BS8_Time_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_BS_MaLSDV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_BS_MaLSDV_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
