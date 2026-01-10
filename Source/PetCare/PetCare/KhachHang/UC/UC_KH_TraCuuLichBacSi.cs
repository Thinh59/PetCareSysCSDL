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
    public partial class UC_KH_TraCuuLichBacSi : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_TraCuuLichBacSi()
        {
            InitializeComponent();

            LoadComboBoxChiNhanh();

            dateTimePicker_ChonNgay.MinDate = DateTime.Today;
            dataGridView_LichBS.CellFormatting += dataGridView_LichBS_CellFormatting;

            btn_KH_ApDung.Click += (s, e) => LoadLichBacSi();

            this.Load += UC_KH_TraCuuLichBacSi_Load;

            btn_KH_TimKiem.Click += (s, e) => TimKiemBacSi();

            // Reset khi thay đổi lựa chọn để tránh sai lệch dữ liệu cũ
            comboBox_KH_ChonCN.SelectedIndexChanged += (s, e) => dataGridView_LichBS.DataSource = null;
            dateTimePicker_ChonNgay.ValueChanged += (s, e) => dataGridView_LichBS.DataSource = null;

            // Tối ưu Grid
            dataGridView_LichBS.AutoGenerateColumns = false;
        }
        private void UC_KH_TraCuuLichBacSi_Load(object sender, EventArgs e)
        {
            // Hiển thị thông báo nhắc nhở người dùng
            MessageBox.Show("Chào mừng! Hãy chọn 'Chi nhánh' và 'Ngày' rồi nhấn 'Áp dụng' để xem lịch!",
                    "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Đảm bảo Grid trống khi vừa mở trang
            dataGridView_LichBS.DataSource = null;
        }
        private void LoadComboBoxChiNhanh()
        {
            DataTable dtCN = dal.GetDanhSachChiNhanh();
            comboBox_KH_ChonCN.DataSource = dtCN;
            comboBox_KH_ChonCN.DisplayMember = "TenCN";
            comboBox_KH_ChonCN.ValueMember = "MaCN";
        }

        private void LoadLichBacSi()
        {
            try
            {
                if (comboBox_KH_ChonCN.SelectedValue == null || string.IsNullOrEmpty(comboBox_KH_ChonCN.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn một chi nhánh cụ thể!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCN = comboBox_KH_ChonCN.SelectedValue.ToString();
                DateTime ngayTraCuu = dateTimePicker_ChonNgay.Value;

                DataTable dt = dal.GetLichBacSi(maCN, ngayTraCuu);

                if (dt == null || dt.Rows.Count == 0)
                {
                    dataGridView_LichBS.DataSource = null;
                    MessageBox.Show("Không có dữ liệu bác sĩ cho ngày và chi nhánh đã chọn.", "Thông báo");
                    return;
                }

                if (dataGridView_LichBS.Columns.Contains("MaBS"))
                    dataGridView_LichBS.Columns["MaBS"].DataPropertyName = "MaBS";

                if (dataGridView_LichBS.Columns.Contains("TenBS"))
                    dataGridView_LichBS.Columns["TenBS"].DataPropertyName = "TenBS";

                if (dataGridView_LichBS.Columns.Contains("Ngay"))
                {
                    dataGridView_LichBS.Columns["Ngay"].DataPropertyName = "Ngay";
                    dataGridView_LichBS.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }    

                if (dataGridView_LichBS.Columns.Contains("TrangThai"))
                    dataGridView_LichBS.Columns["TrangThai"].DataPropertyName = "TrangThai";

                if (dataGridView_LichBS.Columns.Contains("SoLichHen"))
                    dataGridView_LichBS.Columns["SoLichHen"].DataPropertyName = "SoLichHen";

                dataGridView_LichBS.DataSource = dt;

                dataGridView_LichBS.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void dataGridView_LichBS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView_LichBS.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string text = e.Value.ToString();

                // Thiết lập Font chữ đậm mặc định
                e.CellStyle.Font = new Font(dataGridView_LichBS.Font, FontStyle.Bold);

                if (text.Contains("Còn trống"))
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.SelectionForeColor = Color.Green;
                }
                else if (text.Contains("Khá bận"))
                {
                    e.CellStyle.ForeColor = Color.Orange;
                    e.CellStyle.SelectionForeColor = Color.Orange;
                }
                else if (text.Contains("Đã đầy"))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.SelectionForeColor = Color.Red;
                }
            }
        }

        private void TimKiemBacSi()
        {
            if (dataGridView_LichBS.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.Format("TenBS LIKE '%{0}%'", textBox_KH_TimKiem.Text.Trim());
            }
            else
            {
                MessageBox.Show("Vui lòng nhấn 'Áp dụng' để tải danh sách trước khi tìm kiếm.");
            }
        }

        private void btn_KH_ApDung_Click(object sender, EventArgs e)
        {

        }
    }
}
