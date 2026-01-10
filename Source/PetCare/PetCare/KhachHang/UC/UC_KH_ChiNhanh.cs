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
    public partial class UC_KH_ChiNhanh : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_ChiNhanh()
        {
            InitializeComponent();
            this.Load += UC_KH_ChiNhanh_Load;
        }

        private void UC_KH_ChiNhanh_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                // Tắt tự động tạo cột để khớp với thiết kế thủ công
                dataGridView_ChiNhanh.AutoGenerateColumns = false;

                // Nạp dữ liệu từ DAL
                DataTable dt = dal.GetThongTinChiNhanh();
                dataGridView_ChiNhanh.DataSource = dt;

                // Ánh xạ cột (DataPropertyName phải khớp với tên cột trong SQL SELECT)
                dataGridView_ChiNhanh.Columns["TenCN"].DataPropertyName = "TenCN";
                dataGridView_ChiNhanh.Columns["TGMo"].DataPropertyName = "TGMo";
                dataGridView_ChiNhanh.Columns["TGDong"].DataPropertyName = "TGDong";
                dataGridView_ChiNhanh.Columns["DiaChi"].DataPropertyName = "DiaChi";
                dataGridView_ChiNhanh.Columns["SDT"].DataPropertyName = "SDT";
                dataGridView_ChiNhanh.Columns["DVCungCap"].DataPropertyName = "DVCungCap";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin chi nhánh: " + ex.Message);
            }
        }
    }
}
