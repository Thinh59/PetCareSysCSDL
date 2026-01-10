using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN3_HSNV : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN2_DSNV> danhSachNV = new BindingList<Class_QLCN2_DSNV>();

        public Usc_QLCN3_HSNV()
        {
            InitializeComponent();

            dgv_QLCN3_DSNV.AutoGenerateColumns = true;
            dgv_QLCN3_DSNV.DataSource = danhSachNV;

            this.Load += Usc_QLCN3_HSNV_Load;

            dgv_QLCN3_DSNV.CellClick += dgv_QLCN3_DSNV_CellContentClick;
        }

        private void Usc_QLCN3_HSNV_Load(object sender, EventArgs e)
        {

            LoadTimeComboBoxes();

            LoadDanhSachNV();
        }

        private void LoadTimeComboBoxes()
        {
            // Năm
            cmb_QLCN3_Nam.Items.Clear();
            cmb_QLCN3_Nam.Items.Add("Tất cả"); 

            int year = DateTime.Now.Year;
            for (int i = year; i >= year - 5; i--)
            {
                cmb_QLCN3_Nam.Items.Add(i);
            }
            cmb_QLCN3_Nam.SelectedIndex = 0;

            // Quý
            cmb_QLCN3_Quy.Items.Add("Tất cả");
            for (int i = 1; i <= 4; i++) cmb_QLCN3_Quy.Items.Add(i);
            cmb_QLCN3_Quy.SelectedIndex = 0;

            // Tháng
            cmb_QLCN3_Thang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cmb_QLCN3_Thang.Items.Add(i);
            cmb_QLCN3_Thang.SelectedIndex = 0;

            // Ngày
            cmb_QLCN3_Ngay.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cmb_QLCN3_Ngay.Items.Add(i);
            cmb_QLCN3_Ngay.SelectedIndex = 0;
        }

        private void LoadDanhSachNV()
        {
            try
            {
                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                DataTable dt = serviceDAL.GetDSNV(maCN);

                danhSachNV.Clear();
                cmb_QLCN3_MaNV.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    var nv = new Class_QLCN2_DSNV
                    {
                        MaNV = row["MaNV"].ToString(),
                        HoTen = row["HoTenNV"].ToString(),
                        ChucVu = row["ChucVu"].ToString(),
                        TrangThai = row["TrangThaiNV"].ToString(),

                        NgaySinh = row["NgaySinhNV"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["NgaySinhNV"]),
                        NgayBDLV = row["NgayVaoLam"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["NgayVaoLam"]),
                        Luong = row["Luong"] == DBNull.Value ? 0 : Convert.ToInt32(row["Luong"])
                    };

                    danhSachNV.Add(nv);
                    cmb_QLCN3_MaNV.Items.Add(nv.MaNV);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách NV: " + ex.Message);
            }
        }

        private void dgv_QLCN3_DSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_QLCN3_DSNV.Rows[e.RowIndex];

                cmb_QLCN3_MaNV.Text = row.Cells["MaNV"].Value?.ToString();
                txb_QLCN3_TenNV.Text = row.Cells["HoTen"].Value?.ToString();
                txb_QLCN3_ChucVu.Text = row.Cells["ChucVu"].Value?.ToString();
            }
        }


        private void cmb_QLCN3_MaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMa = cmb_QLCN3_MaNV.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedMa))
            {
                var nv = danhSachNV.FirstOrDefault(x => x.MaNV == selectedMa);
                if (nv != null)
                {
                    txb_QLCN3_TenNV.Text = nv.HoTen;
                    txb_QLCN3_ChucVu.Text = nv.ChucVu;
                }
            }
        }

        private void btn_TraCuu_Click(object sender, EventArgs e)
        {
            string maNV = cmb_QLCN3_MaNV.Text;
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xem.");
                return;
            }

            int nam = 0, quy = 0, thang = 0, ngay = 0;

            if (cmb_QLCN3_Nam.SelectedIndex > 0 && cmb_QLCN3_Nam.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN3_Nam.SelectedItem.ToString(), out nam);

            if (cmb_QLCN3_Quy.SelectedIndex > 0 && cmb_QLCN3_Quy.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN3_Quy.SelectedItem.ToString(), out quy);

            if (cmb_QLCN3_Thang.SelectedIndex > 0 && cmb_QLCN3_Thang.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN3_Thang.SelectedItem.ToString(), out thang);

            if (cmb_QLCN3_Ngay.SelectedIndex > 0 && cmb_QLCN3_Ngay.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN3_Ngay.SelectedItem.ToString(), out ngay);

            try
            {
                int soLuong = serviceDAL.GetHieuSuatNV(maNV, nam, quy, thang, ngay);

                txb_QLCN3_SoGD.Text = soLuong.ToString();


                string chucVu = txb_QLCN3_ChucVu.Text;
                if (chucVu.Contains("Tiếp tân") || chucVu.Contains("Thu ngân"))
                    MessageBox.Show($"Nhân viên này đã lập {soLuong} hóa đơn trong khoảng thời gian đã chọn.");
                else if (chucVu.Contains("Bác sĩ"))
                    MessageBox.Show($"Bác sĩ này đã thực hiện {soLuong} ca bệnh (Khám + Tiêm) trong khoảng thời gian đã chọn.");
                else
                    MessageBox.Show($"Tổng số lượng công việc ghi nhận: {soLuong}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_QLCN2_Them_Click(object sender, EventArgs e)
        {
            btn_TraCuu_Click(sender, e);
        }

        private void btn_QLCN3_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);


                this.Dispose();
            }
        }
        private void txb_QLCN3_ChucVu_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCN3_TenNV_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCN3_SoGD_TextChanged(object sender, EventArgs e) { }
        private void cmb_QLCN3_Nam_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN3_Quy_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN3_Thang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN3_Ngay_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
