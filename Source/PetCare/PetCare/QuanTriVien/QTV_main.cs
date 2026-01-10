using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class QTV_main : Form
    {
        private bool isDangXuat = false;

        public QTV_main(int id)
        {
            InitializeComponent();

            this.FormClosed += QTV_main_FormClosed;
        }

        private void LoadControl(UserControl usc)
        {
            if (usc != null)
            {
                pnl_QTV_dashboard.Visible = true;

                pnl_QTV_dashboard.Controls.Clear();

                usc.Dock = DockStyle.Fill;

                pnl_QTV_dashboard.Controls.Add(usc);
                usc.BringToFront();
            }
        }

        private void QTV_main_Load(object sender, EventArgs e)
        {
            btn_QTV_QLTK_Click(sender, e);
        }

        private void btn_QTV_QLTK_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QTV1_QLTK());
        }
        private void QTV_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isDangXuat)
            {
                Application.Exit();
            }
        }

        private void btn_QTV_DX_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi quyền Quản Trị?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                isDangXuat = true; 

                this.Hide(); 

                ALL_LogIn login = new ALL_LogIn();
                login.ShowDialog(); 

                this.Close(); 
            }
        }

        private void btn_BH_TTCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new All_ThongTinCaNhan_UC());
        }

        private void btn_BH_TTDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_ALL_DSDV());
        }
    }
}