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
    public partial class BH_main : Form
    {
        private bool isDangXuat = false;

        public BH_main(int id)
        {
            InitializeComponent();
            this.FormClosed += BH_main_FormClosed;
        }

        private void LoadControl(UserControl usc)
        {
            if (usc != null)
            {
                pnl_BH_HienThiChinh.Controls.Clear();
                usc.Dock = DockStyle.Fill;
                pnl_BH_HienThiChinh.Controls.Add(usc);
                usc.BringToFront();
            }
        }

        private void BH_main_Load(object sender, EventArgs e)
        {
            btn_BH_BHTT_Click(sender, e);
        }

        private void btn_BH_TTCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new All_ThongTinCaNhan_UC());
        }

        private void btn_BH_BHTT_Click(object sender, EventArgs e)
        {
            LoadControl(new BH_BHTT());
        }
        private void btn_BH_HTK_Click(object sender, EventArgs e)
        {
            LoadControl(new BH_HTK());
        }

        private void btn_BH_QLDDH_Click(object sender, EventArgs e)
        {
            LoadControl(new BH_QLDDH());
        }

        private void btn_BH_TCKH_Click(object sender, EventArgs e)
        {
            LoadControl(new BH_TCKH());
        }

        private void btn_BH_TTHD_Click(object sender, EventArgs e)
        {
            LoadControl(new BH_TTHD());
        }


        private void btn_BH_DX_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
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

        private void BH_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isDangXuat)
            {
                Application.Exit();
            }
        }

        private void btn_BH_TTDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_ALL_DSDV());
        }

        private void pic_icon_BH_TTCaNhan_Click(object sender, EventArgs e)
        {

        }

        private void pic_BH_TTDV_Click(object sender, EventArgs e)
        {

        }
    }
}