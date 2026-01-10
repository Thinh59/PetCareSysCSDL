using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN4_TKThuoc : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN4_TKThuoc> danhSachTh = new BindingList<Class_QLCN4_TKThuoc>();

        public Usc_QLCN4_TKThuoc()
        {
            InitializeComponent();

            dgv_QLCN4_TKVacXin.AutoGenerateColumns = true;
            dgv_QLCN4_TKVacXin.DataSource = danhSachTh;

            this.Load += Usc_QLCN4_TKThuoc_Load;
        }

        private void Usc_QLCN4_TKThuoc_Load(object sender, EventArgs e)
        {
            LoadCombos();
        }

        private void LoadCombos()
        {
            int year = DateTime.Now.Year;
            cmb_QLCN4_NamTH.Items.Add("Tất cả");
            for (int i = year; i >= 2020; i--) cmb_QLCN4_NamTH.Items.Add(i);
            cmb_QLCN4_NamTH.SelectedIndex = 1; // Chọn năm hiện tại

            // Quý
            cmb_QLCN4_QuyTH.Items.Add("Tất cả");
            for (int i = 1; i <= 4; i++) cmb_QLCN4_QuyTH.Items.Add(i);
            cmb_QLCN4_QuyTH.SelectedIndex = 0;

            // Tháng
            cmb_QLCN4_ThangTH.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cmb_QLCN4_ThangTH.Items.Add(i);
            cmb_QLCN4_ThangTH.SelectedIndex = 0;

            // Ngày
            cmb_QLCN4_NgayTH.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cmb_QLCN4_NgayTH.Items.Add(i);
            cmb_QLCN4_NgayTH.SelectedIndex = 0;
        }

        // --- HÀM XỬ LÝ CHÍNH ---
        private void ThucHienThongKe(string sortType)
        {
            int nam = 0;
            if (cmb_QLCN4_NamTH.SelectedItem != null && cmb_QLCN4_NamTH.SelectedItem.ToString() != "Tất cả")
                int.TryParse(cmb_QLCN4_NamTH.SelectedItem.ToString(), out nam);

            int quy = 0, thang = 0, ngay = 0;
            if (cmb_QLCN4_QuyTH.SelectedIndex > 0) int.TryParse(cmb_QLCN4_QuyTH.SelectedItem.ToString(), out quy);
            if (cmb_QLCN4_ThangTH.SelectedIndex > 0) int.TryParse(cmb_QLCN4_ThangTH.SelectedItem.ToString(), out thang);
            if (cmb_QLCN4_NgayTH.SelectedIndex > 0) int.TryParse(cmb_QLCN4_NgayTH.SelectedItem.ToString(), out ngay);

            try
            {
                DataTable dt = serviceDAL.ThongKeThuoc(nam, quy, thang, ngay, sortType);

                danhSachTh.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    danhSachTh.Add(new Class_QLCN4_TKThuoc
                    {
                        MaThuoc = row["MaThuoc"].ToString(),
                        TenThuoc = row["TenThuoc"].ToString(),
                        DVTinh = row["DVTinh"].ToString(),
                        HanSD = row["HanSD"] != DBNull.Value ? Convert.ToDateTime(row["HanSD"]) : DateTime.MinValue,
                        SLBan = Convert.ToInt32(row["SLBan"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_QLCN4_ThongKeVX_Click(object sender, EventArgs e) // Nút Thống Kê
        {
            ThucHienThongKe("ALL");
        }

        private void btn_QLCN4_THMax_Click(object sender, EventArgs e) // Nút Nhiều Nhất
        {
            ThucHienThongKe("MAX");
        }

        private void btn_QLCN4_THMin_Click(object sender, EventArgs e) // Nút Ít Nhất (hoặc Tồn kho/Còn lại tùy ý bạn đặt)
        {
            ThucHienThongKe("MIN");
        }

        private void cmb_QLCN4_NamTH_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN4_QuyTH_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN4_ThangTH_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN4_NgayTH_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgv_QLCN4_TKVacXin_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}