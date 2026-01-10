using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT8_LSDieuDong : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_TT8_DSLSDD> danhSachLSDD = new BindingList<Class_TT8_DSLSDD>();

        public Usc_TT8_LSDieuDong()
        {
            InitializeComponent();

            dgv_TT8_DsLSDD.AutoGenerateColumns = true;
            dgv_TT8_DsLSDD.DataSource = danhSachLSDD;

            this.Load += Usc_TT8_LSDieuDong_Load;
            cmb_TT8_TraCuu.SelectedIndexChanged += cmb_TT8_TraCuu_SelectedIndexChanged;
        }

        private void Usc_TT8_LSDieuDong_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionData.MaNV))
            {
                txb_TT8_MaNV.Text = SessionData.MaNV;
            }
            else
            {
                txb_TT8_MaNV.Text = "NV001";
            }

            txb_TT8_MaNV.ReadOnly = true;
            txb_TT8_MaNV.BackColor = SystemColors.Control; 

            cmb_TT8_TraCuu.Items.Clear();
            cmb_TT8_TraCuu.Items.Add("Theo Chi Nhánh");
            cmb_TT8_TraCuu.Items.Add("Theo Năm");

            cmb_TT8_TraCuu.SelectedIndex = 0;

            btn_TT3_Tim_Click(sender, e);
        }

        private void cmb_TT8_TraCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmb_TT8_TraCuu.SelectedItem.ToString();

            cmb_TT8_NhapMa.Visible = true;

            if (selected == "Theo Chi Nhánh")
            {
                LoadDataToCombo_ChiNhanh();
            }
            else if (selected == "Theo Năm")
            {
                LoadDataToCombo_Nam();
            }
        }
        private void LoadDataToCombo_ChiNhanh()
        {
            try
            {
                cmb_TT8_NhapMa.DataSource = null;
                cmb_TT8_NhapMa.Items.Clear();

                DataTable dt = serviceDAL.GetListChiNhanh();

                DataRow dr = dt.NewRow();
                dr["MaCN"] = "";
                dr["TenCN"] = "--- Tất cả ---";
                dt.Rows.InsertAt(dr, 0);

                cmb_TT8_NhapMa.DataSource = dt;
                cmb_TT8_NhapMa.DisplayMember = "TenCN";
                cmb_TT8_NhapMa.ValueMember = "MaCN";
            }
            catch { }
        }
        private void LoadDataToCombo_Nam()
        {
            try
            {
                cmb_TT8_NhapMa.DataSource = null;
                cmb_TT8_NhapMa.Items.Clear();

                DataTable dt = serviceDAL.GetYearsInLSDieuDong();

                cmb_TT8_NhapMa.Items.Add("Tất cả");
                foreach (DataRow row in dt.Rows)
                {
                    cmb_TT8_NhapMa.Items.Add(row["Nam"].ToString());
                }
                cmb_TT8_NhapMa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }
        private void btn_TT3_Tim_Click(object sender, EventArgs e)
        {
            string maNV = txb_TT8_MaNV.Text.Trim();

            string maCN = null;
            int nam = 0;
            string loaiTraCuu = cmb_TT8_TraCuu.SelectedItem != null ? cmb_TT8_TraCuu.SelectedItem.ToString() : "";

            if (loaiTraCuu == "Theo Chi Nhánh")
            {
                if (cmb_TT8_NhapMa.SelectedValue != null)
                    maCN = cmb_TT8_NhapMa.SelectedValue.ToString();
            }
            else if (loaiTraCuu == "Theo Năm")
            {
                if (cmb_TT8_NhapMa.SelectedItem != null && cmb_TT8_NhapMa.SelectedItem.ToString() != "Tất cả")
                    int.TryParse(cmb_TT8_NhapMa.SelectedItem.ToString(), out nam);
            }
            try
            {
                DataTable dt = serviceDAL.GetLSDieuDong(maNV, maCN, nam);

                danhSachLSDD.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        danhSachLSDD.Add(new Class_TT8_DSLSDD
                        {
                            STT = Convert.ToInt32(row["STT"]),
                            MaNV = row["MaNV"].ToString(),
                            HoTenNV = row["HoTenNV"].ToString(),
                            NgayDieuDong = Convert.ToDateTime(row["NgayDieuDong"]),
                            MaCNCu = row["MaCNCu"].ToString(),
                            TenCNCu = row["TenCNCu"].ToString(),
                            MaCNMoi = row["MaCNMoi"].ToString(),
                            TenCNMoi = row["TenCNMoi"].ToString()
                        });
                    }
                    dgv_TT8_DsLSDD.Refresh();
                }
                else
                {
                    if (sender != null) MessageBox.Show("Không tìm thấy dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_TT8_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }

        private void dgv_TT3_DsKB_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lbl_TT2_MaKH_Click(object sender, EventArgs e) { }
        private void cmb_TT3_NhapMa_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btn_TT3_Huy_Click(object sender, EventArgs e) { }
        private void pnl_TT1_BG_Paint(object sender, PaintEventArgs e) { }
        private void pic_TT1_BG_Click(object sender, EventArgs e) { }
        private void btn_TT3_XacNhan_Click(object sender, EventArgs e) { }
        private void lbl_TT1_NhapTTKH_Click(object sender, EventArgs e) { }
        private void cmb_TT3_TraCuu_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void icon_TT_LSDieuDong_Click(object sender, EventArgs e) { }
        private void txb_TT8_MaNV_TextChanged(object sender, EventArgs e) { }
    }
}