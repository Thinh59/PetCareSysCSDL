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
    public partial class UC_KH_TaoGoiTiem : UserControl
    {
        ServiceDAL dal = new ServiceDAL();
        private DataTable dtNoiDungGoi;
        public event Action QuayVeTrangCu;

        public UC_KH_TaoGoiTiem()
        {
            InitializeComponent();
            // Đăng ký sự kiện Load để tự động chạy khi UC được hiển thị
            this.Load += UC_KH_TaoGoiTiem_Load;
        }

        private void UC_KH_TaoGoiTiem_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Tạo cấu trúc DataTable khớp với Type "UDT_NoiDungGoiTiem" trong SQL
                dtNoiDungGoi = new DataTable();
                dtNoiDungGoi.Columns.Add("MaVacXin", typeof(string));
                dtNoiDungGoi.Columns.Add("SoMui", typeof(int));

                // 2. Cấu hình DataGridView trước khi đổ dữ liệu
                dataGridView_KH_VaccineSoMui.AutoGenerateColumns = false;

                if (dataGridView_KH_VaccineSoMui.Columns.Contains("MaVacXin"))
                    dataGridView_KH_VaccineSoMui.Columns["MaVacXin"].DataPropertyName = "MaVacXin";

                if (dataGridView_KH_VaccineSoMui.Columns.Contains("TenVacXin"))
                    dataGridView_KH_VaccineSoMui.Columns["TenVacXin"].DataPropertyName = "TenVacXin";

                if (dataGridView_KH_VaccineSoMui.Columns.Contains("SoMui"))
                    dataGridView_KH_VaccineSoMui.Columns["SoMui"].DataPropertyName = "SoMuiTonKho";

                // 3. Gọi hàm load dữ liệu từ DB
                LoadDanhSachVacXin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }

        private void LoadDanhSachVacXin()
        {
            DataTable dt = dal.GetDanhSachVacXin();

            if (dt != null && dt.Rows.Count > 0)
            {
                dataGridView_KH_VaccineSoMui.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu vắc xin trong hệ thống.");
            }
        }

        private void dataGridView_KH_VaccineSoMui_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_KH_VaccineSoMui.Columns[e.ColumnIndex].Name == "ThemVaoGT")
            {
                string maVX = dataGridView_KH_VaccineSoMui.Rows[e.RowIndex].Cells["MaVacXin"].Value.ToString();

                DataRow existingRow = dtNoiDungGoi.AsEnumerable()
                                                 .FirstOrDefault(r => r.Field<string>("MaVacXin") == maVX);

                if (existingRow != null)
                {
                    existingRow["SoMui"] = (int)existingRow["SoMui"] + 1;
                }
                else
                {
                    dtNoiDungGoi.Rows.Add(maVX, 1);
                }

                MessageBox.Show("Đã thêm vaccine vào danh sách gói.");
            }
        }

        private void btn_KH_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox_KH_TenGT.Text))
                    throw new Exception("Vui lòng nhập tên gói tiêm.");
                if (dtNoiDungGoi.Rows.Count == 0)
                    throw new Exception("Vui lòng chọn ít nhất một loại vaccine cho gói.");

                string tenGoi = textBox_KH_TenGT.Text;
                int soThang = int.TryParse(textBox_KH_SoThang.Text, out int s) ? s : 0;

                DataTable result = dal.TaoGoiTiemMoi(tenGoi, soThang, dtNoiDungGoi);

                if (result.Rows.Count > 0 && result.Rows[0]["Status"].ToString() == "Success")
                {
                    // Hiển thị các giá trị do SQL tính toán (Ưu đãi, Tổng tiền)
                    textBox_KH_CPBD.Text = Convert.ToDecimal(result.Rows[0]["ChiPhiBanDau"]).ToString("N0");
                    textBox_KH_UuDai.Text = result.Rows[0]["UuDai"].ToString() + " %";
                    textBox_KH_ChiPhi.Text = Convert.ToDecimal(result.Rows[0]["TongTien"]).ToString("N0");

                    MessageBox.Show("Tạo gói tiêm thành công! Mã gói: " + result.Rows[0]["MaGoiMoi"], "Thông báo");

                    QuayVeTrangCu?.Invoke();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + result.Rows[0]["Message"]);
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Lỗi: " + ex.Message); 
            }
        }

        private void btn_KH_Huy_Click(object sender, EventArgs e)
        {
            QuayVeTrangCu?.Invoke();
        }
    }
}
