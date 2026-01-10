using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT3_HSCN : UserControl
    {
        private BindingList<Class_QLCT3_HSCN> danhSach = new BindingList<Class_QLCT3_HSCN>();
        private ServiceDAL dal = new ServiceDAL();

        public Usc_QLCT3_HSCN()
        {
            InitializeComponent();
            dgv_QLCT3_HSCN.DataSource = danhSach;
        }

        private void cmb_QLCT3_selectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_QLCT3_MaCN.Visible = false;
            txb_QLCT3_getMaCN.Visible = false;

            if (cmb_QLCT3_selectMode.SelectedItem != null && cmb_QLCT3_selectMode.SelectedItem.ToString() == "Chi nhánh")
            {
                lbl_QLCT3_MaCN.Visible = true;
                txb_QLCT3_getMaCN.Visible = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_QLCT3_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT3_selectMode.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn chế độ xem (Toàn công ty / Chi nhánh).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mode = cmb_QLCT3_selectMode.SelectedItem.ToString();
            DataTable dt = new DataTable();

            try
            {
                if (mode == "Chi nhánh")
                {
                    string maCN = txb_QLCT3_getMaCN.Text.Trim();
                    if (string.IsNullOrEmpty(maCN))
                    {
                        MessageBox.Show("Vui lòng nhập Mã Chi Nhánh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txb_QLCT3_getMaCN.Focus();
                        return;
                    }

                    dt = dal.GetThongKeHieuSuatChiNhanh(maCN);
                }
                else
                {
                    dt = dal.GetThongKeHieuSuatCongTy();
                }

                danhSach.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Class_QLCT3_HSCN item = new Class_QLCT3_HSCN();

                        item.SoHoaDon = row["TongSoHoaDon"] != DBNull.Value ? Convert.ToInt32(row["TongSoHoaDon"]) : 0;
                        item.SoDichVuThucHien = row["TongSoLuongDichVu"] != DBNull.Value ? Convert.ToInt32(row["TongSoLuongDichVu"]) : 0;
                        item.DanhGiaTB = row["DiemDanhGiaTB"] != DBNull.Value ? Convert.ToDecimal(row["DiemDanhGiaTB"]) : 0;

                        danhSach.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu hiệu suất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}