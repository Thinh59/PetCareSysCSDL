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
    public partial class UC_KH_QLThuCung : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<ThuCungView> danhSachTC = new BindingList<ThuCungView>();

        public UC_KH_QLThuCung()
        {
            InitializeComponent();

            SetupDataGridView();
            this.Load += (sender, e) => LoadData();
        }

        private void pnl_KH_Content_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void CenterPanel()
        {
            // Lấy kích thước của Form hiện tại
            int formWidth = pnl_KH_Content.Width;
            int formHeight = pnl_KH_Content.Height;

            // Lấy kích thước của panel cần căn giữa (pnlContent)
            int panelWidth = pnl_KH_QLThuCung.Width;
            int panelHeight = pnl_KH_QLThuCung.Height;

            // Tính toán vị trí Left và Top mới để panel nằm giữa
            pnl_KH_QLThuCung.Left = (formWidth - panelWidth) / 2;
            pnl_KH_QLThuCung.Top = (formHeight - panelHeight) / 2;
        }

        private void UCCenterPanel(UserControl uc)
        {
            // Lấy kích thước của Form hiện tại
            int formWidth = pnl_KH_Content.Width;
            int formHeight = pnl_KH_Content.Height;

            // Lấy kích thước của panel cần căn giữa (pnlContent)
            int panelWidth = uc.Width;
            int panelHeight = uc.Height;

            // Tính toán vị trí Left và Top mới để panel nằm giữa
            uc.Left = (formWidth - panelWidth) / 2;
            uc.Top = (formHeight - panelHeight) / 2;

            uc.Anchor = AnchorStyles.None;
            uc.Dock = DockStyle.None;
            uc.BringToFront();
        }

        private void SetupDataGridView()
        {
            // Liên kết DataGridView với danh sách DTO
            dataGridView_QLThuCung.AutoGenerateColumns = false;
            dataGridView_QLThuCung.DataSource = danhSachTC;

            // Xóa hết các cột đang có trên Designer.cs nếu bạn dùng AutoGenerateColumns = false
            //dataGridView_QLThuCung.Columns.Clear();

            // Khai báo các cột dữ liệu (Ví dụ)
            //dataGridView_QLThuCung.Columns.Add("MaThuCung", "Mã");
            dataGridView_QLThuCung.Columns["MaTC"].DataPropertyName = "MaThuCung";

            // Cột Tên Thú Cưng
            //dataGridView_QLThuCung.Columns.Add("TenThuCung", "Tên");
            dataGridView_QLThuCung.Columns["TenTC"].DataPropertyName = "Ten"; // Dùng tên cột SQL

            // Cột Loại Thú Cưng 
            //dataGridView_QLThuCung.Columns.Add("LoaiThuCung", "Loại");
            dataGridView_QLThuCung.Columns["LoaiTC"].DataPropertyName = "Loai";

            // Cột Giống
            //dataGridView_QLThuCung.Columns.Add("Giong", "Giống");
            dataGridView_QLThuCung.Columns["GiongTC"].DataPropertyName = "Giong";

            // Cột Ngày Sinh
            //dataGridView_QLThuCung.Columns.Add("NgaySinh", "Ngày Sinh");
            dataGridView_QLThuCung.Columns["NgaySinhTC"].DataPropertyName = "NgaySinh";

            // Cột Giới tính
            //dataGridView_QLThuCung.Columns.Add("GioiTinh", "Giới tính");
            dataGridView_QLThuCung.Columns["GioiTinhTC"].DataPropertyName = "GioiTinh";

            // Cột Tình trạng sức khỏe
            //dataGridView_QLThuCung.Columns.Add("TinhTrangSK", "Tình trạng sức khỏe");
            dataGridView_QLThuCung.Columns["TTSK"].DataPropertyName = "TinhTrangSucKhoe";

            string buttonColumnName = "Sua";

            // Kiểm tra và sửa đổi cột Sửa
            if (dataGridView_QLThuCung.Columns.Contains(buttonColumnName))
            {
                // Kiểm tra xem cột có phải là loại nút bấm không
                if (dataGridView_QLThuCung.Columns[buttonColumnName] is DataGridViewButtonColumn btnSua)
                {
                    // Thiết lập Text cho nút
                    btnSua.HeaderText = "Sửa"; // Tiêu đề cột
                    btnSua.Text = "Sửa"; // Text trên nút
                    btnSua.Name = "Sua";
                    btnSua.UseColumnTextForButtonValue = true; // Bật hiển thị Text trên nút
                }
            }

            string deleteColumnName = "XoaTC";
            if (dataGridView_QLThuCung.Columns.Contains(deleteColumnName))
            {
                if (dataGridView_QLThuCung.Columns[deleteColumnName] is DataGridViewButtonColumn btnXoa)
                {
                    btnXoa.HeaderText = "Xóa";
                    btnXoa.Text = "Xóa";
                    btnXoa.Name = "XoaTC";
                    btnXoa.UseColumnTextForButtonValue = true;
                }
            }

            // Đăng ký sự kiện click cell (chỉ cần làm 1 lần)
            dataGridView_QLThuCung.CellContentClick -= dataGridView_QLThuCung_CellContentClick;
            dataGridView_QLThuCung.CellContentClick += dataGridView_QLThuCung_CellContentClick;

            dataGridView_QLThuCung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- HÀM TẢI DỮ LIỆU ---
        public void LoadData()
        {
            try
            {
                string maKH = SessionData.MaKH;
                if (string.IsNullOrEmpty(maKH)) return;

                DataTable dt = serviceDAL.GetDSTC(maKH);
                danhSachTC.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    danhSachTC.Add(new ThuCungView
                    {
                        MaThuCung = row["MaThuCung"].ToString(),
                        Ten = row["TenThuCung"].ToString(),
                        Loai = row["LoaiThuCung"].ToString(),
                        Giong = row["Giong_TC"].ToString(),
                        NgaySinh = Convert.ToDateTime(row["NgaySinh_TC"]),
                        GioiTinh = row["GioiTinh_TC"].ToString(),
                        TinhTrangSucKhoe = row["TinhTrangSK"].ToString()
                    });
                }
                dataGridView_QLThuCung.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thú cưng: " + ex.Message);
            }
        }

        private UC_KH_ThemThuCung UC_ThemThuCung = null;

        private void btn_KH_ThemThuCung_Click(object sender, EventArgs e)
        {
            pnl_KH_Content.Controls.Clear();

            UC_ThemThuCung = new UC_KH_ThemThuCung(this);
            pnl_KH_Content.Controls.Add(UC_ThemThuCung);

            UCCenterPanel(UC_ThemThuCung);

            // Đăng ký Event QuayVeTrangChu
            UC_ThemThuCung.QuayVeTrangChu += UcThem_QuayVeTrangChu;
        }

        private void UcThem_QuayVeTrangChu()
        {
            pnl_KH_Content.Controls.Clear();
            pnl_KH_Content.Controls.Add(pnl_KH_QLThuCung);
            pnl_KH_QLThuCung.Visible = true;
            LoadData();
        }

        private UC_KH_SuaThuCung UC_SuaThuCung = null;
        private void dataGridView_QLThuCung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Lấy dữ liệu DTO từ dòng được click
            var item = dataGridView_QLThuCung.Rows[e.RowIndex].DataBoundItem as ThuCungView;
            if (item == null) return;

            string columnName = dataGridView_QLThuCung.Columns[e.ColumnIndex].Name;

            // 1. Xử lý nút Sửa
            if (columnName == "Sua")
            {
                // Chuyển sang màn hình Sửa
                pnl_KH_QLThuCung.Visible = false;
                UC_SuaThuCung = new UC_KH_SuaThuCung(this, item.MaThuCung);
                pnl_KH_Content.Controls.Add(UC_SuaThuCung);
                UCCenterPanel(UC_SuaThuCung);

                UC_SuaThuCung.QuayVeTrangChu += UcSua_QuayVeTrangChu;
            }
            // 2. Xử lý nút Xóa
            else if (columnName == "XoaTC")
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa thú cưng '{item.Ten}' (Mã: {item.MaThuCung})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string maKH = SessionData.MaKH;
                        serviceDAL.XoaThuCung(item.MaThuCung, maKH);
                        MessageBox.Show("Xóa thú cưng thành công!");
                        LoadData(); // Tải lại dữ liệu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi Database");
                    }
                }
            }
        }

        private void UcSua_QuayVeTrangChu()
        {
            if (UC_SuaThuCung != null)
            {
                // 1. Loại bỏ User Control Sửa khỏi Panel chứa nó
                pnl_KH_Content.Controls.Remove(UC_SuaThuCung);

                // 2. Hiển thị lại Panel chính (chứa DGV)
                pnl_KH_QLThuCung.Visible = true;

                // 3. Tải lại dữ liệu
                LoadData();

                // 4. Dọn dẹp
                UC_SuaThuCung.Dispose();
                UC_SuaThuCung = null;
            }
        }
    }
}
