using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT_DKTKKH: UserControl
    {
        public Form TT1_parent;
        public Usc_TT_DKTKKH(Form frm_TT1_parent)
        {
            InitializeComponent();
            this.TT1_parent = frm_TT1_parent;
        }

        private void pic_TT1_BG_Click(object sender, EventArgs e)
        {

        }

        private void pnl_TT1_TKKH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_TT1_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }

        }

        private void lbl_TT1_NhapTTKH_Click(object sender, EventArgs e)
        {

        }

        private void txtBox_TT1_HoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtp_TT1_NgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBox_TT1_SDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBox_TT1_Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_TT2_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_TT1_Username_TextChanged(object sender, EventArgs e)
        {

        }


        private void btn_TT1_XacNhan_Click(object sender, EventArgs e)
        {
            string hoTen = txtBox_TT1_HoTen.Text.Trim();
            DateTime ngaySinh = dtp_TT1_NgaySinh.Value;
            string gioiTinh = cmb_TT1_GioiTinh.Text.Trim(); 
            string sdt = txtBox_TT1_SDT.Text.Trim();
            string email = txtBox_TT1_Email.Text.Trim();
            string username = txb_TT1_Username.Text.Trim();
            string password = txb_TT1_Password.Text; 

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc (Họ tên, SDT, Username, Password).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passwordHash = PasswordHasher.Hash(password);

            ServiceDAL dal = new ServiceDAL();

            try
            {
                Dictionary<string, object> newAccount = dal.TaoTaiKhoanKhachHang(
                    hoTen, ngaySinh, gioiTinh, sdt, email, username, passwordHash);

                string maKH = newAccount["MaKH"].ToString();

                MessageBox.Show($"Đăng ký tài khoản thành công!\nMã KH: {maKH}\nVui lòng đăng nhập với Username: {username}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_TT1_QuayLai_Click(sender, e);
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Lỗi đăng ký: {ex.Message}", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_TT1_Huy_Click(object sender, EventArgs e)
        {

        }
    }
}
