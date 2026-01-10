using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace PetCare
{
    public partial class Usc_QLCN10_DSHoaDon : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN10_DSHD> danhSach = new BindingList<Class_QLCN10_DSHD>();

        public Usc_QLCN10_DSHoaDon()
        {
            InitializeComponent();

            dgv_QLCN10_DSHD.AutoGenerateColumns = true;
            dgv_QLCN10_DSHD.DataSource = danhSach;

            this.Load += Usc_QLCN10_DSHoaDon_Load;
            dgv_QLCN10_DSHD.CellClick += dgv_QLCN10_DSHD_CellContentClick;
        }

        private void Usc_QLCN10_DSHoaDon_Load(object sender, EventArgs e)
        {
            LoadCombos();
            DateTime now = DateTime.Now;
            cmb_QLCN10_Nam.Text = now.Year.ToString();
            cmb_QLCN10_Thang.Text = now.Month.ToString();

            LoadData();
        }

        private void LoadCombos()
        {
            // Năm
            int year = DateTime.Now.Year;
            cmb_QLCN10_Nam.Items.Add("Tất cả");
            for (int i = year; i >= 2020; i--) cmb_QLCN10_Nam.Items.Add(i);
            cmb_QLCN10_Nam.SelectedIndex = 1;

            // Quý
            cmb_QLCN10_Quy.Items.Add("Tất cả");
            for (int i = 1; i <= 4; i++) cmb_QLCN10_Quy.Items.Add(i);
            cmb_QLCN10_Quy.SelectedIndex = 0;

            // Tháng
            cmb_QLCN10_Thang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cmb_QLCN10_Thang.Items.Add(i);
            cmb_QLCN10_Thang.SelectedIndex = 0;

            // Ngày
            cmb_QLCN10_Ngay.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cmb_QLCN10_Ngay.Items.Add(i);
            cmb_QLCN10_Ngay.SelectedIndex = 0;

        }

        private void LoadData()
        {
            string maCN = SessionData.MaCN;
            if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

            int nam = 0;
            if (cmb_QLCN10_Nam.SelectedItem != null && cmb_QLCN10_Nam.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN10_Nam.SelectedItem.ToString(), out nam);

            int quy = 0, thang = 0, ngay = 0;
            if (cmb_QLCN10_Quy.SelectedIndex > 0) int.TryParse(cmb_QLCN10_Quy.SelectedItem.ToString(), out quy);
            if (cmb_QLCN10_Thang.SelectedIndex > 0) int.TryParse(cmb_QLCN10_Thang.SelectedItem.ToString(), out thang);
            if (cmb_QLCN10_Ngay.SelectedIndex > 0) int.TryParse(cmb_QLCN10_Ngay.SelectedItem.ToString(), out ngay);

            string maHD = "";
            if (!string.IsNullOrEmpty(cmb_QLCN10_MaHD.Text) && cmb_QLCN10_MaHD.Text != "Tất cả")
            {
                maHD = cmb_QLCN10_MaHD.Text;
            }

            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                DataTable dt = serviceDAL.GetDSHoaDon(maCN, nam, quy, thang, ngay, maHD);

                timer.Stop();
                long executionTime = timer.ElapsedMilliseconds;

                if (txb_QLCN10_Time != null)
                {
                    txb_QLCN10_Time.Text = executionTime.ToString() + " ms";

                    if (executionTime < 200)
                    {
                        txb_QLCN10_Time.ForeColor = Color.Green; 
                        txb_QLCN10_Time.BackColor = Color.FromArgb(220, 255, 220);
                    }
                    else
                    {
                        txb_QLCN10_Time.ForeColor = Color.Red;  
                        txb_QLCN10_Time.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }

                danhSach.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    danhSach.Add(new Class_QLCN10_DSHD
                    {
                        MaHD = row["MaHD"].ToString(),
                        MaKH = row["MaKH"].ToString(),
                        TenKH = row["TenKH"].ToString(),
                        NgayLap = Convert.ToDateTime(row["NgayLap"]),
                        TongTien = Convert.ToInt32(row["TongTien"])
                    });
                }

                if (dgv_QLCN10_DSHD.Columns["TongTien"] != null)
                {
                    dgv_QLCN10_DSHD.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    dgv_QLCN10_DSHD.Columns["TongTien"].HeaderText = "Tổng Tiền (VNĐ)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btn_QLCN10_Tim_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btn_QLCN10_Huy_Click(object sender, EventArgs e)
        {
            cmb_QLCN10_Nam.SelectedIndex = 1; // Năm nay
            cmb_QLCN10_Quy.SelectedIndex = 0;
            cmb_QLCN10_Thang.SelectedIndex = 0;
            cmb_QLCN10_Ngay.SelectedIndex = 0;
            cmb_QLCN10_MaHD.Text = "";
            LoadData();
        }

        private void dgv_QLCN10_DSHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_QLCN10_DSHD.CurrentRow != null)
            {
                string maHD = dgv_QLCN10_DSHD.CurrentRow.Cells["MaHD"].Value.ToString();

                try
                {
                    DataTable dtChiTiet = serviceDAL.GetChiTietHD(maHD);
                    string info = $"CHI TIẾT HÓA ĐƠN: {maHD}\n\n";

                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        info += $"- {row["TenMuc"]}: {Convert.ToDecimal(row["ThanhTien"]):N0} VNĐ\n";
                    }

                    MessageBox.Show(info, "Thông tin chi tiết");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải chi tiết: " + ex.Message);
                }
            }
        }

        private void btn_QLCN10_In_Click(object sender, EventArgs e)
        {
            if (dgv_QLCN10_DSHD.CurrentRow != null)
            {
                string maHD = dgv_QLCN10_DSHD.CurrentRow.Cells["MaHD"].Value.ToString();
                MessageBox.Show($"Đang gửi lệnh in hóa đơn {maHD} xuống máy in...", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in.");
            }
        }

        private void btn_QLCN10_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }
        private void cmb_QLCN10_LocTheo_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN10_Nam_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN10_MaHD_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN10_Quy_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN10_Thang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN10_Ngay_SelectedIndexChanged(object sender, EventArgs e) { }

        private void txb_QLCN10_Time_TextChanged(object sender, EventArgs e)
        {

        }
    }
}