using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN4_TKSPKhac : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN4_TKSPK> dsSPK = new BindingList<Class_QLCN4_TKSPK>();

        public Usc_QLCN4_TKSPKhac()
        {
            InitializeComponent();

            dgv_QLCN4_TKSPK.AutoGenerateColumns = true;
            dgv_QLCN4_TKSPK.DataSource = dsSPK;

            this.Load += Usc_QLCN4_TKSPKhac_Load;
        }

        private void Usc_QLCN4_TKSPKhac_Load(object sender, EventArgs e)
        {
            LoadCombos();
        }

        private void LoadCombos()
        {
            int year = DateTime.Now.Year;
            cmb_QLCN4_NamSPK.Items.Add("Tất cả");
            for (int i = year; i >= 2020; i--) cmb_QLCN4_NamSPK.Items.Add(i);
            cmb_QLCN4_NamSPK.SelectedIndex = 1;

            cmb_QLCN4_QuySPK.Items.Add("Tất cả");
            for (int i = 1; i <= 4; i++) cmb_QLCN4_QuySPK.Items.Add(i);
            cmb_QLCN4_QuySPK.SelectedIndex = 0;

            cmb_QLCN4_ThangSPK.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cmb_QLCN4_ThangSPK.Items.Add(i);
            cmb_QLCN4_ThangSPK.SelectedIndex = 0;

            cmb_QLCN4_NgaySPK.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cmb_QLCN4_NgaySPK.Items.Add(i);
            cmb_QLCN4_NgaySPK.SelectedIndex = 0;
        }
        private void ThucHienThongKe(string sortType)
        {
            int nam = 0;
            if (cmb_QLCN4_NamSPK.SelectedItem != null && cmb_QLCN4_NamSPK.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN4_NamSPK.SelectedItem.ToString(), out nam);

            int quy = 0, thang = 0, ngay = 0;
            if (cmb_QLCN4_QuySPK.SelectedIndex > 0) int.TryParse(cmb_QLCN4_QuySPK.SelectedItem.ToString(), out quy);
            if (cmb_QLCN4_ThangSPK.SelectedIndex > 0) int.TryParse(cmb_QLCN4_ThangSPK.SelectedItem.ToString(), out thang);
            if (cmb_QLCN4_NgaySPK.SelectedIndex > 0) int.TryParse(cmb_QLCN4_NgaySPK.SelectedItem.ToString(), out ngay);

            try
            {
                DataTable dt = serviceDAL.ThongKeSanPham(nam, quy, thang, ngay, sortType);

                dsSPK.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dsSPK.Add(new Class_QLCN4_TKSPK
                    {
                        MaSP = row["MaSP"].ToString(),
                        TenSP = row["TenSP"].ToString(),
                        SLBan = Convert.ToInt32(row["SLBan"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_QLCN4_TKSPK_Click(object sender, EventArgs e) 
        {
            ThucHienThongKe("ALL");
        }

        private void btn_QLCN4_SPKMax_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("MAX");
        }

        private void btn_QLCN4_SPKMin_Click(object sender, EventArgs e) 
        {
            ThucHienThongKe("MIN");
        }

        private void cmb_QLCN4_NamSPK_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN4_QuySPK_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN4_ThangSPK_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN4_NgaySPK_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgv_QLCN4_TKSPK_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}