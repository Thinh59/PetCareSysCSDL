using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PetCare
{
    public partial class Usc_QLCT13_LSDV : UserControl
    {
        private ServiceDAL dal = new ServiceDAL();
        private BindingList<Class_QLCT13_DVMH> danhSach_MH = new BindingList<Class_QLCT13_DVMH>();
        private BindingList<Class_QLCT13_DVKB> danhSach_KB = new BindingList<Class_QLCT13_DVKB>();
        private BindingList<Class_QLCT13_DVTP> danhSach_TP = new BindingList<Class_QLCT13_DVTP>();

        public Usc_QLCT13_LSDV()
        {
            InitializeComponent();
            dgv_QLCT13_LSDVMH.DataSource = danhSach_MH;
            dgv_QLCT13_LSDVKB.DataSource = danhSach_KB;
            dgv_QLCT13_LSDVTP.DataSource = danhSach_TP;
            LoadBranchList();
        }

        private void LoadBranchList()
        {
            try
            {
                List<string> branches = dal.GetDanhSachMaChiNhanh();
                cmb_QLCT13_selectBranch.DataSource = branches;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_QLCT13_selectScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_QLCT13_ChiNhanh.Visible = false;
            cmb_QLCT13_selectBranch.Visible = false;

            if (cmb_QLCT13_selectScope.SelectedItem == null)
                return;

            string mode = cmb_QLCT13_selectScope.SelectedItem.ToString();

            switch (mode)
            {
                case "Chi nhánh":
                    lbl_QLCT13_ChiNhanh.Visible = true;
                    cmb_QLCT13_selectBranch.Visible = true;
                    break;
                case "Công ty":
                    lbl_QLCT13_ChiNhanh.Visible = false;
                    cmb_QLCT13_selectBranch.Visible = false;
                    break;
            }
        }

        private void lbl_QLCT13_Quy_Click(object sender, EventArgs e) { }
        private void pnl_QTCT13_Paint(object sender, PaintEventArgs e) { }
        private void dgv_QLCT13_LSDVTP_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btn_QLCT13_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT13_getLoaiDV.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmb_QLCT13_selectScope.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phạm vi (Công ty/Chi nhánh).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmb_QLCT13_selectTimeMode.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn mốc thời gian.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedService = cmb_QLCT13_getLoaiDV.SelectedItem.ToString();
            string scope = cmb_QLCT13_selectScope.SelectedItem.ToString();
            string timeMode = cmb_QLCT13_selectTimeMode.SelectedItem.ToString();
            string maCN = scope == "Chi nhánh" && cmb_QLCT13_selectBranch.SelectedItem != null
                          ? cmb_QLCT13_selectBranch.SelectedItem.ToString()
                          : null;

            if (scope == "Chi nhánh" && string.IsNullOrEmpty(maCN))
            {
                MessageBox.Show("Vui lòng chọn chi nhánh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? day = ParseIntSafe(txb_QLCT13_getDay.Text);
            int? month = ParseIntSafe(txb_QLCT13_getMonth.Text);
            int? year = ParseIntSafe(txb_QLCT13_getYear.Text);
            int? quarter = ParseIntSafe(txb_QLCT13_getQuarter.Text);

            dgv_QLCT13_LSDVMH.Visible = false;
            dgv_QLCT13_LSDVKB.Visible = false;
            dgv_QLCT13_LSDVTP.Visible = false;

            try
            {
                DataTable dt = new DataTable();

                if (selectedService == "Mua hàng")
                {
                    dgv_QLCT13_LSDVMH.Visible = true;
                    danhSach_MH.Clear();

                    if (scope == "Công ty")
                    {
                        if (timeMode == "Quý")
                            dt = dal.XemLSDVMuaHangCongTyQuyNam(quarter, year);
                        else
                            dt = dal.XemLSDVMuaHangCongTyNgayThangNam(day, month, year);
                    }
                    else
                    {
                        if (timeMode == "Quý")
                            dt = dal.XemLSDVMuaHangChiNhanhQuyNam(maCN, quarter, year);
                        else
                            dt = dal.XemLSDVMuaHangChiNhanhNgayThangNam(maCN, day, month, year);
                    }

                    MapDataTableToMuaHang(dt);
                }
                else if (selectedService == "Khám bệnh")
                {
                    dgv_QLCT13_LSDVKB.Visible = true;
                    danhSach_KB.Clear();

                    if (scope == "Công ty")
                    {
                        if (timeMode == "Quý")
                            dt = dal.XemLSDVKhamBenhCongTyQuyNam(quarter, year);
                        else
                            dt = dal.XemLSDVKhamBenhCongTyNgayThangNam(day, month, year);
                    }
                    else
                    {
                        if (timeMode == "Quý")
                            dt = dal.XemLSDVKhamBenhChiNhanhQuyNam(maCN, quarter, year);
                        else
                            dt = dal.XemLSDVKhamBenhChiNhanhNgayThangNam(maCN, day, month, year);
                    }

                    MapDataTableToKhamBenh(dt);
                }
                else if (selectedService == "Tiêm phòng")
                {
                    dgv_QLCT13_LSDVTP.Visible = true;
                    danhSach_TP.Clear();

                    if (scope == "Công ty")
                    {
                        if (timeMode == "Quý")
                            dt = dal.XemLSDVTiemPhongCongTyQuyNam(quarter, year);
                        else
                            dt = dal.XemLSDVTiemPhongCongTyNgayThangNam(day, month, year);
                    }
                    else
                    {
                        if (timeMode == "Quý")
                            dt = dal.XemLSDVTiemPhongChiNhanhQuyNam(maCN, quarter, year);
                        else
                            dt = dal.XemLSDVTiemPhongChiNhanhNgayThangNam(maCN, day, month, year);
                    }

                    MapDataTableToTiemPhong(dt);
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp với điều kiện lọc.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Đã tải xong dữ liệu. Tìm thấy {dt.Rows.Count} bản ghi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_QLCT13_selectTimeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT13_getDay.Visible = false;
            txb_QLCT13_getMonth.Visible = false;
            txb_QLCT13_getYear.Visible = false;
            txb_QLCT13_getQuarter.Visible = false;

            lbl_QLCT13_Ngay.Visible = false;
            lbl_QLCT13_Thang.Visible = false;
            lbl_QLCT13_Nam.Visible = false;
            lbl_QLCT13_Quy.Visible = false;

            if (cmb_QLCT13_selectTimeMode.SelectedItem == null)
                return;

            string mode = cmb_QLCT13_selectTimeMode.SelectedItem.ToString();

            switch (mode)
            {
                case "Ngày/Tháng":
                    txb_QLCT13_getDay.Visible = true;
                    txb_QLCT13_getMonth.Visible = true;
                    txb_QLCT13_getYear.Visible = true;
                    lbl_QLCT13_Ngay.Visible = true;
                    lbl_QLCT13_Thang.Visible = true;
                    lbl_QLCT13_Nam.Visible = true;
                    break;

                case "Quý":
                    txb_QLCT13_getQuarter.Visible = true;
                    txb_QLCT13_getYear.Visible = true;
                    lbl_QLCT13_Quy.Visible = true;
                    lbl_QLCT13_Nam.Visible = true;
                    break;

                case "Toàn bộ":
                    break;
            }
        }

        private int? ParseIntSafe(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            if (int.TryParse(input.Trim(), out int result)) return result;
            return null;
        }

        private void MapDataTableToMuaHang(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                danhSach_MH.Add(new Class_QLCT13_DVMH
                {
                    MaLSDV = row["MaLSDV"] != DBNull.Value ? row["MaLSDV"].ToString() : "",
                    MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString() : "",
                    MaDichVu = row["MaDichVu"] != DBNull.Value ? row["MaDichVu"].ToString() : "",
                    TrangThaiGD = row["TrangThaiGD"] != DBNull.Value ? row["TrangThaiGD"].ToString() : "",
                    NgayDatTruoc = row["NgayDatTruoc"] != DBNull.Value ? Convert.ToDateTime(row["NgayDatTruoc"]) : DateTime.MinValue,
                    HinhThucMH = row["HinhThucMH"] != DBNull.Value ? row["HinhThucMH"].ToString() : ""
                });
            }
        }

        private void MapDataTableToKhamBenh(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                danhSach_KB.Add(new Class_QLCT13_DVKB
                {
                    MaLSDV = row["MaLSDV"] != DBNull.Value ? row["MaLSDV"].ToString() : "",
                    MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString() : "",
                    MaDichVu = row["MaDichVu"] != DBNull.Value ? row["MaDichVu"].ToString() : "",
                    TrangThaiGD = row["TrangThaiGD"] != DBNull.Value ? row["TrangThaiGD"].ToString() : "",
                    NgayDatTruoc = row["NgayDatTruoc"] != DBNull.Value ? Convert.ToDateTime(row["NgayDatTruoc"]) : DateTime.MinValue,
                    BacSiPhuTrach = row["BacSiPhuTrach"] != DBNull.Value ? row["BacSiPhuTrach"].ToString() : "",
                    NgayHen = row["NgayHen"] != DBNull.Value ? Convert.ToDateTime(row["NgayHen"]) : DateTime.MinValue,
                    MaThuCung = row["MaThuCung"] != DBNull.Value ? row["MaThuCung"].ToString() : ""
                });
            }
        }

        private void MapDataTableToTiemPhong(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                danhSach_TP.Add(new Class_QLCT13_DVTP
                {
                    MaLSDV = row["MaLSDV"] != DBNull.Value ? row["MaLSDV"].ToString() : "",
                    MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString() : "",
                    MaDichVu = row["MaDichVu"] != DBNull.Value ? row["MaDichVu"].ToString() : "",
                    TrangThaiGD = row["TrangThaiGD"] != DBNull.Value ? row["TrangThaiGD"].ToString() : "",
                    NgayDatTruoc = row["NgayDatTruoc"] != DBNull.Value ? Convert.ToDateTime(row["NgayDatTruoc"]) : DateTime.MinValue,
                    BacSiPhuTrach = row["BacSiPhuTrach"] != DBNull.Value ? row["BacSiPhuTrach"].ToString() : "",
                    MaGoiTiem = row["MaGoiTiem"] != DBNull.Value ? row["MaGoiTiem"].ToString() : "",
                    MaVacXin = row["MaVacXin"] != DBNull.Value ? row["MaVacXin"].ToString() : "",
                    LieuLuong = row["LieuLuong"] != DBNull.Value ? row["LieuLuong"].ToString() : "",
                    NgayTiem = row["NgayTiem"] != DBNull.Value ? Convert.ToDateTime(row["NgayTiem"]) : DateTime.MinValue,
                    MaThuCung = row["MaThuCung"] != DBNull.Value ? row["MaThuCung"].ToString() : ""
                });
            }
        }
    }
}