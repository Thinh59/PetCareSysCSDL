using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PetCare
{
    public partial class TT_main : Form
    {
        private bool isDangXuat = false;
        public TT_main()
        {
            InitializeComponent();
            this.FormClosed += TT_main_FormClosed;
        }

        private void TT_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isDangXuat) return;
            Application.Exit();
        }
        public TT_main(int idTK) : this()
        {
            
        }

        private void TT_main_Load(object sender, EventArgs e)
        {
            this.Text = $"Hệ thống Tiếp Tân - {SessionData.TenHienThi} ({SessionData.TenCN})";
        }

        private void LoadUserControl(UserControl usc)
        {
            pnl_TT_dashboard.Visible = true;
            pnl_TT_dashboard.Controls.Clear();
            usc.Dock = DockStyle.Fill;
            pnl_TT_dashboard.Controls.Add(usc);
        }
        private void btn_TT_TTCaNhan_Click(object sender, EventArgs e)
        {
            LoadUserControl(new All_ThongTinCaNhan_UC());
        }

        private void btn_TT_DKTKKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT_DKTKKH(this));
        }

        private void btn_TT_DKHVKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT2_DKHVKH());
        }

        private void btn_TT_XNKB_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT3_XNKhamBenh());
        }

        private void btn_TT_XNTP_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT4_XNTiemPhong());
        }

        private void btn_DKGTKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT5_DKGoiTiemKH());
        }
        private void btn_TT_LapHoaDonKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT6_LapHoaDon());
        }
        private void btn_TT_XemHoaDonLap_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT7_XemHDLap());
        }
        private void btn_TT_XemLSDD_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT8_LSDieuDong());
        }
        private void btn_TT_DX_Click(object sender, EventArgs e)
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
                SessionData.Clear();

                this.Hide();

                ALL_LogIn loginForm = new ALL_LogIn();
                loginForm.ShowDialog();

                this.Close();
            }
        }
        private void btn_TT_TTCN_Click(object sender, EventArgs e)
        {
            btn_TT_TTCaNhan_Click(sender, e);
        }

        private void btn_TT_DatDVQuay_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT9_DatLichTaiQuay());
        }

        private void btn_TT10_TraCuuPet_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_TT10_TraCuuThuCung());
        }

        private void btn_TT_TTDV_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Usc_ALL_DSDV());
        }
    }
}