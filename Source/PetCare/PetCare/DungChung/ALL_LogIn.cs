using PetCare.HoiVien;
using PetCare.KhachHang;
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
    public partial class ALL_LogIn : Form
    {
        DataConnection dc = new DataConnection();

        public ALL_LogIn()
        {
            InitializeComponent();
            this.AcceptButton = btn_ALL_LI; 
            this.FormClosed += LogIn_FormClosed;
        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btn_ALL_LI_Click(object sender, EventArgs e)
        {
            string user = tbox_ALL_LI_TDN.Text.Trim();
            string pass = tbox_ALL_LI_MK.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            bool cheDoToiUu = checkBox_ALL1_DM.Checked; 

            DataConnection.ChuyenCheDoKetNoi(cheDoToiUu);

            if (cheDoToiUu)
                this.Text = "Đăng nhập - [MODE: OPTIMIZED]";
            else
                this.Text = "Đăng nhập - [MODE: NORMAL]";

            string query = "SELECT ID_TK, LoaiTK FROM TAIKHOAN WHERE TenDangNhap = @user AND MatKhau = @pass";
            SqlParameter[] p = {
                new SqlParameter("@user", user),
                new SqlParameter("@pass", pass)
            };

            DataTable dt = dc.ExecuteQuery(query, p); 
            if (dt.Rows.Count > 0)
            {
                SessionData.Clear();

                int idTK = Convert.ToInt32(dt.Rows[0]["ID_TK"]);
                string loaiTK = dt.Rows[0]["LoaiTK"].ToString();

                SessionData.ID_TK = idTK;
                SessionData.Quyen = loaiTK;


                if (loaiTK == "Khách hàng" || loaiTK == "Hội viên")
                {
                    string sqlKH = "SELECT MaKH, HoTen_KH FROM KHACHHANG WHERE ID_TK = @id";
                    DataTable dtKH = dc.ExecuteQuery(sqlKH, new SqlParameter[] { new SqlParameter("@id", idTK) });

                    if (dtKH.Rows.Count > 0)
                    {
                        SessionData.MaKH = dtKH.Rows[0]["MaKH"].ToString();
                        SessionData.TenHienThi = dtKH.Rows[0]["HoTen_KH"].ToString();
                        SessionData.MaNV = null; 
                        SessionData.MaCN = null; 
                    }
                }
                else
                {
                    string sqlNV = @"SELECT nv.MaNV, nv.HoTenNV, nv.ChiNhanhLamViec, cn.TenCN 
                                     FROM NHANVIEN nv 
                                     JOIN CHINHANH cn ON nv.ChiNhanhLamViec = cn.MaCN
                                     WHERE nv.ID_TK = @id";

                    DataTable dtNV = dc.ExecuteQuery(sqlNV, new SqlParameter[] { new SqlParameter("@id", idTK) });

                    if (dtNV.Rows.Count > 0)
                    {
                        SessionData.MaNV = dtNV.Rows[0]["MaNV"].ToString();
                        SessionData.TenHienThi = dtNV.Rows[0]["HoTenNV"].ToString();
                        SessionData.MaCN = dtNV.Rows[0]["ChiNhanhLamViec"].ToString();
                        SessionData.TenCN = dtNV.Rows[0]["TenCN"].ToString();
                        SessionData.MaKH = null; 
                    }
                }

                MessageBox.Show($"Chào mừng {SessionData.TenHienThi} đăng nhập thành công!");

                MoGiaoDienTheoQuyen(loaiTK, idTK);

                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }

        private void MoGiaoDienTheoQuyen(string loai, int id)
        {
            switch (loai)
            {
                case "Bán hàng":
                    BH_main banHang = new BH_main(id);
                    banHang.Show();
                    break;

                case "Bác sĩ":
                    BacSi_main bs = new BacSi_main(id);
                    bs.Show();
                    break;

                case "Tiếp tân":
                    TT_main tt = new TT_main(id);
                    tt.Show();
                    break;

                case "Quản lý chi nhánh":
                    QLCN_main qlcn = new QLCN_main(id);
                    qlcn.Show();
                    break;

                case "Khách hàng":
                    KH_main kh = new KH_main(id);
                    kh.Show();
                    break;

                case "Hội viên":
                    HV_main hv = new HV_main(id);
                    hv.Show();
                    break;

                case "Quản lý công ty":
                    QLCT_main qlct = new QLCT_main(id);
                    qlct.Show();
                    break;

                case "Quản trị viên":
                    QTV_main qtv = new QTV_main(id);
                    qtv.Show();
                    break;


                default:
                    MessageBox.Show("Chức năng cho quyền này chưa được phát triển.");
                    this.Show(); 
                    break;
            }
        }

        private void linkLab_ALL_LI_QMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ Quản trị viên để cấp lại mật khẩu!");
        }

        private void lab_ALL_TDN_Click(object sender, EventArgs e) { }
        private void tbox_ALL_LI_TDN_TextChanged(object sender, EventArgs e) { }
        private void tbox_ALL_LI_MK_TextChanged(object sender, EventArgs e) { }
        private void btn_ALL_LI_QMK_Click(object sender, EventArgs e) { }

        private void checkBox_ALL1_DM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ALL1_DM.Checked)
                this.Text = "PetCare System [OPTIMIZED DB]";
            else
                this.Text = "PetCare System [NORMAL DB]";
        }
    }
}