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

namespace PetCare
{
    public partial class All_ThongTinCaNhan_UC : UserControl
    {
        DataConnection dc = new DataConnection();

        public All_ThongTinCaNhan_UC()
        {
            InitializeComponent();
        }

        private void All_ThongTinCaNhan_UC_Load(object sender, EventArgs e)
        {
            if (SessionData.ID_TK > 0)
            {
                ThietLapTrangThaiBanDau();
                LoadDataByRole();
            }
        }

        private void ThietLapTrangThaiBanDau()
        {
            tbox_ALL_TTCN_HT.ReadOnly = true;
            txt_ALL_TTCN_SDT.ReadOnly = true;
            txt_ALL_TTCN_Email.ReadOnly = true;
            txt_ALL_TTCN_GT.ReadOnly = true;
            dateTime_ALL_TTCN_NS.Enabled = false;

            btn_TTCN_Luu.Enabled = false;
            btn_ALL_TTCN_Huy.Enabled = false;
            btn_ALL_TTCN_CN.Visible = true;
            btn_ALL_TTCN_CN.Enabled = true;

            string role = SessionData.Quyen;

            if (role == "Khách hàng" || role == "Hội viên")
            {
                label_TTCN_SDT.Text = "Số điện thoại:";
                label_TTCN_Email.Text = "Email:";
                label_TTCN_GioiTinh.Text = "Giới tính:";

                txt_ALL_TTCN_SDT.Visible = true;
            }
            else 
            {
                label_TTCN_SDT.Text = "Chi nhánh:";
                txt_ALL_TTCN_SDT.Visible = true; 

                label_TTCN_Email.Text = "Chức vụ:";
                label_TTCN_GioiTinh.Text = "Lương:";
            }
        }

