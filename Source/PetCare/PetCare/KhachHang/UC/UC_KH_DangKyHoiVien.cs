using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang.UC
{
    public partial class UC_KH_DangKyHoiVien : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_DangKyHoiVien()
        {
            InitializeComponent();
        }

        private void btn_KH_DangKyHV_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi thực hiện
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng ký chương trình Hội viên không?",
                                             "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (!string.IsNullOrEmpty(dal.DangKyHoiVien(SessionData.MaKH)))
                    {
                        MessageBox.Show("Chúc mừng! Bạn đã trở thành Hội viên Cơ bản của PetCare.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btn_KH_DangKyHV.Enabled = false;
                        btn_KH_DangKyHV.Text = "Đã là Hội viên";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
