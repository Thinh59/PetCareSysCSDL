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
    public partial class UC_KH_DanhGiaDichVu : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_DanhGiaDichVu()
        {
            InitializeComponent();
            LoadDichVu();
            SetupRatingComboBoxes();
        }

        private void LoadDichVu()
        {
            DataTable dt = dal.GetDichVuChoDanhGia(SessionData.MaKH);
            comboBox_KH_ChonDichVu.DataSource = dt;
            comboBox_KH_ChonDichVu.DisplayMember = "TenDV";
            comboBox_KH_ChonDichVu.ValueMember = "MaDichVu";
        }

        private void SetupRatingComboBoxes()
        {
            // Đổ số từ 1-5 vào các combo chọn điểm
            object[] levels = { 1, 2, 3, 4, 5 };
            comboBox_KH_TDNV.Items.AddRange(levels);
            comboBox_KH_CLDV.Items.AddRange(levels);
            comboBox_KH_TDNV.SelectedIndex = 4; // Mặc định 5 sao
            comboBox_KH_CLDV.SelectedIndex = 4;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (comboBox_KH_ChonDichVu.SelectedValue == null || comboBox_KH_ChonDichVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ đã hoàn thành để đánh giá!", "Thông báo");
                return;
            }

            try
            {
                string maDV = comboBox_KH_ChonDichVu.SelectedValue?.ToString();

                int diemDV = Convert.ToInt32(comboBox_KH_CLDV.SelectedItem ?? 5);
                int diemNV = Convert.ToInt32(comboBox_KH_TDNV.SelectedItem ?? 5);
                string binhLuan = textBox_KH_BinhLuan.Text.Trim();

                bool ThanhCong = dal.LuuDanhGia(SessionData.MaKH, maDV, diemDV, diemNV, binhLuan);
                if (ThanhCong)
                {
                    MessageBox.Show("Cảm ơn bạn đã gửi đánh giá dịch vụ!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox_KH_BinhLuan.Clear();
                    LoadDichVu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu đánh giá: " + ex.Message);
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                this.Hide();
                parentForm.Show();
                parentForm.BringToFront();
            }
        }
    }
}
