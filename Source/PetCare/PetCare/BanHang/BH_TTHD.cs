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
    public partial class BH_TTHD : UserControl
    {
        DataConnection dc = new DataConnection();

        public BH_TTHD()
        {
            InitializeComponent();
        }

        private void tbox_TTHD_NVL_TextChanged(object sender, EventArgs e)
        {

        }

        private void BH_TTHD_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LuuMaHoaDon.MaHDVuaLap))
            {
                tbox_BH_TTHD_MaHD.Text = LuuMaHoaDon.MaHDVuaLap;
                LoadThongTinThanhToan(LuuMaHoaDon.MaHDVuaLap);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadThongTinThanhToan(string maHD)
        {
            // Câu lệnh SQL lấy thông tin tổng hợp
            string sql = @"SELECT h.MaKH, kh.Loai_KH, h.NV_Lap, h.NgayLap, h.TienTruocKM, 
                      sp.MaSP, sp.TenSP, ct.SoLuongSP, ct.ThanhTienMH
               FROM HOADON h
               JOIN KHACHHANG kh ON h.MaKH = kh.MaKH -- PHẢI CÓ DÒNG NÀY ĐỂ LẤY Loai_KH
               JOIN CT_HOADON cthd ON h.MaHD = cthd.MaHD
               JOIN CT_MUAHANG ct ON cthd.MaLSGD = ct.MaLSDVMH
               JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
               WHERE h.MaHD = @MaHD";

            System.Data.SqlClient.SqlParameter[] p = { new System.Data.SqlClient.SqlParameter("@MaHD", maHD) };
            DataTable dt = dc.ExecuteQuery(sql, p);

            if (dt.Rows.Count > 0)
            {
                // 1. Điền thông tin chung vào các TextBox
                tbox_BH_TTHD_MaHD.Text = maHD;
                tbox_BH_TTHD_MKH.Text = dt.Rows[0]["MaKH"].ToString();
                tbox_BH_TTHD_MKH.Tag = dt.Rows[0]["Loai_KH"].ToString();
                tbox_TTHD_NVL.Text = dt.Rows[0]["NV_Lap"].ToString();
                txtBox_BH_TTHD_TTKM.Text = Convert.ToDecimal(dt.Rows[0]["TienTruocKM"]).ToString("N0");
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["NgayLap"]);

                // 2. Đổ danh sách sản phẩm vào DataGridView
                data_BH_TTHD.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin hóa đơn!");
            }
        }

        private void BH_ThanhToan_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                // Kiểm tra xem biến tĩnh có đang giữ mã hóa đơn nào không
                if (!string.IsNullOrEmpty(LuuMaHoaDon.MaHDVuaLap))
                {
                    tbox_BH_TTHD_MaHD.Text = LuuMaHoaDon.MaHDVuaLap;

                    LoadThongTinThanhToan(LuuMaHoaDon.MaHDVuaLap);

                    LuuMaHoaDon.MaHDVuaLap = ""; 
                }
            }
        }

        private void tbox_BH_TTHD_MaHD_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BH_TTHD_MKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void data_BH_TTHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBox_BH_TTHD_TTKM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBox_BH_TTHD_TSKM_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBox_BH_TTHD_SDCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_BH_TTHD_ADKM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox_BH_TTHD_TTKM.Text)) return;

            double tienGoc = double.Parse(txtBox_BH_TTHD_TTKM.Text.Replace(",", ""));
            double tienGiam = 0;
            string loaiKH = tbox_BH_TTHD_MKH.Tag?.ToString() ?? "Thường";

            // Ví dụ: Hội viên giảm 5%, khách thường không giảm
            if (loaiKH == "Hội viên")
            {
                tienGiam = tienGoc * 0.05;
                MessageBox.Show("Khách hàng là Hội viên: Giảm 5%");
            }
            else
            {
                MessageBox.Show("Khách hàng thường: Không áp dụng giảm giá");
            }

            double tienSauGiam = tienGoc - tienGiam;
            txtBox_BH_TTHD_TSKM.Text = tienSauGiam.ToString("N0");
        }

        private void btn_BH_TTHD_TT_Click(object sender, EventArgs e)
        {
            if (cbox_BH_TTHD.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán!");
                return;
            }

            try
            {
                string sql = @"UPDATE HOADON 
                       SET TrangThaiHD = N'Đã thanh toán', 
                           TienThanhToan = @tienTT, 
                           HinhThucPay = @payMethod 
                       WHERE MaHD = @maHD";

                SqlParameter[] p = {
            new SqlParameter("@maHD", tbox_BH_TTHD_MaHD.Text),
            new SqlParameter("@tienTT", double.Parse(txtBox_BH_TTHD_TSKM.Text.Replace(",", ""))),
            new SqlParameter("@payMethod", cbox_BH_TTHD.SelectedItem.ToString())
        };

                dc.ExecuteNonQuery(sql, p);
                MessageBox.Show("Thanh toán thành công bằng: " + cbox_BH_TTHD.SelectedItem.ToString());

                XoaForm();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void XoaForm()
        {
            tbox_BH_TTHD_MaHD.Clear();
            tbox_BH_TTHD_MKH.Clear();
            tbox_TTHD_NVL.Clear();

            txtBox_BH_TTHD_TTKM.Clear();
            txtBox_BH_TTHD_TSKM.Clear();

            if (txtBox_BH_TTHD_SDCD != null) txtBox_BH_TTHD_SDCD.Clear();

            dateTimePicker1.Value = DateTime.Now;

            data_BH_TTHD.DataSource = null;

            // if (data_BH_TTHD.DataSource is DataTable dt) dt.Clear();

            tbox_BH_TTHD_MKH.Tag = null;
        }
    }
}
