using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN2_QLNV : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN2_DSNV> danhSachNV = new BindingList<Class_QLCN2_DSNV>();

        private string currMode = "";

        public Usc_QLCN2_QLNV()
        {
            InitializeComponent();
            dgv_QLCN2_DSNV.AutoGenerateColumns = true; 
            dgv_QLCN2_DSNV.DataSource = danhSachNV;

            this.Load += Usc_QLCN2_QLNV_Load;


            dgv_QLCN2_DSNV.CellClick += dgv_QLCN2_DSNV_CellContentClick;
        }

        private void Usc_QLCN2_QLNV_Load(object sender, EventArgs e)
        {

            if (cmb_QLCN2_trangThai.Items.Count == 0)
            {
                cmb_QLCN2_trangThai.Items.AddRange(new object[] { "Rảnh", "Bận", "Nghỉ phép" });
            }


            LoadData();


            SetInputState(false);
            SetButtonState(true);
        }


        private void LoadData()
        {
            try
            {
                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01"; // Fallback test

                DataTable dt = serviceDAL.GetDSNV(maCN);
                danhSachNV.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    danhSachNV.Add(new Class_QLCN2_DSNV
                    {
                        Chon = false,
                        MaNV = row["MaNV"].ToString(),
                        HoTen = row["HoTenNV"].ToString(),
                        NgaySinh = Convert.ToDateTime(row["NgaySinhNV"]),
                        NgayBDLV = Convert.ToDateTime(row["NgayVaoLam"]),
                        ChucVu = row["ChucVu"].ToString(),
                        Luong = Convert.ToInt32(row["Luong"]),
                        TrangThai = row["TrangThaiNV"].ToString()
                    });
                }
                dgv_QLCN2_DSNV.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btn_QLCN2_Them_Click(object sender, EventArgs e)
        {
            currMode = "ADD";
            ClearInputs();
            SetInputState(true); 
            cmb_QLCN2_MaNV.Enabled = true;
            cmb_QLCN2_MaNV.Focus();

            SetButtonState(false); 
        }

        private void btn_QLCN2_Sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_QLCN2_MaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.");
                return;
            }

            currMode = "EDIT";
            SetInputState(true);

            cmb_QLCN2_MaNV.Enabled = false;

            SetButtonState(false);
        }

        private void btn_QLCN2_Xoa_Click(object sender, EventArgs e)
        {
            string maNV = cmb_QLCN2_MaNV.Text;
            if (string.IsNullOrEmpty(maNV)) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa nhân viên {maNV}?\n(Dữ liệu tài khoản cũng sẽ bị xóa)", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    serviceDAL.XoaNV(maNV);
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btn_QLCN2_XacNhan_Click(object sender, EventArgs e)
        {
            string maNV = cmb_QLCN2_MaNV.Text.Trim();
            string hoTen = txb_QLCN2_TenNV.Text.Trim();
            string chucVu = txb_QLCN2_ChucVu.Text.Trim();
            string trangThai = cmb_QLCN2_trangThai.Text;
            DateTime ngSinh = atp_QLCN2_NgaySinh.Value;
            DateTime ngLam = dtp_QLCN2_NgayVaoLam.Value;

            int luong = 0;
            int.TryParse(txb_QLCN2_Luong.Text, out luong);

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Mã NV và Họ Tên.");
                return;
            }

            try
            {
                if (currMode == "ADD")
                {
                    string maCN = SessionData.MaCN;
                    if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                    serviceDAL.ThemNV(maNV, hoTen, ngSinh, ngLam, chucVu, luong, trangThai, maCN);
                    MessageBox.Show("Thêm nhân viên thành công!\nTài khoản mặc định: " + maNV + " / 123");
                }
                else if (currMode == "EDIT")
                {
                    serviceDAL.SuaNV(maNV, hoTen, ngSinh, ngLam, chucVu, luong, trangThai);
                    MessageBox.Show("Cập nhật thành công!");
                }

                LoadData();
                btn_QLCN2_Huy_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_QLCN2_Huy_Click(object sender, EventArgs e)
        {
            currMode = "";
            ClearInputs();
            SetInputState(false);
            SetButtonState(true);
        }

        private void dgv_QLCN2_DSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_QLCN2_DSNV.CurrentRow != null)
            {
                if (currMode == "")
                {
                    var item = dgv_QLCN2_DSNV.CurrentRow.DataBoundItem as Class_QLCN2_DSNV;
                    if (item != null)
                    {
                        cmb_QLCN2_MaNV.Text = item.MaNV;
                        txb_QLCN2_TenNV.Text = item.HoTen;
                        atp_QLCN2_NgaySinh.Value = item.NgaySinh;
                        dtp_QLCN2_NgayVaoLam.Value = item.NgayBDLV;
                        txb_QLCN2_ChucVu.Text = item.ChucVu;
                        txb_QLCN2_Luong.Text = item.Luong.ToString();
                        cmb_QLCN2_trangThai.Text = item.TrangThai;
                    }
                }
            }
        }

        private void ClearInputs()
        {
            cmb_QLCN2_MaNV.Text = "";
            txb_QLCN2_TenNV.Clear();
            txb_QLCN2_ChucVu.Clear();
            txb_QLCN2_Luong.Clear();
            cmb_QLCN2_trangThai.SelectedIndex = -1;
            atp_QLCN2_NgaySinh.Value = DateTime.Now;
            dtp_QLCN2_NgayVaoLam.Value = DateTime.Now;
        }

        private void SetInputState(bool enable)
        {
            txb_QLCN2_TenNV.ReadOnly = !enable;
            txb_QLCN2_ChucVu.ReadOnly = !enable;
            txb_QLCN2_Luong.ReadOnly = !enable;
            cmb_QLCN2_trangThai.Enabled = enable;
            atp_QLCN2_NgaySinh.Enabled = enable;
            dtp_QLCN2_NgayVaoLam.Enabled = enable;
        }

        private void SetButtonState(bool isNormalMode)
        {
            btn_QLCN2_Them.Visible = isNormalMode;
            btn_QLCN2_Sua.Visible = isNormalMode;
            btn_QLCN2_Xoa.Visible = isNormalMode;

            btn_QLCN2_XacNhan.Visible = !isNormalMode;
            btn_QLCN2_Huy.Visible = !isNormalMode;
        }

        private void btn_QLCN2_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }

        private void cmb_QLCN2_MaNV_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txb_QLCN2_TenNV_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCN2_ChucVu_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCN2_Luong_TextChanged(object sender, EventArgs e) { }
        private void atp_QLCN2_NgaySinh_ValueChanged(object sender, EventArgs e) { }
        private void dtp_QLCN2_NgayVaoLam_ValueChanged(object sender, EventArgs e) { }
        private void cmb_QLCN2_trangThai_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}