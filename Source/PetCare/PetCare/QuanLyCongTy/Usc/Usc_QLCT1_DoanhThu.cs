using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT1_DoanhThu : UserControl
    {
        private BindingList<Class_QLCT1_DoanhThu> danhSach = new BindingList<Class_QLCT1_DoanhThu>();
        private ServiceDAL serviceDAL = new ServiceDAL();

        public Usc_QLCT1_DoanhThu()
        {
            InitializeComponent();
            dgv_QLCT1_DoanhThu.DataSource = danhSach;
        }

        private void Usc_QLCT1_DoanhThu_Load(object sender, EventArgs e)
        {
            LoadBranches();

            if (cmb_QLCT1_selectTimeMode.Items.Count > 0)
                cmb_QLCT1_selectTimeMode.SelectedIndex = 0;

            if (cmb_QLCT1_selectScope.Items.Count > 0)
                cmb_QLCT1_selectScope.SelectedIndex = 0;
        }

        private void LoadBranches()
        {
            try
            {
                cmb_QLCT1_selectBranch.Items.Clear();

                var dt = serviceDAL.GetThongTinChiNhanhDonGian();

                foreach (DataRow row in dt.Rows)
                {
                    cmb_QLCT1_selectBranch.Items.Add(row["MaCN"].ToString());
                }

                if (cmb_QLCT1_selectBranch.Items.Count > 0)
                    cmb_QLCT1_selectBranch.SelectedIndex = 0;

                cmb_QLCT1_selectBranch.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chi nhánh: " + ex.Message);
            }
        }

        private int? ParseNullableInt(string text, string fieldName, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            if (int.TryParse(text, out int value))
            {
                if (value < min || value > max)
                    throw new ArgumentException($"{fieldName} phải nằm trong khoảng {min} - {max}.");

                return value;
            }

            throw new ArgumentException($"{fieldName} không hợp lệ.");
        }

        private void btn_QLCT1_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string timeMode = cmb_QLCT1_selectTimeMode.SelectedItem?.ToString() ?? "Toàn bộ";
                string scope = cmb_QLCT1_selectScope.SelectedItem?.ToString() ?? "Công ty";
                string branch = cmb_QLCT1_selectBranch.SelectedItem?.ToString();

                if (scope == "Chi nhánh" && string.IsNullOrEmpty(branch))
                {
                    MessageBox.Show("Vui lòng chọn chi nhánh.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? day = null, month = null, year = null, quarter = null;

                if (timeMode == "Ngày/Tháng")
                {
                    day = ParseNullableInt(txb_QLCT1_getDay.Text, "Ngày", 1, 31);
                    month = ParseNullableInt(txb_QLCT1_getMonth.Text, "Tháng", 1, 12);
                    year = ParseNullableInt(txb_QLCT1_getYear.Text, "Năm", 2000, 2100);
                }
                else if (timeMode == "Quý")
                {
                    quarter = ParseNullableInt(txb_QLCT1_getQuarter.Text, "Quý", 1, 4);
                    year = ParseNullableInt(txb_QLCT1_getYear.Text, "Năm", 2000, 2100);
                }

                decimal totalRevenue = 0;

                if (scope == "Chi nhánh")
                {
                    if (timeMode == "Quý")
                    {
                        totalRevenue = serviceDAL.GetDoanhThuChiNhanhTheoQuy(branch, quarter, year);
                    }
                    else
                    {
                        totalRevenue = serviceDAL.GetDoanhThuChiNhanhTheoNgay(branch, day, month, year);
                    }
                }
                else // Công ty scope
                {
                    if (timeMode == "Quý")
                    {
                        totalRevenue = serviceDAL.GetDoanhThuCongTyTheoQuy(quarter, year);
                    }
                    else
                    {
                        totalRevenue = serviceDAL.GetDoanhThuCongTyTheoNgay(day, month, year);
                    }
                }

                danhSach.Clear();
                danhSach.Add(new Class_QLCT1_DoanhThu
                {
                    DoanhThu = Convert.ToInt32(totalRevenue)
                });
                dgv_QLCT1_DoanhThu.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + sqlEx.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi chuyển đổi kiểu dữ liệu. Vui lòng kiểm tra lại.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_QLCT1_selectTimeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT1_getDay.Visible =
            txb_QLCT1_getMonth.Visible =
            txb_QLCT1_getYear.Visible =
            txb_QLCT1_getQuarter.Visible = false;

            lbl_QLCT1_Ngay.Visible =
            lbl_QLCT1_Thang.Visible =
            lbl_QLCT1_Nam.Visible =
            lbl_QLCT1_Quy.Visible = false;

            if (cmb_QLCT1_selectTimeMode.SelectedItem == null) return;

            switch (cmb_QLCT1_selectTimeMode.SelectedItem.ToString())
            {
                case "Ngày/Tháng":
                    txb_QLCT1_getDay.Visible = true;
                    txb_QLCT1_getMonth.Visible = true;
                    txb_QLCT1_getYear.Visible = true;
                    lbl_QLCT1_Ngay.Visible = true;
                    lbl_QLCT1_Thang.Visible = true;
                    lbl_QLCT1_Nam.Visible = true;
                    break;

                case "Quý":
                    txb_QLCT1_getQuarter.Visible = true;
                    txb_QLCT1_getYear.Visible = true;
                    lbl_QLCT1_Quy.Visible = true;
                    lbl_QLCT1_Nam.Visible = true;
                    break;
            }
        }

        private void cmb_QLCT1_selectScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_QLCT1_selectBranch.Visible = false;
            lbl_QLCT1_ChiNhanh.Visible = false;

            if (cmb_QLCT1_selectScope.SelectedItem?.ToString() == "Chi nhánh")
            {
                cmb_QLCT1_selectBranch.Visible = true;
                lbl_QLCT1_ChiNhanh.Visible = true;

                if (cmb_QLCT1_selectBranch.SelectedIndex == -1 && cmb_QLCT1_selectBranch.Items.Count > 0)
                    cmb_QLCT1_selectBranch.SelectedIndex = 0;
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void txb_QLCT1_getYear_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCT1_getMonth_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCT1_getDay_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}