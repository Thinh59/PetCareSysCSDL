using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.KhachHang.UC
{
    public partial class UC_KH_DatGoiTiem : UserControl
    {
        ServiceDAL serviceDAL = new ServiceDAL();
        public UC_KH_DatGoiTiem()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadInitialData();
        }
        private void LoadInitialData()
        {
            try
            {
                // 1. Đổ dữ liệu vào DataGridView
                dataGridView_DSGoiTiem.AutoGenerateColumns = false;
                DataTable dtGoiTiem = serviceDAL.GetDanhSachGoiTiem();
                dataGridView_DSGoiTiem.DataSource = dtGoiTiem;

                // 2. Mapping (Tên trong ngoặc kép phải khớp với từ khóa AS trong SQL)
                dataGridView_DSGoiTiem.Columns["MaGT"].DataPropertyName = "MaGT";
                dataGridView_DSGoiTiem.Columns["TenGT"].DataPropertyName = "TenGT";
                dataGridView_DSGoiTiem.Columns["SoThang"].DataPropertyName = "SoThang";
                dataGridView_DSGoiTiem.Columns["Vacxin"].DataPropertyName = "Vacxin";
                dataGridView_DSGoiTiem.Columns["SoMui"].DataPropertyName = "SoMui";
                dataGridView_DSGoiTiem.Columns["UuDai"].DataPropertyName = "UuDai";
                dataGridView_DSGoiTiem.Columns["GiaTien"].DataPropertyName = "GiaTien";

                // 3. Đổ dữ liệu vào ComboBox Gói Tiêm
                DataTable dtGoiTiemDuyNhat = serviceDAL.GetDanhSachGoiTiemRutGon();
                comboBox_KH_ChonGoiTiem.DataSource = dtGoiTiemDuyNhat;
                comboBox_KH_ChonGoiTiem.DisplayMember = "TenGoi";
                comboBox_KH_ChonGoiTiem.ValueMember = "MaGoiTiem";

                // 4. Đổ dữ liệu vào ComboBox Thú Cưng
                string maKH = SessionData.MaKH;
                DataTable dtThuCung = serviceDAL.GetDSTC(maKH);
                comboBox_KH_ChonThuCung.DataSource = dtThuCung;
                comboBox_KH_ChonThuCung.DisplayMember = "TenThuCung";
                comboBox_KH_ChonThuCung.ValueMember = "MaThuCung";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu: " + ex.Message);
            }
        }
        private void CenterControlInPanel(Control controlToCenter, Panel parentPanel)
        {
            controlToCenter.Anchor = AnchorStyles.None;
            controlToCenter.Dock = DockStyle.None;

            int parentWidth = parentPanel.ClientSize.Width;
            int parentHeight = parentPanel.ClientSize.Height;

            int controlWidth = controlToCenter.Width;
            int controlHeight = controlToCenter.Height;

            // Chỉ căn giữa nếu controlToCenter có kích thước đã được xác định hợp lệ
            if (controlWidth > 0 && controlHeight > 0)
            {
                controlToCenter.Left = (parentWidth - controlWidth) / 2;
                controlToCenter.Top = (parentHeight - controlHeight) / 2;
            }
            // Nếu kích thước không hợp lệ hoặc không đủ chỗ, có thể đặt vị trí mặc định
            else
            {
                controlToCenter.Left = 0;
                controlToCenter.Top = 0;
            }
        }

        private void btn_KH_TaoGoiTiem_Click(object sender, EventArgs e)
        {
            UC_KH_TaoGoiTiem UC_TaoGT = new UC_KH_TaoGoiTiem();

            // Đăng ký sự kiện Callback khi nhấn Hủy bên UC_TaoGoiTiem
            UC_TaoGT.QuayVeTrangCu += () => {
                pnl_KH_Content.Controls.Remove(UC_TaoGT);
                pnl_KH_DatGoiTiemPhong.Visible = true;
                pnl_KH_DatGoiTiemPhong.BringToFront();
                LoadInitialData();
            };

            pnl_KH_DatGoiTiemPhong.Visible = false;
            pnl_KH_Content.Controls.Add(UC_TaoGT);
            CenterControlInPanel(UC_TaoGT, pnl_KH_Content);
            UC_TaoGT.BringToFront();
        }

        private void btn_KH_TiepTheo_Click(object sender, EventArgs e)
        {
            if (comboBox_KH_ChonGoiTiem.SelectedValue == null || comboBox_KH_ChonThuCung.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn gói tiêm và thú cưng trước khi tiếp tục!");
                return;
            }
            string maGoi = comboBox_KH_ChonGoiTiem.SelectedValue.ToString();
            string maTC = comboBox_KH_ChonThuCung.SelectedValue.ToString();
            
            UC_KH_GoiTiem_ChonMuiTiemDau UC_ChonMT = new UC_KH_GoiTiem_ChonMuiTiemDau(maGoi, maTC);
            UC_ChonMT.QuayVeTrangChu += Uc_QuayVeTrangChu_Callback;
            currentContentUc = UC_ChonMT;

            pnl_KH_Content.Controls.Clear();
            pnl_KH_Content.Controls.Add(UC_ChonMT);

            pnl_KH_DatGoiTiemPhong.Visible = false;

            // Canh giữa
            CenterControlInPanel(UC_ChonMT, pnl_KH_Content);
            UC_ChonMT.BringToFront();
        }

        public event Action QuayVeTrangChu;
        private void btn_KH_Huy_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();

            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                this.Hide();
                parentForm.Show();
                parentForm.BringToFront();
            }
        }

        private UserControl currentContentUc;
        private void Uc_QuayVeTrangChu_Callback()
        {
            // 1. Chỉ dọn dẹp UserControl đang hiển thị
            if (currentContentUc != null)
            {
                // Hủy đăng ký sự kiện
                if (currentContentUc is UC_KH_GoiTiem_ChonMuiTiemDau ucCon)
                {
                    ucCon.QuayVeTrangChu -= Uc_QuayVeTrangChu_Callback;
                }

                // CHỈ XÓA UC NÀY KHỎI PANEL, KHÔNG CLEAR HẾT PANEL CHA
                pnl_KH_Content.Controls.Remove(currentContentUc);
                currentContentUc.Dispose();
                currentContentUc = null;
            }

            // 2. Bây giờ pnl_KH_DatGoiTiemPhong vẫn còn tồn tại, chỉ cần hiện nó lên
            pnl_KH_Content.Controls.Add(pnl_KH_DatGoiTiemPhong);
            pnl_KH_DatGoiTiemPhong.Visible = true;
            pnl_KH_DatGoiTiemPhong.BringToFront();

            // 3. Căn giữa
            CenterControlInPanel(pnl_KH_DatGoiTiemPhong, pnl_KH_Content);
        }
    }
}
