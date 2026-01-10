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
    public partial class Usc_QLCN9_QLDV : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN9_DSDV> danhSachDV = new BindingList<Class_QLCN9_DSDV>();
        private string currMode = "";

        public Usc_QLCN9_QLDV()
        {
            InitializeComponent();

            SetupGrid();

            dgv_QLCN9_DSDV.DataSource = danhSachDV;
            dgv_QLCN9_DSDV.CellClick += dgv_QLCN9_DSDV_CellContentClick;
        }
        private void SetupGrid()
        {
            dgv_QLCN9_DSDV.Columns.Clear();
            dgv_QLCN9_DSDV.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn();
            colMa.DataPropertyName = "MaDV"; 
            colMa.HeaderText = "Mã DV";
            colMa.Width = 100;
            dgv_QLCN9_DSDV.Columns.Add(colMa);

            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn();
            colTen.DataPropertyName = "TenDichVu";
            colTen.HeaderText = "Tên Dịch Vụ";
            colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
            dgv_QLCN9_DSDV.Columns.Add(colTen);

            DataGridViewTextBoxColumn colGia = new DataGridViewTextBoxColumn();
            colGia.DataPropertyName = "DonGia";
            colGia.HeaderText = "Đơn Giá";
            colGia.Width = 120;
            dgv_QLCN9_DSDV.Columns.Add(colGia);

            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.DataPropertyName = "TrangThai";
            colTrangThai.HeaderText = "Trạng Thái";
            colTrangThai.Width = 150;
            dgv_QLCN9_DSDV.Columns.Add(colTrangThai);
        }

        private void Usc_QLCN9_QLDV_Load(object sender, EventArgs e)
        {
            if (cmb_QLCN9_trangThai.Items.Count == 0)
            {
                cmb_QLCN9_trangThai.Items.AddRange(new object[] { "Đang hoạt động", "Ngừng kinh doanh", "Tạm ngưng" });
            }

            LoadData();
            SetInputState(false);
            SetButtonState(true);
        }

        private void LoadData()
        {
            try
            {
                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                DataTable dt = serviceDAL.GetDSDV(maCN);
                danhSachDV.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    danhSachDV.Add(new Class_QLCN9_DSDV
                    {
                        Chon = false,
                        MaDV = row["MaDichVu"].ToString(),
                        TenDichVu = row["TenDV"].ToString(),
                        DonGia = string.Format("{0:N0}", Convert.ToDecimal(row["GiaTien"])),
                        TrangThai = row["TrangThai"].ToString()
                    });
                }
                dgv_QLCN9_DSDV.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btn_QLCN9_Them_Click(object sender, EventArgs e)
        {
            currMode = "ADD";
            ClearInputs();
            SetInputState(true);
            cmb_QLCN9_MaDV.Enabled = true;
            cmb_QLCN9_MaDV.Focus();
            SetButtonState(false);
        }

        private void btn_QLCN9_Sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_QLCN9_MaDV.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa.");
                return;
            }
            currMode = "EDIT";
            SetInputState(true);
            cmb_QLCN9_MaDV.Enabled = false; 
            SetButtonState(false);
        }

        private void btn_QLCN9_Xoa_Click(object sender, EventArgs e)
        {
            string maDV = cmb_QLCN9_MaDV.Text;
            if (string.IsNullOrEmpty(maDV)) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa dịch vụ {maDV}?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string maCN = SessionData.MaCN;
                    if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                    serviceDAL.XoaDV(maDV, maCN);

                    MessageBox.Show("Xóa thành công!");

                    var itemCanXoa = danhSachDV.FirstOrDefault(x => x.MaDV == maDV);
                    if (itemCanXoa != null)
                    {
                        danhSachDV.Remove(itemCanXoa);
                    }

                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btn_QLCN9_XacNhan_Click(object sender, EventArgs e)
        {
            string maDV = cmb_QLCN9_MaDV.Text.Trim();
            string tenDV = txb_QLCN9_TenDV.Text.Trim();
            string trangThai = cmb_QLCN9_trangThai.Text;
            int giaTien = 0;

            string strGia = txb_QLCN9_DonGia.Text.Replace(",", "").Replace(".", "").Replace(" ", "");
            int.TryParse(strGia, out giaTien);

            if (string.IsNullOrEmpty(maDV) || string.IsNullOrEmpty(tenDV))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                serviceDAL.ThemSuaDV(maDV, tenDV, giaTien, trangThai, maCN);
                MessageBox.Show("Lưu thành công!");
                LoadData();
                btn_QLCN9_Huy_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btn_QLCN9_Huy_Click(object sender, EventArgs e)
        {
            currMode = "";
            ClearInputs();
            SetInputState(false);
            SetButtonState(true);
        }

        private void dgv_QLCN9_DSDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_QLCN9_DSDV.CurrentRow != null && currMode == "")
            {
                var item = dgv_QLCN9_DSDV.CurrentRow.DataBoundItem as Class_QLCN9_DSDV;
                if (item != null)
                {
                    cmb_QLCN9_MaDV.Text = item.MaDV;
                    txb_QLCN9_TenDV.Text = item.TenDichVu;
                    txb_QLCN9_DonGia.Text = item.DonGia; 
                    cmb_QLCN9_trangThai.Text = item.TrangThai;
                }
            }
        }

        private void ClearInputs()
        {
            cmb_QLCN9_MaDV.Text = "";
            txb_QLCN9_TenDV.Clear();
            txb_QLCN9_DonGia.Clear();
            cmb_QLCN9_trangThai.SelectedIndex = -1;
        }

        private void SetInputState(bool enable)
        {
            txb_QLCN9_TenDV.ReadOnly = !enable;
            txb_QLCN9_DonGia.ReadOnly = !enable;
            cmb_QLCN9_trangThai.Enabled = enable;
        }

        private void SetButtonState(bool isNormalMode)
        {
            btn_QLCN9_Them.Visible = isNormalMode;
            btn_QLCN9_Sua.Visible = isNormalMode;
            btn_QLCN9_Xoa.Visible = isNormalMode;
            btn_QLCN9_XacNhan.Visible = !isNormalMode;
            btn_QLCN9_Huy.Visible = !isNormalMode;
        }

        private void btn_QLCN9_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        private void pnl_TT1_BG_Paint(object sender, PaintEventArgs e) { }
        private void icon_TT_TTDV_Click(object sender, EventArgs e) { }
        private void txb_QLCN9_DonGia_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void txb_QLCN9_TenDV_TextChanged(object sender, EventArgs e) { }
        private void lbl_TT2_SDT_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void cmb_QLCN9_MaDV_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void cmb_QLCN9_trangThai_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lbl_TT2_MaKH_Click(object sender, EventArgs e) { }
        private void pic_TT1_BG_Click(object sender, EventArgs e) { }
        private void lbl_TT1_NhapTTKH_Click(object sender, EventArgs e) { }
    }
}