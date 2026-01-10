using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace PetCare
{
    public partial class Usc_QLCT7_HSNV : UserControl
    {
        private BindingList<Class_QLCT7_HSNV> danhSach = new BindingList<Class_QLCT7_HSNV>();
        private ServiceDAL serviceDAL = new ServiceDAL();

        public Usc_QLCT7_HSNV()
        {
            InitializeComponent();
            dgv_QLCT7_HSNV.DataSource = danhSach;
        }

        private void cmb_QLCT7_selectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_QLCT7_MaCN.Visible = false;
            txb_QLCT7_getMaCN.Visible = false;

            if (cmb_QLCT7_selectMode.SelectedItem != null && cmb_QLCT7_selectMode.SelectedItem.ToString() == "Chi nhánh")
            {
                lbl_QLCT7_MaCN.Visible = true;
                txb_QLCT7_getMaCN.Visible = true;
            }
        }

        private void btn_QLCT7_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = null;

                if (cmb_QLCT7_selectMode.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn chế độ xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string mode = cmb_QLCT7_selectMode.SelectedItem.ToString();

                if (mode == "Công ty")
                {
                    dataTable = serviceDAL.GetThongKeHieuSuatCongTy();
                }
                else if (mode == "Chi nhánh")
                {
                    string maCN = txb_QLCT7_getMaCN.Text.Trim();

                    if (string.IsNullOrEmpty(maCN))
                    {
                        MessageBox.Show("Vui lòng nhập mã chi nhánh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dataTable = serviceDAL.GetThongKeHieuSuatChiNhanh(maCN);
                }
                else
                {
                    MessageBox.Show("Chế độ không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                danhSach.Clear();

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Class_QLCT7_HSNV item = new Class_QLCT7_HSNV();


                        if (dataTable.Columns.Contains("MaCN"))
                        {
                            item.MaCN = row["MaCN"] != DBNull.Value ? row["MaCN"].ToString() : "";
                        }
                        else if (dataTable.Columns.Contains("ChiNhanhLamViec"))
                        {
                            item.MaCN = row["ChiNhanhLamViec"] != DBNull.Value ? row["ChiNhanhLamViec"].ToString() : "";
                        }
                        else
                        {
                            item.MaCN = "Chưa rõ";
                        }

                        if (dataTable.Columns.Contains("MaNV"))
                            item.MaNV = row["MaNV"]?.ToString() ?? "";

                        if (dataTable.Columns.Contains("HoTenNV"))
                            item.TenNV = row["HoTenNV"]?.ToString() ?? "";

                        if (dataTable.Columns.Contains("DanhGiaNV") && row["DanhGiaNV"] != DBNull.Value)
                            item.DanhGiaNV = Convert.ToDecimal(row["DanhGiaNV"]);
                        else
                            item.DanhGiaNV = 0;

                        danhSach.Add(item);
                    }

                    MessageBox.Show($"Đã tải {danhSach.Count} nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_QLCT7_HSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txb_QLCT7_getMaCN_TextChanged(object sender, EventArgs e)
        {

        }

        private void Usc_QLCT7_HSNV_Load(object sender, EventArgs e)
        {

        }

        private void txb_QLCT7_getMaCN_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dgv_QLCT7_HSNV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnl_QLCT7_HSNV_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}