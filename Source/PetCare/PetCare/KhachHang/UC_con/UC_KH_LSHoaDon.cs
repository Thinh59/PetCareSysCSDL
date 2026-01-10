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
    public partial class UC_KH_LSHoaDon : UserControl, IReturnToMainPage
    {
        ServiceDAL dal = new ServiceDAL();

        public UC_KH_LSHoaDon()
        {
            InitializeComponent();
            // Nạp dữ liệu ngay khi UC được load
            this.Load += UC_KH_LSHoaDon_Load;
        }

        private void UC_KH_LSHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData(string maHDFilter = null, DateTime? ngayFilter = null, string trangThaiFilter = null)
        {
            try
            {
                dataGridView_KH_DSHD.AutoGenerateColumns = false;

                // Gọi DAL lấy dữ liệu
                DataTable dt = dal.GetLichSuHoaDon(SessionData.MaKH, maHDFilter, ngayFilter, trangThaiFilter);

                dataGridView_KH_DSHD.DataSource = dt;

                // Ánh xạ các cột dựa trên thiết kế Designer
                dataGridView_KH_DSHD.Columns["MaHD"].DataPropertyName = "MaHD";
                dataGridView_KH_DSHD.Columns["NgayLap"].DataPropertyName = "NgayLap";
                dataGridView_KH_DSHD.Columns["NV_Lap"].DataPropertyName = "NV_Lap";
                dataGridView_KH_DSHD.Columns["TongTien"].DataPropertyName = "TongTien";
                dataGridView_KH_DSHD.Columns["TrangThai"].DataPropertyName = "TrangThai";

                dataGridView_KH_DSHD.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message);
            }
        }

        private void btn_KH_TimKiem_Click(object sender, EventArgs e)
        {
            string keyword = textBox_KH_TimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
            }
            else
            {
                LoadData(maHDFilter: keyword);
            }
        }

        private void btn_ApDung_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều kiện lọc
            string maHDFilter = textBox_KH_LocMa.Text.Trim();
            DateTime? ngayFilter = dateTimePicker_KH_LocNgay.Value.Date;

            // Xử lý ComboBox Trạng thái
            string trangThaiFilter = comboBox_KH_LocDV.SelectedItem?.ToString();
            if (trangThaiFilter == "Tất cả" || string.IsNullOrEmpty(trangThaiFilter))
                trangThaiFilter = null;

            LoadData(maHDFilter, ngayFilter, trangThaiFilter);
        }

        private void dataGridView_KH_DSHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_KH_DSHD.Columns[e.ColumnIndex].Name == "XemChiTiet" && e.RowIndex >= 0)
            {
                string maHD = dataGridView_KH_DSHD.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();

                if (!string.IsNullOrEmpty(maHD))
                {
                    UC_CTHD = new UC_KH_ChiTietHoaDon(maHD);
                    UC_CTHD.QuayVeTrangChu += UcXemChiTiet_QuayVeTrangChu;

                    // Ẩn thành phần cũ
                    dataGridView_KH_DSHD.Visible = false;
                    btn_KH_QuayLai.Visible = false;
                    lb_KH_DanhSachHD.Visible = false;

                    pnl_KH_LSHoaDon.Controls.Add(UC_CTHD);
                    UC_CTHD.Left = (pnl_KH_LSHoaDon.Width - UC_CTHD.Width) / 2;
                    UC_CTHD.Top = (pnl_KH_LSHoaDon.Height - UC_CTHD.Height) / 2;
                    UC_CTHD.BringToFront();
                }
            }
        }

        private UC_KH_ChiTietHoaDon UC_CTHD = null;
        private void UcXemChiTiet_QuayVeTrangChu()
        {
            if (UC_CTHD != null)
            {
                pnl_KH_LSHoaDon.Controls.Remove(UC_CTHD);
                UC_CTHD.QuayVeTrangChu -= UcXemChiTiet_QuayVeTrangChu;
                UC_CTHD.Dispose();
                UC_CTHD = null;

                dataGridView_KH_DSHD.Visible = true;
                btn_KH_QuayLai.Visible = true;
                lb_KH_DanhSachHD.Visible = true;
            }
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
