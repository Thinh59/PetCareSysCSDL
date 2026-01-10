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
    public partial class QLCN_main : Form
    {
        private bool isDangXuat = false;
        public QLCN_main()
        {
            InitializeComponent();
            this.FormClosed += QLCN_main_FormClosed;
        }
        private void QLCN_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isDangXuat) return; 
            Application.Exit();
        }

        public QLCN_main(int idTK) : this()
        {
        }

        private void QLCN_main_Load(object sender, EventArgs e)
        {
        }

        private void LoadControl(UserControl usc)
        {
            pnl_QLCN_dashboard.Visible = true;
            pnl_QLCN_dashboard.Controls.Clear();
            usc.Dock = DockStyle.Fill;
            pnl_QLCN_dashboard.Controls.Add(usc);
        }
        private void btn_QLCN_TTCN_Click(object sender, EventArgs e)
        {
            LoadControl(new All_ThongTinCaNhan_UC());
        }
        private void btn_QLCN_TKDT_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN1_TKDoanhThu());
        }
        private void btn_QLCN_QLNV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN2_QLNV());
        }

        private void btn_QLCN_HSNV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN3_HSNV());
        }

        private void btn_QLCN_TKSP_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN4_TKSP());
        }

        private void btn_QLCN_SPTrongKho_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN5_QLSP());
        }

        private void btn_QLCN_DSPet_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN6_DSPet());
        }
        private void btn_QLCN_TKKH_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN7_TKKH());
        }
        private void btn_QLCN_QLDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN9_QLDV());
        }
        private void btn_QLCN_DSHD_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN10_DSHoaDon());
        }
        private void btn_QLCN_TKDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_QLCN11_TKDV());
        }
        private void btn_QLCN_DX_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                isDangXuat = true;

                SessionData.Clear();
                this.Hide();
                ALL_LogIn login = new ALL_LogIn();
                login.ShowDialog();
                this.Close();
            }
        }

        private void icon_TT_DangXuat_Click(object sender, EventArgs e)
        {

        }
    }
}