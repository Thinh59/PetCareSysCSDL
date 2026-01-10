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
    public partial class BacSi_main : Form
    {
        private bool isDangXuat = false;

        public BacSi_main(int id)
        {
            InitializeComponent();

            // Đăng ký sự kiện đóng form
            this.FormClosed += BacSi_main_FormClosed;
        }
        private void LoadControl(UserControl usc)
        {
            if (usc != null)
            {
                pnl_BS_HienThiChinh.Controls.Clear();
                usc.Dock = DockStyle.Fill;
                pnl_BS_HienThiChinh.Controls.Add(usc);
                usc.BringToFront();
            }
        }

        private void BacSi_main_Load(object sender, EventArgs e)
        {
            btn_BS_QLHSKB_Click(sender, e);
        }

        // 1. Quản lý Hồ sơ Khám bệnh (Tiếp nhận/Khám)
        private void btn_BS_QLHSKB_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_QLHSKB());
        }

        // 2. Tạo Toa thuốc
        private void btn_BS_TTT_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_TaoTT());
        }

        // 3. Tiêm phòng (Lịch tiêm)
        private void btn_BS_TPL_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_TPL());
        }

        // 4. Quản lý Gói tiêm
        private void btn_BS_QLGT_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_QLGT());
        }

        // 5. Lịch sử Khám (Của bác sĩ)
        private void btn_BS_LSK_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_LSK());
        }

        // 6. Xem Vắc xin (Tra cứu kho)
        private void btn_BS_XVC_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_TCVC());
        }

        // 7. Xem Đánh giá (Feedback)
        private void btn_BS_XDG_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_XDG());
        }

        // 8. Xem Hồ sơ Khám bệnh (Tra cứu lịch sử cũ)
        private void btn_BS_XHSKB_Click(object sender, EventArgs e)
        {
            LoadControl(new BS_XemHSKB());
        }

        // 9. Thông tin cá nhân
        private void btn_BS_TTCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new All_ThongTinCaNhan_UC());
        }

        // --- XỬ LÝ ĐĂNG XUẤT VÀ THOÁT ---

        private void btn_BS_DX_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                isDangXuat = true;

                this.Hide();

                ALL_LogIn loginForm = new ALL_LogIn();
                loginForm.ShowDialog();

                this.Close();
            }
        }

        private void BacSi_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isDangXuat)
            {
                Application.Exit();
            }
        }

        private void btn_BS_CSTK_Click(object sender, EventArgs e)
        {

        }

        private void btn_BS_TTDV_Click(object sender, EventArgs e)
        {
            LoadControl(new Usc_ALL_DSDV());
        }
    }
}