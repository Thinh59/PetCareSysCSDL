using PetCare.HoiVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang
{
    public partial class UC_KH_GoiTiem_ChonMuiTiemDau : UserControl, IReturnToMainPage
    {
        private string _maGoiDuocChon;
        private string _maThuCungDuocChon;
        ServiceDAL serviceDAL = new ServiceDAL();
        public UC_KH_GoiTiem_ChonMuiTiemDau(string maGoi, string maTC)
        {
            InitializeComponent();
            this._maGoiDuocChon = maGoi;
            this._maThuCungDuocChon = maTC;

            LoadComboboxData();
        }

        private void LoadComboboxData()
        {
            // 1. Load Chi nhánh
            comboBox_KH_ChonChiNhanh.DataSource = serviceDAL.GetDanhSachChiNhanh();
            comboBox_KH_ChonChiNhanh.DisplayMember = "TenCN";
            comboBox_KH_ChonChiNhanh.ValueMember = "MaCN";

            // 2. Load Vaccine thuộc Gói tiêm (Quan trọng!)
            DataTable dtVacxin = serviceDAL.GetVacXinTheoGoi(this._maGoiDuocChon);

            if (dtVacxin.Rows.Count > 0)
            {
                comboBox_KH_ChonVacxin.DataSource = dtVacxin;
                comboBox_KH_ChonVacxin.DisplayMember = "TenVacXin";
                comboBox_KH_ChonVacxin.ValueMember = "MaVacXin";
            }
            else
            {
                MessageBox.Show("Gói tiêm này hiện chưa có danh sách vaccine cụ thể.");
            }

            // Đặt ngày mặc định cho dateTimePicker
            dateTimePicker_KH_ThoiGian.MinDate = DateTime.Now;
            dateTimePicker_KH_ThoiGian.Format = DateTimePickerFormat.Custom;
            dateTimePicker_KH_ThoiGian.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void btn_KH_XemDSVacxin_Click(object sender, EventArgs e)
        {
            UC_KH_XemDSVacxin UC_XemDS = new UC_KH_XemDSVacxin();

            UC_XemDS.QuayVeTrangChu += () => {
                Control parentPanel = this.Parent;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Remove(UC_XemDS);
                    UC_XemDS.Dispose();
                }
                this.Visible = true;
            };
            if (this.Parent != null)
            {
                this.Visible = false;
                this.Parent.Controls.Add(UC_XemDS);

                UC_XemDS.Anchor = AnchorStyles.None;
                UC_XemDS.BringToFront();
            }
        }

        private void btn_KH_HoanTat_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra dữ liệu đầu vào
                if (comboBox_KH_ChonChiNhanh.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn chi nhánh thực hiện tiêm!", "Thông báo");
                    return;
                }

                if (comboBox_KH_ChonVacxin.SelectedValue == null)
                {
                    MessageBox.Show("Gói này không có vaccine hợp lệ để đặt lịch!", "Thông báo");
                    return;
                }

                // 2. Lấy thông tin từ giao diện
                string maCN = comboBox_KH_ChonChiNhanh.SelectedValue.ToString();
                string maVX = comboBox_KH_ChonVacxin.SelectedValue.ToString();
                DateTime ngayHen = dateTimePicker_KH_ThoiGian.Value;

                if (ngayHen < DateTime.Now.Date)
                {
                    MessageBox.Show("Ngày hẹn không thể là ngày trong quá khứ!", "Thông báo");
                    return;
                }

                // Gọi Stored Procedure từ lớp DAL
                bool result = serviceDAL.DatGoiTiemPhong(
                    SessionData.MaKH,
                    _maGoiDuocChon,
                    _maThuCungDuocChon,
                    maCN,
                    maVX,
                    ngayHen
                );

                if (result)
                {
                    MessageBox.Show("Đặt gói tiêm và lịch hẹn mũi đầu tiên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    QuayVeTrangChu?.Invoke();
                }
                else
                {
                    MessageBox.Show("Đặt gói tiêm thất bại. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_Huy_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
