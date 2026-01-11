using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang.UC
{
    public partial class UC_KH_MuaHangTrucTuyen : UserControl
    {
        ServiceDAL dal = new ServiceDAL();
        private UC_KH_GioHang ucGioHang; // Khai báo biến toàn cục để quản lý

        public UC_KH_MuaHangTrucTuyen()
        {
            InitializeComponent();
            SetVisibilityForShopping(false);
            this.Load += UC_KH_MuaHangTrucTuyen_Load;
        }
        private void SetVisibilityForShopping(bool isVisible)
        {
            // Duyệt qua tất cả các Control có trên Panel chính
            foreach (Control c in pnl_KH_MuaHangTrucTuyen.Controls)
            {
                // Danh sách các thành phần luôn luôn hiện (Dựa trên Name của Control)
                if (c.Name == "lb_KH_MuaHangTrucTuyen" ||
                    c.Name == "pic_icon_KH_MuaHangTrucTuyen" ||
                    c.Name == "lb_KH_ChonChiNhanh" ||
                    c.Name == "comboBox_KH_ChonChiNhanh" ||
                    c.Name == "btn_KH_ChonChiNhanh")
                {
                    c.Visible = true;
                }
                else
                {
                    // Các thành phần còn lại (Lọc, Grid, nút Áp dụng, Giỏ hàng) sẽ ẩn/hiện theo tham số
                    c.Visible = isVisible;
                }
            }
        }
        private void btn_KH_ChonChiNhanh_Click(object sender, EventArgs e)
        {
            if (comboBox_KH_ChonChiNhanh.SelectedValue != null)
            {
                SessionData.MaCN_DangChon = comboBox_KH_ChonChiNhanh.SelectedValue.ToString();
                SetVisibilityForShopping(true);
                LoadDanhSachSanPham();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chi nhánh!");
            }
        }
        private void btn_KH_GioHang_Click(object sender, EventArgs e)
        {
            ucGioHang = new UC_KH_GioHang();

            ucGioHang.QuayVeTrangChu += UcGioHang_QuayVeTrangChu;

            pnl_KH_MuaHangTrucTuyen.Visible = false;

            if (!pnl_KH_Content.Controls.Contains(ucGioHang))
            {
                pnl_KH_Content.Controls.Add(ucGioHang);
            }

            ucGioHang.Left = (pnl_KH_Content.Width - ucGioHang.Width) / 2;
            ucGioHang.Top = (pnl_KH_Content.Height - ucGioHang.Height) / 2;
            ucGioHang.BringToFront();
        }

        private void UcGioHang_QuayVeTrangChu()
        {
            if (ucGioHang != null)
            {
                ucGioHang.QuayVeTrangChu -= UcGioHang_QuayVeTrangChu;

                pnl_KH_Content.Controls.Remove(ucGioHang);
                ucGioHang.Dispose(); 
                ucGioHang = null;

                pnl_KH_MuaHangTrucTuyen.Visible = true;
                LoadDanhSachSanPham();
            }
        }
        private void UC_KH_MuaHangTrucTuyen_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCN = dal.GetDanhSachChiNhanh();

                if (dtCN != null && dtCN.Rows.Count > 0)
                {
                    comboBox_KH_ChonChiNhanh.DisplayMember = "TenCN"; 
                    comboBox_KH_ChonChiNhanh.ValueMember = "MaCN";   
                    comboBox_KH_ChonChiNhanh.DataSource = dtCN;  
                    comboBox_KH_ChonChiNhanh.SelectedIndex = -1; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách chi nhánh: " + ex.Message);
            }
        }

        private void btn_KH_ApDung_Click(object sender, EventArgs e)
        {
            LoadDanhSachSanPham();
        }
        private void btn_KH_TimKiem_Click(object sender, EventArgs e)
        {
            if (comboBox_KH_ChonChiNhanh.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh trước!");
                return;
            }

            try
            {
                string maCN = comboBox_KH_ChonChiNhanh.SelectedValue.ToString();
                string value = textBox_KH_TimKiem.Text.Trim();

                DataTable dt = dal.TimKiemThongMinh(maCN, value);

                dataGridView_DanhSachSP.AutoGenerateColumns = false;
                dataGridView_DanhSachSP.DataSource = dt;

                if (dt.Rows.Count == 0 && !string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào khớp với mã, tên hoặc loại sản phẩm bạn nhập.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                if (SessionData.MaCN_DangChon == null) return;

                string maCN = SessionData.MaCN_DangChon;
                string maSP = textBox_KH_LocMa.Text.Trim();
                string tenSP = textBox_KH_LocTen.Text.Trim();
                string loaiSP = comboBox_KH_LocLoai.SelectedIndex > 0 ? comboBox_KH_LocLoai.Text : null;

                DataTable dtSP = dal.LocSanPham(maCN, maSP, tenSP, loaiSP, null, null);

                dataGridView_DanhSachSP.AutoGenerateColumns = false;
                dataGridView_DanhSachSP.DataSource = dtSP;
                dataGridView_DanhSachSP.Columns["MaSP"].DataPropertyName = "MaSP";
                dataGridView_DanhSachSP.Columns["TenSP"].DataPropertyName = "TenSP";
                dataGridView_DanhSachSP.Columns["LoaiSP"].DataPropertyName = "LoaiSP";
                dataGridView_DanhSachSP.Columns["GiaBan"].DataPropertyName = "GiaBan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dataGridView_DanhSachSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
        dataGridView_DanhSachSP.Columns[e.ColumnIndex].Name == "ThemGio")
            {
                var cellMaSP = dataGridView_DanhSachSP.Rows[e.RowIndex].Cells["MaSP"].Value;
                string maCN = comboBox_KH_ChonChiNhanh.SelectedValue?.ToString();

                if (cellMaSP == null || string.IsNullOrEmpty(maCN)) return;

                try
                {
                    dal.ThemVaoGioHang(SessionData.MaKH, cellMaSP.ToString(), maCN);

                    MessageBox.Show("Sản phẩm đã được thêm vào giỏ hàng thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
