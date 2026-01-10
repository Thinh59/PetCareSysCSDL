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
    public partial class Usc_TT9_DatLichTaiQuay : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public Usc_TT9_DatLichTaiQuay()
        {
            InitializeComponent();
            this.Load += Usc_TT9_DatLichTaiQuay_Load;
        }

        private void Usc_TT9_DatLichTaiQuay_Load(object sender, EventArgs e)
        {
            LoadInitData();
            ResetForm();
        }
        private void LoadInitData()
        {
            try
            {
                DataTable dtKH = dal.GetAllKhachHang();
                dtKH.Columns.Add("Display", typeof(string), "HoTen_KH + ' - ' + SDT_KH");

                cmb_TT9_ChonKhachHang.DataSource = dtKH;
                cmb_TT9_ChonKhachHang.DisplayMember = "Display";
                cmb_TT9_ChonKhachHang.ValueMember = "MaKH";
                cmb_TT9_ChonKhachHang.SelectedIndex = -1;

                cmb_TT9_ChonDichVu.DataSource = dal.GetDanhSachDichVu();
                cmb_TT9_ChonDichVu.DisplayMember = "TenDV";
                cmb_TT9_ChonDichVu.ValueMember = "TenDV";
                cmb_TT9_ChonDichVu.SelectedIndex = -1;
                cmb_TT9_ChonVacxin.DataSource = dal.GetDanhSachVacXin();
                cmb_TT9_ChonVacxin.DisplayMember = "TenVacXin";
                cmb_TT9_ChonVacxin.ValueMember = "MaVacXin";


                string currentMaCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(currentMaCN)) currentMaCN = "CN01"; 
                cmb_TT9_ChonChiNhanh.DataSource = null;
                cmb_TT9_ChonChiNhanh.Items.Clear();
                cmb_TT9_ChonChiNhanh.Items.Add(currentMaCN); 
                cmb_TT9_ChonChiNhanh.SelectedIndex = 0;
                cmb_TT9_ChonChiNhanh.Enabled = false; 

                LoadBacSi(currentMaCN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadBacSi(string maCN)
        {
            DataTable dtBS = dal.GetDSBacSiRanh(maCN);

            DataRow row = dtBS.NewRow();
            row["MaNV"] = "";
            row["HoTenNV"] = "-- Chọn tự động / Chỉ định sau --";
            dtBS.Rows.InsertAt(row, 0);

            cmb_TT9_ChonBacSi.DataSource = dtBS;
            cmb_TT9_ChonBacSi.DisplayMember = "HoTenNV";
            cmb_TT9_ChonBacSi.ValueMember = "MaNV";
            cmb_TT9_ChonBacSi.SelectedIndex = 0;
        }

        private void cmb_TT9_ChonKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_TT9_ChonKhachHang.SelectedIndex == -1 || cmb_TT9_ChonKhachHang.SelectedValue == null)
            {
                cmb_TT9_ChonThuCung.DataSource = null;
                return;
            }

            try
            {
                string maKH = cmb_TT9_ChonKhachHang.SelectedValue.ToString();
                if (maKH.Contains("DataRowView")) return;

                cmb_TT9_ChonThuCung.DataSource = null;
                cmb_TT9_ChonThuCung.Items.Clear();

                DataTable dt = dal.GetDSTC(maKH);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmb_TT9_ChonThuCung.DisplayMember = "TenThuCung";
                    cmb_TT9_ChonThuCung.ValueMember = "MaThuCung";
                    cmb_TT9_ChonThuCung.DataSource = dt;
                    cmb_TT9_ChonThuCung.SelectedIndex = 0;
                }
            }
            catch (Exception) {  }
        }

        private void cmb_TT9_ChonDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDV = cmb_TT9_ChonDichVu.Text;
            if (tenDV == "Tiêm phòng")
            {
                cmb_TT9_ChonVacxin.Visible = true;
                btn_TT9_XemDSVacxin.Visible = true;
            }
            else
            {
                cmb_TT9_ChonVacxin.Visible = false;
                btn_TT9_XemDSVacxin.Visible = false;
            }
        }

        private void cmb_TT9_ChonChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_TT9_ChonKhachHang.SelectedIndex == -1 || cmb_TT9_ChonKhachHang.SelectedValue == null)
            {
                cmb_TT9_ChonThuCung.DataSource = null;
                return;
            }

            try
            {
                string maKH = cmb_TT9_ChonKhachHang.SelectedValue.ToString();
                if (maKH.Contains("DataRowView")) return;

                cmb_TT9_ChonThuCung.DataSource = null;
                cmb_TT9_ChonThuCung.Items.Clear();

                DataTable dt = dal.GetDSTC(maKH);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmb_TT9_ChonThuCung.DisplayMember = "TenThuCung";
                    cmb_TT9_ChonThuCung.ValueMember = "MaThuCung";
                    cmb_TT9_ChonThuCung.DataSource = dt;

                    cmb_TT9_ChonThuCung.SelectedIndex = 0;
                }
            }
            catch (Exception) {  }
        }

        private void btn_TT9_HoanTat_Click(object sender, EventArgs e)
        {
            if (cmb_TT9_ChonKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }

            if (cmb_TT9_ChonThuCung.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn thú cưng!");
                return;
            }


            if (cmb_TT9_ChonDichVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!");
                return;
            }

            try
            {
                string maKH = cmb_TT9_ChonKhachHang.SelectedValue?.ToString();

                string maTC = cmb_TT9_ChonThuCung.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(maTC))
                {
                    MessageBox.Show("Lỗi kỹ thuật: Không lấy được Mã Thú Cưng dù đã chọn.\nNguyên nhân: Tên cột trong 'ValueMember' không khớp với SQL.\nVui lòng kiểm tra lại code DAL.", "Lỗi Dev", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tenDV = cmb_TT9_ChonDichVu.Text;

                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01"; 

                string maBS = cmb_TT9_ChonBacSi.SelectedValue?.ToString(); 

                DateTime thoiGian = DateTime.Now; 

                string maVX = null;
                if (tenDV == "Tiêm phòng")
                {
                    if (cmb_TT9_ChonVacxin.SelectedIndex == -1)
                    {
                        MessageBox.Show("Vui lòng chọn loại vắc xin!");
                        return;
                    }
                    maVX = cmb_TT9_ChonVacxin.SelectedValue?.ToString();
                }

                string maPhieu = dal.DatLichTaiQuay(maKH, maCN, tenDV, maTC, thoiGian, maBS, maVX);

                MessageBox.Show($"Tạo phiếu thành công!\nMã phiếu: {maPhieu}\n\nTrạng thái: Chờ thực hiện.",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo phiếu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_TT9_Huy_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            cmb_TT9_ChonKhachHang.SelectedIndex = -1;
            cmb_TT9_ChonThuCung.DataSource = null;
            cmb_TT9_ChonDichVu.SelectedIndex = -1;
            cmb_TT9_ChonVacxin.SelectedIndex = -1;

            if (cmb_TT9_ChonBacSi.Items.Count > 0) cmb_TT9_ChonBacSi.SelectedIndex = 0;
        }

        private void btn_TT9_XemDSVacxin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xem chi tiết vắc xin đang phát triển.", "Thông báo");
        }

        private void btn_TT9_XacNhanDichVu_Click(object sender, EventArgs e)
        {
            string maCN = SessionData.MaCN;
            if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

            string tenDV = cmb_TT9_ChonDichVu.Text;

            if (dal.KiemTraDichVuTaiChiNhanh(maCN, tenDV))
            {
                MessageBox.Show("Dịch vụ khả dụng! Mời nhập tiếp thông tin thú cưng.");
            }
            else
            {
                MessageBox.Show("Chi nhánh này hiện không cung cấp dịch vụ này!");
            }
        }
        private void pnl_KH_DatDichVu_Paint(object sender, PaintEventArgs e) { }
        private void cmb_TT9_ChonThuCung_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_TT9_ChonVacxin_SelectedIndexChanged(object sender, EventArgs e) { }

        private void cmb_TT9_ChonChiNhanh_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lb_KH_ChonVacxin_Click(object sender, EventArgs e)
        {

        }

        private void lb_KH_LoiNhac2_Click(object sender, EventArgs e)
        {

        }

        private void dtp_TT9_ThoiGian_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lb_KH_ThoiGian_Click(object sender, EventArgs e)
        {

        }

        private void lb_KH_DichVu_Click(object sender, EventArgs e)
        {

        }

        private void lb_KH_ChonThuCung_Click(object sender, EventArgs e)
        {

        }

        private void lb_KH_DVvaTG_Click(object sender, EventArgs e)
        {

        }

        private void lb_KH_ChonChiNhanh_Click(object sender, EventArgs e)
        {

        }

        private void pic_icon_KH_DatDichVu_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lb_KH_DatDichVu_Click(object sender, EventArgs e)
        {

        }

        private void cmb_TT9_ChonBacSi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnl_KH_Content_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}