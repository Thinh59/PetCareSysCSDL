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
    public partial class Usc_QLCN1_TKDoanhThu : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN1_DoanhThuDV> listHienThi = new BindingList<Class_QLCN1_DoanhThuDV>();

        public Usc_QLCN1_TKDoanhThu()
        {
            InitializeComponent();

            dgv_QLCN1_TKDoanhThu.AutoGenerateColumns = true;
            dgv_QLCN1_TKDoanhThu.DataSource = listHienThi;

            this.Load += Usc_QLCN1_TKDoanhThu_Load;

            cmb_QLCN1_LocTheo.SelectedIndexChanged += cmb_QLCN1_LocTheo_SelectedIndexChanged;
        }

        private void Usc_QLCN1_TKDoanhThu_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();


            if (cmb_QLCN1_LocTheo.Items.Count == 0)
            {
                cmb_QLCN1_LocTheo.Items.AddRange(new object[] { "Theo Năm", "Theo Quý", "Theo Tháng", "Theo Ngày" });
            }
            cmb_QLCN1_LocTheo.SelectedIndex = 0; 
        }

        private void LoadComboBoxData()
        {

            int year = DateTime.Now.Year;
            for (int i = year; i >= year - 5; i--) cmb_QLCN1_Nam.Items.Add(i);
            cmb_QLCN1_Nam.SelectedIndex = 0;


            for (int i = 1; i <= 4; i++) cmb_QLCN1_Quy.Items.Add(i);
            cmb_QLCN1_Quy.SelectedIndex = 0;


            for (int i = 1; i <= 12; i++) cmb_QLCN1_Thang.Items.Add(i);
            cmb_QLCN1_Thang.SelectedIndex = 0; 

            for (int i = 1; i <= 31; i++) cmb_QLCN1_Ngay.Items.Add(i);
            cmb_QLCN1_Ngay.SelectedIndex = 0;
        }

        private void cmb_QLCN1_LocTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loc = cmb_QLCN1_LocTheo.SelectedItem.ToString();

            cmb_QLCN1_Nam.Enabled = true;
            cmb_QLCN1_Quy.Enabled = false;
            cmb_QLCN1_Thang.Enabled = false;
            cmb_QLCN1_Ngay.Enabled = false;

            if (loc == "Theo Quý")
            {
                cmb_QLCN1_Quy.Enabled = true;
            }
            else if (loc == "Theo Tháng")
            {
                cmb_QLCN1_Thang.Enabled = true;
            }
            else if (loc == "Theo Ngày")
            {
                cmb_QLCN1_Thang.Enabled = true; 
                cmb_QLCN1_Ngay.Enabled = true;
            }
        }

        private void btn_QLCN1_ThongKe_Click(object sender, EventArgs e)
        {
            string maCN = SessionData.MaCN;
            if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

            int nam = int.Parse(cmb_QLCN1_Nam.SelectedItem.ToString());
            int quy = 0, thang = 0, ngay = 0;
            string typeSQL = "";

            string loc = cmb_QLCN1_LocTheo.SelectedItem.ToString();

            if (loc == "Theo Năm") typeSQL = "Nam";
            else if (loc == "Theo Quý")
            {
                typeSQL = "Quy";
                if (cmb_QLCN1_Quy.SelectedItem != null) quy = int.Parse(cmb_QLCN1_Quy.SelectedItem.ToString());
            }
            else if (loc == "Theo Tháng")
            {
                typeSQL = "Thang";
                if (cmb_QLCN1_Thang.SelectedItem != null) thang = int.Parse(cmb_QLCN1_Thang.SelectedItem.ToString());
            }
            else if (loc == "Theo Ngày")
            {
                typeSQL = "Ngay";
                if (cmb_QLCN1_Thang.SelectedItem != null) thang = int.Parse(cmb_QLCN1_Thang.SelectedItem.ToString());
                if (cmb_QLCN1_Ngay.SelectedItem != null) ngay = int.Parse(cmb_QLCN1_Ngay.SelectedItem.ToString());
            }

            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                DataTable dt = serviceDAL.GetDoanhThu(maCN, typeSQL, nam, quy, thang, ngay);

                timer.Stop();
                long executionTime = timer.ElapsedMilliseconds;

                if (txb_QLCN1_Time != null)
                {
                    txb_QLCN1_Time.Text = executionTime.ToString() + " ms";

                    if (executionTime < 100)
                    {
                        txb_QLCN1_Time.ForeColor = Color.Green; 
                        txb_QLCN1_Time.BackColor = Color.FromArgb(220, 255, 220);
                    }
                    else
                    {
                        txb_QLCN1_Time.ForeColor = Color.Red;  
                        txb_QLCN1_Time.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }

                listHienThi.Clear();
                decimal tongDoanhThu = 0;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal dtDV = Convert.ToDecimal(row["DoanhThu"]);
                        tongDoanhThu += dtDV;

                        listHienThi.Add(new Class_QLCN1_DoanhThuDV
                        {
                            MaDV = row["MaDichVu"].ToString(),
                            TenDichVu = row["TenDichVu"].ToString(),
                            SoLuongLSDV = Convert.ToInt32(row["SoLuongLSDV"]),
                            DoanhThu = dtDV
                        });
                    }
                    dgv_QLCN1_TKDoanhThu.Refresh();
                }
                else
                {
                    MessageBox.Show("Không có doanh thu trong khoảng thời gian này.", "Thông báo");
                }

                txtBox_QLCN1_TongDoanhThu.Text = tongDoanhThu.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_QLCN1_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }
        private void cmb_QLCN1_Nam_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN1_Quy_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN1_Thang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN1_Ngay_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgv_QLCN1_TKDoanhThu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtBox_QLCN1_TongDoanhThu_TextChanged(object sender, EventArgs e) { }

        private void txb_QLCN1_Time_TextChanged(object sender, EventArgs e)
        {

        }
    }
}