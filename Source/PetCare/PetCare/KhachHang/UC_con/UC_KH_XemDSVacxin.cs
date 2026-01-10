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
    public partial class UC_KH_XemDSVacxin : UserControl
    {
        ServiceDAL dal = new ServiceDAL();
        public event Action QuayVeTrangChu;

        public UC_KH_XemDSVacxin()
        {
            InitializeComponent();
            // Đăng ký sự kiện Load để tự động nạp dữ liệu khi UC hiển thị
            this.Load += UC_KH_XemDSVacxin_Load;
        }

        private void UC_KH_XemDSVacxin_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Ngăn tự động tạo cột để khớp với thiết kế trong Designer
                dataGridView_KH_DSVacxin.AutoGenerateColumns = false;

                // Lấy dữ liệu từ database thông qua DAL
                DataTable dt = dal.GetDanhSachVacXin();

                // Ánh xạ dữ liệu vào các cột đã thiết kế
                dataGridView_KH_DSVacxin.DataSource = dt;
                dataGridView_KH_DSVacxin.Columns["MaVacxin"].DataPropertyName = "MaVacXin";
                dataGridView_KH_DSVacxin.Columns["TenVacxin"].DataPropertyName = "TenVacXin";
                dataGridView_KH_DSVacxin.Columns["GiaTien"].DataPropertyName = "GiaTien";
                dataGridView_KH_DSVacxin.Columns["SoMui"].DataPropertyName = "SoMuiTonKho";

                // Định dạng hiển thị tiền tệ cho cột Giá tiền
                dataGridView_KH_DSVacxin.Columns["GiaTien"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vắc xin: " + ex.Message);
            }
        }

        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
