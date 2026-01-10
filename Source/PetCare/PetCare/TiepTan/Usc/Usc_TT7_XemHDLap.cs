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
    public partial class Usc_TT7_XemHDLap : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();

        private BindingList<Class_TT7_DSHDLap> danhSachHienThi = new BindingList<Class_TT7_DSHDLap>();

        public Usc_TT7_XemHDLap()
        {
            InitializeComponent();

            dgv_TT7_DSHDLap.AutoGenerateColumns = true;
            dgv_TT7_DSHDLap.DataSource = danhSachHienThi;

            if (cmb_TT7_TraCuu.Items.Count == 0)
            {
                cmb_TT7_TraCuu.Items.Add("Tất cả");
                cmb_TT7_TraCuu.Items.Add("Mã HĐ");
                cmb_TT7_TraCuu.Items.Add("Mã KH");
                cmb_TT7_TraCuu.SelectedIndex = 0;
            }

            this.Load += Usc_TT7_XemHD_Load;

            if (string.IsNullOrEmpty(SessionData.MaNV))
            {
            }
        }

        private void Usc_TT7_XemHD_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionData.MaNV))
            {
                cmb_TT6_NVLap.Text = SessionData.MaNV;
            }
            else
            {
                cmb_TT6_NVLap.Text = "NV001"; 
            }

            cmb_TT6_NVLap.Enabled = false;
            cmb_TT6_NVLap.BackColor = SystemColors.Control;
        }

        private void btn_TT7_Tim_Click(object sender, EventArgs e)
        {
            string tuKhoa = cmb_TT7_NhapMa.Text.Trim();
            string maNVHienTai = SessionData.MaNV;

            if (string.IsNullOrEmpty(maNVHienTai))
            {
                MessageBox.Show("Phiên đăng nhập hết hạn. Vui lòng đăng nhập lại!", "Lỗi Session", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = serviceDAL.TimKiemHoaDon(tuKhoa, maNVHienTai);

                danhSachHienThi.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        danhSachHienThi.Add(new Class_TT7_DSHDLap
                        {
                            MaHD = row["MaHD"].ToString(),
                            MaKH = row["MaKH"].ToString(),
                            HoTen_KH = row["HoTen_KH"].ToString(),
                            NgayLap = row["NgayLap"] != DBNull.Value ? Convert.ToDateTime(row["NgayLap"]) : DateTime.MinValue,
                            TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0,
                            TrangThaiHD = row["TrangThaiHD"].ToString(),
                            TenNVLap = row["TenNVLap"].ToString()
                        });
                    }
                    dgv_TT7_DSHDLap.Refresh();
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy hóa đơn nào của nhân viên {SessionData.TenHienThi} với từ khóa này.", "Kết quả tìm kiếm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy xuất dữ liệu: " + ex.Message);
            }
        }
        private void btn_TT7_In_Click(object sender, EventArgs e)
        {
            if (dgv_TT7_DSHDLap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.");
                return;
            }

            var item = dgv_TT7_DSHDLap.CurrentRow.DataBoundItem as Class_TT7_DSHDLap;

            if (item != null)
            {
                MessageBox.Show($"Thông tin hóa đơn:\n- Mã HĐ: {item.MaHD}\n- Khách hàng: {item.HoTen_KH}\n- Tổng tiền: {item.TongTien:N0} VNĐ", "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btn_TT7_Huy_Click(object sender, EventArgs e)
        {
            cmb_TT7_NhapMa.Text = "";
            cmb_TT7_TraCuu.SelectedIndex = 0;
            danhSachHienThi.Clear();
            dgv_TT7_DSHDLap.Refresh();
        }
        private void btn_TT7_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }
        private void cmb_TT7_TraCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchType = cmb_TT7_TraCuu.SelectedItem?.ToString();
            string maNVHienTai = SessionData.MaNV;

            cmb_TT7_NhapMa.DataSource = null;
            cmb_TT7_NhapMa.Items.Clear();
            cmb_TT7_NhapMa.Text = "";

            try
            {
                if (searchType == "Mã HĐ")
                {
                    cmb_TT7_NhapMa.DataSource = serviceDAL.GetMyMaHD(maNVHienTai);
                    cmb_TT7_NhapMa.DisplayMember = "MaHD";
                    cmb_TT7_NhapMa.ValueMember = "MaHD";
                }
                else if (searchType == "Mã KH")
                {
                    cmb_TT7_NhapMa.DataSource = serviceDAL.GetMyCustomers(maNVHienTai);
                    cmb_TT7_NhapMa.DisplayMember = "MaKH";
                    cmb_TT7_NhapMa.ValueMember = "MaKH";
                }
                cmb_TT7_NhapMa.SelectedIndex = -1;
            }
            catch (Exception) 
            {
            }
        }

        private void lbl_TT1_NhapTTKH_Click(object sender, EventArgs e) { }
        private void cmb_TT7_NhapMa_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgv_TT7_DSHDLap_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void cmb_TT6_NVLap_SelectedIndexChanged(object sender, EventArgs e) { }
    }

}