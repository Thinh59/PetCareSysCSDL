using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT4_XNTiemPhong : UserControl
    {
        private BindingList<Class_TT4_DSTiemPhong> danhSachTP = new BindingList<Class_TT4_DSTiemPhong>();
        private readonly ServiceDAL serviceDAL = new ServiceDAL();

        public Usc_TT4_XNTiemPhong()
        {
            InitializeComponent();

            dgv_TT4_DSTP.AutoGenerateColumns = true;
            dgv_TT4_DSTP.DataSource = danhSachTP;

            ConfigureDataGridView();

            LoadDataCombobox();
        }

        private void ConfigureDataGridView()
        {
            if (dgv_TT4_DSTP.Columns.Count > 0)
            {
                if (dgv_TT4_DSTP.Columns.Contains("MaLSDVTP"))
                {
                    dgv_TT4_DSTP.Columns["MaLSDVTP"].Visible = false;
                }

                if (dgv_TT4_DSTP.Columns.Contains("NgayDat"))
                {
                    dgv_TT4_DSTP.Columns["NgayDat"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgv_TT4_DSTP.Columns["NgayDat"].HeaderText = "Ngày Đặt";
                }

                if (dgv_TT4_DSTP.Columns.Contains("Chon"))
                    dgv_TT4_DSTP.Columns["Chon"].HeaderText = "Chọn";

                if (dgv_TT4_DSTP.Columns.Contains("MaLSDV"))
                    dgv_TT4_DSTP.Columns["MaLSDV"].HeaderText = "Mã LSDV";

                if (dgv_TT4_DSTP.Columns.Contains("MaKH"))
                    dgv_TT4_DSTP.Columns["MaKH"].HeaderText = "Mã KH";

                if (dgv_TT4_DSTP.Columns.Contains("MaThuCung"))
                    dgv_TT4_DSTP.Columns["MaThuCung"].HeaderText = "Mã TC";

                if (dgv_TT4_DSTP.Columns.Contains("TenThuCung"))
                    dgv_TT4_DSTP.Columns["TenThuCung"].HeaderText = "Tên Thú Cưng";

                if (dgv_TT4_DSTP.Columns.Contains("BacSi"))
                    dgv_TT4_DSTP.Columns["BacSi"].HeaderText = "Bác Sĩ";

                if (dgv_TT4_DSTP.Columns.Contains("LoaiVacXin"))
                    dgv_TT4_DSTP.Columns["LoaiVacXin"].HeaderText = "Loại Vắc Xin";

                if (dgv_TT4_DSTP.Columns.Contains("MaGoiTiem"))
                    dgv_TT4_DSTP.Columns["MaGoiTiem"].HeaderText = "Gói Tiêm";
            }
        }

        private void LoadDataCombobox()
        {
            try
            {
                cmb_TT4_MaBS.DataSource = null;
                cmb_TT4_MaBS.Items.Clear();

                DataTable dtBS = serviceDAL.GetAvailableDoctors();

                cmb_TT4_MaBS.DataSource = dtBS;
                cmb_TT4_MaBS.DisplayMember = "HoTenNV";
                cmb_TT4_MaBS.ValueMember = "MaNV";
                cmb_TT4_MaBS.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load bác sĩ: " + ex.Message);
            }

            cmb_TT4_TraCuu.Items.Clear();
            cmb_TT4_TraCuu.Items.Add("Tất cả");
            cmb_TT4_TraCuu.Items.Add("Mã LSDV");
            cmb_TT4_TraCuu.Items.Add("SĐT Khách");
            cmb_TT4_TraCuu.SelectedIndex = 0;

            LoadComboboxNhapMa();
        }

        private void LoadComboboxNhapMa()
        {
            cmb_TT4_NhapMa.Items.Clear();

            string loaiTraCuu = cmb_TT4_TraCuu.Text;

            try
            {
                if (loaiTraCuu == "Mã LSDV")
                {
                    DataTable dt = serviceDAL.GetAllMaLSDV_TiemPhong();
                    foreach (DataRow row in dt.Rows)
                    {
                        cmb_TT4_NhapMa.Items.Add(row["MaLSDV"].ToString());
                    }
                }
                else if (loaiTraCuu == "SĐT Khách")
                {
                    DataTable dt = serviceDAL.GetAllSDT_TiemPhong();
                    foreach (DataRow row in dt.Rows)
                    {
                        cmb_TT4_NhapMa.Items.Add(row["SDT_KH"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu tra cứu: " + ex.Message);
            }
        }

        private void btn_TT4_Tim_Click(object sender, EventArgs e)
        {
            string keyword = null;

            if (cmb_TT4_TraCuu.Text == "Tất cả")
            {
                keyword = null;
            }
            else
            {
                keyword = cmb_TT4_NhapMa.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập hoặc chọn giá trị tìm kiếm!", "Thiếu thông tin");
                    return;
                }
            }

            try
            {
                DataTable dt = serviceDAL.SearchLSDV_TiemPhong(keyword);
                danhSachTP.Clear();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy lịch tiêm phòng nào!", "Thông báo");
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    danhSachTP.Add(new Class_TT4_DSTiemPhong
                    {
                        Chon = false,
                        MaLSDV = row["MaLSDV"].ToString(),
                        MaKH = row["MaKH"].ToString(),
                        MaThuCung = row["MaThuCung"].ToString(),
                        TenThuCung = row["TenThuCung"].ToString(),
                        BacSi = row["BacSi"] != DBNull.Value ? row["BacSi"].ToString() : "",
                        LoaiVacXin = row["LoaiVacXin"].ToString(),
                        MaGoiTiem = row["MaGoiTiem"].ToString(),
                        NgayDat = Convert.ToDateTime(row["NgayDat"]),
                        MaLSDVTP = row["MaLSDVTP"].ToString()
                    });
                }

                ConfigureDataGridView();

                MessageBox.Show($"Tìm thấy {dt.Rows.Count} lịch tiêm phòng!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_TT4_TraCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComboboxNhapMa();
        }

        private void dgv_TT4_DSTP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = danhSachTP[e.RowIndex];

            CheckLichSuTiem(item.MaThuCung, item.TenThuCung);

            if (!string.IsNullOrEmpty(item.BacSi) && item.BacSi != "Chưa chỉ định")
            {
                cmb_TT4_MaBS.Text = item.BacSi;
            }
            else
            {
                cmb_TT4_MaBS.SelectedIndex = -1;
            }
        }

        private void CheckLichSuTiem(string maThuCung, string tenThuCung)
        {
            try
            {
                DataTable dtLS = serviceDAL.CheckVaccineHistory(maThuCung);
                if (dtLS.Rows.Count > 0)
                {
                    DataRow last = dtLS.Rows[0];
                    string message = $"LƯU Ý: Bé {tenThuCung} đã tiêm {last["TenVacXin"]} " +
                                   $"ngày {Convert.ToDateTime(last["NgayTiem"]):dd/MM/yyyy}\n" +
                                   $"Liều lượng: {last["LieuLuong"]}";

                    MessageBox.Show(message, "Cảnh báo lịch sử",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi check lịch sử: " + ex.Message);
            }
        }

        private void btn_TT4_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_TT4_MaBS.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Bác sĩ phụ trách!", "Thiếu thông tin",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maBacSi = cmb_TT4_MaBS.SelectedValue.ToString();

            var cacDongDuocChon = danhSachTP.Where(x => x.Chon == true).ToList();

            if (cacDongDuocChon.Count == 0) {  return; }

            DialogResult result = MessageBox.Show($"Xác nhận {cacDongDuocChon.Count} đơn?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            int countSuccess = 0;
            StringBuilder errors = new StringBuilder();

            foreach (var item in cacDongDuocChon)
            {
                try
                {
                    serviceDAL.XacNhanTiemPhong_Update(
                        item.MaLSDV,
                        item.MaLSDVTP,
                        maBacSi,
                        item.LoaiVacXin,
                        "Tiêu chuẩn",
                        DateTime.Now
                    );
                    countSuccess++;
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"• {item.MaLSDV}: {ex.Message}");
                }
            }

            if (countSuccess > 0)
            {
                MessageBox.Show($"Thành công: {countSuccess} đơn.", "Thông báo");

                LoadDataCombobox();

                if (!string.IsNullOrEmpty(cmb_TT4_NhapMa.Text))
                {
                    btn_TT4_Tim_Click(null, null);
                }
                else
                {
                    danhSachTP.Clear();
                }
                cmb_TT4_MaBS.SelectedIndex = -1;
                cmb_TT4_MaBS.Text = "";
            }

            if (errors.Length > 0)
            {
                MessageBox.Show("Có lỗi xảy ra:\n" + errors.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_TT4_Huy_Click(object sender, EventArgs e)
        {
            danhSachTP.Clear();
            cmb_TT4_NhapMa.Text = "";
            cmb_TT4_MaBS.SelectedIndex = -1;
            cmb_TT4_TraCuu.SelectedIndex = 0;
        }

        private void dgv_TT3_DsKB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb_TT4_MaBS.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Bác sĩ phụ trách!", "Thiếu thông tin",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maBacSi = cmb_TT4_MaBS.SelectedValue.ToString();

            var cacDongDuocChon = danhSachTP.Where(x => x.Chon == true).ToList();

            if (cacDongDuocChon.Count == 0) {  return; }

            DialogResult result = MessageBox.Show($"Xác nhận {cacDongDuocChon.Count} đơn?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            int countSuccess = 0;
            StringBuilder errors = new StringBuilder();

            foreach (var item in cacDongDuocChon)
            {
                try
                {
                    serviceDAL.XacNhanTiemPhong_Update(
                        item.MaLSDV,
                        item.MaLSDVTP,
                        maBacSi,
                        item.LoaiVacXin,
                        "Tiêu chuẩn", 
                        DateTime.Now
                    );
                    countSuccess++;
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"• {item.MaLSDV}: {ex.Message}");
                }
            }

            if (countSuccess > 0)
            {
                MessageBox.Show($"Thành công: {countSuccess} đơn.", "Thông báo");

                LoadDataCombobox();

                if (!string.IsNullOrEmpty(cmb_TT4_NhapMa.Text))
                {
                    btn_TT4_Tim_Click(null, null);
                }
                else
                {
                    danhSachTP.Clear();
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show("Có lỗi xảy ra:\n" + errors.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic_TT1_BG_Click(object sender, EventArgs e) { }
        private void pnl_TT1_BG_Paint(object sender, PaintEventArgs e) { }
        private void cmb_TT4_NhapMa_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btn_TT4_QuayLai_Click(object sender, EventArgs e) 
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }
    }
}