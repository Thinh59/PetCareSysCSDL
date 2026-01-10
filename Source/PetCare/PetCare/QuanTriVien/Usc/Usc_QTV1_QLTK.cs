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
    public partial class Usc_QTV1_QLTK : UserControl
    {
        private ServiceDAL serviceDAL = new ServiceDAL();

        public Usc_QTV1_QLTK()
        {
            InitializeComponent();

            this.Load += Usc_QTV1_QLTK_Load;
        }

        private void Usc_QTV1_QLTK_Load(object sender, EventArgs e)
        {
            cmb_QTV1_getAction.Items.Clear();
            cmb_QTV1_getAction.Items.AddRange(new string[] {
                "Thêm tài khoản",
                "Xóa tài khoản",
                "Đổi loại tài khoản",
                "Thay đổi mật khẩu"
            });
            cmb_QTV1_getAction.SelectedIndex = 0;

            cmb_QTV1_getLoaiTK.Items.Clear();
            cmb_QTV1_getLoaiTK.Items.AddRange(new string[] {
                "Quản trị viên",
                "Quản lý công ty",
                "Quản lý chi nhánh",
                "Tiếp tân",
                "Bác sĩ",
                "Bán hàng",
                "Khách hàng", 
                "Hội viên"
            });
        }

        private void pnl_QTV1_Paint(object sender, PaintEventArgs e) { }

        private void cmb_QTV1_QLTK_getAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_QTV1_TenNguoiDung.Visible = false;
            lbl_QTV1_LoaiTK.Visible = false;
            lbl_QTV1_MatKhau.Visible = false;
            lbl_QTV1_MatKhauMoi.Visible = false;

            txb_QTV1_getTenNguoiDung.Visible = false;
            cmb_QTV1_getLoaiTK.Visible = false;
            txb_QTV1_getMatKhau.Visible = false;

            string action = cmb_QTV1_getAction.SelectedItem?.ToString();

            switch (action)
            {
                case "Thêm tài khoản":
                    lbl_QTV1_TenNguoiDung.Visible = true;
                    lbl_QTV1_LoaiTK.Visible = true;
                    lbl_QTV1_MatKhau.Visible = true;

                    txb_QTV1_getTenNguoiDung.Visible = true;
                    cmb_QTV1_getLoaiTK.Visible = true;
                    txb_QTV1_getMatKhau.Visible = true;
                    break;

                case "Xóa tài khoản":
                    lbl_QTV1_TenNguoiDung.Visible = true;
                    txb_QTV1_getTenNguoiDung.Visible = true;
                    break;

                case "Đổi loại tài khoản":
                    lbl_QTV1_TenNguoiDung.Visible = true;
                    lbl_QTV1_LoaiTK.Visible = true; 

                    txb_QTV1_getTenNguoiDung.Visible = true;
                    cmb_QTV1_getLoaiTK.Visible = true;
                    break;

                case "Thay đổi mật khẩu":
                    lbl_QTV1_TenNguoiDung.Visible = true;
                    lbl_QTV1_MatKhauMoi.Visible = true; 

                    txb_QTV1_getTenNguoiDung.Visible = true;
                    txb_QTV1_getMatKhau.Visible = true; 
                    break;
            }
        }

        private void btn_QTV1_XacNhan_Click(object sender, EventArgs e)
        {
            string action = cmb_QTV1_getAction.SelectedItem?.ToString();
            string username = txb_QTV1_getTenNguoiDung.Text.Trim();
            string password = txb_QTV1_getMatKhau.Text.Trim(); 
            string role = cmb_QTV1_getLoaiTK.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                switch (action)
                {
                    case "Thêm tài khoản":
                        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
                        {
                            MessageBox.Show("Vui lòng nhập đầy đủ Mật khẩu và Loại tài khoản!", "Cảnh báo");
                            return;
                        }
                        serviceDAL.ThemTaiKhoan(username, password, role);
                        MessageBox.Show($"Thêm tài khoản '{username}' thành công!", "Thông báo");
                        break;

                    case "Xóa tài khoản":
                        if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{username}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            serviceDAL.XoaTaiKhoan(username);
                            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo");
                        }
                        else return; 
                        break;

                    case "Đổi loại tài khoản":
                        if (string.IsNullOrEmpty(role))
                        {
                            MessageBox.Show("Vui lòng chọn Loại tài khoản mới!", "Cảnh báo");
                            return;
                        }
                        serviceDAL.DoiLoaiTaiKhoan(username, role);
                        MessageBox.Show($"Đã đổi loại tài khoản '{username}' sang '{role}'!", "Thông báo");
                        break;

                    case "Thay đổi mật khẩu":
                        if (string.IsNullOrEmpty(password))
                        {
                            MessageBox.Show("Vui lòng nhập Mật khẩu mới!", "Cảnh báo");
                            return;
                        }
                        serviceDAL.DoiMatKhau(username, password);
                        MessageBox.Show($"Đổi mật khẩu cho '{username}' thành công!", "Thông báo");
                        break;

                    default:
                        MessageBox.Show("Vui lòng chọn hành động hợp lệ.");
                        return;
                }

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txb_QTV1_getTenNguoiDung.Clear();
            txb_QTV1_getMatKhau.Clear();
            cmb_QTV1_getLoaiTK.SelectedIndex = -1;
            txb_QTV1_getTenNguoiDung.Focus();
        }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void lbl_QTV1_LoaiTK_Click(object sender, EventArgs e) { }
        private void txb_QTV1_getTenNguoiDung_TextChanged(object sender, EventArgs e) { }
        private void txb_QTV1_getMatKhau_TextChanged(object sender, EventArgs e) { }
        private void cmb_QTV1_getLoaiTK_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}