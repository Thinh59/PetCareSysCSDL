using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_ALL_DSDV : UserControl
    {
        // 1. Maintain BindingList approach
        private BindingList<Class_ALL_DSDV> danhSach = new BindingList<Class_ALL_DSDV>();
        private ServiceDAL service = new ServiceDAL();

        public Usc_ALL_DSDV()
        {
            InitializeComponent();

            // Setup DataGridView
            dgv_ALL_DSDV.DataSource = danhSach;
            dgv_ALL_DSDV.AutoGenerateColumns = true; // Ensure columns appear

            // Load Branch Data into ComboBox on Init
            LoadComboBoxChiNhanh();

            // Set default selection
            cmb_ALL_DSDV_selectScope.SelectedIndex = 0; // Default to "Toàn công ty" or first item
        }

        private void LoadComboBoxChiNhanh()
        {
            try
            {
                DataTable dtBranch = service.GetThongTinChiNhanhDonGian();

                // Use the DataTable directly for binding
                cmb_ALL_DSDV_selectBranch.DataSource = dtBranch;
                cmb_ALL_DSDV_selectBranch.DisplayMember = "TenCN";
                cmb_ALL_DSDV_selectBranch.ValueMember = "MaCN";

                // Reset selection so it doesn't trigger logic immediately if not needed
                cmb_ALL_DSDV_selectBranch.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Preserve Original Event Handlers
        private void label6_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void cmb_ALL_DSDV_selectScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 3. Keep exact UI Control visibility logic
            if (cmb_ALL_DSDV_selectScope.SelectedItem != null && cmb_ALL_DSDV_selectScope.SelectedItem.ToString() == "Chi nhánh")
            {
                lbl_QLCT1_ChiNhanh.Visible = true;
                cmb_ALL_DSDV_selectBranch.Visible = true;
            }
            else
            {
                lbl_QLCT1_ChiNhanh.Visible = false;
                cmb_ALL_DSDV_selectBranch.Visible = false;
                cmb_ALL_DSDV_selectBranch.SelectedIndex = -1; // Clear selection when hiding
            }
        }

        private void lbl_QLCT1_ChiNhanh_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void cmb_ALL_DSDV_selectBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Empty as requested
        }

        // 4. Implement Main Logic (View/Filter)
        private void btn_ALL_DSDV_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string targetMaCN = null;
                string scope = cmb_ALL_DSDV_selectScope.SelectedItem?.ToString();

                // Validation and Filter Logic
                if (scope == "Chi nhánh")
                {
                    if (cmb_ALL_DSDV_selectBranch.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn chi nhánh cần xem.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    targetMaCN = cmb_ALL_DSDV_selectBranch.SelectedValue.ToString();
                }
                // If scope is "Toàn công ty" or anything else, targetMaCN remains null (View All)

                // Call DAL
                DataTable resultData = service.GetAllDichVuByChiNhanh(targetMaCN);

                // Update BindingList (Refreshes UI automatically)
                danhSach.Clear();

                if (resultData != null && resultData.Rows.Count > 0)
                {
                    foreach (DataRow row in resultData.Rows)
                    {
                        Class_ALL_DSDV item = new Class_ALL_DSDV();

                        // Map DataRow to Class Object with safe type conversion
                        item.MaCN = row["MaCN"] != DBNull.Value ? row["MaCN"].ToString() : "";
                        item.MaDichVu = row["MaDichVu"] != DBNull.Value ? row["MaDichVu"].ToString() : "";
                        item.TenDV = row["TenDV"] != DBNull.Value ? row["TenDV"].ToString() : "";

                        // Handle Decimal/Money
                        if (row["GiaDV_CN"] != DBNull.Value)
                            item.GiaDV_CN = Convert.ToDecimal(row["GiaDV_CN"]);
                        else
                            item.GiaDV_CN = 0;

                        // Map "TrangThaiDV" from SP to "TrangThai_HD" in Class
                        item.TrangThai_HD = row["TrangThaiDV"] != DBNull.Value ? row["TrangThaiDV"].ToString() : "";

                        danhSach.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu dịch vụ phù hợp.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}