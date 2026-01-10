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
    public partial class BS_XemHSKB : UserControl
    {
        DataConnection dc = new DataConnection();

        int currentPage = 1;
        int pageSize = 20;
        public BS_XemHSKB()
        {
            InitializeComponent();
            data_BS_XemHSKB.AutoGenerateColumns = false;
        }

        private void BS_XemHSKB_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoSo();
        }

        private void LoadDanhSachHoSo()
        {
            string maTC = txt_BS_XHSKB_TK_MTC.Text.Trim();

            SqlParameter[] p = {
        new SqlParameter("@MaTC", string.IsNullOrEmpty(maTC) ? (object)DBNull.Value : maTC),
        new SqlParameter("@PageNumber", currentPage),
        new SqlParameter("@PageSize", pageSize)
    };

            data_BS_XemHSKB.DataSource = dc.ExecuteProcedure("sp_GetDanhSachHoSoKB", p);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void data_BS_XemHSKB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_BS_XHSKB_TK_MTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_BS_XHSKB_TK_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadDanhSachHoSo();
        }

        private void btn_BS_XemHSKB_Click(object sender, EventArgs e)
        {
            if (data_BS_XemHSKB.CurrentRow != null)
            {
                DataRowView row = (DataRowView)data_BS_XemHSKB.CurrentRow.DataBoundItem;

                tbox_BS_XHSKB_MaTC.Text = row["MaThuCung"].ToString();
                tbox_BS_XHSKB_MaKH.Text = row["MaKH"].ToString();
                tbox_XHSKB_Loai.Text = row["LoaiThuCung"].ToString();

                if (row["NgaySinh_TC"] != DBNull.Value)
                {
                    DateTime ngaySinh = Convert.ToDateTime(row["NgaySinh_TC"]);
                    tbox_XHSKB_Tuoi.Text = (DateTime.Now.Year - ngaySinh.Year).ToString();
                }

                dateTime_BS_XHSKB_NK.Value = Convert.ToDateTime(row["NgayKham"]);
                tbox_XHSKB_TC.Text = row["TrieuChung"].ToString();
                tbox_XHSKB_CD.Text = row["ChuanDoan"].ToString();
                tbox_XHSKB_BSPT.Text = row["BacSiPhuTrach"].ToString();

                if (row["NgayHen"] != DBNull.Value)
                {
                    dateTime_BS_XHSKB_NTK.Value = Convert.ToDateTime(row["NgayHen"]);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ trong danh sách!");
            }
        }

        private void dateTime_BS_XHSKB_NK_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_XHSKB_MaTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BS_XHSKB_MaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_XHSKB_Loai_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_XHSKB_Tuoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_XHSKB_CN_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_XHSKB_TC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_XHSKB_CD_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTime_BS_XHSKB_NTK_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbox_XHSKB_BSPT_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnl_BS_QLHSKB_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_BS_XemHSKB_Next_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadDanhSachHoSo();
        }

        private void btn_BS_XemHSKB_Tr_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDanhSachHoSo();
            }
        }
    }
}
