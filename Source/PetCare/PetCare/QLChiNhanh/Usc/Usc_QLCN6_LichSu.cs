using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN6_LichSu : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN6_LichSu> danhSachLS = new BindingList<Class_QLCN6_LichSu>();
        private string currentMaPet = "";

        public Usc_QLCN6_LichSu(string maPet)
        {
            InitializeComponent();

            this.currentMaPet = maPet; // Lưu lại mã để dùng

            dgv_QLCN6_LSPet.AutoGenerateColumns = true;
            dgv_QLCN6_LSPet.DataSource = danhSachLS;

            this.Load += Usc_QLCN6_LichSu_Load;
        }
        public Usc_QLCN6_LichSu() { InitializeComponent(); }

        private void Usc_QLCN6_LichSu_Load(object sender, EventArgs e)
        {
            txb_QLCN6_MaPet.Text = currentMaPet;
            txb_QLCN6_MaPet.ReadOnly = true;

            LoadDataLichSu();

            FillFilterCombos();
        }

        private void LoadDataLichSu()
        {
            string filterMaLS = cmb_QLCN6_MaLSDV.Text.Trim();
            string filterMaSP = cmb_QLCN6_MaSP.Text.Trim(); 

            try
            {
                DataTable dt = serviceDAL.GetLichSuPet(currentMaPet, filterMaLS);

                DataView dv = dt.DefaultView;
                if (!string.IsNullOrEmpty(filterMaSP))
                {
                    dv.RowFilter = string.Format("MaSP LIKE '%{0}%'", filterMaSP);
                }

                danhSachLS.Clear();
                foreach (DataRow row in dv.ToTable().Rows)
                {
                    danhSachLS.Add(new Class_QLCN6_LichSu
                    {
                        MaLSDV = row["MaLSDV"].ToString(),
                        BacSi = row["BacSi"].ToString(),
                        MaSP = row["MaSP"].ToString(),
                        TenSP = row["TenSP"].ToString(),
                        LoaiSP = row["LoaiSP"].ToString(),
                        NgaySD = Convert.ToDateTime(row["NgaySD"])
                    });
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btn_QLCN6_Tim_Click(object sender, EventArgs e)
        {
            LoadDataLichSu();
        }

        private void btn_QLCN6_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }
        private void FillFilterCombos()
        {
            try
            {
                // 1. Nạp Mã LSDV
                DataTable dtMaLS = serviceDAL.GetMaLSDVByPet(currentMaPet);
                cmb_QLCN6_MaLSDV.DataSource = dtMaLS;
                cmb_QLCN6_MaLSDV.DisplayMember = "MaLSDV";
                cmb_QLCN6_MaLSDV.ValueMember = "MaLSDV";
                cmb_QLCN6_MaLSDV.SelectedIndex = -1; // Không chọn mặc định

                // 2. Nạp Mã SP
                DataTable dtMaSP = serviceDAL.GetMaSPByPet(currentMaPet);
                cmb_QLCN6_MaSP.DataSource = dtMaSP;
                cmb_QLCN6_MaSP.DisplayMember = "MaSP";
                cmb_QLCN6_MaSP.ValueMember = "MaSP";
                cmb_QLCN6_MaSP.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách lọc: " + ex.Message);
            }
        }

        // Sự kiện rác
        private void pnl_QLCN6_UC_Paint(object sender, PaintEventArgs e) { }
        private void cmb_QLCN6_MaSP_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_QLCN6_MaLSDV_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txb_QLCN6_MaPet_TextChanged(object sender, EventArgs e) { }
        private void dgv_QLCN6_LSPet_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btn_QLCN6_Huy_Click(object sender, EventArgs e)
        {
            try
            {
                cmb_QLCN6_MaLSDV.SelectedIndex = -1;
                cmb_QLCN6_MaLSDV.Text = "";

                cmb_QLCN6_MaSP.SelectedIndex = -1;
                cmb_QLCN6_MaSP.Text = "";

                LoadDataLichSu();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy lọc: " + ex.Message);
            }
        }
    }
}