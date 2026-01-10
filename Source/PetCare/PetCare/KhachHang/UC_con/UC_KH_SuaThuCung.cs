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

namespace PetCare.KhachHang
{
    public delegate void SuaQuayVeTrangChuDelegate();

    public partial class UC_KH_SuaThuCung : UserControl
    {
        // 1. Khai báo Event
        public event SuaQuayVeTrangChuDelegate QuayVeTrangChu;

        private string MaThuCungHienTai;
        private ThuCungView currentPet;

        ServiceDAL serviceDAL = new ServiceDAL();
        private UC_KH_QLThuCung parent;

        public UC_KH_SuaThuCung()
        {
            InitializeComponent();
        }

        public UC_KH_SuaThuCung(UC_KH_QLThuCung uc, string maTC)
        {
            InitializeComponent();
            parent = uc;
            MaThuCungHienTai = maTC;
            LoadPetDetails();
        }

        private void LoadPetDetails()
        {
            // Cần tạo hàm GetThuCungChiTiet(string maTC) trong ServiceDAL
            // Giả sử hàm này trả về một đối tượng ThuCungView
            currentPet = serviceDAL.GetThuCungChiTiet(MaThuCungHienTai);

            if (currentPet != null)
            {
                // Gán dữ liệu từ đối tượng ThuCungView lên các Control trên Form
                textBox_KH_Ten.Text = currentPet.Ten;
                textBox_KH_Loai.Text = currentPet.Loai;
                textBox_KH_Giong.Text = currentPet.Giong;
                dtp_NgaySinh.Value = currentPet.NgaySinh;
                cb_GioiTinh.Text = currentPet.GioiTinh;
                textBox_KH_TTSK.Text = currentPet.TinhTrangSucKhoe;
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu thú cưng này!");
                // Gọi Event Hủy để quay về trang chính nếu không tìm thấy
                if (QuayVeTrangChu != null) QuayVeTrangChu();
            }
        }


        private void btn_KH_Luu_Click(object sender, EventArgs e)
        {
            // 1.Thu thập dữ liệu từ các controls
                currentPet.Ten = textBox_KH_Ten.Text;
            currentPet.Loai = textBox_KH_Loai.Text;
            currentPet.Giong = textBox_KH_Giong.Text;
            currentPet.NgaySinh = dtp_NgaySinh.Value;
            currentPet.GioiTinh = cb_GioiTinh.Text;
            currentPet.TinhTrangSucKhoe = textBox_KH_TTSK.Text;

            // 2. Lấy Mã Khách Hàng từ SessionData
            string maKH = SessionData.MaKH;

            // 3. Gọi hàm Sửa (Update) trong DAL
            bool success = serviceDAL.SuaThuCung(currentPet, maKH);

            if (success)
            {
                MessageBox.Show($"Cập nhật thông tin cho {currentPet.Ten} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Kích hoạt Event để quay về trang chính và tải lại DGV
                if (QuayVeTrangChu != null)
                {
                    QuayVeTrangChu();
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thú cưng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_KH_Huy_Click(object sender, EventArgs e)
        {
            if (QuayVeTrangChu != null)
            {
                QuayVeTrangChu();
            }
        }
    }
}
