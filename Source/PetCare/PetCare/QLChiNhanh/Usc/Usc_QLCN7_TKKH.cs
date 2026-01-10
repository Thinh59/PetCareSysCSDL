using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCN7_TKKH : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        private BindingList<Class_QLCN7_DSKH> danhSachKH = new BindingList<Class_QLCN7_DSKH>();

        public Usc_QLCN7_TKKH()
        {
            InitializeComponent();

            // Setup Grid
            dgv_QLCN7_TKKH.AutoGenerateColumns = true;
            dgv_QLCN7_TKKH.DataSource = danhSachKH;

            this.Load += Usc_QLCN7_TKKH_Load;
        }

        private void Usc_QLCN7_TKKH_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            dtp_QLCN7_TuNgay.Value = new DateTime(today.Year, today.Month, 1);
            dtp_QLCN7_DenNgay.Value = today;

            btn_QLCN7_KHMoi_Click(sender, e);
        }

        private void ThucHienThongKe(string loaiTK, string tieuDe)
        {
            DateTime tuNgay = dtp_QLCN7_TuNgay.Value.Date;
            DateTime denNgay = dtp_QLCN7_DenNgay.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            try
            {
                DataTable dt = serviceDAL.GetThongKeKhachHang(tuNgay, denNgay, loaiTK);

                danhSachKH.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    danhSachKH.Add(new Class_QLCN7_DSKH
                    {
                        MaKH = row["MaKH"].ToString(),
                        HoTen = row["HoTen"].ToString(),
                        SDT = row["SDT"].ToString(),
                        LoaiKH = row["LoaiKH"].ToString(),
                        NgayGiaoDichGanNhat = Convert.ToDateTime(row["NgayGiaoDichGanNhat"]),
                        TongChiTieu = Convert.ToDecimal(row["TongChiTieu"])
                    });
                }

                txtBox_QLCN7_TongKH.Text = danhSachKH.Count.ToString();

                if (dgv_QLCN7_TKKH.Columns["TongChiTieu"] != null)
                {
                    dgv_QLCN7_TKKH.Columns["TongChiTieu"].DefaultCellStyle.Format = "N0";
                    dgv_QLCN7_TKKH.Columns["TongChiTieu"].HeaderText = "Tổng Chi Tiêu (VNĐ)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void btn_QLCN7_KHMoi_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("NEW", "Danh sách Khách hàng Mới");
        }

        private void btn_QLCN7_KHCu_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("OLD", "Danh sách Khách hàng Quay lại");
        }

        private void btn_QLCN7_KHVip_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("VIP", "Danh sách Khách hàng VIP");
        }

        private void btn_QLCN7_KHChuaQL_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("LOST", "Danh sách Khách hàng Lâu chưa quay lại");
        }

        private void btn_QLCN7_ThongKe_Click(object sender, EventArgs e)
        {
            ThucHienThongKe("ALL", "Thống kê tất cả khách hàng trong khoảng thời gian");
        }

        private void btn_QLCN7_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

               this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }

        private void dtp_QLCN7_TuNgay_ValueChanged(object sender, EventArgs e) { }
        private void dtp_QLCN7_DenNgay_ValueChanged(object sender, EventArgs e) { }
        private void txtBox_QLCN7_TongKH_TextChanged(object sender, EventArgs e) { }
        private void dgv_QLCN7_TKKH_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}