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
using System.Diagnostics;

namespace PetCare
{
    public partial class BS_LSK : UserControl
    {
        DataConnection dc = new DataConnection();

        public BS_LSK()
        {
            InitializeComponent();
            data_BS_LSK.AutoGenerateColumns = false;
            data_BS_LSTP.AutoGenerateColumns = false;
        }

        private void lab_BS_LSK_MaTC_Click(object sender, EventArgs e)
        {

        }

        private void tbox_BS_LSK_MaTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void data_BS_LSTP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void data_BS_LSK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_BS_LSK_Click(object sender, EventArgs e)
        {
            string maTC = tbox_BS_LSK_MaTC.Text.Trim();

            if (string.IsNullOrEmpty(maTC))
            {
                MessageBox.Show("Vui lòng nhập Mã Thú Cưng!", "Thông báo");
                return;
            }

            LoadDuLieu(maTC);
        }

        private void LoadDuLieu(string maTC)
        {
            try
            {
                // --- BẮT ĐẦU ĐO ---
                Stopwatch timer = new Stopwatch();
                timer.Start();

                // 1. Thực thi Procedure (Đo cả 2 truy vấn vì đây là chức năng tổng hợp)
                SqlParameter[] parameters = { new SqlParameter("@MaThuCung", maTC) };
                DataTable dtKhamBenh = dc.ExecuteProcedure("sp_GetLichSuKhamBenh", parameters);

                SqlParameter[] parameters2 = { new SqlParameter("@MaThuCung", maTC) };
                DataTable dtTiemPhong = dc.ExecuteProcedure("sp_GetLichSuTiemPhong", parameters2);

                // --- KẾT THÚC ĐO ---
                timer.Stop();
                long executionTime = timer.ElapsedMilliseconds;

                // Hiển thị thời gian lên TextBox (txb_BS8_Time)
                if (txb_BS8_Time != null)
                {
                    txb_BS8_Time.Text = executionTime.ToString() + " ms";

                    if (executionTime < 100)
                    {
                        txb_BS8_Time.ForeColor = Color.Green;
                        txb_BS8_Time.BackColor = Color.FromArgb(220, 255, 220);
                    }
                    else
                    {
                        txb_BS8_Time.ForeColor = Color.Red;
                        txb_BS8_Time.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }

                // 2. Gán dữ liệu và Cấu hình hiển thị nhiều dòng
                SetupDataGridView(data_BS_LSK);
                data_BS_LSK.DataSource = dtKhamBenh;

                SetupDataGridView(data_BS_LSTP);
                data_BS_LSTP.DataSource = dtTiemPhong;

                // 3. Kiểm tra dữ liệu
                if (dtKhamBenh.Rows.Count == 0 && dtTiemPhong.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy lịch sử cho Mã TC này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép xuống dòng
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Tự giãn cao hàng
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft; // Căn lề trên để dễ đọc
        }

        private void lab_BS_LSK_Click(object sender, EventArgs e)
        {

        }

        private void pnl_BS_LSK_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BS_LSK_Load(object sender, EventArgs e)
        {

        }

        private void txb_BS8_Time_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
