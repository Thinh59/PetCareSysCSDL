using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang
{
    public partial class UC_KH_GioHang : UserControl, IReturnToMainPage
    {
        public event Action QuayVeTrangChu;
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_GioHang()
        {
            InitializeComponent();
            this.Load += UC_KH_GioHang_Load;
        }

        private void UC_KH_GioHang_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SessionData.MaCN_DangChon))
            {
                lb_ChiNhanh.Text = "Chưa chọn chi nhánh mua sắm!";
                btn_KH_ThanhToan.Enabled = false;
            }
            else
            {
                lb_ChiNhanh.Text = "Đang mua tại chi nhánh: " + SessionData.MaCN_DangChon;
            }

            LoadGioHang();
        }

        private void EnsureColumnExists(string columnName, string headerText, string dataProperty)
        {
            if (!dataGridView_KH_GioHang.Columns.Contains(columnName))
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = columnName;
                col.DataPropertyName = dataProperty;
                col.HeaderText = headerText;
                col.Visible = false; 
                dataGridView_KH_GioHang.Columns.Add(col);
            }
            else
            {
                dataGridView_KH_GioHang.Columns[columnName].DataPropertyName = dataProperty;
            }
        }

        private void LoadGioHang()
        {
            lb_ChiNhanh.Text = "Đang mua tại chi nhánh: " + SessionData.MaCN_DangChon;
            try
            {
                DataTable dt = dal.GetChiTietGioHang(SessionData.MaKH, SessionData.MaCN_DangChon);

                EnsureColumnExists("MaSP", "Mã SP", "MaSP");
                EnsureColumnExists("DonGia", "Đơn Giá", "DonGia");
                EnsureColumnExists("ThanhTien", "Thành Tiền", "ThanhTien");
                EnsureColumnExists("MaLSDV", "Mã GD", "MaLSDV_GioHang"); 

                dataGridView_KH_GioHang.AutoGenerateColumns = false;
                if (dataGridView_KH_GioHang.Columns.Contains("SanPham"))
                    dataGridView_KH_GioHang.Columns["SanPham"].DataPropertyName = "SanPham";
                if (dataGridView_KH_GioHang.Columns.Contains("SoLuong"))
                    dataGridView_KH_GioHang.Columns["SoLuong"].DataPropertyName = "SoLuong";

                dataGridView_KH_GioHang.DataSource = dt;

                btn_KH_ThanhToan.Enabled = (dt != null && dt.Rows.Count > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải giỏ hàng: " + ex.Message);
            }
        }
        private void dataGridView_KH_GioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_KH_GioHang.Columns[e.ColumnIndex].Name == "Xoa")
            {
                try
                {
                    var maSP = dataGridView_KH_GioHang.Rows[e.RowIndex].Cells["MaSP"].Value?.ToString();
                    var maLSDV = dataGridView_KH_GioHang.Rows[e.RowIndex].Cells["MaLSDV"].Value?.ToString();

                    if (maSP == null || maLSDV == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin sản phẩm để xóa!");
                        return;
                    }

                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?",
                                                      "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        string maCN = SessionData.MaCN_DangChon;
                        if (dal.CapNhatGioHang(maLSDV, maSP, 0, maCN))
                        {
                            MessageBox.Show("Đã xóa sản phẩm thành công!");
                            LoadGioHang();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void dataGridView_KH_GioHang_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView_KH_GioHang.IsCurrentCellDirty)
            {
                dataGridView_KH_GioHang.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGridView_KH_GioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_KH_GioHang.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                object val = dataGridView_KH_GioHang.Rows[e.RowIndex].Cells["SoLuong"].Value;
                if (val == null || !int.TryParse(val.ToString(), out int soLuongMoi))
                {
                    MessageBox.Show("Số lượng phải là số nguyên!");
                    LoadGioHang(); 
                    return;
                }

                if (soLuongMoi < 1)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0. Nếu muốn xoá hãy nhấn nút Xoá.");
                    LoadGioHang();
                    return;
                }

                string maSP = dataGridView_KH_GioHang.Rows[e.RowIndex].Cells["MaSP"].Value.ToString();
                string maLSDV = dataGridView_KH_GioHang.Rows[e.RowIndex].Cells["MaLSDV"].Value.ToString();

                if (dal.CapNhatGioHang(maLSDV, maSP, soLuongMoi, SessionData.MaCN_DangChon))
                {
                    LoadGioHang();
                }
            }
        }

        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }

        private void btn_KH_ThanhToan_Click(object sender, EventArgs e)
        {
            if (dataGridView_KH_GioHang.Rows.Count == 0) return;

            try
            {
                var cellMa = dataGridView_KH_GioHang.Rows[0].Cells["MaLSDV"].Value;

                if (cellMa == null || string.IsNullOrEmpty(cellMa.ToString()))
                {
                    MessageBox.Show("Lỗi dữ liệu: Không tìm thấy Mã giỏ hàng (MaLSDV)!");
                    return;
                }
                string maLSDV = cellMa.ToString();

                int tongTien = 0;
                foreach (DataGridViewRow row in dataGridView_KH_GioHang.Rows)
                {
                    if (row.IsNewRow) continue;
                    int sl = Convert.ToInt32(row.Cells["SoLuong"].Value ?? 0);
                    int gia = Convert.ToInt32(row.Cells["DonGia"].Value ?? 0);
                    tongTien += (sl * gia);
                }

                UC_KH_ThanhToan uc = new UC_KH_ThanhToan(maLSDV, tongTien);

                uc.QuayVeTrangChu += () => {
                    this.Controls.Remove(uc);
                    if (pnl_KH_GioHang != null) pnl_KH_GioHang.Visible = true;
                    else { dataGridView_KH_GioHang.Visible = true; btn_KH_ThanhToan.Visible = true; }
                    LoadGioHang();
                };

                if (pnl_KH_GioHang != null) pnl_KH_GioHang.Visible = false;
                else { dataGridView_KH_GioHang.Visible = false; btn_KH_ThanhToan.Visible = false; }

                this.Controls.Add(uc);
                uc.Dock = DockStyle.Fill;
                uc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chuyển trang: " + ex.Message);
            }
        }
    }
}