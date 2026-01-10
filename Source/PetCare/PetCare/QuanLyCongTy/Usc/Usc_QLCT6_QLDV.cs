using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT6_QLDV : UserControl
    {
        private ServiceDAL service = new ServiceDAL();
        private BindingList<Class_QLCT6_QLDV> danhSach = new BindingList<Class_QLCT6_QLDV>();

        public Usc_QLCT6_QLDV()
        {
            InitializeComponent();
            dgv_QLCT6_QLDV.DataSource = danhSach;
            LoadDanhSach();
        }

        private void LoadDanhSach(string maDV = null, string tenDV = null, int? giaDV = null)
        {
            try
            {
                danhSach.Clear();
                DataTable dt = service.XemDichVu(maDV, tenDV, giaDV);

                foreach (DataRow row in dt.Rows)
                {
                    danhSach.Add(new Class_QLCT6_QLDV
                    {
                        MaDV = row["MaDichVu"].ToString(),
                        TenDV = row["TenDV"].ToString(),
                        GiaDV = Convert.ToInt32(row["GiaTienDV"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_QLCT6_selectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT6_getMaDV.Visible = false;
            txb_QLCT6_getTenDV.Visible = false;
            txb_QLCT6_getGiaDV.Visible = false;

            lbl_QLCT6_MaDV.Visible = false;
            lbl_QLCT6_TenDV.Visible = false;
            lbl_QLCT6_GiaDV.Visible = false;

            txb_QLCT6_getMaDV.Clear();
            txb_QLCT6_getTenDV.Clear();
            txb_QLCT6_getGiaDV.Clear();

            if (cmb_QLCT6_selectAction.SelectedItem == null)
                return;

            string mode = cmb_QLCT6_selectAction.SelectedItem.ToString();

            switch (mode)
            {
                case "Thêm":
                case "Sửa":
                case "Xem":
                    txb_QLCT6_getMaDV.Visible = true;
                    txb_QLCT6_getTenDV.Visible = true;
                    txb_QLCT6_getGiaDV.Visible = true;

                    lbl_QLCT6_MaDV.Visible = true;
                    lbl_QLCT6_TenDV.Visible = true;
                    lbl_QLCT6_GiaDV.Visible = true;
                    break;

                case "Xóa":
                    txb_QLCT6_getMaDV.Visible = true;
                    lbl_QLCT6_MaDV.Visible = true;
                    break;
            }
        }

        private void btn_QLCT6_ThucHien_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT6_selectAction.SelectedItem == null) return;

            string mode = cmb_QLCT6_selectAction.SelectedItem.ToString();
            string maDV = txb_QLCT6_getMaDV.Text.Trim();
            string tenDV = txb_QLCT6_getTenDV.Text.Trim();
            string giaText = txb_QLCT6_getGiaDV.Text.Trim();

            try
            {
                switch (mode)
                {
                    case "Thêm":
                        XuLyThem(maDV, tenDV, giaText);
                        break;
                    case "Sửa":
                        XuLySua(maDV, tenDV, giaText);
                        break;
                    case "Xóa":
                        XuLyXoa(maDV);
                        break;
                    case "Xem":
                        XuLyXem(maDV, tenDV, giaText);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyThem(string ma, string ten, string giaStr)
        {
            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(giaStr))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(giaStr, out int gia))
            {
                MessageBox.Show("Giá dịch vụ phải là số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (service.ThemDichVu(ma, ten, gia))
            {
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
            }
            else
            {
                MessageBox.Show("Thêm thất bại. Mã dịch vụ có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLySua(string ma, string tenInput, string giaInput)
        {
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập Mã Dịch Vụ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable currentData = service.XemDichVu(maDichVu: ma);
            if (currentData.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dịch vụ với mã này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow currentRow = currentData.Rows[0];

            string tenFinal = string.IsNullOrEmpty(tenInput) ? currentRow["TenDV"].ToString() : tenInput;

            int giaFinal;
            if (string.IsNullOrEmpty(giaInput))
            {
                giaFinal = Convert.ToInt32(currentRow["GiaTienDV"]);
            }
            else
            {
                if (!int.TryParse(giaInput, out giaFinal))
                {
                    MessageBox.Show("Giá dịch vụ mới không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (service.SuaDichVu(ma, tenFinal, giaFinal))
            {
                MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyXoa(string ma)
        {
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập Mã Dịch Vụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ {ma}?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (service.XoaDichVu(ma))
                {
                    MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại. Mã dịch vụ có thể không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XuLyXem(string ma, string ten, string giaStr)
        {
            string filterMa = string.IsNullOrEmpty(ma) ? null : ma;
            string filterTen = string.IsNullOrEmpty(ten) ? null : ten;
            int? filterGia = null;

            if (!string.IsNullOrEmpty(giaStr))
            {
                if (int.TryParse(giaStr, out int g))
                {
                    filterGia = g;
                }
                else
                {
                    MessageBox.Show("Giá tìm kiếm phải là số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            LoadDanhSach(filterMa, filterTen, filterGia);
        }

        private void btn_QLCT6_XacNhan_Click(object sender, EventArgs e)
        {
            btn_QLCT6_ThucHien_Click(sender, e);
        }

        private void txb_QLCT6_getMaDV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_QLCT6_getTenDV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_QLCT6_getGiaDV_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_QLCT6_QLDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}