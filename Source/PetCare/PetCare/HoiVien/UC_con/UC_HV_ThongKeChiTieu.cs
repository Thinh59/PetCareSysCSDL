using PetCare.KhachHang;
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
    public partial class UC_HV_ThongKeChiTieu : UserControl
    {
        ServiceDAL dal = new ServiceDAL();
        public event Action QuayVeTrangChu;

        public UC_HV_ThongKeChiTieu()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadThongKe();
        }
        private void LoadThongKe()
        {
            try
            {
                DataSet ds = dal.GetThongKeChiTieu(SessionData.MaKH);

                // 1. Hiển thị tổng chi tiêu năm hiện tại vào TextBox
                if (ds.Tables[0].Rows.Count > 0)
                {
                    decimal tongHienTai = Convert.ToDecimal(ds.Tables[0].Rows[0]["TongChiTieuHienTai"]);
                    textBox_HV_CTieuHienTai.Text = tongHienTai.ToString("N0"); // Định dạng tiền tệ
                }

                // 2. Đổ dữ liệu lịch sử vào DataGridView
                dataGridView_HV_ChiTieuNam.AutoGenerateColumns = false;
                dataGridView_HV_ChiTieuNam.DataSource = ds.Tables[1];

                // Mapping cột
                dataGridView_HV_ChiTieuNam.Columns["Nam"].DataPropertyName = "Nam";
                dataGridView_HV_ChiTieuNam.Columns["ChiTieu"].DataPropertyName = "ChiTieu";

                // Định dạng hiển thị tiền cho lưới
                dataGridView_HV_ChiTieuNam.Columns["ChiTieu"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê chi tiêu: " + ex.Message);
            }
        }

        private void btn_HV_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke(); // Quay lại trang thông tin hội viên
        }
    }
}
