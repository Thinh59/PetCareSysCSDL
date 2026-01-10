using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang.UC
{
    public partial class UC_KH_ThanhToanHoaDon : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_ThanhToanHoaDon()
        {
            InitializeComponent();

            this.Load += (s, e) => LoadDanhSachChuaThanhToan();

            btn_KH_ThanhToan.Click -= btn_KH_ThanhToan_Click; 
            btn_KH_ThanhToan.Click += btn_KH_ThanhToan_Click; 
        }

        private void LoadDanhSachChuaThanhToan()
        {
            try
            {
                DataTable dt = dal.GetDichVuChuaThanhToan(SessionData.MaKH);

                dataGridView_KH_DSHDchuaTT.DataSource = null; 
                dataGridView_KH_DSHDchuaTT.Columns.Clear();   
                dataGridView_KH_DSHDchuaTT.AutoGenerateColumns = false; 

                DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn();
                colMa.Name = "MaLSDV";
                colMa.DataPropertyName = "MaLSDV"; 
                colMa.HeaderText = "Mã Giao Dịch";
                colMa.Width = 120;
                dataGridView_KH_DSHDchuaTT.Columns.Add(colMa);

                DataGridViewTextBoxColumn colDV = new DataGridViewTextBoxColumn();
                colDV.Name = "LoaiDichVu";
                colDV.DataPropertyName = "LoaiDichVu";
                colDV.HeaderText = "Dịch Vụ";
                colDV.Width = 150;
                dataGridView_KH_DSHDchuaTT.Columns.Add(colDV);

                DataGridViewTextBoxColumn colNgay = new DataGridViewTextBoxColumn();
                colNgay.Name = "NgayLap";
                colNgay.DataPropertyName = "NgayLap";
                colNgay.HeaderText = "Ngày Lập";
                colNgay.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; 
                colNgay.Width = 150;
                dataGridView_KH_DSHDchuaTT.Columns.Add(colNgay);

                DataGridViewTextBoxColumn colTien = new DataGridViewTextBoxColumn();
                colTien.Name = "TongTien";
                colTien.DataPropertyName = "TongTien";
                colTien.HeaderText = "Tổng Tiền";
                colTien.DefaultCellStyle.Format = "N0"; 
                colTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colTien.Width = 120;
                dataGridView_KH_DSHDchuaTT.Columns.Add(colTien);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView_KH_DSHDchuaTT.DataSource = dt;
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void btn_KH_ThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_KH_DSHDchuaTT.CurrentRow == null || dataGridView_KH_DSHDchuaTT.CurrentRow.Index < 0)
                {
                    MessageBox.Show("Vui lòng chọn một dòng hóa đơn để thanh toán!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dataGridView_KH_DSHDchuaTT.CurrentRow;

                if (row.Cells["MaLSDV"].Value == null) return;

                string maLSGD = row.Cells["MaLSDV"].Value.ToString();

                int tongTien = 0;
                if (row.Cells["TongTien"].Value != null)
                {
                    int.TryParse(row.Cells["TongTien"].Value.ToString(), out tongTien);
                }

                MessageBox.Show("Mã đang chọn là: " + maLSGD);

                UC_KH_ThanhToan ucXacNhan = new UC_KH_ThanhToan(maLSGD, tongTien);

                ucXacNhan.QuayVeTrangChu += () => {
                    if (pnl_KH_Content != null)
                        pnl_KH_Content.Controls.Remove(ucXacNhan);

                    ucXacNhan.Dispose();

                    if (pnl_KH_ThanhToanHoaDon != null)
                        pnl_KH_ThanhToanHoaDon.Visible = true;

                    LoadDanhSachChuaThanhToan();
                };

                if (pnl_KH_ThanhToanHoaDon != null)
                    pnl_KH_ThanhToanHoaDon.Visible = false;

                if (pnl_KH_Content != null)
                {
                    pnl_KH_Content.Controls.Add(ucXacNhan);
                    ucXacNhan.Dock = DockStyle.Fill;
                    ucXacNhan.BringToFront();
                }
                else
                {
                    MessageBox.Show("Lỗi giao diện: Không tìm thấy pnl_KH_Content");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển trang: " + ex.Message);
            }
        }

        private void dataGridView_KH_ChuaTT_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void btn_KH_XemLSHoaDon_Click(object sender, EventArgs e)
        {
            UC_KH_LSHoaDon UC_LSHD = new UC_KH_LSHoaDon();

            UC_LSHD.QuayVeTrangChu += () => {
                pnl_KH_Content.Controls.Remove(UC_LSHD);
                UC_LSHD.Dispose();
                pnl_KH_ThanhToanHoaDon.Visible = true;
            };

            pnl_KH_ThanhToanHoaDon.Visible = false;
            pnl_KH_Content.Controls.Add(UC_LSHD);
            UC_LSHD.Left = (pnl_KH_Content.Width - UC_LSHD.Width) / 2;
            UC_LSHD.Top = (pnl_KH_Content.Height - UC_LSHD.Height) / 2;
            UC_LSHD.BringToFront();
        }

    }
}