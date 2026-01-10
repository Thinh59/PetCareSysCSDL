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
    public partial class BS_TCVC : UserControl
    {
        DataConnection dc = new DataConnection();

        public BS_TCVC()
        {
            InitializeComponent();

            data_BC_TC_T.AutoGenerateColumns = false;
            data_BS_TC_VC.AutoGenerateColumns = false;
        }

        private void BS_TCVC_Load(object sender, EventArgs e)
        {
            LoadVatTu("");
        }

        private void lab_BS_TCVC_Click(object sender, EventArgs e)
        {

        }

        private void data_BS_TCVC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbox_BS_TCVC_MaVC_TextChanged(object sender, EventArgs e)
        {
            LoadVatTu(tbox_BS_TCVC_MaVC.Text.Trim());
        }

        private void tbox_BS_TCTC_TVC_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadVatTu(string tuKhoa = "")
        {
            try
            {
                string loai = (tabControl_BS_TC.SelectedTab.Text == "Thuốc") ? "Thuoc" : "Vaccine";
                string maNV = SessionData.MaNV;

                SqlParameter[] p = {
            new SqlParameter("@TuKhoa", tuKhoa),
            new SqlParameter("@Loai", loai),
            new SqlParameter("@MaNV", maNV)
                };

                DataTable dt = dc.ExecuteProcedure("sp_TraCuuVatTu", p);

                if (loai == "Thuoc")
                {
                    data_BC_TC_T.DataSource = dt;
                }
                else
                {
                    data_BS_TC_VC.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        private void btn_BS_TCVC_Click(object sender, EventArgs e)
        {
            string tuKhoa = tbox_BS_TCVC_MaVC.Text.Trim();

            string loai = (tabControl_BS_TC.SelectedTab.Text == "Thuốc") ? "Thuoc" : "Vaccine";

            try
            {
                SqlParameter[] p = {
            new SqlParameter("@TuKhoa", tuKhoa),
            new SqlParameter("@Loai", loai),
            new SqlParameter("@MaNV", SessionData.MaNV)
        };

                DataTable dt = dc.ExecuteProcedure("sp_TraCuuVatTu", p);

                if (loai == "Thuoc")
                    data_BC_TC_T.DataSource = dt;
                else
                    data_BS_TC_VC.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy sản phẩm phù hợp!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu: " + ex.Message);
            }
        }

        private void data_BS_TC_VC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void data_BC_TC_T_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl_BS_TC_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbox_BS_TCVC_MaVC.Clear(); 
            LoadVatTu("");        
        }
    }
}
