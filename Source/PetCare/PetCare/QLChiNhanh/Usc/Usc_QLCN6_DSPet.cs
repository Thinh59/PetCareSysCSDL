using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN6_DSPet : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN6_DSThuCung> danhSach = new BindingList<Class_QLCN6_DSThuCung>();

        public Usc_QLCN6_DSPet()
        {
            InitializeComponent();
            dgv_QLCN6_DSThuCung.AutoGenerateColumns = true;
            dgv_QLCN6_DSThuCung.DataSource = danhSach;
            this.Load += Usc_QLCN6_DSPet_Load;

            dgv_QLCN6_DSThuCung.CellClick += dgv_QLCN6_DSThuCung_CellContentClick;
        }

        private void Usc_QLCN6_DSPet_Load(object sender, EventArgs e)
        {
            LoadCombos();
        }

        private void LoadCombos()
        {
            // 1. Setup ComboBox Lọc Theo
            cmb_QLCN6_LocTheo.Items.Clear();
            cmb_QLCN6_LocTheo.Items.AddRange(new object[] { "Theo Năm", "Theo Quý", "Theo Tháng", "Theo Ngày" });
            cmb_QLCN6_LocTheo.SelectedIndex = 0; // Mặc định là Theo Năm

            // 2. Setup Năm
            cmb_QLCN6_Nam.Items.Clear();
            cmb_QLCN6_Nam.Items.Add("Tất cả"); // Thêm lựa chọn này đầu tiên

            int year = DateTime.Now.Year;
            for (int i = year; i >= year - 5; i--)
            {
                cmb_QLCN6_Nam.Items.Add(i);
            }
            cmb_QLCN6_Nam.SelectedIndex = 0;

            // 3. Setup Quý
            cmb_QLCN6_Quy.Items.Clear();
            for (int i = 1; i <= 4; i++) cmb_QLCN6_Quy.Items.Add(i);
            cmb_QLCN6_Quy.SelectedIndex = 0;

            // 4. Setup Tháng
            cmb_QLCN6_Thang.Items.Clear();
            for (int i = 1; i <= 12; i++) cmb_QLCN6_Thang.Items.Add(i);
            cmb_QLCN6_Thang.SelectedIndex = 0;

            // 5. Setup Ngày
            cmb_QLCN6_Ngay.Items.Clear();
            for (int i = 1; i <= 31; i++) cmb_QLCN6_Ngay.Items.Add(i);
            cmb_QLCN6_Ngay.SelectedIndex = 0;

            // 6. Setup Loại Dịch Vụ
            cmb_QLCN6_ChonDV.Items.Clear();
            cmb_QLCN6_ChonDV.Items.Add("Khám bệnh");
            cmb_QLCN6_ChonDV.Items.Add("Tiêm phòng");
            cmb_QLCN6_ChonDV.SelectedIndex = 0;
        }

        // --- NÚT THỐNG KÊ (Hiện danh sách) ---
        private void btn_QLCN6_ThongKe_Click(object sender, EventArgs e)
        {
            // Lấy tham số
            int nam = 0;
            if (cmb_QLCN6_Nam.SelectedItem != null && cmb_QLCN6_Nam.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN6_Nam.SelectedItem.ToString(), out nam);

            int quy = 0, thang = 0, ngay = 0;
            if (cmb_QLCN6_Quy.SelectedIndex > 0) int.TryParse(cmb_QLCN6_Quy.SelectedItem.ToString(), out quy);
            if (cmb_QLCN6_Thang.SelectedIndex > 0) int.TryParse(cmb_QLCN6_Thang.SelectedItem.ToString(), out thang);
            if (cmb_QLCN6_Ngay.SelectedIndex > 0) int.TryParse(cmb_QLCN6_Ngay.SelectedItem.ToString(), out ngay);

            string loaiDV = cmb_QLCN6_ChonDV.SelectedIndex == 0 ? "KHAM" : "TIEM";

            try
            {
                DataTable dt = serviceDAL.GetThongKePet(nam, quy, thang, ngay, loaiDV);
                danhSach.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    danhSach.Add(new Class_QLCN6_DSThuCung
                    {
                        MaPet = row["MaPet"].ToString(),
                        TenPet = row["TenPet"].ToString(),
                        LoaiPet = row["LoaiPet"].ToString(),
                        SoLan = Convert.ToInt32(row["SoLan"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- NÚT CHI TIẾT LỊCH SỬ (Chuyển Form) ---
        private void btn_QLCN6_LichSu_Click(object sender, EventArgs e)
        {
            // Lấy Mã Pet đang chọn trên lưới
            string maPetChon = "";

            // Cách 1: Chọn từ Grid
            if (dgv_QLCN6_DSThuCung.CurrentRow != null)
            {
                maPetChon = dgv_QLCN6_DSThuCung.CurrentRow.Cells["MaPet"].Value?.ToString();
            }

            // Cách 2: Nếu Grid chưa chọn, xem có nhập tay ở ô ComboBox/Textbox Mã không
            if (string.IsNullOrEmpty(maPetChon) && !string.IsNullOrEmpty(cmb_QLCN6_MaThuCung.Text))
            {
                maPetChon = cmb_QLCN6_MaThuCung.Text;
            }

            if (string.IsNullOrEmpty(maPetChon))
            {
                MessageBox.Show("Vui lòng chọn một thú cưng từ danh sách hoặc nhập mã để xem lịch sử.");
                return;
            }

            // --- QUAN TRỌNG: TRUYỀN MÃ PET SANG FORM CON ---
            Usc_QLCN6_LichSu ucLichSu = new Usc_QLCN6_LichSu(maPetChon); // Truyền qua Constructor
            ucLichSu.Dock = DockStyle.Fill;

            pnl_QLCN6_UC.Controls.Clear();
            pnl_QLCN6_UC.Controls.Add(ucLichSu);
            pnl_QLCN6_UC.Visible = true;
            pnl_QLCN6_UC.BringToFront();
        }

        private void btn_QLCN6_QuayLai1_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                // 1. Ẩn cái Panel đi -> Để lộ ra giao diện danh sách bên dưới
                this.Parent.Visible = false;

                // 2. Xóa UserControl này khỏi Panel để giải phóng bộ nhớ (reset trạng thái cho lần mở sau)
                this.Parent.Controls.Remove(this);

                // 3. Hủy đối tượng (Optional - tốt cho RAM)
                this.Dispose();
            }
        }

        // Sự kiện rác
        private void cmb_QLCN6_Nam_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgv_QLCN6_DSThuCung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng click vào hàng dữ liệu (e.RowIndex >= 0)
            // Tránh lỗi khi click vào hàng tiêu đề (header)
            if (e.RowIndex >= 0)
            {
                // Lấy dòng hiện tại
                DataGridViewRow row = dgv_QLCN6_DSThuCung.Rows[e.RowIndex];

                // Lấy mã thú cưng từ cột "MaPet" (Đảm bảo tên cột khớp với Class_QLCN6_DSThuCung)
                string maPet = row.Cells["MaPet"].Value?.ToString();

                if (!string.IsNullOrEmpty(maPet))
                {
                    // Hiển thị mã vào ComboBox
                    cmb_QLCN6_MaThuCung.Text = maPet;

                    // Nếu ComboBox có DataSource, hãy dùng SelectedValue thay vì Text:
                    // cmb_QLCN6_MaThuCung.SelectedValue = maPet;
                }
            }
        }
        private void cmb_QLCN6_MaThuCung_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN6_ChonDV_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN6_Quy_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN6_LocTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loc = cmb_QLCN6_LocTheo.SelectedItem.ToString();

            // Mặc định: Năm luôn hiện, các cái khác ẩn
            cmb_QLCN6_Nam.Enabled = true;
            cmb_QLCN6_Quy.Enabled = false;
            cmb_QLCN6_Thang.Enabled = false;
            cmb_QLCN6_Ngay.Enabled = false;

            if (loc == "Theo Quý") cmb_QLCN6_Quy.Enabled = true;
            else if (loc == "Theo Tháng") cmb_QLCN6_Thang.Enabled = true;
            else if (loc == "Theo Ngày")
            {
                cmb_QLCN6_Thang.Enabled = true;
                cmb_QLCN6_Ngay.Enabled = true;
            }
        }
        private void cmb_QLCN6_Thang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN6_Ngay_SelectedIndexChanged(object sender, EventArgs e) { }

        private void pnl_QLCN6_UC_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}