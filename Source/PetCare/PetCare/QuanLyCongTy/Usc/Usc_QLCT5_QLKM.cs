using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT5_QLKM : UserControl
    {
        private BindingList<Class_QLCT5_QLKM> danhSach = new BindingList<Class_QLCT5_QLKM>();
        private ServiceDAL dal = new ServiceDAL();

        public Usc_QLCT5_QLKM()
        {
            InitializeComponent();
            dgv_QLCT5_QLKM.DataSource = danhSach;
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            try
            {
                DataTable dt = dal.XemKhuyenMai();
                RefillDataGrid(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void RefillDataGrid(DataTable dt)
        {
            danhSach.Clear();
            foreach (DataRow row in dt.Rows)
            {
                danhSach.Add(new Class_QLCT5_QLKM
                {
                    MaKM = row["MaKM"].ToString(),
                    LoaiKM = row["LoaiKM"].ToString(),
                    GiaKM = Convert.ToInt32(row["GiaKM"])
                });
            }
        }

        private void txb_QLCT5_getGiaKM_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_QLCT5_selectAction_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txb_QLCT5_getMaKM.Visible = false;
            txb_QLCT5_getLoaiKM.Visible = false;
            txb_QLCT5_getGiaKM.Visible = false;

            lbl_QLCT5_MaKM.Visible = false;
            lbl_QLCT5_LoaiKM.Visible = false;
            lbl_QLCT5_GiaKM.Visible = false;

            if (cmb_QLCT5_selectAction.SelectedItem == null)
                return;

            string mode = cmb_QLCT5_selectAction.SelectedItem.ToString();

            switch (mode)
            {
                case "Thêm":
                case "Sửa":
                case "Xem":
                    txb_QLCT5_getMaKM.Visible = true;
                    txb_QLCT5_getLoaiKM.Visible = true;
                    txb_QLCT5_getGiaKM.Visible = true;

                    lbl_QLCT5_MaKM.Visible = true;
                    lbl_QLCT5_LoaiKM.Visible = true;
                    lbl_QLCT5_GiaKM.Visible = true;
                    break;

                case "Xóa":
                    txb_QLCT5_getMaKM.Visible = true;
                    lbl_QLCT5_MaKM.Visible = true;
                    break;
            }
        }

        private void btn_QLCT5_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT5_selectAction.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một hành động!");
                return;
            }

            string action = cmb_QLCT5_selectAction.SelectedItem.ToString();
            string maKM = txb_QLCT5_getMaKM.Text.Trim();
            string loaiKM = txb_QLCT5_getLoaiKM.Text.Trim();
            string giaText = txb_QLCT5_getGiaKM.Text.Trim();

            try
            {
                switch (action)
                {
                    case "Thêm":
                        if (string.IsNullOrEmpty(maKM) || string.IsNullOrEmpty(loaiKM) || string.IsNullOrEmpty(giaText))
                        {
                            MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Mã, Loại, Giá)!");
                            return;
                        }

                        if (!int.TryParse(giaText, out int giaKM_Add))
                        {
                            MessageBox.Show("Giá khuyến mãi phải là số nguyên!");
                            return;
                        }

                        if (dal.TaoKhuyenMai(maKM, loaiKM, giaKM_Add))
                        {
                            MessageBox.Show("Thêm khuyến mãi thành công!");
                            LoadInitialData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại. Mã có thể đã tồn tại.");
                        }
                        break;

                    case "Sửa":
                        if (string.IsNullOrEmpty(maKM))
                        {
                            MessageBox.Show("Vui lòng nhập Mã khuyến mãi cần sửa!");
                            return;
                        }

                        DataTable currentData = dal.XemKhuyenMai(maKM, null, null);

                        if (currentData.Rows.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy Mã khuyến mãi này để sửa!");
                            return;
                        }

                        DataRow currentRow = currentData.Rows[0];
                        string currentLoai = currentRow["LoaiKM"].ToString();
                        int currentGia = Convert.ToInt32(currentRow["GiaKM"]);

                        string newLoai = string.IsNullOrEmpty(loaiKM) ? currentLoai : loaiKM;
                        int newGia;

                        if (string.IsNullOrEmpty(giaText))
                        {
                            newGia = currentGia;
                        }
                        else
                        {
                            if (!int.TryParse(giaText, out newGia))
                            {
                                MessageBox.Show("Giá trị nhập vào không hợp lệ!");
                                return;
                            }
                        }

                        if (dal.SuaKhuyenMai(maKM, newLoai, newGia))
                        {
                            MessageBox.Show("Sửa khuyến mãi thành công!");
                            LoadInitialData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thất bại.");
                        }
                        break;

                    case "Xóa":
                        if (string.IsNullOrEmpty(maKM))
                        {
                            MessageBox.Show("Vui lòng nhập Mã khuyến mãi cần xóa!");
                            return;
                        }

                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (dal.XoaKhuyenMai(maKM))
                            {
                                MessageBox.Show("Xóa thành công!");
                                LoadInitialData();
                                ClearInputs();
                            }
                            else
                            {
                                MessageBox.Show("Xóa thất bại. Mã có thể không tồn tại.");
                            }
                        }
                        break;

                    case "Xem":
                        string searchMa = string.IsNullOrEmpty(maKM) ? null : maKM;
                        string searchLoai = string.IsNullOrEmpty(loaiKM) ? null : loaiKM;
                        int? searchGia = null;

                        if (!string.IsNullOrEmpty(giaText))
                        {
                            if (int.TryParse(giaText, out int parsedGia))
                            {
                                searchGia = parsedGia;
                            }
                            else
                            {
                                MessageBox.Show("Giá tìm kiếm phải là số nguyên!");
                                return;
                            }
                        }

                        DataTable dtResult = dal.XemKhuyenMai(searchMa, searchLoai, searchGia);
                        RefillDataGrid(dtResult);

                        if (dtResult.Rows.Count == 0)
                            MessageBox.Show("Không tìm thấy kết quả nào.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txb_QLCT5_getMaKM.Clear();
            txb_QLCT5_getLoaiKM.Clear();
            txb_QLCT5_getGiaKM.Clear();
        }
    }
}