using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT5_DKGoiTiemKH : UserControl
    {
        private ServiceDAL serviceDAL = new ServiceDAL();

        private DataTable dtKhachHang;
        private DataTable dtGoiTiem;

        public Usc_TT5_DKGoiTiemKH()
        {
            InitializeComponent();
            this.Load += Usc_TT5_DKGoiTiemKH_Load; 
        }

        private void Usc_TT5_DKGoiTiemKH_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxes();
            ResetForm();
        }

        private void LoadDataToComboBoxes()
        {
            try
            {
                dtKhachHang = serviceDAL.GetAllCustomersForService();

                cmb_TT5_MaKH.DataSource = dtKhachHang;
                cmb_TT5_MaKH.DisplayMember = "MaKH";
                cmb_TT5_MaKH.ValueMember = "MaKH";
                cmb_TT5_MaKH.SelectedIndex = -1;

                dtGoiTiem = serviceDAL.GetPackages();

                cmb_TT5_ChonGoi.DataSource = dtGoiTiem;
                cmb_TT5_ChonGoi.DisplayMember = "TenGoi";
                cmb_TT5_ChonGoi.ValueMember = "MaGoiTiem";
                cmb_TT5_ChonGoi.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb_TT5_MaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_TT5_MaKH.SelectedIndex != -1 && dtKhachHang != null)
            {
                DataRowView row = cmb_TT5_MaKH.SelectedItem as DataRowView;
                if (row != null)
                {
                    txtBox_TT5_HoTen.Text = row["HoTen_KH"].ToString();
                    txtBox_TT5_SDT.Text = row["SDT_KH"].ToString();
                }
            }
            else
            {
                txtBox_TT5_HoTen.Clear();
                txtBox_TT5_SDT.Clear();
            }
        }
        private void cmb_TT5_ChonGoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_TT5_ChonGoi.SelectedIndex != -1 && dtGoiTiem != null)
            {
                DataRowView row = cmb_TT5_ChonGoi.SelectedItem as DataRowView;
                if (row != null)
                {
                    int soThang = Convert.ToInt32(row["SoThang"]);
                    txb_TT5_SoThang.Text = soThang.ToString();

                    UpdateNgayHetHan();
                }
            }
            else
            {
                txb_TT5_SoThang.Clear();
                dtp_TT5_NgayHetHan.Value = dtp_TT5_NgayDK.Value;
            }
        }

        private void dtp_TT5_NgayDK_ValueChanged(object sender, EventArgs e)
        {
            UpdateNgayHetHan();
        }

        private void UpdateNgayHetHan()
        {
            if (int.TryParse(txb_TT5_SoThang.Text, out int soThang))
            {
                DateTime ngayDK = dtp_TT5_NgayDK.Value;
                DateTime ngayHetHan = ngayDK.AddMonths(soThang);
                dtp_TT5_NgayHetHan.Value = ngayHetHan;
            }
        }

        private void btn_TT5_DangKy_Click(object sender, EventArgs e)
        {
            if (cmb_TT5_MaKH.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmb_TT5_ChonGoi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Gói tiêm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKH = cmb_TT5_MaKH.SelectedValue.ToString();
            string maGoi = cmb_TT5_ChonGoi.SelectedValue.ToString();
            DateTime ngayDK = dtp_TT5_NgayDK.Value;

            try
            {
                serviceDAL.RegisterPackage(maGoi, maKH, ngayDK);
                MessageBox.Show("Đăng ký gói tiêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmb_TT5_ChonGoi.SelectedIndex = -1;
                txb_TT5_SoThang.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng ký thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_TT5_Huy_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            cmb_TT5_MaKH.SelectedIndex = -1;
            txtBox_TT5_HoTen.Clear();
            txtBox_TT5_SDT.Clear();

            cmb_TT5_ChonGoi.SelectedIndex = -1;
            txb_TT5_SoThang.Clear();

            dtp_TT5_NgayDK.Value = DateTime.Now;
            dtp_TT5_NgayHetHan.Value = DateTime.Now;
        }

        private void btn_TT5_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

               this.Parent.Controls.Remove(this);

               this.Dispose();
            }
        }

        private void txtBox_TT5_SDT_TextChanged(object sender, EventArgs e) { }
        private void txtBox_TT5_HoTen_TextChanged(object sender, EventArgs e) { }
        private void txb_TT5_SoThang_TextChanged(object sender, EventArgs e) { }
        private void dtp_TT5_NgayHetHan_ValueChanged(object sender, EventArgs e) { }
    }
}