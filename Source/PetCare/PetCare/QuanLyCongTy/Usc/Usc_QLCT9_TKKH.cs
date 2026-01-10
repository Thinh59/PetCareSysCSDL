using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT9_TKKH : UserControl
    {
        private BindingList<Class_QLCT9_TKKH> danhSach = new BindingList<Class_QLCT9_TKKH>();
        private ServiceDAL service = new ServiceDAL();

        public Usc_QLCT9_TKKH()
        {
            InitializeComponent();
            dgv_QLCT9_TKKH.DataSource = danhSach;
            dgv_QLCT9_TKKH.ReadOnly = true;
        }

        private void cmb_QLCT9_selectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_QLCT9_MaCN.Visible = false;
            txb_QLCT9_getMaCN.Visible = false;

            if (cmb_QLCT9_selectMode.SelectedItem != null && cmb_QLCT9_selectMode.SelectedItem.ToString() == "Chi nhánh")
            {
                lbl_QLCT9_MaCN.Visible = true;
                txb_QLCT9_getMaCN.Visible = true;
            }
        }

        private void btn_QLCT9_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedMode = cmb_QLCT9_selectMode.SelectedItem?.ToString();
                DataTable dtResult = new DataTable();

                if (selectedMode == "Chi nhánh")
                {
                    string maCN = txb_QLCT9_getMaCN.Text.Trim();

                    if (string.IsNullOrEmpty(maCN))
                    {
                        MessageBox.Show("Vui lòng nhập Mã Chi Nhánh để xem thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dtResult = service.ThongKeKhachHangChiNhanh(maCN);
                }
                else
                {
                    dtResult = service.ThongKeKhachHangCongTy();
                }

                danhSach.Clear();

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    foreach (DataRow row in dtResult.Rows)
                    {
                        Class_QLCT9_TKKH item = new Class_QLCT9_TKKH();

                        item.MaCN = row["MaCN"] != DBNull.Value ? row["MaCN"].ToString() : "N/A";
                        item.TenCN = row["TenCN"] != DBNull.Value ? row["TenCN"].ToString() : "Toàn Công Ty";
                        item.SoLuongKhach = row["SoLuongKhach"] != DBNull.Value ? Convert.ToInt32(row["SoLuongKhach"]) : 0;
                        item.SoLuongKhachMoi = row["SoLuongKhachMoi"] != DBNull.Value ? Convert.ToInt32(row["SoLuongKhachMoi"]) : 0;

                        if (dtResult.Columns.Contains("SoLuongKhachLauKhongTroLai"))
                        {
                            item.SoLuongKhachLauChuaTroLai = row["SoLuongKhachLauKhongTroLai"] != DBNull.Value ? Convert.ToInt32(row["SoLuongKhachLauKhongTroLai"]) : 0;
                        }
                        else
                        {
                            item.SoLuongKhachLauChuaTroLai = 0;
                        }

                        danhSach.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu thống kê phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e) { }
        private void dgv_QLCT9_TKKH_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txb_QLCT9_getMaCN_TextChanged(object sender, EventArgs e) { }
        private void lbl_QLCT9_MaCN_Click(object sender, EventArgs e) { }
        private void pnl_QLCT9_TKKH_Paint(object sender, PaintEventArgs e) { }
    }
}