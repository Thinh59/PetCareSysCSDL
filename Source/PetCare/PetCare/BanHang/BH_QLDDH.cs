using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PetCare.DataConnection;

namespace PetCare
{
    public partial class BH_QLDDH : UserControl
    {
        int currentPage = 1;
        int pageSize = 20;

        DataConnection dc = new DataConnection();

        public BH_QLDDH()
        {
            InitializeComponent();
            data_BH_QLDDH.CellFormatting += data_BH_QLDDH_CellFormatting;
            cbox_BH_QLDDH_TT.SelectedIndex = 0;
            this.VisibleChanged += BH_QLDDH_VisibleChanged;
            data_BH_QLDDH.AutoGenerateColumns = false;
            btn_BH_QLDDH_Tr.Click += btn_BH_QLDDH_Tr_Click;
            btn_BH_QLDDH_Next.Click += btn_BH_QLDDH_Next_Click;

        }

        private void LoadDSDonHang()
        {
            try
            {
                string makh = txtBox_BH_QLDDH_MKH.Text.Trim();
                string trangThai = cbox_BH_QLDDH_TT.SelectedItem != null ? cbox_BH_QLDDH_TT.SelectedItem.ToString() : "";

                SqlParameter[] p = {
            new SqlParameter("@MaKH", string.IsNullOrEmpty(makh) ? (object)DBNull.Value : makh),
            new SqlParameter("@TrangThai", trangThai),
            new SqlParameter("@PageNumber", currentPage),
            new SqlParameter("@PageSize", pageSize)
        };

                // Gọi Store Procedure đã tối ưu
                data_BH_QLDDH.DataSource = dc.ExecuteProcedure("sp_GetDSDonHang_Paging", p);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BH_QLDDH_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                currentPage = 1;
                LoadDSDonHang();
            }
        }

        private void pnl_BH_QLSP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lab_BH_QLDDH_DSSP_Click(object sender, EventArgs e)
        {

        }

        private void txtBox_BH_QLDDH_MKH_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1; 
            LoadDSDonHang();
        }

        private void cbox_BH_QLDDH_TT_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1; 
            LoadDSDonHang();
        }

        private void btn_BH_QLDDH_TK_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadDSDonHang();
        }

        private void data_BH_QLDDH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_BH_QLDDH_XN_Click(object sender, EventArgs e)
        {
            if (data_BH_QLDDH.CurrentRow == null || data_BH_QLDDH.CurrentRow.IsNewRow) return;

            try
            {
                string maLS = data_BH_QLDDH.CurrentRow.Cells["MaLSDV"].Value.ToString();
                string trangThai = data_BH_QLDDH.CurrentRow.Cells["TrangThaiGD"].Value.ToString();

                if (trangThai == "Chờ thanh toán" || trangThai == "Đã có sản phẩm")
                {
                    if (MessageBox.Show("Xác nhận đơn hàng này đã hoàn tất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SqlParameter[] p = {
                    new SqlParameter("@MaLSDV", maLS),
                    new SqlParameter("@MaCN", SessionData.MaCN)
                };

                        dc.ExecuteProcedure("sp_XacNhanVaTruKho", p);
                        MessageBox.Show("Đơn hàng đã được xác nhận hoàn tất!");
                        LoadDSDonHang();
                    }
                }
                else
                {
                    MessageBox.Show("Không thể thay đổi trạng thái của đơn hàng " + trangThai);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        

        private void btn_BH_QLDDH_HUY_Click(object sender, EventArgs e)
        {
            if (data_BH_QLDDH.CurrentRow == null) return;

            string maLS = data_BH_QLDDH.CurrentRow.Cells["MaLSDV"].Value.ToString();
            string trangThai = data_BH_QLDDH.CurrentRow.Cells["TrangThaiGD"].Value.ToString();

            if (trangThai == "Chờ thanh toán" || trangThai == "Đã có sản phẩm")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn HỦY đơn này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = "UPDATE LS_DV SET TrangThaiGD = N'Đã hủy' WHERE MaLSDV = @ma";
                    SqlParameter[] p = { new SqlParameter("@ma", maLS) };
                    dc.ExecuteNonQuery(sql, p);

                    string sqlHD = "UPDATE hd SET TrangThaiHD = N'Đã hủy' FROM HOADON hd JOIN CT_HOADON ct ON hd.MaHD = ct.MaHD WHERE ct.MaLSGD = @ma";
                    dc.ExecuteNonQuery(sqlHD, p);

                    MessageBox.Show("Đã hủy đơn hàng thành công!");
                    LoadDSDonHang();
                }
            }
            else
            {
                MessageBox.Show("Đơn hàng ở trạng thái " + trangThai + " không thể hủy!");
            }
        }

        private void data_BH_QLDDH_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (data_BH_QLDDH.Columns[e.ColumnIndex].Name == "TrangThaiGD" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Hoàn thành" || status == "Đã thanh toán")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else if (status == "Đã hủy")
                {
                    e.CellStyle.ForeColor = Color.Silver; 
                }
                else if (status == "Chờ xử lý")
                {
                    e.CellStyle.ForeColor = Color.Red; 
                    e.CellStyle.Font = new Font(data_BH_QLDDH.Font, FontStyle.Bold);
                }
            }
        }

        private void btn_BH_QLDDH_Tr_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDSDonHang();
            }
        }

        private void btn_BH_QLDDH_Next_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadDSDonHang();
        }
    }
}
