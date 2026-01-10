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
    public partial class UC_KH_LSTiemPhong : UserControl, IReturnToMainPage
    {
        public event Action QuayVeTrangChu;

        private ServiceDAL serviceDAL = new ServiceDAL();
        private List<TiemPhongView> danhSachLSTP = new List<TiemPhongView>();

        public UC_KH_LSTiemPhong()
        {
            InitializeComponent();
            SetupDataGridView(); // Thiết lập cột
            LoadData();          // Tải dữ liệu
        }

        private void SetupDataGridView()
        {
            dataGridView_KH_LSTiemPhong.AutoGenerateColumns = false;

            // Đảm bảo tên cột trên Designer khớp với tên được sử dụng ở đây

            // Cột MaLSDV
            dataGridView_KH_LSTiemPhong.Columns["MaLSDV"].DataPropertyName = "MaLSDV";

            // Cột MaThuCung
            dataGridView_KH_LSTiemPhong.Columns["MaThuCung"].DataPropertyName = "MaThuCung";

            // Cột TenThuCung
            dataGridView_KH_LSTiemPhong.Columns["TenThuCung"].DataPropertyName = "TenThuCung";

            // Cột NgayTiem
            dataGridView_KH_LSTiemPhong.Columns["NgayTiem"].DataPropertyName = "NgayTiem";

            // Cột LoaiVacxin (Hiển thị TenVacXin từ SP)
            dataGridView_KH_LSTiemPhong.Columns["LoaiVacxin"].DataPropertyName = "TenVacXin";

            // Cột LieuLuong
            dataGridView_KH_LSTiemPhong.Columns["LieuLuong"].DataPropertyName = "LieuLuong";

            // Cột BacSi (Hiển thị tên Bác sĩ từ SP)
            dataGridView_KH_LSTiemPhong.Columns["BacSi"].DataPropertyName = "BacSiPhuTrach";

            // Cột MaGoiTiem
            dataGridView_KH_LSTiemPhong.Columns["MaGoiTiem"].DataPropertyName = "MaGoiTiem";

            dataGridView_KH_LSTiemPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadData()
        {
            try
            {
                string maKH = SessionData.MaKH; // Lấy Mã KH từ Session
                if (string.IsNullOrEmpty(maKH)) return;

                danhSachLSTP = serviceDAL.GetLichSuTiemPhong(maKH);

                // Gán DataSource (Liên kết dữ liệu)
                dataGridView_KH_LSTiemPhong.DataSource = danhSachLSTP;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử tiêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
