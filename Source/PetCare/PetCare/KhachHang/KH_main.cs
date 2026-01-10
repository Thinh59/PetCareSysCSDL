using PetCare.KhachHang.UC; // Namespace chứa các UserControl của bạn
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang
{
    public partial class KH_main : Form
    {
        private bool isDangXuat = false;
        private int idTaiKhoan;

        public KH_main()
        {
            InitializeComponent();
            this.FormClosed += KH_main_FormClosed;
        }

        public KH_main(int id) : this()
        {
            this.idTaiKhoan = id;
           
        }

        private void LoadControl(UserControl usc)
        {
            if (usc != null)
            {
                pnl_KH_Content.Controls.Clear();
                usc.Dock = DockStyle.Fill;

                pnl_KH_Content.Controls.Add(usc);
                usc.BringToFront();
            }
        }

        private void KH_main_Load(object sender, EventArgs e)
        {
            btn_KH_ThuCung_Click(sender, e);
        }


        private void btn_KH_TTCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new All_ThongTinCaNhan_UC());
        }

        private void btn_KH_ThuCung_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_QLThuCung());
        }

        private void btn_KH_DichVu_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DatDichVu());
        }

        private void btn_KH_GoiTiem_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DatGoiTiem());
        }

        private void btn_KH_MuaHang_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_MuaHangTrucTuyen());
        }

        private void btn_KH_ThanhToan_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_ThanhToanHoaDon());
        }

        private void btn_KH_LichSu_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_LSDichVu());
        }

        private void btn_KH_HoiVien_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DangKyHoiVien());
        }

        private void btn_KH_UuDai_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_XemKhuyenMai());
        }

        private void btn_KH_ChiNhanh_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_ChiNhanh());
        }

        private void btn_KH_DanhGia_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DanhGiaDichVu());
        }

        private void btn_TraCuuBacSi_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_TraCuuLichBacSi());
        }
        private void KH_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isDangXuat)
            {
                Application.Exit();
            }
        }

        private void btn_KH_DangXuat_Click_1(object sender, EventArgs e)
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

        private void pnl_KH_Menu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}