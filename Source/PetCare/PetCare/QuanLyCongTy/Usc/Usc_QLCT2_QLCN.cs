using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT2_QLCN : UserControl
    {
        private ServiceDAL service = new ServiceDAL();
        private BindingList<Class_QLCT2_QLCN> danhSach = new BindingList<Class_QLCT2_QLCN>();

        public Usc_QLCT2_QLCN()
        {
            InitializeComponent();
            dgv_QLCT2_QLCN.DataSource = danhSach;
        }

        private void Usc_QLCT2_QLCN_Load(object sender, EventArgs e)
        {
            dtp_QLCT2_getOpenTime.Value = DateTime.Today;
            dtp_QLCT2_getCloseTime.Value = DateTime.Today.AddSeconds(1);
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            try
            {
                DataTable dt = service.XemChiNhanh();
                UpdateBindingList(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBindingList(DataTable dt)
        {
            danhSach.Clear();
            foreach (DataRow row in dt.Rows)
            {
                Class_QLCT2_QLCN item = new Class_QLCT2_QLCN
                {
                    MaCN = row["MaCN"].ToString(),
                    TenCN = row["TenCN"].ToString(),
                    DiaChi = row["DiaChiCN"].ToString(),
                    SDT = row["SDT_CN"].ToString()
                };

                if (row["TimeMoCua"] != DBNull.Value)
                {
                    TimeSpan ts = (TimeSpan)row["TimeMoCua"];
                    item.TGMoCua = DateTime.Today.Add(ts);
                }

                if (row["TimeDongCua"] != DBNull.Value)
                {
                    TimeSpan ts = (TimeSpan)row["TimeDongCua"];
                    item.TGDongCua = DateTime.Today.Add(ts);
                }

                danhSach.Add(item);
            }
        }

        private void cmb_QLCT2_selectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT2_getMaCN.Visible = false;
            txb_QLCT2_getTenCN.Visible = false;
            dtp_QLCT2_getOpenTime.Visible = false;
            dtp_QLCT2_getCloseTime.Visible = false;
            txb_QLCT2_getAddress.Visible = false;
            txb_QLCT2_getSDT.Visible = false;

            lbl_QLCT2_MaCN.Visible = false;
            lbl_QLCT2_TenCN.Visible = false;
            lbl_QLCT2_OpenTime.Visible = false;
            lbl_QLCT2_CloseTime.Visible = false;
            lbl_QLCT2_Address.Visible = false;
            lbl_QLCT2_SDT.Visible = false;

            if (cmb_QLCT2_selectAction.SelectedItem == null)
                return;

            string mode = cmb_QLCT2_selectAction.SelectedItem.ToString();

            switch (mode)
            {
                case "Thêm":
                case "Sửa":
                    txb_QLCT2_getMaCN.Visible = true;
                    txb_QLCT2_getTenCN.Visible = true;
                    dtp_QLCT2_getOpenTime.Visible = true;
                    dtp_QLCT2_getCloseTime.Visible = true;
                    txb_QLCT2_getAddress.Visible = true;
                    txb_QLCT2_getSDT.Visible = true;

                    lbl_QLCT2_MaCN.Visible = true;
                    lbl_QLCT2_TenCN.Visible = true;
                    lbl_QLCT2_OpenTime.Visible = true;
                    lbl_QLCT2_CloseTime.Visible = true;
                    lbl_QLCT2_Address.Visible = true;
                    lbl_QLCT2_SDT.Visible = true;
                    break;

                case "Xóa":
                    txb_QLCT2_getMaCN.Visible = true;
                    lbl_QLCT2_MaCN.Visible = true;
                    break;

                case "Xem":
                    txb_QLCT2_getMaCN.Visible = true;
                    txb_QLCT2_getTenCN.Visible = true;
                    dtp_QLCT2_getOpenTime.Visible = true;
                    dtp_QLCT2_getCloseTime.Visible = true;
                    txb_QLCT2_getAddress.Visible = true;
                    txb_QLCT2_getSDT.Visible = true;

                    lbl_QLCT2_MaCN.Visible = true;
                    lbl_QLCT2_TenCN.Visible = true;
                    lbl_QLCT2_OpenTime.Visible = true;
                    lbl_QLCT2_CloseTime.Visible = true;
                    lbl_QLCT2_Address.Visible = true;
                    lbl_QLCT2_SDT.Visible = true;
                    break;
            }
        }

        private void btn_QLCT2_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT2_selectAction.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tác vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mode = cmb_QLCT2_selectAction.SelectedItem.ToString();
            string maCN = txb_QLCT2_getMaCN.Text.Trim();
            string tenCN = txb_QLCT2_getTenCN.Text.Trim();
            string diaChi = txb_QLCT2_getAddress.Text.Trim();
            string sdt = txb_QLCT2_getSDT.Text.Trim();

            TimeSpan inputOpenTime = dtp_QLCT2_getOpenTime.Value.TimeOfDay;
            TimeSpan inputCloseTime = dtp_QLCT2_getCloseTime.Value.TimeOfDay;

            TimeSpan defaultOpenVal = TimeSpan.Zero;
            TimeSpan defaultCloseVal = TimeSpan.FromSeconds(1);

            try
            {
                switch (mode)
                {
                    case "Thêm":
                        if (string.IsNullOrEmpty(maCN) || string.IsNullOrEmpty(tenCN))
                        {
                            MessageBox.Show("Vui lòng nhập Mã CN và Tên CN!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (service.ThemChiNhanh(maCN, tenCN, diaChi, sdt, inputOpenTime, inputCloseTime))
                        {
                            MessageBox.Show("Thêm chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại. Có thể Mã CN đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "Sửa":
                        if (string.IsNullOrEmpty(maCN))
                        {
                            MessageBox.Show("Vui lòng nhập Mã CN cần sửa!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DataTable currentData = service.XemChiNhanh(maCN: maCN);
                        if (currentData.Rows.Count == 0)
                        {
                            MessageBox.Show("Mã Chi Nhánh không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        DataRow oldRow = currentData.Rows[0];

                        string finalTenCN = string.IsNullOrEmpty(tenCN) ? oldRow["TenCN"].ToString() : tenCN;
                        string finalDiaChi = string.IsNullOrEmpty(diaChi) ? oldRow["DiaChiCN"].ToString() : diaChi;
                        string finalSDT = string.IsNullOrEmpty(sdt) ? oldRow["SDT_CN"].ToString() : sdt;

                        TimeSpan finalOpenTime = inputOpenTime;
                        if (inputOpenTime == defaultOpenVal && oldRow["TimeMoCua"] != DBNull.Value)
                        {
                            finalOpenTime = (TimeSpan)oldRow["TimeMoCua"];
                        }

                        TimeSpan finalCloseTime = inputCloseTime;
                        if (inputCloseTime == defaultCloseVal && oldRow["TimeDongCua"] != DBNull.Value)
                        {
                            finalCloseTime = (TimeSpan)oldRow["TimeDongCua"];
                        }

                        if (service.SuaChiNhanh(maCN, finalTenCN, finalDiaChi, finalSDT, finalOpenTime, finalCloseTime))
                        {
                            MessageBox.Show("Cập nhật chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "Xóa":
                        if (string.IsNullOrEmpty(maCN))
                        {
                            MessageBox.Show("Vui lòng nhập Mã CN cần xóa!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DialogResult confirm = MessageBox.Show($"Bạn có chắc muốn xóa chi nhánh {maCN}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm == DialogResult.Yes)
                        {
                            if (service.XoaChiNhanh(maCN))
                            {
                                MessageBox.Show("Xóa chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshDataGrid();
                            }
                            else
                            {
                                MessageBox.Show("Xóa thất bại. Mã CN không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;

                    case "Xem":
                        string filterMa = string.IsNullOrEmpty(maCN) ? null : maCN;
                        string filterTen = string.IsNullOrEmpty(tenCN) ? null : tenCN;
                        string filterDiaChi = string.IsNullOrEmpty(diaChi) ? null : diaChi;
                        string filterSDT = string.IsNullOrEmpty(sdt) ? null : sdt;

                        DataTable dt = service.XemChiNhanh(filterMa, filterTen, filterDiaChi, filterSDT, null, null);
                        UpdateBindingList(dt);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void lbl_QLCT2_DiaChi_Click(object sender, EventArgs e) { }
        private void txb_QLCT2_getAddress_Add_TextChanged(object sender, EventArgs e) { }

        private void txb_QLCT2_getTenCN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_QLCT2_getSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_QLCT2_QLCN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}