using PetCare.HoiVien.UC_Con;
using PetCare.KhachHang.UC;   
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.HoiVien
{
    public partial class HV_main : Form
    {
        private bool isDangXuat = false;
        private int idTaiKhoan;

        public HV_main()
        {
            InitializeComponent();
            this.FormClosed += HV_main_FormClosed;
        }

        public HV_main(int id) : this()
        {
            this.idTaiKhoan = id;
        }
        private void LoadControl(UserControl usc)
        {
            if (usc != null)
            {
                pnl_HV_Content.Controls.Clear();

                usc.Dock = DockStyle.Fill;

                pnl_HV_Content.Controls.Add(usc);
                usc.BringToFront();
            }
        }

        private void HV_main_Load(object sender, EventArgs e)
        {
            btn_HV_HoiVien_Click(sender, e);
        }

        private void btn_HV_TTCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new All_ThongTinCaNhan_UC());
        }

        private void btn_HV_ThuCung_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_QLThuCung());
        }

        private void btn_HV_DichVu_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DatDichVu());
        }

        private void btn_HV_GoiTiem_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DatGoiTiem());
        }

        private void btn_HV_MuaHang_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_MuaHangTrucTuyen());
        }

        private void btn_HV_ThanhToan_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_ThanhToanHoaDon());
        }

        private void btn_HV_LichSu_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_LSDichVu());
        }

        private void btn_HV_UuDai_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_XemKhuyenMai());
        }

        private void btn_HV_ChiNhanh_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_ChiNhanh());
        }

        private void btn_HV_DanhGia_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_DanhGiaDichVu());
        }

        private void btn_TraCuuBacSi_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_KH_TraCuuLichBacSi());
        }

        private void btn_HV_HoiVien_Click(object sender, EventArgs e)
        {
            LoadControl(new UC_HV_ThongTinHoiVien());
        }
        private void btn_HV_DangXuat_Click(object sender, EventArgs e)
        {
            
        }

        private void HV_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isDangXuat)
            {
                Application.Exit();
            }
        }

        private void btn_HV_DangXuat_Click_1(object sender, EventArgs e)
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
    }
}