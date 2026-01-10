using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT12_DSHD : UserControl
    {
        private ServiceDAL service = new ServiceDAL();
        private BindingList<Class_QLCT12_DSHD> danhSach = new BindingList<Class_QLCT12_DSHD>();

        public Usc_QLCT12_DSHD()
        {
            InitializeComponent();
            dgv_QLCT12_DSHD.DataSource = danhSach;
        }

        private void Usc_QLCT12_DSHD_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> branchList = service.GetDanhSachMaChiNhanh();
                cmb_QLCT12_selectBranch.DataSource = branchList;

                if (cmb_QLCT12_selectTimeMode.Items.Count > 0) cmb_QLCT12_selectTimeMode.SelectedIndex = 0;
                if (cmb_QLCT12_selectScope.Items.Count > 0) cmb_QLCT12_selectScope.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ban đầu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_QLCT12_selectTimeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT12_getDay.Visible = false;
            txb_QLCT12_getMonth.Visible = false;
            txb_QLCT12_getYear.Visible = false;
            txb_QLCT12_getQuarter.Visible = false;

            lbl_QLCT12_Ngay.Visible = false;
            lbl_QLCT12_Thang.Visible = false;
            lbl_QLCT12_Nam.Visible = false;
            lbl_QLCT12_Quy.Visible = false;

            if (cmb_QLCT12_selectTimeMode.SelectedItem == null)
                return;

            string mode = cmb_QLCT12_selectTimeMode.SelectedItem.ToString();

            switch (mode)
            {
                case "Ngày/Tháng":
                    txb_QLCT12_getDay.Visible = true;
                    txb_QLCT12_getMonth.Visible = true;
                    txb_QLCT12_getYear.Visible = true;
                    lbl_QLCT12_Ngay.Visible = true;
                    lbl_QLCT12_Thang.Visible = true;
                    lbl_QLCT12_Nam.Visible = true;
                    break;
                case "Quý":
                    txb_QLCT12_getQuarter.Visible = true;
                    txb_QLCT12_getYear.Visible = true;
                    lbl_QLCT12_Quy.Visible = true;
                    lbl_QLCT12_Nam.Visible = true;
                    break;
                case "Toàn bộ":
                    break;
            }
        }

        private void cmb_QLCT12_selectScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_QLCT12_ChiNhanh.Visible = false;
            cmb_QLCT12_selectBranch.Visible = false;

            if (cmb_QLCT12_selectScope.SelectedItem == null)
                return;

            string mode = cmb_QLCT12_selectScope.SelectedItem.ToString();

            switch (mode)
            {
                case "Chi nhánh":
                    lbl_QLCT12_ChiNhanh.Visible = true;
                    cmb_QLCT12_selectBranch.Visible = true;
                    break;
                case "Công ty":
                    lbl_QLCT12_ChiNhanh.Visible = false;
                    cmb_QLCT12_selectBranch.Visible = false;
                    break;
            }
        }

        private void btn_QLCT12_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_QLCT12_selectTimeMode.SelectedItem == null || cmb_QLCT12_selectScope.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn chế độ thời gian và phạm vi xem.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string timeMode = cmb_QLCT12_selectTimeMode.SelectedItem.ToString();
                string scopeMode = cmb_QLCT12_selectScope.SelectedItem.ToString();
                DataTable dtResult = new DataTable();

                int? day = GetIntFromTextBox(txb_QLCT12_getDay);
                int? month = GetIntFromTextBox(txb_QLCT12_getMonth);
                int? year = GetIntFromTextBox(txb_QLCT12_getYear);
                int? quarter = GetIntFromTextBox(txb_QLCT12_getQuarter);

                if (scopeMode == "Công ty")
                {
                    if (timeMode == "Ngày/Tháng" || timeMode == "Toàn bộ")
                    {
                        dtResult = service.XemDSHoaDonCongTyNgayThangNam(day, month, year);
                    }
                    else if (timeMode == "Quý")
                    {
                        dtResult = service.XemDSHoaDonCongTyQuyNam(quarter, year);
                    }
                }
                else if (scopeMode == "Chi nhánh")
                {
                    if (cmb_QLCT12_selectBranch.SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn chi nhánh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string maCN = cmb_QLCT12_selectBranch.SelectedItem.ToString();

                    if (timeMode == "Ngày/Tháng" || timeMode == "Toàn bộ")
                    {
                        dtResult = service.XemDSHoaDonChiNhanhNgayThangNam(maCN, day, month, year);
                    }
                    else if (timeMode == "Quý")
                    {
                        dtResult = service.XemDSHoaDonChiNhanhQuyNam(maCN, quarter, year);
                    }
                }

                danhSach.Clear();

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    foreach (DataRow row in dtResult.Rows)
                    {
                        Class_QLCT12_DSHD hd = new Class_QLCT12_DSHD
                        {
                            MaHD = row["MaHD"] != DBNull.Value ? row["MaHD"].ToString() : "",
                            NgayLap = row["NgayLap"] != DBNull.Value ? Convert.ToDateTime(row["NgayLap"]) : DateTime.MinValue,
                            NV_Lap = row["NV_Lap"] != DBNull.Value ? row["NV_Lap"].ToString() : "",
                            TienTruocKM = row["TienTruocKM"] != DBNull.Value ? Convert.ToDecimal(row["TienTruocKM"]) : 0,
                            TienThanhToan = row["TienThanhToan"] != DBNull.Value ? Convert.ToDecimal(row["TienThanhToan"]) : 0,
                            HinhThucPay = row["HinhThucPay"] != DBNull.Value ? row["HinhThucPay"].ToString() : "",
                            TrangThaiHD = row["TrangThaiHD"] != DBNull.Value ? row["TrangThaiHD"].ToString() : "",
                            CongLoyalty = row["CongLoyalty"] != DBNull.Value ? Convert.ToInt32(row["CongLoyalty"]) : 0,
                            MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString() : ""
                        };
                        danhSach.Add(hd);
                    }
                    MessageBox.Show($"Tìm thấy {dtResult.Rows.Count} hóa đơn.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập định dạng số hợp lệ cho Ngày/Tháng/Năm/Quý.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? GetIntFromTextBox(TextBox tb)
        {
            if (tb == null || !tb.Visible || string.IsNullOrWhiteSpace(tb.Text))
            {
                return null;
            }
            return int.Parse(tb.Text.Trim());
        }

        private void lbl_QLCT12_Nam_Click(object sender, EventArgs e) { }
        private void txb_QLCT12_getDay_TextChanged(object sender, EventArgs e) { }
        private void dgv_QLCT12_DSHD_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}