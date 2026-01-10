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
    public partial class UC_KH_XemToaThuoc : UserControl, IReturnToMainPage
    {
        private ServiceDAL serviceDAL = new ServiceDAL();
        private string maLSDVKB;

        // Thay thế constructor mặc định bằng constructor này
        public UC_KH_XemToaThuoc(string maLSDVKB)
        {
            InitializeComponent();
            this.maLSDVKB = maLSDVKB;
            SetupDataGridView();
            LoadData();

            // Đăng ký sự kiện Quay Lại của nút trong UC này
            btn_KH_QuayLai.Click += btn_KH_QuayLai_Click;
        }

        private void SetupDataGridView()
        {
            // Cấu hình DataPropertyName khớp với ToaThuocView
            dataGridView_KH_CTThuoc.AutoGenerateColumns = false;

            // Dựa trên ảnh EFC2E3C7-5E0C-4BCC-B6BE-036EBDAC2629.png
            dataGridView_KH_CTThuoc.Columns["MaThuoc"].DataPropertyName = "MaThuoc";
            dataGridView_KH_CTThuoc.Columns["SoLuong"].DataPropertyName = "SoLuongThuoc";
            dataGridView_KH_CTThuoc.Columns["DonViTinh"].DataPropertyName = "DonViTinh";
            dataGridView_KH_CTThuoc.Columns["LieuDung"].DataPropertyName = "LieuDung";
            dataGridView_KH_CTThuoc.Columns["ThanhTien"].DataPropertyName = "ThanhTienFormat";

            dataGridView_KH_CTThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadData()
        {
            try
            {
                if (string.IsNullOrEmpty(maLSDVKB)) return;

                List<ToaThuocView> danhSachToaThuoc = serviceDAL.GetChiTietToaThuoc(maLSDVKB);
                dataGridView_KH_CTThuoc.DataSource = danhSachToaThuoc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết toa thuốc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
