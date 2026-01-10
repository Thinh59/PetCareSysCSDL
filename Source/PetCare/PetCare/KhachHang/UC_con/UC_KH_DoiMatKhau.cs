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
    public partial class UC_KH_DoiMatKhau : UserControl
    {
        // 1. XÓA dòng khai báo string connectionString cũ đi
        // string connectionString = ...; // XÓA DÒNG NÀY

        // 2. Gọi class DataConnection để dùng chung kết nối
        DataConnection dc = new DataConnection();

        public event Action OnBack;

        public UC_KH_DoiMatKhau()
        {
            InitializeComponent();
            textBox_KH_MKCu.UseSystemPasswordChar = true;
            textBox_KH_MKMoi.UseSystemPasswordChar = true;
            textBox_KH_NhapLaiMK.UseSystemPasswordChar = true;
        }

        private void btn_KH_CapNhat_Click(object sender, EventArgs e)
        {
            string mkCu = textBox_KH_MKCu.Text;
            string mkMoi = textBox_KH_MKMoi.Text;
            string xacNhanMK = textBox_KH_NhapLaiMK.Text;

            // --- VALIDATION ---
            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mkMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không trùng khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mkMoi.Length < 3)
            {
                MessageBox.Show("Mật khẩu mới quá ngắn (tối thiểu 3 ký tự)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- XỬ LÝ DATABASE (SỬ DỤNG DataConnection) ---
            try
            {
                // 3. Sử dụng dc.getConnect() thay vì new SqlConnection(connectionString)
                using (SqlConnection conn = dc.getConnect())
                {
                    conn.Open();

                    // BƯỚC 1: Kiểm tra mật khẩu cũ
                    string queryCheck = "SELECT COUNT(*) FROM TAIKHOAN WHERE ID_TK = @ID AND MatKhau = @MatKhauCu";

                    using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@ID", SessionData.ID_TK);
                        cmdCheck.Parameters.AddWithValue("@MatKhauCu", mkCu);

                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // BƯỚC 2: Cập nhật mật khẩu mới
                    string queryUpdate = "UPDATE TAIKHOAN SET MatKhau = @MatKhauMoi WHERE ID_TK = @ID";

                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MatKhauMoi", mkMoi);
                        cmdUpdate.Parameters.AddWithValue("@ID", SessionData.ID_TK);

                        int rowsAffected = cmdUpdate.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearInputs();
                            // OnBack?.Invoke(); // Tự động quay lại nếu muốn
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra, không thể cập nhật mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + sqlEx.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_KH_Huy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy thao tác?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ClearInputs();
            }
        }

        private void btn_TT10_QuayLaiLST_Click(object sender, EventArgs e)
        {
            OnBack?.Invoke();
        }

        private void ClearInputs()
        {
            textBox_KH_MKCu.Clear();
            textBox_KH_MKMoi.Clear();
            textBox_KH_NhapLaiMK.Clear();
        }

        private void textBox_KH_MKCu_TextChanged(object sender, EventArgs e) { }
        private void textBox_KH_MKMoi_TextChanged(object sender, EventArgs e) { }
        private void textBox_KH_NhapLaiMK_TextChanged(object sender, EventArgs e) { }
    }
}