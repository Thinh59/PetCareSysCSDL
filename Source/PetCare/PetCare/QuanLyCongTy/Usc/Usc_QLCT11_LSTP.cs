using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT11_LSTP : UserControl
    {
        private BindingList<Class_QLCT11_LSTP> danhSach = new BindingList<Class_QLCT11_LSTP>();
        private ServiceDAL service = new ServiceDAL();

        public Usc_QLCT11_LSTP()
        {
            InitializeComponent();
            dgv_QLCT11_LSTP.DataSource = danhSach;
        }

        private void Usc_QLCT11_LSTP_Load(object sender, EventArgs e)
        {
            dtp_QLCT11_getNgayTiem.Value = new DateTime(1900, 1, 1);
        }

        private void btn_QLCT11_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string maThuCung = txb_QLCT11_getMaTC.Text.Trim();

                if (string.IsNullOrEmpty(maThuCung))
                {
                    MessageBox.Show("Vui lòng nhập Mã Thú Cưng để xem lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime? ngayTiem = null;
                if (dtp_QLCT11_getNgayTiem.Value.Year != 1900)
                {
                    ngayTiem = dtp_QLCT11_getNgayTiem.Value;
                }

                DataTable dt = service.GetLSTiemPhongByPet(maThuCung, ngayTiem);
                danhSach.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        danhSach.Add(new Class_QLCT11_LSTP
                        {
                            MaLSTP = row["MaLSTP"].ToString(),
                            BacSiPhuTrach = row["BacSiPhuTrach"].ToString(),
                            MaGoiTiem = row["MaGoiTiem"].ToString(),
                            MaVacXin = row["MaVacXin"].ToString(),
                            LieuLuong = row["LieuLuong"].ToString(),
                            NgayTiem = Convert.ToDateTime(row["NgayTiem"]),
                            MaThuCung = row["MaThuCung"].ToString()
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy lịch sử tiêm phòng nào cho thú cưng này với bộ lọc hiện tại.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_QLCT3_MaCN_Click(object sender, EventArgs e) { }
        private void txb_QLCT3_getMaCN_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void lbl_QLCT11_NgayTiem_Click(object sender, EventArgs e) { }
        private void Usc_QLCT11_LSTP_Load_1(object sender, EventArgs e) { }
    }
}