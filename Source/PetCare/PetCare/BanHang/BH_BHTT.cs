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
using static System.Collections.Specialized.BitVector32;

namespace PetCare
{
    public partial class BH_BHTT : UserControl
    {

        DataConnection dc = new DataConnection();
        DataTable dtGioHang = new DataTable();

        public BH_BHTT()
        {
            InitializeComponent();
            SetupGioHang();
        }

        private void SetupGioHang()
        {
            // Cấu hình DataTable làm giỏ hàng tạm
            dtGioHang.Columns.Add("MaSP");
            dtGioHang.Columns.Add("TenSP");
            dtGioHang.Columns.Add("SoLuong", typeof(int));
            dtGioHang.Columns.Add("TTien", typeof(decimal));

            data_BH_BHTT.DataSource = dtGioHang;
            data_BH_BHTT.AutoGenerateColumns = false;
        }

        private void BH_BHTT_Load(object sender, EventArgs e)
        {

        }

        private void tbox_BH_BHTT_SP_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_BH_BHTT_LHD_Click(object sender, EventArgs e)
        {
            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }

            try
            {
                // Chuẩn bị TVP để gửi danh sách SP vào SQL
                DataTable tvp = new DataTable();
                tvp.Columns.Add("MaSP", typeof(string));
                tvp.Columns.Add("SoLuong", typeof(int));
                foreach (DataRow r in dtGioHang.Rows) tvp.Rows.Add(r["MaSP"], r["SoLuong"]);

                SqlParameter[] p = {
                    new SqlParameter("@MaKH", txtBox_BH_BHTT_MKH.Text.Trim()),
                    new SqlParameter("@MaNV", SessionData.MaNV),
                    new SqlParameter("@MaCN", SessionData.MaCN),
                    new SqlParameter("@Items", tvp) { TypeName = "dbo.TVP_SaleItems" }
                };

                // Gọi Procedure lập hóa đơn (Phí ship mặc định 0)
                DataTable res = dc.ExecuteProcedure("sp_LapHoaDonTaiQuay", p);

                if (res.Rows.Count > 0)
                {
                    string maHD = res.Rows[0]["GeneratedMaHD"].ToString();
                    LuuMaHoaDon.MaHDVuaLap = maHD;
                    MessageBox.Show("Lập hóa đơn thành công: " + maHD);

                    // Xóa giỏ hàng để tiếp tục khách mới
                    dtGioHang.Clear();
                    BH_BHTT_TT.Text = "0";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lập hóa đơn: " + ex.Message); }
        }

        private void data_BH_BHTT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BH_BHTT_TT_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_BH_BHTT_SL_TextChanged(object sender, EventArgs e)
        {

        }

        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataRow r in dtGioHang.Rows) tong += Convert.ToDecimal(r["TTien"]);

            BH_BHTT_TT.Text = tong.ToString("N0");
        }

        private void btn_BH_BHTT_SP_Click(object sender, EventArgs e)
        {
            string maSP = tbox_BH_BHTT_SP.Text.Trim();
            int sl;
            if (!int.TryParse(tbox_BH_BHTT_SL.Text, out sl) || sl <= 0) return;

            string sql = "SELECT s.TenSP, ISNULL(ct.GiaSPCN, s.GiaBan) as Gia " +
                         "FROM SANPHAM s LEFT JOIN CT_SPCN ct ON s.MaSP = ct.MaSP " +
                         "WHERE s.MaSP = @MaSP AND ct.MaCN = @MaCN";

            SqlParameter[] p = {
        new SqlParameter("@MaSP", maSP),
        new SqlParameter("@MaCN", SessionData.MaCN)
    };

            try
            {
                // Kiểm tra xem dc có null không trước khi gọi
                if (dc == null) dc = new DataConnection();

                DataTable dtCheck = dc.ExecuteQuery(sql, p);

                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    decimal gia = Convert.ToDecimal(dtCheck.Rows[0]["Gia"]);
                    string ten = dtCheck.Rows[0]["TenSP"].ToString();
                    dtGioHang.Rows.Add(maSP, ten, sl, gia * sl);
                    TinhTongTien();
                }
                else
                {
                    MessageBox.Show("Sản phẩm không tồn tại hoặc chưa được nhập vào kho chi nhánh này!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }

        private void txtBox_BH_BHTT_MKH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
