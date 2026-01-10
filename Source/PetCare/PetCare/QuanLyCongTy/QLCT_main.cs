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
    public partial class QLCT_main : Form
    {
        private bool isDangXuat = false;

        public QLCT_main(int id)
        {
            InitializeComponent();
            
            this.FormClosed += QLCT_main_FormClosed;
        }

        private void LoadControl(UserControl usc)
        {
            if (usc != null)
            {
                pnl_QLCT_dashboard.Visible = true;
                
                pnl_QLCT_dashboard.Controls.Clear();

                usc.Dock = DockStyle.Fill;

                pnl_QLCT_dashboard.Controls.Add(usc);
                usc.BringToFront();
            }
        }

        private void QLCT_main_Load(object sender, EventArgs e)
        {
            btn_QLCT_DoanhThu_Click(sender, e);
        }

        private void btn_QLCT_DoanhThu_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT1_DoanhThu());
        }

        private void btn_QLCT_QLCN_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT2_QLCN());
        }

        private void btn_QLCT_HSCN_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT3_HSCN());
        }

        private void btn_QLCT_QLNV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT4_QLNV());
        }
        private void btn_QLCT_QLKM_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT5_QLKM());
        }

        private void btn_QLCT_QLDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT6_QLDV());
        }

        private void btn_QLCT_HSNV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT7_HSNV());
        }

        private void btn_QLCT_TKSP_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT8_TKSP());
        }

        private void btn_QLCT_TKKH_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT9_TKKH());
        }
        private void btn_QLCT_QLTC_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT10_QLTC());
        }
        private void btn_QLCT_LSTP_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT11_LSTP());
        }

        private void btn_QLCT_DSHD_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT12_DSHD());
        }

        private void btn_QLCT_LSDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT13_LSDV());
        }
        private void btn_QLCT_TKHV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT14_TKHV());
        }
        private void btn_QLCT_LSDD_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCT15_LSDD());
        }

        private void btn_ALL_DSDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_ALL_DSDV());
        }

        private void QLCT_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isDangXuat)
            {
                Application.Exit();
            }
        }

        private void btn_QLCT_DX_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống quản lý?",
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
    }
}