using PetCare.KhachHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.HoiVien.UC_Con
{
    public partial class UC_HV_ThongTinHoiVien : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_HV_ThongTinHoiVien()
        {
            InitializeComponent();
            // Tự động load dữ liệu khi UserControl được nạp
            this.Load += UC_HV_ThongTinHoiVien_Load;
        }

        private void UC_HV_ThongTinHoiVien_Load(object sender, EventArgs e)
        {
            LoadThongTinHoiVien();
        }

        private void LoadThongTinHoiVien()
        {
            try
            {
                DataTable dt = dal.GetThongTinHoiVien(SessionData.MaKH);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    // Gán dữ liệu vào các TextBox theo tên trong SP
                    textBox_HV_CapDo.Text = row["Cấp độ hiện tại"].ToString();
                    textBox_HV_DKDuyTri.Text = row["Điều kiện duy trì"].ToString();
                    textBox_HV_DKThangHang.Text = row["Điều kiện thăng hạng"].ToString();
                    textBox_HV_QuyenLoi.Text = row["Quyền lợi"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin hội viên: " + ex.Message);
            }
        }

        private void btn_HV_XemDiemLoyalty_Click(object sender, EventArgs e)
        {
            UC_HV_DiemLoyalty UC_Diem = new UC_HV_DiemLoyalty();

            // Đăng ký callback quay lại
            UC_Diem.QuayVeTrangChu += () =>
            {
                pnl_HV_Content.Controls.Remove(UC_Diem);
                pnl_HV_ThongTinHV.Visible = true;
                LoadThongTinHoiVien(); // Refresh dữ liệu
            };

            pnl_HV_ThongTinHV.Visible = false;
            pnl_HV_Content.Controls.Add(UC_Diem);
            UC_Diem.Anchor = AnchorStyles.None;
            UC_Diem.BringToFront();
        }

        private void btn_HV_ThongKeChiTieu_Click(object sender, EventArgs e)
        {
            UC_HV_ThongKeChiTieu UC_ThongKe = new UC_HV_ThongKeChiTieu();

            UC_ThongKe.QuayVeTrangChu += () =>
            {
                pnl_HV_Content.Controls.Remove(UC_ThongKe);
                pnl_HV_ThongTinHV.Visible = true;
            };

            pnl_HV_ThongTinHV.Visible = false;
            pnl_HV_Content.Controls.Add(UC_ThongKe);
            UC_ThongKe.Anchor = AnchorStyles.None;
            UC_ThongKe.BringToFront();
        }

        private void btn_HV_QuayLai_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                this.Hide();
                parentForm.Show();
                parentForm.BringToFront();
            }
        }
    }
}
