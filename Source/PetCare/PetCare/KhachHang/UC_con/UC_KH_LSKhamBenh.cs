using PetCare.HoiVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang
{
    public partial class UC_KH_LSKhamBenh : UserControl, IReturnToMainPage
    {
        private ServiceDAL serviceDAL = new ServiceDAL();
        private List<KhamBenhView> danhSachLSKB = new List<KhamBenhView>();

        public UC_KH_LSKhamBenh()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadData();
        }

        private void SetupDataGridView()
        {
            dataGridView_KH_LSKhamBenh.AutoGenerateColumns = false;

            // Thiết lập DataPropertyName
            dataGridView_KH_LSKhamBenh.Columns["MaLSDV"].DataPropertyName = "MaLichSuDichVu";
            dataGridView_KH_LSKhamBenh.Columns["ThuCung"].DataPropertyName = "ThuCung";
            dataGridView_KH_LSKhamBenh.Columns["NgayKham"].DataPropertyName = "NgayKham";
            dataGridView_KH_LSKhamBenh.Columns["BacSi"].DataPropertyName = "BacSi";
            dataGridView_KH_LSKhamBenh.Columns["TrieuChung"].DataPropertyName = "TrieuChung";
            dataGridView_KH_LSKhamBenh.Columns["ChanDoan"].DataPropertyName = "ChuanDoan";
            dataGridView_KH_LSKhamBenh.Columns["NgayHen"].DataPropertyName = "NgayHen";

            // Cột Nút Toa Thuốc (DataPropertyName không cần thiết, chỉ dùng cho logic)
            if (dataGridView_KH_LSKhamBenh.Columns["ToaThuoc"] is DataGridViewButtonColumn btnToaThuoc)
            {
                btnToaThuoc.UseColumnTextForButtonValue = true;
                btnToaThuoc.Text = "Xem chi tiết";
            }

            dataGridView_KH_LSKhamBenh.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView_KH_LSKhamBenh.Columns["NgayHen"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Xử lý giá trị trống cho Triệu chứng/Chuẩn đoán ngay trên Grid
            dataGridView_KH_LSKhamBenh.CellFormatting += (s, e) => {
                if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    if (dataGridView_KH_LSKhamBenh.Columns[e.ColumnIndex].Name == "NgayHen")
                        e.Value = "Không có hẹn";
                    else
                        e.Value = "---";
                }
            };

            dataGridView_KH_LSKhamBenh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đăng ký sự kiện Click cho nút Toa Thuốc
            dataGridView_KH_LSKhamBenh.CellContentClick += DataGridView_KH_LSKhamBenh_CellContentClick;
        }

        public void LoadData()
        {
            try
            {
                string maKH = SessionData.MaKH;
                if (string.IsNullOrEmpty(maKH)) return;

                // 2. Bắt đầu đo giờ
                Stopwatch timer = new Stopwatch();
                timer.Start();

                // Gọi xuống Database (đây là đoạn tốn thời gian nhất cần đo)
                danhSachLSKB = serviceDAL.GetLichSuKhamBenh(maKH);

                // Gán DataSource
                dataGridView_KH_LSKhamBenh.DataSource = danhSachLSKB;

                // 3. Dừng đo giờ
                timer.Stop();
                long thoiGianChay = timer.ElapsedMilliseconds;

                // 4. Hiển thị lên TextBox (txb_KH4_Time)
                // Giả sử bạn đã kéo một TextBox tên txb_KH4_Time vào giao diện
                if (txb_KH4_Time != null)
                {
                    txb_KH4_Time.Text = thoiGianChay.ToString() + " ms";

                    // Đổi màu để dễ nhìn hiệu suất
                    if (thoiGianChay < 100) // Nhanh
                    {
                        txb_KH4_Time.ForeColor = Color.Green;
                        txb_KH4_Time.BackColor = Color.FromArgb(220, 255, 220);
                    }
                    else // Chậm (cần tối ưu)
                    {
                        txb_KH4_Time.ForeColor = Color.Red;
                        txb_KH4_Time.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử khám bệnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }

        private UC_KH_XemToaThuoc UC_ToaThuoc = null;

        private void DataGridView_KH_LSKhamBenh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Lấy tên cột được click
            string columnName = dataGridView_KH_LSKhamBenh.Columns[e.ColumnIndex].Name;
            KhamBenhView selectedItem = danhSachLSKB[e.RowIndex];

            if (columnName == "ToaThuoc")
            {
                if (selectedItem.CoToaThuoc == 1)
                {
                    // 1. Ẩn DataGridView và các Control chính của UC_KH_LSKhamBenh
                    dataGridView_KH_LSKhamBenh.Visible = false;
                    btn_KH_QuayLai.Visible = false; // Ẩn nút Quay Lại chính 

                    // 2. Tải UC Xem Toa Thuốc
                    UC_ToaThuoc = new UC_KH_XemToaThuoc(selectedItem.MaLichSuDichVu);
                    pnl_KH_LSKhamBenh.Controls.Add(UC_ToaThuoc);

                    // 3. Cấu hình vị trí và hiển thị
                    CenterControlInPanel(UC_ToaThuoc, pnl_KH_LSKhamBenh);
                    UC_ToaThuoc.BringToFront();

                    // 4. Đăng ký Event Quay Lại từ UC Toa Thuốc về UC LSKhamBenh
                    UC_ToaThuoc.QuayVeTrangChu += UcToaThuoc_QuayVeTrangChu;
                }
                else
                {
                    MessageBox.Show("Không có toa thuốc được kê cho lần khám này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Hàm xử lý Quay lại từ UC Xem Toa Thuốc (BỔ SUNG)
        private void UcToaThuoc_QuayVeTrangChu()
        {
            if (UC_ToaThuoc != null)
            {
                // 1. Loại bỏ và dọn dẹp UC Toa Thuốc
                pnl_KH_LSKhamBenh.Controls.Remove(UC_ToaThuoc);
                UC_ToaThuoc.QuayVeTrangChu -= UcToaThuoc_QuayVeTrangChu;
                UC_ToaThuoc.Dispose();
                UC_ToaThuoc = null;

                // 2. Hiển thị lại DGV và nút Quay Lại chính
                dataGridView_KH_LSKhamBenh.Visible = true;
                btn_KH_QuayLai.Visible = true;
            }
        }

        private void CenterControlInPanel(Control controlToCenter, Panel parentPanel)
        {
            controlToCenter.Anchor = AnchorStyles.None;
            controlToCenter.Dock = DockStyle.None;
            int parentWidth = parentPanel.ClientSize.Width;
            int parentHeight = parentPanel.ClientSize.Height;
            int controlWidth = controlToCenter.Width;
            int controlHeight = controlToCenter.Height;

            if (controlWidth > 0 && controlHeight > 0)
            {
                controlToCenter.Left = (parentWidth - controlWidth) / 2;
                controlToCenter.Top = (parentHeight - controlHeight) / 2;
            }
            else
            {
                controlToCenter.Left = 0;
                controlToCenter.Top = 0;
            }
        }

        private void dataGridView_KH_LSKhamBenh_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txb_KH4_Time_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
