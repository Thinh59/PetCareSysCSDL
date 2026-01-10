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
    public partial class UC_KH_XemKhuyenMai : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_XemKhuyenMai()
        {
            InitializeComponent();
            this.Load += UC_KH_KhuyenMai_Load;
        }

        private void UC_KH_KhuyenMai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                // Tắt tự động tạo cột để quản lý cột thủ công trong Designer
                dataGridView_XemKhuyenMai.AutoGenerateColumns = false;

                // Nạp dữ liệu
                DataTable dt = dal.GetDanhSachKhuyenMai(SessionData.MaKH);
                dataGridView_XemKhuyenMai.DataSource = dt;

                // Ánh xạ dữ liệu vào cột (DataPropertyName phải khớp với tên cột trong SQL SELECT)
                if (dataGridView_XemKhuyenMai.Columns.Contains("MaKM"))
                    dataGridView_XemKhuyenMai.Columns["MaKM"].DataPropertyName = "MaKM";

                if (dataGridView_XemKhuyenMai.Columns.Contains("LoaiKM"))
                    dataGridView_XemKhuyenMai.Columns["LoaiKM"].DataPropertyName = "LoaiKM";

                if (dataGridView_XemKhuyenMai.Columns.Contains("GiaKM"))
                    dataGridView_XemKhuyenMai.Columns["GiaKM"].DataPropertyName = "HienThi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin khuyến mãi: " + ex.Message);
            }
        }

        private void dataGridView_XemKhuyenMai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
