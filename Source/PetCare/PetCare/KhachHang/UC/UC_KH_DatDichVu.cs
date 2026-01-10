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
    public partial class UC_KH_DatDichVu : UserControl
    {
        public event Action QuayVeTrangChu;
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_DatDichVu()
        {
            InitializeComponent();
            LoadAllComboBoxes();
            // Mặc định ẩn phần chọn vắc xin, chỉ hiện khi chọn dịch vụ Tiêm phòng
            lb_KH_ChonVacxin.Visible = false;
            comboBox_KH_ChonVacxin.Visible = false;
            btn_KH_XemDSVacxin.Visible = false;
        }

        private void LoadAllComboBoxes()
        {
            // 1. Load Dịch vụ
            DataTable dtDV = dal.GetDanhSachDichVu();
            comboBox_KH_ChonDichVu.DataSource = dtDV;
            comboBox_KH_ChonDichVu.DisplayMember = "TenDV";
            comboBox_KH_ChonDichVu.ValueMember = "TenDV";

            // 2. Load Chi nhánh
            DataTable dtCN = dal.GetDanhSachChiNhanh();
            comboBox_KH_ChonChiNhanh.DataSource = dtCN;
            comboBox_KH_ChonChiNhanh.DisplayMember = "TenCN";
            comboBox_KH_ChonChiNhanh.ValueMember = "MaCN";  

            // 3. Load Thú cưng (theo MaKH từ Session)
            comboBox_KH_ChonThuCung.DataSource = dal.GetDSTC(SessionData.MaKH);
            comboBox_KH_ChonThuCung.DisplayMember = "TenThuCung";
            comboBox_KH_ChonThuCung.ValueMember = "MaThuCung";

            // 4. Load Vắc xin
            comboBox_KH_ChonVacxin.DataSource = dal.GetDanhSachVacXin();
            comboBox_KH_ChonVacxin.DisplayMember = "TenVacXin";
            comboBox_KH_ChonVacxin.ValueMember = "MaVacXin";
        }
        private void btn_XacNhanDichVu_Click(object sender, EventArgs e)
        {
            if (comboBox_KH_ChonChiNhanh.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh trước!");
                return;
            }

            string tenDV = comboBox_KH_ChonDichVu.Text;
            string maCN = comboBox_KH_ChonChiNhanh.SelectedValue.ToString();

            // Gọi hàm DAL để thực thi Store SP_KH_KiemTraCungCap
            if (dal.KiemTraDichVuTaiChiNhanh(maCN, tenDV))
            {
                // --- HIỂN THỊ NGAY LẬP TỨC ---
                if (tenDV == "Tiêm phòng")
                {
                    lb_KH_ChonVacxin.Visible = true;
                    comboBox_KH_ChonVacxin.Visible = true;
                    btn_KH_XemDSVacxin.Visible = true;
                }
                else
                {
                    lb_KH_ChonVacxin.Visible = false;
                    comboBox_KH_ChonVacxin.Visible = false;
                    btn_KH_XemDSVacxin.Visible = false;
                }

                // Mở khóa các bước tiếp theo cho người dùng nhập
                lb_KH_ThoiGian.Visible = true;
                dateTimePicker_KH_ThoiGian.Visible = true;
                lb_KH_ChonThuCung.Visible = true;
                comboBox_KH_ChonThuCung.Visible = true;
                lb_KH_LoiNhac2.Visible = true;
                btn_KH_HoanTat.Enabled = true;
            }
            else
            {
                MessageBox.Show("Chi nhánh này không cung cấp dịch vụ này hoặc đang tạm ngưng!");
                // Ẩn/Khóa các nút nhập liệu để tránh người dùng bấm nhầm
                lb_KH_ChonVacxin.Visible = false;
                comboBox_KH_ChonVacxin.Visible = false;
                btn_KH_HoanTat.Enabled = false;
            }
        }

        // Nút Hoàn tất đặt lịch
        private void btn_KH_HoanTat_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_KH_ChonThuCung.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn thú cưng!");
                    return;
                }

                string maKH = SessionData.MaKH;
                string tenDV = comboBox_KH_ChonDichVu.Text;
                string maCN = comboBox_KH_ChonChiNhanh.SelectedValue?.ToString();
                string maTC = comboBox_KH_ChonThuCung.SelectedValue.ToString();
                DateTime thoiGian = dateTimePicker_KH_ThoiGian.Value.Date;

                string maVX = null;
                if (tenDV == "Tiêm phòng")
                {
                    if (comboBox_KH_ChonVacxin.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn loại vắc xin!");
                        return;
                    }
                    maVX = comboBox_KH_ChonVacxin.SelectedValue.ToString();
                }

                string maDonMoi = dal.DatDichVu(maKH, maCN, tenDV, maTC, thoiGian, maVX);

                MessageBox.Show($"Đặt lịch {tenDV} thành công! Mã đơn: {maDonMoi}", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_KH_XemDSVacxin_Click(object sender, EventArgs e)
        {
            UC_KH_XemDSVacxin UC_XemDS = new UC_KH_XemDSVacxin();

            UC_XemDS.QuayVeTrangChu += () => {
                pnl_KH_Content.Controls.Remove(UC_XemDS);
                UC_XemDS.Dispose();
                pnl_KH_DatDichVu.Visible = true;
            };

            pnl_KH_DatDichVu.Visible = false;
            pnl_KH_Content.Controls.Add(UC_XemDS);

            UC_XemDS.Anchor = AnchorStyles.None;
            UC_XemDS.BringToFront();
        }

        private void btn_KH_Huy_Click(object sender, EventArgs e)
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
