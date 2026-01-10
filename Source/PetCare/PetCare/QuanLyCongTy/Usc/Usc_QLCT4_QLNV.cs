using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT4_QLNV : UserControl
    {
        // 1. Initialize Service_DAL
        private ServiceDAL service = new ServiceDAL();
        private BindingList<Class_QLCT4_QLNV> danhSach = new BindingList<Class_QLCT4_QLNV>();

        public Usc_QLCT4_QLNV()
        {
            InitializeComponent();
            dgv_QLCT4_QLNV.DataSource = danhSach;

            // Auto-generate columns must be true or columns manually added in designer
            dgv_QLCT4_QLNV.AutoGenerateColumns = true;
        }

        private void Usc_QLCT4_QLNV_Load(object sender, EventArgs e)
        {
            // Reset DatePickers to a "null-like" visual state or default
            dtp_QLCT4_getNgaySinh.Value = new DateTime(1900, 1, 1);
            dtp_QLCT4_getNgayVaoLam.Value = new DateTime(1900, 1, 1);

            // Optional: Pre-load ComboBoxes with Data (e.g., Branch IDs)
            LoadComboBoxData();

            // Load initial list (Show all)
            LoadDataToGrid();
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Populate Branch ComboBox
                List<string> branches = service.GetDanhSachMaChiNhanh();
                cmb_QLCT4_getChiNhanh.Items.Clear();
                foreach (var cn in branches)
                {
                    cmb_QLCT4_getChiNhanh.Items.Add(cn);
                }

                // Add static items for Status if not in DB (Example)
                cmb_QLCT4_getTrangThai.Items.Clear();
                cmb_QLCT4_getTrangThai.Items.Add("Đang làm việc");
                cmb_QLCT4_getTrangThai.Items.Add("Đã nghỉ việc");

                // Add static items for Position
                cmb_QLCT4_getChucVu.Items.Clear();
                cmb_QLCT4_getChucVu.Items.Add("Tiếp tân");
                cmb_QLCT4_getChucVu.Items.Add("Bác sĩ");
                cmb_QLCT4_getChucVu.Items.Add("Quản lý chi nhánh");
                cmb_QLCT4_getChucVu.Items.Add("Quản lý công ty");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu danh mục: " + ex.Message);
            }
        }

        private void LoadDataToGrid(DataTable dtSource = null)
        {
            danhSach.Clear();
            DataTable dt = dtSource;

            if (dt == null)
            {
                // If no specific source provided, load all
                dt = service.XemNhanVien();
            }

            foreach (DataRow row in dt.Rows)
            {
                danhSach.Add(new Class_QLCT4_QLNV
                {
                    MaNV = row["MaNV"].ToString(),
                    TenNV = row["HoTenNV"].ToString(),
                    ChucVu = row["ChucVu"].ToString(),
                    Luong = row["Luong"] != DBNull.Value ? Convert.ToInt32(row["Luong"]) : 0,
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    ChiNhanh = row["ChiNhanhLamViec"].ToString(),
                    TrangThai = row["TrangThaiNV"].ToString(),
                    NgaySinh = row["NgaySinhNV"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinhNV"]) : new DateTime(1900, 1, 1),
                    NgayVaoLam = row["NgayVaoLam"] != DBNull.Value ? Convert.ToDateTime(row["NgayVaoLam"]) : new DateTime(1900, 1, 1)
                });
            }
        }

        private void btn_QLCT2_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT4_selectAction.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hành động!");
                return;
            }

            string action = cmb_QLCT4_selectAction.SelectedItem.ToString();
            string maNV = txb_QLCT4_getMaNV.Text.Trim();

            try
            {
                switch (action)
                {
                    case "Thêm":
                        XuLyThem(maNV);
                        break;
                    case "Sửa":
                        XuLySua(maNV);
                        break;
                    case "Xóa":
                        XuLyXoa(maNV);
                        break;
                    case "Xem":
                        XuLyXem();
                        break;
                    default:
                        MessageBox.Show("Hành động không hợp lệ.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyThem(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Mã nhân viên không được để trống.");
                return;
            }

            string hoTen = txb_QLCT4_getTenNV.Text.Trim();
            string chucVu = cmb_QLCT4_getChucVu.Text;
            string chiNhanh = cmb_QLCT4_getChiNhanh.Text;
            string trangThai = cmb_QLCT4_getTrangThai.Text;
            string tenDangNhap = txb_QLCT4_getTenDangNhap.Text.Trim();

            int luong = 0;
            if (!int.TryParse(txb_QLCT4_getLuong.Text, out luong)) luong = 0;

            DateTime ngaySinh = dtp_QLCT4_getNgaySinh.Value;
            DateTime ngayVaoLam = dtp_QLCT4_getNgayVaoLam.Value;

            bool result = service.ThemNhanVien(maNV, hoTen, ngaySinh, chucVu, ngayVaoLam, luong, chiNhanh, trangThai, tenDangNhap);

            if (result)
            {
                MessageBox.Show("Thêm thành công! Mật khẩu mặc định là: " + maNV + "password");
                LoadDataToGrid(); // Refresh grid
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại.");
            }
        }

        private void XuLySua(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên cần sửa.");
                return;
            }

            // 1. Fetch current data
            DataTable currentData = service.XemNhanVien(maNV: maNV);
            if (currentData.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên với mã này.");
                return;
            }

            DataRow oldRow = currentData.Rows[0];

            // 2. Merge logic: If UI is empty/default, use Old Data. If UI has data, use UI.

            string newHoTen = string.IsNullOrWhiteSpace(txb_QLCT4_getTenNV.Text)
                ? oldRow["HoTenNV"].ToString()
                : txb_QLCT4_getTenNV.Text.Trim();

            string newChucVu = string.IsNullOrWhiteSpace(cmb_QLCT4_getChucVu.Text)
                ? oldRow["ChucVu"].ToString()
                : cmb_QLCT4_getChucVu.Text;

            string newChiNhanh = string.IsNullOrWhiteSpace(cmb_QLCT4_getChiNhanh.Text)
                ? oldRow["ChiNhanhLamViec"].ToString()
                : cmb_QLCT4_getChiNhanh.Text;

            string newTrangThai = string.IsNullOrWhiteSpace(cmb_QLCT4_getTrangThai.Text)
                ? oldRow["TrangThaiNV"].ToString()
                : cmb_QLCT4_getTrangThai.Text;

            string newTenDangNhap = string.IsNullOrWhiteSpace(txb_QLCT4_getTenDangNhap.Text)
                ? oldRow["TenDangNhap"].ToString()
                : txb_QLCT4_getTenDangNhap.Text.Trim();

            int newLuong;
            if (string.IsNullOrWhiteSpace(txb_QLCT4_getLuong.Text))
            {
                newLuong = oldRow["Luong"] != DBNull.Value ? Convert.ToInt32(oldRow["Luong"]) : 0;
            }
            else
            {
                if (!int.TryParse(txb_QLCT4_getLuong.Text, out newLuong))
                    throw new Exception("Lương phải là số nguyên.");
            }

            // Check DateTimes (Assuming 1900-01-01 is the 'null/default' set in Load)
            DateTime newNgaySinh = dtp_QLCT4_getNgaySinh.Value.Year == 1900
                ? (oldRow["NgaySinhNV"] != DBNull.Value ? Convert.ToDateTime(oldRow["NgaySinhNV"]) : DateTime.Now)
                : dtp_QLCT4_getNgaySinh.Value;

            DateTime newNgayVaoLam = dtp_QLCT4_getNgayVaoLam.Value.Year == 1900
                ? (oldRow["NgayVaoLam"] != DBNull.Value ? Convert.ToDateTime(oldRow["NgayVaoLam"]) : DateTime.Now)
                : dtp_QLCT4_getNgayVaoLam.Value;

            // 3. Execute Update
            bool result = service.SuaNhanVien(maNV, newHoTen, newNgaySinh, newChucVu, newNgayVaoLam, newLuong, newChiNhanh, newTrangThai, newTenDangNhap);

            if (result)
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                LoadDataToGrid(); // Refresh grid
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
            }
        }

        private void XuLyXoa(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên cần xóa.");
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {maNV}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                bool result = service.XoaNhanVien(maNV);
                if (result)
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDataToGrid();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại. Có thể nhân viên không tồn tại hoặc có ràng buộc dữ liệu.");
                }
            }
        }

        private void XuLyXem()
        {
            // Fields act as filters. If empty, pass null.
            string maNV = string.IsNullOrWhiteSpace(txb_QLCT4_getMaNV.Text) ? null : txb_QLCT4_getMaNV.Text.Trim();
            string hoTen = string.IsNullOrWhiteSpace(txb_QLCT4_getTenNV.Text) ? null : txb_QLCT4_getTenNV.Text.Trim();
            string chiNhanh = string.IsNullOrWhiteSpace(cmb_QLCT4_getChiNhanh.Text) ? null : cmb_QLCT4_getChiNhanh.Text;
            string chucVu = string.IsNullOrWhiteSpace(cmb_QLCT4_getChucVu.Text) ? null : cmb_QLCT4_getChucVu.Text;

            DataTable dt = service.XemNhanVien(maNV, hoTen, chiNhanh, chucVu);
            LoadDataToGrid(dt);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả nào.");
            }
        }

        private void cmb_QLCT4_selectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT4_getMaNV.Visible = false;
            txb_QLCT4_getTenNV.Visible = false;
            txb_QLCT4_getLuong.Visible = false;
            txb_QLCT4_getTenDangNhap.Visible = false;

            cmb_QLCT4_getChiNhanh.Visible = false;
            cmb_QLCT4_getTrangThai.Visible = false;
            cmb_QLCT4_getChucVu.Visible = false;

            dtp_QLCT4_getNgaySinh.Visible = false;
            dtp_QLCT4_getNgayVaoLam.Visible = false;

            lbl_QLCT4_MaNV.Visible = false;
            lbl_QLCT4_TenNV.Visible = false;
            lbl_QLCT4_ChucVu.Visible = false;
            lbl_QLCT4_Luong.Visible = false;
            lbl_QLCT4_TenDangNhap.Visible = false;
            lbl_QLCT4_ChiNhanh.Visible = false;
            lbl_QLCT4_TrangThai.Visible = false;
            lbl_QLCT4_NgaySinh.Visible = false;
            lbl_QLCT4_NgayVaoLam.Visible = false;

            if (cmb_QLCT4_selectAction.SelectedItem == null)
                return;

            string mode = cmb_QLCT4_selectAction.SelectedItem.ToString();

            switch (mode)
            {
                case "Thêm":
                case "Sửa":
                    // Show all fields for editing/adding
                    txb_QLCT4_getMaNV.Visible = true;
                    txb_QLCT4_getTenNV.Visible = true;
                    txb_QLCT4_getLuong.Visible = true;
                    txb_QLCT4_getTenDangNhap.Visible = true;
                    cmb_QLCT4_getChiNhanh.Visible = true;
                    cmb_QLCT4_getTrangThai.Visible = true;
                    cmb_QLCT4_getChucVu.Visible = true;
                    dtp_QLCT4_getNgaySinh.Visible = true;
                    dtp_QLCT4_getNgayVaoLam.Visible = true;

                    lbl_QLCT4_MaNV.Visible = true;
                    lbl_QLCT4_TenNV.Visible = true;
                    lbl_QLCT4_ChucVu.Visible = true;
                    lbl_QLCT4_Luong.Visible = true;
                    lbl_QLCT4_TenDangNhap.Visible = true;
                    lbl_QLCT4_ChiNhanh.Visible = true;
                    lbl_QLCT4_TrangThai.Visible = true;
                    lbl_QLCT4_NgaySinh.Visible = true;
                    lbl_QLCT4_NgayVaoLam.Visible = true;
                    break;

                case "Xem":
                    // Show relevant fields for filtering (Based on stored proc capability: MaNV, Ten, ChiNhanh, ChucVu)
                    txb_QLCT4_getMaNV.Visible = true;
                    txb_QLCT4_getTenNV.Visible = true;
                    cmb_QLCT4_getChiNhanh.Visible = true;
                    cmb_QLCT4_getChucVu.Visible = true;

                    // Others hidden for simple search (can enable if needed, but SP restricts filters)
                    txb_QLCT4_getLuong.Visible = false;
                    txb_QLCT4_getTenDangNhap.Visible = false;
                    cmb_QLCT4_getTrangThai.Visible = false;
                    dtp_QLCT4_getNgaySinh.Visible = false;
                    dtp_QLCT4_getNgayVaoLam.Visible = false;

                    lbl_QLCT4_MaNV.Visible = true;
                    lbl_QLCT4_TenNV.Visible = true;
                    lbl_QLCT4_ChiNhanh.Visible = true;
                    lbl_QLCT4_ChucVu.Visible = true;
                    break;

                case "Xóa":
                    txb_QLCT4_getMaNV.Visible = true;
                    lbl_QLCT4_MaNV.Visible = true;
                    break;
            }
        }

        private void dgv_QLCT4_QLNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: When clicking a row, fill the boxes to make "Sửa" easier
            if (e.RowIndex >= 0 && e.RowIndex < danhSach.Count)
            {
                var item = danhSach[e.RowIndex];
                txb_QLCT4_getMaNV.Text = item.MaNV;
                txb_QLCT4_getTenNV.Text = item.TenNV;
                txb_QLCT4_getLuong.Text = item.Luong.ToString();
                txb_QLCT4_getTenDangNhap.Text = item.TenDangNhap;
                cmb_QLCT4_getChiNhanh.Text = item.ChiNhanh;
                cmb_QLCT4_getTrangThai.Text = item.TrangThai;
                cmb_QLCT4_getChucVu.Text = item.ChucVu;

                // Only set date if valid (>1900)
                if (item.NgaySinh.Year > 1900) dtp_QLCT4_getNgaySinh.Value = item.NgaySinh;
                if (item.NgayVaoLam.Year > 1900) dtp_QLCT4_getNgayVaoLam.Value = item.NgayVaoLam;
            }
        }

        private void cmb_QLCT4_getChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Keeps original empty handler
        }
    }
}