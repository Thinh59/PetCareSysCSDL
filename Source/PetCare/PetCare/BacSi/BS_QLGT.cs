using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class BS_QLGT : UserControl
    {
        DataConnection dc = new DataConnection();
        public BS_QLGT()
        {
            InitializeComponent();
            data_BS_QLGT.AutoGenerateColumns = false;
        }

        private void data_BS_QLGT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnl_BS_QLGT_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbox_BS_QLGT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_BS_QLGT.SelectedValue == null || cbox_BS_QLGT.SelectedValue is DataRowView) return;

            string maGoi = cbox_BS_QLGT.SelectedValue.ToString();
            string maTC = tbox_BS_QLGT_MaTC.Text.Trim();

            LoadChiTietMuiTiem(maGoi, maTC);
        }

        private void LoadChiTietMuiTiem(string maGoi, string maTC)
        {
            try
            {
                SqlParameter[] p = {
            new SqlParameter("@MaGoiTiem", maGoi),
            new SqlParameter("@MaThuCung", maTC)
        };
                DataTable dt = dc.ExecuteProcedure("sp_GetChiTietGoiTiemVoiTrangThai", p);
                data_BS_QLGT.DataSource = dt;

                foreach (DataGridViewRow row in data_BS_QLGT.Rows)
                {
                    if (row.Cells["ColNgayTiem"].Value != DBNull.Value && row.Cells["ColNgayTiem"].Value != null)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DimGray;

                        if (row.Cells["ColDanhDau"] is DataGridViewCheckBoxCell chk)
                        {
                            chk.Value = true;
                            chk.ReadOnly = true;
                        }
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        if (row.Cells["ColDanhDau"] is DataGridViewCheckBoxCell chk)
                        {
                            chk.Value = false;
                            chk.ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết: " + ex.Message);
            }
        }

        private void tbox_BS_QLGT_MaTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void lab_BS_DSGT_Click(object sender, EventArgs e)
        {

        }

        private void lab_BS_QLGT_MaTC_Click(object sender, EventArgs e)
        {

        }

        private void lab_BS_QLGT_Click(object sender, EventArgs e)
        {

        }

        private void btn_BS_QLGT_Click(object sender, EventArgs e)
        {
            string maTC = tbox_BS_QLGT_MaTC.Text.Trim();
            if (string.IsNullOrEmpty(maTC))
            {
                MessageBox.Show("Vui lòng nhập Mã Thú Cưng!", "Thông báo");
                return;
            }

            LoadGoiTiemDaMua(maTC);
        }

        private void LoadGoiTiemDaMua(string maTC)
        {
            try
            {
                SqlParameter[] p = { new SqlParameter("@MaThuCung", maTC) };
                DataTable dt = dc.ExecuteProcedure("sp_GetGoiTiemDaMua", p);

                if (dt.Rows.Count > 0)
                {
                    cbox_BS_QLGT.DataSource = dt;
                    cbox_BS_QLGT.DisplayMember = "TenGoi";
                    cbox_BS_QLGT.ValueMember = "MaGoiTiem";
                }
                else
                {
                    cbox_BS_QLGT.DataSource = null;
                    data_BS_QLGT.DataSource = null;
                    MessageBox.Show("Thú cưng này chưa đăng ký gói tiêm nào!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_BS_QLGT_Luu_Click(object sender, EventArgs e)
        {
            string maTC = tbox_BS_QLGT_MaTC.Text.Trim();
            string maGoi = cbox_BS_QLGT.SelectedValue?.ToString();
            bool thanhCong = false;

            foreach (DataGridViewRow row in data_BS_QLGT.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["ColDanhDau"] as DataGridViewCheckBoxCell;

                if (chk != null && chk.ReadOnly == false && Convert.ToBoolean(chk.Value) == true)
                {
                    string tenVX = row.Cells["colTenVX"].Value?.ToString() ?? "";
                    string maVC = row.Cells["MaVC"].Value?.ToString() ?? "";

                    DialogResult dr = MessageBox.Show($"Xác nhận đã tiêm vaccine {tenVX} cho thú cưng {maTC}?",
                                                      "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        string maLSDV = "TP" + DateTime.Now.ToString("ssmmHH");

                        SqlParameter[] p = {
                    new SqlParameter("@MaLSDVTP", maLSDV),
                    new SqlParameter("@BacSiPhuTrach", SessionData.MaNV),
                    new SqlParameter("@MaGoiTiem", maGoi),
                    new SqlParameter("@LoaiVacXin", maVC),
                    new SqlParameter("@NgayTiem", DateTime.Now),
                    new SqlParameter("@MaThuCung", maTC)
                };
                        dc.ExecuteProcedure("sp_LuuTiemPhongTheoGoi", p);
                        thanhCong = true;
                    }
                }
            }

            if (thanhCong)
            {
                MessageBox.Show("Lưu lịch sử thành công!");
                LoadChiTietMuiTiem(maGoi, maTC);
            }
        }

        private void BS_QLGT_Load(object sender, EventArgs e)
        {

        }
    }
}
