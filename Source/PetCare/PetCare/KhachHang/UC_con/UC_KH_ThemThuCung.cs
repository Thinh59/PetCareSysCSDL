using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PetCare;
using PetCare.KhachHang.UC;

namespace PetCare.KhachHang
{
    public delegate void ThemQuayVeTrangChuDelegate();

    public partial class UC_KH_ThemThuCung : UserControl
    {
        // 1. Khai báo Event
        public event ThemQuayVeTrangChuDelegate QuayVeTrangChu;

        ServiceDAL serviceDAL = new ServiceDAL();
        private UC_KH_QLThuCung parent;

        public UC_KH_ThemThuCung()
        {
            InitializeComponent();
        }

        public UC_KH_ThemThuCung(UC_KH_QLThuCung uc)
        {
            InitializeComponent();
            parent = uc;
            SetupControls();
        }

        private void SetupControls()
        {
            // Thiết lập giá trị cho ComboBox Giới Tính
            if (comboBox_GioiTinh.Items.Count == 0)
            {
                comboBox_GioiTinh.Items.AddRange(new object[] { "Đực", "Cái", "Khác" });
            }
            comboBox_GioiTinh.SelectedIndex = 0; // Chọn giá trị mặc định
        }

        // --- XỬ LÝ NÚT LƯU ---
        private void btn_KH_Luu_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ UI
            string ten = textBox_KH_Ten.Text.Trim();
            string loai = textBox_KH_Loai.Text.Trim();
            string giong = textBox_KH_Giong.Text.Trim();
            DateTime ngSinh = dateTimePicker_NgaySinh.Value;
            string gioiTinh = comboBox_GioiTinh.Text;
            string tinhTrangSK = textBox_KH_TTSK.Text.Trim();

            string maKH = SessionData.MaKH; // Lấy Mã Khách Hàng từ Session

            // 2. Validate dữ liệu cơ bản (Đảm bảo kiểm tra NOT NULL đã được SP thực hiện)
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(loai) || string.IsNullOrEmpty(giong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên, Loại và Giống thú cưng.", "Thiếu thông tin");
                return;
            }
            // SP đã kiểm tra NgaySinh_TC > GETDATE() nhưng kiểm tra trước ở UI sẽ tốt hơn
            if (ngSinh > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.", "Lỗi ràng buộc");
                return;
            }

            try
            {
                // 3. Gọi DAL với 7 tham số đã sửa
                serviceDAL.ThemThuCung(ten, loai, giong, ngSinh, gioiTinh, tinhTrangSK, maKH);

                MessageBox.Show($"Thêm thú cưng '{ten}' thành công!", "Thành công");

                if (QuayVeTrangChu != null)
                {
                    QuayVeTrangChu();
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi từ SP
                MessageBox.Show("Lỗi: " + ex.Message, "Lưu dữ liệu thất bại");
            }
        }

        // --- XỬ LÝ NÚT HỦY ---
        private void btn_KH_Huy_Click(object sender, EventArgs e)
        {
            // 2. Kích hoạt Event để thông báo cho Form Cha
            if (QuayVeTrangChu != null)
            {
                QuayVeTrangChu();
            }
        }
    }
}
