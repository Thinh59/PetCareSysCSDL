using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT2_DKHVKH : UserControl
    {
        private bool isLoading = false;

        public Usc_TT2_DKHVKH()
        {
            InitializeComponent();
            LoadCustomerComboBox();
        }

        private void LoadCustomerComboBox()
        {
            ServiceDAL dal = new ServiceDAL();
            try
            {
                isLoading = true; 

                DataTable dt = dal.GetAvailableCustomers();

                DataRow emptyRow = dt.NewRow();
                emptyRow["MaKH"] = "";
                emptyRow["HoTen_KH"] = "-- Chọn khách hàng --";
                dt.Rows.InsertAt(emptyRow, 0);

                cmb_TT2_MaKH.DataSource = dt;


                cmb_TT2_MaKH.DisplayMember = "MaKH";

                cmb_TT2_MaKH.ValueMember = "MaKH";

                cmb_TT2_MaKH.SelectedIndex = 0;

                txtBox_TT2_HoTen.Clear();
                txtBox_TT2_SDT.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách khách hàng: " + ex.Message,
                                "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false; 
            }
        }

        private void cmb_TT2_MaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cmb_TT2_MaKH.SelectedValue == null ||
                string.IsNullOrEmpty(cmb_TT2_MaKH.SelectedValue.ToString()))
            {
                txtBox_TT2_HoTen.Clear();
                txtBox_TT2_SDT.Clear();
                return;
            }

            string maKH = cmb_TT2_MaKH.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(maKH))
            {
                txtBox_TT2_HoTen.Clear();
                txtBox_TT2_SDT.Clear();
                return;
            }

            ServiceDAL dal = new ServiceDAL();

            try
            {
                DataTable dtCustomer = dal.GetCustomerInfo(maKH);

                if (dtCustomer.Rows.Count > 0)
                {
                    DataRow row = dtCustomer.Rows[0];

                    txtBox_TT2_HoTen.Text = row["HoTen_KH"].ToString();
                    txtBox_TT2_SDT.Text = row["SDT_KH"].ToString();
                }
                else
                {
                    txtBox_TT2_HoTen.Clear();
                    txtBox_TT2_SDT.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi truy vấn thông tin KH",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_TT2_DangKy_Click(object sender, EventArgs e)
        {
            if (cmb_TT2_MaKH.SelectedValue == null ||
                string.IsNullOrWhiteSpace(cmb_TT2_MaKH.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần đăng ký Hội viên.",
                                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKH = cmb_TT2_MaKH.SelectedValue.ToString();
            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn đăng ký Hội viên cho khách hàng:\n{txtBox_TT2_HoTen.Text} ({maKH})?",
                "Xác nhận đăng ký",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            ServiceDAL dal = new ServiceDAL();
            try
            {
                string maKHThanhCong = dal.DangKyHoiVien(maKH);

                MessageBox.Show($"Đăng ký Hội viên thành công!\n\nMã KH: {maKHThanhCong}\nHọ tên: {txtBox_TT2_HoTen.Text}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCustomerComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng ký Hội viên:\n{ex.Message}",
                                "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_TT2_Huy_Click(object sender, EventArgs e)
        {
            LoadCustomerComboBox();
        }

        private void lbl_TT1_GioiTinh_Click(object sender, EventArgs e) { }
        private void pic_TT1_BG_Click(object sender, EventArgs e) { }
        private void lbl_TT1_NhapTTKH_Click(object sender, EventArgs e) { }
        private void txtBox_TT2_HoTen_TextChanged(object sender, EventArgs e) { }
        private void txtBox_TT2_SDT_TextChanged(object sender, EventArgs e) { }
        private void lbl_TT2_GTLoaiTK_Click(object sender, EventArgs e) { }

        private void btn_TT2_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }
    }
}