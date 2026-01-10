using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace PetCare
{
    public partial class Usc_QLCN11_TKDV : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN11_DSLDGDD> danhSach = new BindingList<Class_QLCN11_DSLDGDD>();

        public Usc_QLCN11_TKDV()
        {
            InitializeComponent();

            dgv_QLCN11_TKDichVu.AutoGenerateColumns = true;
            dgv_QLCN11_TKDichVu.DataSource = danhSach;

            this.Load += Usc_QLCN11_TKDV_Load;
        }

        private void Usc_QLCN11_TKDV_Load(object sender, EventArgs e)
        {
            LoadCombos();
            LoadDSDichVu();

            DateTime now = DateTime.Now;
            cmb_QLCN11_Nam.Text = now.Year.ToString();
            cmb_QLCN11_Thang.Text = now.Month.ToString();

            btn_QLCN11_ThongKe_Click(sender, e);
        }

        private void LoadCombos()
        {
            // Năm
            int year = DateTime.Now.Year;
            cmb_QLCN11_Nam.Items.Add("Tất cả");
            for (int i = year; i >= 2020; i--) cmb_QLCN11_Nam.Items.Add(i);
            cmb_QLCN11_Nam.SelectedIndex = 1;

            // Quý
            cmb_QLCN11_Quy.Items.Add("Tất cả");
            for (int i = 1; i <= 4; i++) cmb_QLCN11_Quy.Items.Add(i);
            cmb_QLCN11_Quy.SelectedIndex = 0;

            // Tháng
            cmb_QLCN11_Thang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cmb_QLCN11_Thang.Items.Add(i);
            cmb_QLCN11_Thang.SelectedIndex = 0;

            // Ngày
            cmb_QLCN11_Ngay.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cmb_QLCN11_Ngay.Items.Add(i);
            cmb_QLCN11_Ngay.SelectedIndex = 0;
        }

        private void LoadDSDichVu()
        {
            try
            {
                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                DataTable dt = serviceDAL.GetDSDV(maCN);

                cmb_QLCN11_DichVu.Items.Clear();
                cmb_QLCN11_DichVu.Items.Add("Tất cả");

                foreach (DataRow row in dt.Rows)
                {
                    cmb_QLCN11_DichVu.Items.Add($"{row["MaDichVu"]} - {row["TenDV"]}");
                }
                cmb_QLCN11_DichVu.SelectedIndex = 0;
            }
            catch { }
        }

        private void btn_QLCN11_ThongKe_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            string maCN = SessionData.MaCN;
            if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

            int nam = 0;
            if (cmb_QLCN11_Nam.SelectedItem != null && cmb_QLCN11_Nam.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN11_Nam.SelectedItem.ToString(), out nam);

            int quy = 0, thang = 0, ngay = 0;
            if (cmb_QLCN11_Quy.SelectedIndex > 0) int.TryParse(cmb_QLCN11_Quy.SelectedItem.ToString(), out quy);
            if (cmb_QLCN11_Thang.SelectedIndex > 0) int.TryParse(cmb_QLCN11_Thang.SelectedItem.ToString(), out thang);
            if (cmb_QLCN11_Ngay.SelectedIndex > 0) int.TryParse(cmb_QLCN11_Ngay.SelectedItem.ToString(), out ngay);

            string selectedDV = cmb_QLCN11_DichVu.SelectedItem?.ToString();
            string maDV = "Tất cả";
            if (!string.IsNullOrEmpty(selectedDV) && selectedDV != "Tất cả")
            {
                maDV = selectedDV.Split('-')[0].Trim();
            }

            try
            {
                DataTable dt = serviceDAL.GetThongKeLSDV(maCN, nam, quy, thang, ngay, maDV);

                danhSach.Clear();
                long tongDoanhThu = 0;

                foreach (DataRow row in dt.Rows)
                {
                    int thanhTien = Convert.ToInt32(row["ThanhTien"]);
                    tongDoanhThu += thanhTien;

                    danhSach.Add(new Class_QLCN11_DSLDGDD
                    {
                        MaLSDV = row["MaLSDV"].ToString(),
                        MaDV = row["MaDV"].ToString(),
                        TenDV = row["TenDV"].ToString(),
                        NgayDat = Convert.ToDateTime(row["NgayDat"]),
                        NgaySD = Convert.ToDateTime(row["NgaySD"]),
                        ThanhTien = thanhTien,
                        TrangThai = row["TrangThai"].ToString()
                    });
                }

                txtBox_QLCN11_TongSoGD.Text = danhSach.Count.ToString();

                double diemTB = serviceDAL.GetDiemDanhGiaTB(maDV);
                txb_QLCN11_DiemTB.Text = diemTB.ToString("0.0") + " / 5.0";

                if (dgv_QLCN11_TKDichVu.Columns["ThanhTien"] != null)
                {
                    dgv_QLCN11_TKDichVu.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                }
                timer.Stop();
                long thoiGianChay = timer.ElapsedMilliseconds;

                txb_QLCN11_Time.Text = thoiGianChay.ToString() + " ms";


                if (thoiGianChay < 100)
                {
                    txb_QLCN11_Time.ForeColor = Color.Green;
                    txb_QLCN11_Time.BackColor = Color.FromArgb(220, 255, 220); // Xanh nhạt
                }
                else
                {
                    txb_QLCN11_Time.ForeColor = Color.Red;
                    txb_QLCN11_Time.BackColor = Color.FromArgb(255, 220, 220); // Đỏ nhạt
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_QLCN11_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }

        private void cmb_QLCN11_Nam_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN11_Quy_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN11_Thang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN11_Ngay_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN11_LocTheo_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN11_DichVu_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtBox_QLCN11_TongSoGD_TextChanged(object sender, EventArgs e) { }
        private void txb_QLCN11_DiemTB_TextChanged(object sender, EventArgs e) { }
        private void dgv_QLCN11_TKDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void txb_QLCN11_Time_TextChanged(object sender, EventArgs e)
        {

        }
    }
}