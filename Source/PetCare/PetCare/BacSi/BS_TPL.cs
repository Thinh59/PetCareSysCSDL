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
    public partial class BS_TPL : UserControl
    {
        DataConnection dc = new DataConnection();
        public static string CurrentMaLSDV_Tiem = ""; 

        public BS_TPL()
        {
            InitializeComponent();
            this.txb_BS_MaLSDV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_BS_MaLSDV_KeyDown);
        }

        private void txb_BS_MaLSDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string inputMa = txb_BS_MaLSDV.Text.Trim();
                
                if (!string.IsNullOrEmpty(inputMa))
                {
                    LoadThongTinCaTiem(inputMa);
                }
                else
                {
                    ResetForm(); 
                }
            }
        }
        private void LoadThongTinCaTiem(string maLSDV)
        {
            try
            {
                SqlParameter[] p = { new SqlParameter("@MaLSDV", maLSDV) };
                
                DataTable dt = dc.ExecuteProcedure("sp_BS_GetInfoByMaLSDV", p);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    CurrentMaLSDV_Tiem = r["MaLSDV"].ToString(); 

                    tbox_BS_TPL_MaTC.Text = r["MaThuCung"] != DBNull.Value ? r["MaThuCung"].ToString() : "";
                    tbox_BS_TPL_MaKH.Text = r["HoTen_KH"] != DBNull.Value ? r["HoTen_KH"].ToString() : "";
                    tbox_TPL_Loai.Text = r["LoaiThuCung"] != DBNull.Value ? r["LoaiThuCung"].ToString() : "";

                    tbox_TPL_Tuoi.Text = (dt.Columns.Contains("Tuoi") && r["Tuoi"] != DBNull.Value) ? r["Tuoi"].ToString() : "";
                    tbox_TPL_CN.Text = r["TinhTrangSK"] != DBNull.Value ? r["TinhTrangSK"].ToString() : "";

                    if (dt.Columns.Contains("LoaiVacXin") && r["LoaiVacXin"] != DBNull.Value)
                        tbox_BS_TPL_VC.Text = r["LoaiVacXin"].ToString();
                    else
                        tbox_BS_TPL_VC.Clear();

                    if (dt.Columns.Contains("LieuLuong") && r["LieuLuong"] != DBNull.Value)
                         tbox_BS_TPL_L.Text = r["LieuLuong"].ToString();
                    else
                         tbox_BS_TPL_L.Clear();

                    tbox_BS_TPL_VC.Focus();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phiếu tiêm: " + maLSDV);
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                ResetForm();
            }
        }

        private void btn_BS_TPL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentMaLSDV_Tiem))
            {
                MessageBox.Show("Vui lòng chọn phiếu tiêm trước!");
                txb_BS_MaLSDV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbox_BS_TPL_VC.Text))
            {
                MessageBox.Show("Vui lòng nhập loại Vaccine!");
                tbox_BS_TPL_VC.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("Xác nhận hoàn tất ca tiêm này?\n(Hồ sơ sẽ chuyển sang Thu ngân)", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                SqlParameter[] p = {
            new SqlParameter("@MaLSDV", CurrentMaLSDV_Tiem),     // Mã LSDV (NVARCHAR)
            new SqlParameter("@MaBacSi", SessionData.MaNV),       // Mã Bác sĩ (NVARCHAR)
            new SqlParameter("@LoaiVacXin", tbox_BS_TPL_VC.Text.Trim()), // Mã Vaccine hoặc Tên
            new SqlParameter("@LieuLuong", tbox_BS_TPL_L.Text.Trim())    // Liều lượng
        };

                dc.ExecuteProcedure("sp_BS_LuuKetQuaTiem", p);

                MessageBox.Show("Lưu thành công! Bác sĩ đã rảnh tay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            CurrentMaLSDV_Tiem = "";
            txb_BS_MaLSDV.Clear();
            tbox_BS_TPL_MaTC.Clear();
            tbox_BS_TPL_MaKH.Clear();
            tbox_TPL_Loai.Clear();
            tbox_TPL_Tuoi.Clear();
            tbox_TPL_CN.Clear();
            tbox_BS_TPL_VC.Clear();
            tbox_BS_TPL_L.Clear();
            
            txb_BS_MaLSDV.Focus(); 
        }
    }
}