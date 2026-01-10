using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class BS_XDG : UserControl
    {
        int currentPage = 1;
        int pageSize = 20;

        DataConnection dc = new DataConnection();
        public BS_XDG()
        {
            InitializeComponent();
            data_BS_XDG.AutoGenerateColumns = false;
        }

        private void lab_BS_TCVC_Click(object sender, EventArgs e)
        {

        }

        private void lab_BS_TCVC_MaVC_Click(object sender, EventArgs e)
        {

        }

        private void tbox_BS_XDG_TC_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void combo_BS_XDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void data_BS_XDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BS_XDG_Load(object sender, EventArgs e)
        {
            if (combo_BS_XDG.Items.Count > 0) combo_BS_XDG.SelectedIndex = 0;
            currentPage = 1;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string maKH = tbox_BS_XDG_TC.Text.Trim();
                string loaiDV = combo_BS_XDG.SelectedItem != null ? combo_BS_XDG.SelectedItem.ToString() : "";

                if (loaiDV == "---Tất cả---") loaiDV = "";

                SqlParameter[] p = {
                    new SqlParameter("@MaKH", maKH),
                    new SqlParameter("@LoaiDichVu", loaiDV),
                    new SqlParameter("@PageNumber", currentPage),
                    new SqlParameter("@PageSize", pageSize)
                };

                Stopwatch timer = new Stopwatch();
                timer.Start();

                DataTable dt = dc.ExecuteProcedure("sp_GetDanhGiaBacSi", p);

                timer.Stop();
                long executionTime = timer.ElapsedMilliseconds;

                if (txb_BS_Time != null)
                {
                    txb_BS_Time.Text = executionTime.ToString() + " ms";

                    if (executionTime < 50)
                    {
                        txb_BS_Time.ForeColor = Color.Green; // Có Index
                        txb_BS_Time.BackColor = Color.FromArgb(220, 255, 220);
                    }
                    else
                    {
                        txb_BS_Time.ForeColor = Color.Red;   // Chưa Index
                        txb_BS_Time.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }

                data_BS_XDG.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp đánh giá: " + ex.Message);
            }
        }

        private void btn_BS_XDG_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void btn_BS_XDG_Tr_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void btn_BS_XDG_Next_Click(object sender, EventArgs e)
        {
            if (data_BS_XDG.Rows.Count == pageSize)
            {
                currentPage++;
                LoadData();
            }
        }

        private void txb_BS_Time_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnl_BS_XDG_L_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
