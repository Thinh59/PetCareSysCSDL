using System;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Frm_TT6_ChonKhuyenMai : Form
    {
        // Các biến kết quả để trả về cho Form cha
        public decimal TongTienGiam { get; private set; } = 0;
        public int DiemDaDung { get; private set; } = 0;

        private int _diemHienCo = 0;
        private decimal _tongTienHoaDon = 0;

        public Frm_TT6_ChonKhuyenMai(string maKH, int diemHienCo, decimal tongTienHoaDon, string capDo)
        {
            InitializeComponent();
            _diemHienCo = diemHienCo;
            _tongTienHoaDon = tongTienHoaDon;

            SetupForm(capDo);
        }

        private void SetupForm(string capDo)
        {
            // 1. Hiển thị điểm
            lbl_DiemHienCo.Text = $"Điểm hiện có: {_diemHienCo} điểm";
            num_DiemSuDung.Maximum = _diemHienCo; // Không được nhập quá số điểm hiện có

            // 2. Tạo dữ liệu giả lập cho danh sách khuyến mãi (Hoặc lấy từ DB nếu có bảng Voucher)
            DataTable dtKM = new DataTable();
            dtKM.Columns.Add("Chon", typeof(bool)); // Cột checkbox
            dtKM.Columns.Add("TenKM", typeof(string));
            dtKM.Columns.Add("GiaTri", typeof(decimal));

            // -- Logic tính tiền giảm theo Hạng (như cũ) --
            decimal giamGiaHang = 0;
            if (capDo == "VIP") giamGiaHang = _tongTienHoaDon * 0.1m;
            else if (capDo == "Thân thiết") giamGiaHang = _tongTienHoaDon * 0.05m;
            else if (capDo == "Cơ bản") giamGiaHang = _tongTienHoaDon * 0.03m;

            if (giamGiaHang > 0)
            {
                dtKM.Rows.Add(true, $"Ưu đãi hạng {capDo}", giamGiaHang);
            }

            // -- Tại đây bạn có thể load thêm Voucher từ DB nếu có --
            // Ví dụ: dtKM.Rows.Add(false, "Voucher sinh nhật", 50000);

            dgv_KhuyenMai.DataSource = dtKM;

            // Format cột tiền
            dgv_KhuyenMai.Columns["GiaTri"].DefaultCellStyle.Format = "N0";
            dgv_KhuyenMai.Columns["TenKM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        // Sự kiện khi nhập số điểm loyalty
        private void num_DiemSuDung_ValueChanged(object sender, EventArgs e)
        {
            int diemNhap = (int)num_DiemSuDung.Value;
            decimal tienQuyDoi = diemNhap * 1000; // 1 điểm = 1000 VND

            // Kiểm tra logic: Tiền giảm không được vượt quá tổng hóa đơn
            // (Lưu ý: chưa trừ khuyến mãi danh sách, ở đây chỉ hiện thông tin)
            lbl_QuyDoiTien.Text = $"Quy đổi: -{tienQuyDoi:N0} VNĐ";
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            decimal tongGiamTuDanhSach = 0;

            // 1. Duyệt qua Grid để tính tổng các khuyến mãi được Check
            foreach (DataGridViewRow row in dgv_KhuyenMai.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value) == true)
                {
                    tongGiamTuDanhSach += Convert.ToDecimal(row.Cells["GiaTri"].Value);
                }
            }

            // 2. Tính tiền giảm từ điểm
            int diemNhap = (int)num_DiemSuDung.Value;
            decimal tienGiamTuDiem = diemNhap * 1000;

            // 3. Kiểm tra tổng hợp lệ
            decimal tongGiamTamTinh = tongGiamTuDanhSach + tienGiamTuDiem;

            if (tongGiamTamTinh > _tongTienHoaDon)
            {
                MessageBox.Show($"Tổng giảm giá ({tongGiamTamTinh:N0}) lớn hơn giá trị hóa đơn ({_tongTienHoaDon:N0})! Vui lòng điều chỉnh lại.", "Cảnh báo");
                return;
            }

            // Gán giá trị để form cha lấy
            TongTienGiam = tongGiamTamTinh;
            DiemDaDung = diemNhap;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}