        private void MoKhoaDeChinhSua()
        {
            string role = SessionData.Quyen;

            tbox_ALL_TTCN_HT.ReadOnly = false;
            dateTime_ALL_TTCN_NS.Enabled = true;

            if (role == "Khách hàng" || role == "Hội viên")
            {
                txt_ALL_TTCN_SDT.ReadOnly = false;
                txt_ALL_TTCN_Email.ReadOnly = false;
                txt_ALL_TTCN_GT.ReadOnly = false;
            }
            else
            {
                txt_ALL_TTCN_SDT.ReadOnly = true;   
                txt_ALL_TTCN_Email.ReadOnly = true; 
                txt_ALL_TTCN_GT.ReadOnly = true;    
            }

            btn_TTCN_Luu.Enabled = true;
            btn_ALL_TTCN_Huy.Enabled = true;
            btn_ALL_TTCN_CN.Enabled = false;
        }
        private void LoadDataByRole()
        {
            string role = SessionData.Quyen;
            string idCanTim = "";
            string sql = "";

            if (role == "Khách hàng" || role == "Hội viên")
            {
                idCanTim = SessionData.MaKH;
                sql = "SELECT HoTen_KH, SDT_KH, NgaySinh_KH, GioiTinh_KH, Email_KH FROM KHACHHANG WHERE MaKH = @Ma";
            }
            else
            {
                idCanTim = SessionData.MaNV;
                sql = @"SELECT NV.HoTenNV, NV.NgaySinhNV, NV.ChucVu, NV.Luong, CN.TenCN 
                        FROM NHANVIEN NV 
                        LEFT JOIN CHINHANH CN ON NV.ChiNhanhLamViec = CN.MaCN 
                        WHERE NV.MaNV = @Ma";
            }

            try
            {
                if (string.IsNullOrEmpty(idCanTim)) return;

                SqlParameter[] p = { new SqlParameter("@Ma", idCanTim) };
                DataTable dt = dc.ExecuteQuery(sql, p);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    if (role == "Khách hàng" || role == "Hội viên")
                    {
                        tbox_ALL_TTCN_HT.Text = row["HoTen_KH"].ToString();
                        txt_ALL_TTCN_SDT.Text = row["SDT_KH"].ToString();
                        txt_ALL_TTCN_Email.Text = row["Email_KH"].ToString();
                        txt_ALL_TTCN_GT.Text = row["GioiTinh_KH"].ToString();
                        if (row["NgaySinh_KH"] != DBNull.Value)
                            dateTime_ALL_TTCN_NS.Value = Convert.ToDateTime(row["NgaySinh_KH"]);
                    }
                    else
                    {
                        tbox_ALL_TTCN_HT.Text = row["HoTenNV"].ToString();
                        if (row["NgaySinhNV"] != DBNull.Value)
                            dateTime_ALL_TTCN_NS.Value = Convert.ToDateTime(row["NgaySinhNV"]);

                        txt_ALL_TTCN_Email.Text = row["ChucVu"].ToString();

                        decimal luong = row["Luong"] != DBNull.Value ? Convert.ToDecimal(row["Luong"]) : 0;
                        txt_ALL_TTCN_GT.Text = luong.ToString("N0") + " VNĐ";

                        if (role == "Quản lý công ty")
                        {
                            txt_ALL_TTCN_SDT.Text = "Trụ sở chính";
                        }
                        else
                        {
                            txt_ALL_TTCN_SDT.Text = row["TenCN"] != DBNull.Value ? row["TenCN"].ToString() : "Chưa phân công";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        private void btn_ALL_TTCN_CN_Click(object sender, EventArgs e)
        {
            MoKhoaDeChinhSua();
        }

        private void btn_ALL_TTCN_Huy_Click(object sender, EventArgs e)
        {
            ThietLapTrangThaiBanDau();
            LoadDataByRole();
        }

        private void btn_TTCN_Luu_Click(object sender, EventArgs e)
        {
            string role = SessionData.Quyen;
            string hoTen = tbox_ALL_TTCN_HT.Text.Trim();
            DateTime ngaySinh = dateTime_ALL_TTCN_NS.Value;

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống!");
                return;
            }

            try
            {
                string sqlUpdate = "";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@MaID", (role == "Khách hàng" || role == "Hội viên") ? SessionData.MaKH : SessionData.MaNV));
                parameters.Add(new SqlParameter("@Ten", hoTen));
                parameters.Add(new SqlParameter("@NS", ngaySinh));

                if (role == "Khách hàng" || role == "Hội viên")
                {
                    string sdt = txt_ALL_TTCN_SDT.Text.Trim();
                    string email = txt_ALL_TTCN_Email.Text.Trim();
                    string gioiTinh = txt_ALL_TTCN_GT.Text.Trim();

                    sqlUpdate = "UPDATE KHACHHANG SET HoTen_KH = @Ten, SDT_KH = @SDT, Email_KH = @Email, GioiTinh_KH = @GT, NgaySinh_KH = @NS WHERE MaKH = @MaID";

                    parameters.Add(new SqlParameter("@SDT", sdt));
                    parameters.Add(new SqlParameter("@Email", email));
                    parameters.Add(new SqlParameter("@GT", gioiTinh));
                }
                else
                {
                    sqlUpdate = "UPDATE NHANVIEN SET HoTenNV = @Ten, NgaySinhNV = @NS WHERE MaNV = @MaID";
                }

                dc.ExecuteQuery(sqlUpdate, parameters.ToArray());

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ThietLapTrangThaiBanDau();
                LoadDataByRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btn_BS_QLHSK_THS_Click(object sender, EventArgs e)
        {
            if (pnl_KH_Content != null)
            {
                UC_KH_DoiMatKhau UC_DoiMK = new UC_KH_DoiMatKhau();
                UC_DoiMK.Dock = DockStyle.Fill;
                UC_DoiMK.OnBack += () =>
                {
                    pnl_KH_Content.Controls.Remove(UC_DoiMK);
                    LoadDataByRole();
                };
                pnl_KH_Content.Controls.Add(UC_DoiMK);
                UC_DoiMK.BringToFront();
            }
        }
        private void tbox_ALL_TTCN_HT_TextChanged(object sender, EventArgs e) { }
        private void txt_ALL_TTCN_SDT_TextChanged(object sender, EventArgs e) { }
        private void dateTime_ALL_TTCN_NS_ValueChanged(object sender, EventArgs e) { }
        private void txt_ALL_TTCN_GT_TextChanged(object sender, EventArgs e) { }
        private void txt_ALL_TTCN_Email_TextChanged(object sender, EventArgs e) { }
        private void pnl_BS_QLHSKB_Paint(object sender, PaintEventArgs e) { }
        private void lab_BS_HSKB_MaTC_Click(object sender, EventArgs e) { }
        private void label_TTCN_SDT_Click(object sender, EventArgs e) { }
        private void label_TTCN_NgaySinh_Click(object sender, EventArgs e) { }
        private void label_TTCN_GioiTinh_Click(object sender, EventArgs e) { }
        private void label_TTCN_Email_Click(object sender, EventArgs e) { }
        private void label_TTCN_SDT_Click_1(object sender, EventArgs e) { }
    }
}