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
    public partial class UC_KH_ChiTietHoaDon : UserControl, IReturnToMainPage
    {
        private string _maHD;
        ServiceDAL dal = new ServiceDAL();
        public event Action QuayVeTrangChu;

        public UC_KH_ChiTietHoaDon(string maHD)
        {
            InitializeComponent();
            _maHD = maHD;
            this.Load += (s, e) => LoadChiTiet();
        }

        private void LoadChiTiet()
        {
            try
            {
                DataSet ds = dal.GetChiTietHoaDon(_maHD);
                if (ds != null && ds.Tables.Count >= 3)
                {
                    // 1. Đổ dữ liệu Header (Table 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        textBox_KH_CTHoaDon.Text = row["MaHD"].ToString();
                        textBox_KH_NgayLap.Text = Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy");
                        textBox_KH_DichVu.Text = row["TenDichVu"].ToString();
                        textBox_KH_TongTien.Text = string.Format("{0:N0} VNĐ", row["TongTien"]);
                        comboBox_KH_HTTT.Text = row["HinhThucPay"].ToString();
                    }

                    // 2. Đổ dữ liệu Lưới (Table 1)
                    dataGridView_KH_CTHD.AutoGenerateColumns = false;
                    // Đảm bảo DataPropertyName trong Designer khớp: SanPham, DonGia, SoLuong, ThanhTien
                    dataGridView_KH_CTHD.DataSource = ds.Tables[1];
                    dataGridView_KH_CTHD.Columns["SanPham"].DataPropertyName = "SanPham";
                    dataGridView_KH_CTHD.Columns["DonGia"].DataPropertyName = "DonGia";
                    dataGridView_KH_CTHD.Columns["SoLuong"].DataPropertyName = "SoLuong";
                    dataGridView_KH_CTHD.Columns["ThanhTien"].DataPropertyName = "ThanhTien";

                    // 3. Đổ dữ liệu Khuyến mãi (Table 2)
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        textBox_KH_KhuyenMai.Text = ds.Tables[2].Rows[0]["ChuoiKhuyenMai"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết: " + ex.Message);
            }
        }

        private void btn_KH_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
