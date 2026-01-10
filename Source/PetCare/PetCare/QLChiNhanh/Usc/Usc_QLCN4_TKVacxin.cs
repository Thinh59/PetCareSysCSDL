using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN4_TKVacxin : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN4_TKVacxin> danhSachVX = new BindingList<Class_QLCN4_TKVacxin>();

        public Usc_QLCN4_TKVacxin()
        {
            InitializeComponent();
            dgv_QLCN4_TKVacXin.AutoGenerateColumns = true;
            dgv_QLCN4_TKVacXin.DataSource = danhSachVX;

            this.Load += Usc_QLCN4_TKVacxin_Load;
        }

        private void Usc_QLCN4_TKVacxin_Load(object sender, EventArgs e)
        {
            LoadCombos();
        }

        private void LoadCombos()
        {
            // Load Năm
            int year = DateTime.Now.Year;
            cmb_QLCN4_NamVX.Items.Add("Tất cả"); 
            for (int i = year; i >= 2020; i--) cmb_QLCN4_NamVX.Items.Add(i);
            cmb_QLCN4_NamVX.SelectedIndex = 1; 

            // Quý, Tháng, Ngày
            cmb_QLCN4_QuyVX.Items.Add("Tất cả");
            for (int i = 1; i <= 4; i++) cmb_QLCN4_QuyVX.Items.Add(i);
            cmb_QLCN4_QuyVX.SelectedIndex = 0;

            cmb_QLCN4_ThangVX.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cmb_QLCN4_ThangVX.Items.Add(i);
            cmb_QLCN4_ThangVX.SelectedIndex = 0;

            cmb_QLCN4_NgayVX.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cmb_QLCN4_NgayVX.Items.Add(i);
            cmb_QLCN4_NgayVX.SelectedIndex = 0;
        }

        private void ThucHienThongKe(string sortType)
        {
            // Lấy tham số
            int nam = 0;
            if (cmb_QLCN4_NamVX.SelectedItem != null && cmb_QLCN4_NamVX.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN4_NamVX.SelectedItem.ToString(), out nam);

            int quy = 0, thang = 0, ngay = 0;
            if (cmb_QLCN4_QuyVX.SelectedIndex > 0) int.TryParse(cmb_QLCN4_QuyVX.SelectedItem.ToString(), out quy);
            if (cmb_QLCN4_ThangVX.SelectedIndex > 0) int.TryParse(cmb_QLCN4_ThangVX.SelectedItem.ToString(), out thang);
            if (cmb_QLCN4_NgayVX.SelectedIndex > 0) int.TryParse(cmb_QLCN4_NgayVX.SelectedItem.ToString(), out ngay);

            try
            {
                DataTable dt = serviceDAL.ThongKeVacxin(nam, quy, thang, ngay, sortType);

                danhSachVX.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    danhSachVX.Add(new Class_QLCN4_TKVacxin
                    {
                        MaVacXin = row["MaVacXin"].ToString(),
                        TenVacXin = row["TenVacXin"].ToString(),
                        SLDat = Convert.ToInt32(row["SLDat"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- CÁC NÚT BẤM ---
        private void btn_QLCN4_ThongKeVX_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("ALL"); // Thống kê bình thường
        }

        private void btn_QLCN4_VXMax_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("MAX"); // Sắp xếp nhiều nhất lên đầu
        }

        private void btn_QLCN4_VXMin_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("MIN"); // Sắp xếp ít nhất lên đầu
        }

        private void cmb_QLCN4_NamVX_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgv_QLCN4_TKVacXin_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}