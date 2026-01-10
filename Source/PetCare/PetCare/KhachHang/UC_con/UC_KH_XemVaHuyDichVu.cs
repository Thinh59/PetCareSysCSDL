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
    public partial class UC_KH_XemVaHuyDichVu : UserControl, IReturnToMainPage
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_XemVaHuyDichVu()
        {
            InitializeComponent();
            // Gán sự kiện load dữ liệu khi UserControl sẵn sàng
            this.Load += UC_KH_XemVaHuyDichVu_Load;
        }

        private void UC_KH_XemVaHuyDichVu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Hàm nạp dữ liệu vào DataGridView
        public void LoadData()
        {
            try
            {
                dataGridView_KH_XemHuyDV.AutoGenerateColumns = false;
                DataTable dt = dal.GetDichVuDaDat(SessionData.MaKH);
                if (dt != null)
                {
                    dataGridView_KH_XemHuyDV.DataSource = dt;
                    dataGridView_KH_XemHuyDV.Columns["Ngay"].DataPropertyName = "Ngay";
                    dataGridView_KH_XemHuyDV.Columns["MaLSDV"].DataPropertyName = "MaLSDV";
                    dataGridView_KH_XemHuyDV.Columns["MaDV"].DataPropertyName = "MaDV";
                    dataGridView_KH_XemHuyDV.Columns["TenDV"].DataPropertyName = "TenDV";
                    dataGridView_KH_XemHuyDV.Columns["TrangThai"].DataPropertyName = "TrangThai";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }
        private void dataGridView_KH_XemHuyDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_KH_XemHuyDV.Columns[e.ColumnIndex].Name == "HuyDV" && e.RowIndex >= 0)
            {
                string maLSDV = dataGridView_KH_XemHuyDV.Rows[e.RowIndex].Cells["MaLSDV"].Value.ToString();
                if (string.IsNullOrEmpty(maLSDV))
                {
                    MessageBox.Show("Không thể xác định mã dịch vụ cho dòng này.", "Thông báo");
                    return;
                }

                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn hủy dịch vụ {maLSDV}?",
                                                    "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool thanhCong = dal.HuyDichVu(maLSDV);

                        if (thanhCong)
                        {
                            MessageBox.Show("Đã hủy dịch vụ thành công!", "Thông báo");
                            LoadData();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị lỗi từ RAISERROR trong SQL (như lỗi đã có hóa đơn, v.v.)
                        MessageBox.Show("Lỗi: " + ex.Message, "Không thể hủy");
                    }
                }
            }
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
