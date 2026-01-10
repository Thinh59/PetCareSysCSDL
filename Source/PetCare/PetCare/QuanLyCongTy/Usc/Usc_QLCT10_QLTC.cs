using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PetCare
{
    public partial class Usc_QLCT10_QLTC : UserControl
    {
        private ServiceDAL service = new ServiceDAL();
        private BindingList<Class_QLCT10_QLTC> danhSach = new BindingList<Class_QLCT10_QLTC>();

        public Usc_QLCT10_QLTC()
        {
            InitializeComponent();
            dgv_QLCT10_QLTC.DataSource = danhSach;
        }

        private void Usc_QLCT10_QLTC_Load(object sender, EventArgs e)
        {
            dtp_QLCT10_getNgaySinh.Value = new DateTime(1900, 1, 1);

            if (cmb_QLCT10_selectAction.Items.Count == 0)
            {
                cmb_QLCT10_selectAction.Items.AddRange(new object[] { "Thêm", "Sửa", "Xóa", "Xem" });
            }
            cmb_QLCT10_selectAction.SelectedIndex = 0;

            cmb_QLCT10_getGioiTinhTC.Items.Clear(); 

            cmb_QLCT10_getGioiTinhTC.Items.AddRange(new object[] { "Đực", "Cái", "Khác" });

            cmb_QLCT10_getGioiTinhTC.DropDownStyle = ComboBoxStyle.DropDownList;

            cmb_QLCT10_getGioiTinhTC.SelectedIndex = 0;

            LoadCustomerComboBox();
            LoadDataToGrid();
        }

        private void LoadCustomerComboBox()
        {
            try
            {
                DataTable dt = service.ThongKeKhachHangCongTy();
                cmb_QLCT10_getMaKH.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dt.Columns.Contains("MaKH"))
                    {
                        cmb_QLCT10_getMaKH.Items.Add(dr["MaKH"].ToString());
                    }
                }
            }
            catch { }
        }

        private void LoadDataToGrid(string maTC = null, string tenTC = null, string maKH = null, string loaiTC = null)
        {
            try
            {
                DataTable dt = service.XemDanhSachThuCung(maTC, tenTC, maKH, loaiTC);
                danhSach.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    danhSach.Add(new Class_QLCT10_QLTC
                    {
                        MaTC = row["MaThuCung"].ToString(),
                        TenTC = row["TenThuCung"].ToString(),
                        LoaiTC = row["LoaiThuCung"].ToString(),
                        GiongTC = row["Giong_TC"].ToString(),
                        NgaySinh = row["NgaySinh_TC"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh_TC"]) : DateTime.MinValue,
                        GioiTinh = row["GioiTinh_TC"].ToString(),
                        TinhTrangSK = row["TinhTrangSK"].ToString(),
                        MaKH = row["MaKH"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_QLCT10_selectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT10_getMaTC.Visible = false;
            txb_QLCT10_getTenTC.Visible = false;
            txb_QLCT10_getLoaiTC.Visible = false;
            txb_QLCT10_getGiongTC.Visible = false;
            txb_QLCT10_getTinhTrangSK.Visible = false;

            cmb_QLCT10_getGioiTinhTC.Visible = false;
            cmb_QLCT10_getMaKH.Visible = false;

            dtp_QLCT10_getNgaySinh.Visible = false;

            lbl_QLCT10_MaTC.Visible = false;
            lbl_QLCT10_TenTC.Visible = false;
            lbl_QLCT10_LoaiTC.Visible = false;
            lbl_QLCT10_GiongTC.Visible = false;
            lbl_QLCT10_NgaySinh.Visible = false;
            lbl_QLCT10_GioiTinhTC.Visible = false;
            lbl_QLCT10_TinhTrangSK.Visible = false;
            lbl_QLCT10_MaKH.Visible = false;

            if (cmb_QLCT10_selectAction.SelectedItem == null)
                return;

            string mode = cmb_QLCT10_selectAction.SelectedItem.ToString();

            switch (mode)
            {
                case "Thêm":
                case "Sửa":
                case "Xem":
                    txb_QLCT10_getMaTC.Visible = true;
                    txb_QLCT10_getTenTC.Visible = true;
                    txb_QLCT10_getLoaiTC.Visible = true;
                    txb_QLCT10_getGiongTC.Visible = true;
                    txb_QLCT10_getTinhTrangSK.Visible = true;

                    cmb_QLCT10_getGioiTinhTC.Visible = true;
                    cmb_QLCT10_getMaKH.Visible = true;

                    dtp_QLCT10_getNgaySinh.Visible = true;

                    lbl_QLCT10_MaTC.Visible = true;
                    lbl_QLCT10_TenTC.Visible = true;
                    lbl_QLCT10_LoaiTC.Visible = true;
                    lbl_QLCT10_GiongTC.Visible = true;
                    lbl_QLCT10_NgaySinh.Visible = true;
                    lbl_QLCT10_GioiTinhTC.Visible = true;
                    lbl_QLCT10_TinhTrangSK.Visible = true;
                    lbl_QLCT10_MaKH.Visible = true;
                    break;

                case "Xóa":
                    txb_QLCT10_getMaTC.Visible = true;
                    lbl_QLCT10_MaTC.Visible = true;
                    break;
            }
        }

        private void btn_QLCT10_XacNhan_Click(object sender, EventArgs e)
        {
            
            if (cmb_QLCT10_selectAction.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hành động (Thêm/Sửa/Xóa/Xem)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mode = cmb_QLCT10_selectAction.SelectedItem.ToString();
            string maTC = txb_QLCT10_getMaTC.Text.Trim();
            string tenTC = txb_QLCT10_getTenTC.Text.Trim();
            string loaiTC = txb_QLCT10_getLoaiTC.Text.Trim();
            string giongTC = txb_QLCT10_getGiongTC.Text.Trim();
            string tinhTrangSK = txb_QLCT10_getTinhTrangSK.Text.Trim();
            string maKH = cmb_QLCT10_getMaKH.SelectedItem?.ToString() ?? cmb_QLCT10_getMaKH.Text.Trim();
            DateTime ngaySinh = dtp_QLCT10_getNgaySinh.Value;


            string gioiTinh = cmb_QLCT10_getGioiTinhTC.Text.Trim(); 
            if (string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính thú cưng!", "Thiếu thông tin");
                return;
            }

            try
            {
                switch (mode)
                {
                    case "Xem":
                        LoadDataToGrid(
                            string.IsNullOrEmpty(maTC) ? null : maTC,
                            string.IsNullOrEmpty(tenTC) ? null : tenTC,
                            string.IsNullOrEmpty(maKH) ? null : maKH,
                            string.IsNullOrEmpty(loaiTC) ? null : loaiTC
                        );
                        break;

                    case "Thêm":
                        if (string.IsNullOrEmpty(maTC) || string.IsNullOrEmpty(tenTC) || string.IsNullOrEmpty(maKH))
                        {
                            MessageBox.Show("Vui lòng nhập Mã TC, Tên TC và Mã KH!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (service.ThemThuCung(maTC, tenTC, loaiTC, giongTC, ngaySinh, gioiTinh, tinhTrangSK, maKH))
                        {
                            MessageBox.Show("Thêm thú cưng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataToGrid();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại. Mã thú cưng có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "Sửa":
                        if (string.IsNullOrEmpty(maTC))
                        {
                            MessageBox.Show("Vui lòng nhập Mã Thú Cưng cần sửa!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DataTable currentData = service.XemDanhSachThuCung(maTC);
                        if (currentData.Rows.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy thú cưng có mã này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        DataRow current = currentData.Rows[0];

                        string newTen = string.IsNullOrEmpty(tenTC) ? current["TenThuCung"].ToString() : tenTC;
                        string newLoai = string.IsNullOrEmpty(loaiTC) ? current["LoaiThuCung"].ToString() : loaiTC;
                        string newGiong = string.IsNullOrEmpty(giongTC) ? current["Giong_TC"].ToString() : giongTC;
                        string newTinhTrang = string.IsNullOrEmpty(tinhTrangSK) ? current["TinhTrangSK"].ToString() : tinhTrangSK;
                        string newGioiTinh = string.IsNullOrEmpty(gioiTinh) ? current["GioiTinh_TC"].ToString() : gioiTinh;
                        string newMaKH = string.IsNullOrEmpty(maKH) ? current["MaKH"].ToString() : maKH;

                        DateTime newNgaySinh = (ngaySinh == new DateTime(1900, 1, 1) && current["NgaySinh_TC"] != DBNull.Value)
                            ? Convert.ToDateTime(current["NgaySinh_TC"])
                            : ngaySinh;

                        if (service.CapNhatThuCung(maTC, newTen, newLoai, newGiong, newNgaySinh, newGioiTinh, newTinhTrang, newMaKH))
                        {
                            MessageBox.Show("Cập nhật thú cưng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataToGrid();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "Xóa":
                        if (string.IsNullOrEmpty(maTC))
                        {
                            MessageBox.Show("Vui lòng nhập Mã Thú Cưng cần xóa!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa thú cưng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (service.XoaThuCung(maTC))
                            {
                                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataToGrid();
                            }
                            else
                            {
                                MessageBox.Show("Xóa thất bại. Mã thú cưng không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống: " + ex.Message, "Lỗi Critical", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pnl_QTCT10_Paint(object sender, PaintEventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lbl_QLCT10_MaKH_Click(object sender, EventArgs e) { }
        private void cmb_QLCT10_getMaKH_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txb_QLCT10_getTinhTrangSK_TextChanged(object sender, EventArgs e) { }

        private void dtp_QLCT10_getNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}