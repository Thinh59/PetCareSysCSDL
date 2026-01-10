using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN5_QLSP : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN5_DSSP> danhSachSP = new BindingList<Class_QLCN5_DSSP>();
        private string currMode = "";

        public Usc_QLCN5_QLSP()
        {
            InitializeComponent();
            SetupGrid();
            dgv_QLCN5_DSSP.DataSource = danhSachSP;

            this.Load += Usc_QLCN5_QLSP_Load;
            dgv_QLCN5_DSSP.CellClick += dgv_QLCN5_DSSP_CellContentClick;
        }

        private void SetupGrid()
        {
            dgv_QLCN5_DSSP.Columns.Clear();
            dgv_QLCN5_DSSP.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn();
            colMa.DataPropertyName = "MaSP";
            colMa.HeaderText = "Mã SP";
            colMa.Width = 80;
            dgv_QLCN5_DSSP.Columns.Add(colMa);

            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn();
            colTen.DataPropertyName = "TenSP";
            colTen.HeaderText = "Tên Sản Phẩm";
            colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_QLCN5_DSSP.Columns.Add(colTen);

            DataGridViewTextBoxColumn colLoai = new DataGridViewTextBoxColumn();
            colLoai.DataPropertyName = "LoaiSP";
            colLoai.HeaderText = "Loại";
            colLoai.Width = 100;
            dgv_QLCN5_DSSP.Columns.Add(colLoai);

            DataGridViewTextBoxColumn colGia = new DataGridViewTextBoxColumn();
            colGia.DataPropertyName = "DonGia";
            colGia.HeaderText = "Đơn Giá";
            colGia.Width = 100;
            colGia.DefaultCellStyle.Format = "N0";
            dgv_QLCN5_DSSP.Columns.Add(colGia);

            DataGridViewTextBoxColumn colTon = new DataGridViewTextBoxColumn();
            colTon.DataPropertyName = "SLTonKho";
            colTon.HeaderText = "Tồn Kho";
            colTon.Width = 80;
            dgv_QLCN5_DSSP.Columns.Add(colTon);
        }

        private void Usc_QLCN5_QLSP_Load(object sender, EventArgs e)
        {
            if (cmb_QLCN5_LoaiSP.Items.Count == 0)
            {
                cmb_QLCN5_LoaiSP.Items.AddRange(new object[] { "Thức ăn", "Thuốc", "Phụ kiện", "Vắc xin" });
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

                DataTable dt = serviceDAL.GetDSSP(maCN);
                danhSachSP.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        danhSachSP.Add(new Class_QLCN5_DSSP
                        {
                            MaSP = row["MaSP"].ToString(),
                            TenSP = row["TenSP"].ToString(),
                            LoaiSP = row["LoaiSP"].ToString(),
                            DonGia = Convert.ToInt32(row["DonGia"]),
                            SLTonKho = Convert.ToInt32(row["SLTonKho"])
                        });
                    }
                }
                dgv_QLCN5_DSSP.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu SP: " + ex.Message);
            }
        }

        private void btn_QLCN5_Them_Click(object sender, EventArgs e)
        {
            currMode = "ADD";
            ClearInputs();
            SetInputState(true);
            cmb_QLCN5_MaSP.Enabled = true;
            cmb_QLCN5_MaSP.Focus();
            SetButtonState(false);
        }

        private void btn_QLCN5_Sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_QLCN5_MaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.");
                return;
            }
            currMode = "EDIT";
            SetInputState(true);
            cmb_QLCN5_MaSP.Enabled = false; 
            SetButtonState(false);
        }

        private void btn_QLCN5_Xoa_Click(object sender, EventArgs e)
        {
            string maSP = cmb_QLCN5_MaSP.Text;
            if (string.IsNullOrEmpty(maSP)) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa SP {maSP} khỏi chi nhánh này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string maCN = SessionData.MaCN;
                    if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                    serviceDAL.XoaSP(maSP, maCN);
                    MessageBox.Show("Đã xóa khỏi kho chi nhánh!");
                    LoadData();
                    ClearInputs();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btn_QLCN5_XacNhan_Click(object sender, EventArgs e)
        {
            string maSP = cmb_QLCN5_MaSP.Text.Trim();
            string tenSP = txb_QLCN5_TenSP.Text.Trim();
            string loaiSP = cmb_QLCN5_LoaiSP.Text;
            int donGia = 0, slTon = 0;

            int.TryParse(txb_QLCN5_DonGia.Text, out donGia);

            int.TryParse(txb_QLCN5_SLTon.Text, out slTon);

            if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên sản phẩm.");
                return;
            }

            try
            {
                string maCN = SessionData.MaCN;
                if (string.IsNullOrEmpty(maCN)) maCN = "CN01";

                if (currMode == "ADD")
                {
                    serviceDAL.ThemSP(maSP, tenSP, loaiSP, donGia, slTon, maCN);
                    MessageBox.Show("Thêm mới thành công!");
                }
                else if (currMode == "EDIT")
                {
                    serviceDAL.SuaSP(maSP, tenSP, loaiSP, donGia, slTon, maCN);
                    MessageBox.Show("Cập nhật thành công!");
                }

                LoadData();
                btn_QLCN5_Huy_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btn_QLCN5_Huy_Click(object sender, EventArgs e)
        {
            currMode = "";
            ClearInputs();
            SetInputState(false);
            SetButtonState(true);
        }

        private void dgv_QLCN5_DSSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_QLCN5_DSSP.CurrentRow != null && currMode == "")
            {
                var item = dgv_QLCN5_DSSP.CurrentRow.DataBoundItem as Class_QLCN5_DSSP;
                if (item != null)
                {
                    cmb_QLCN5_MaSP.Text = item.MaSP;
                    txb_QLCN5_TenSP.Text = item.TenSP;
                    cmb_QLCN5_LoaiSP.Text = item.LoaiSP;
                    txb_QLCN5_DonGia.Text = item.DonGia.ToString();

                    txb_QLCN5_SLTon.Text = item.SLTonKho.ToString();
                }
            }
        }
        private void ClearInputs()
        {
            cmb_QLCN5_MaSP.Text = "";
            txb_QLCN5_TenSP.Clear();
            cmb_QLCN5_LoaiSP.SelectedIndex = -1;
            txb_QLCN5_DonGia.Clear();

            txb_QLCN5_SLTon.Clear();
        }

        private void SetInputState(bool enable)
        {
            txb_QLCN5_TenSP.ReadOnly = !enable;
            txb_QLCN5_DonGia.ReadOnly = !enable;
            cmb_QLCN5_LoaiSP.Enabled = enable;

            txb_QLCN5_SLTon.ReadOnly = !enable;
        }

        private void SetButtonState(bool isNormalMode)
        {
            btn_QLCN5_Them.Visible = isNormalMode;
            btn_QLCN5_Sua.Visible = isNormalMode;
            btn_QLCN5_Xoa.Visible = isNormalMode;
            btn_QLCN5_XacNhan.Visible = !isNormalMode;
            btn_QLCN5_Huy.Visible = !isNormalMode;
        }

        private void btn_QLCN5_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        private void cmb_QLCN5_MaSP_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txb_QLCN5_TenSP_TextChanged(object sender, EventArgs e) { }
        private void cmb_QLCN5_LoaiSP_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txb_QLCN5_DonGia_TextChanged(object sender, EventArgs e) { }
    }
}