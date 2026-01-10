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
    public partial class UC_KH_LSDichVu : UserControl
    {
        private UserControl currentContentUc;

        public UC_KH_LSDichVu()
        {
            InitializeComponent();

            pnl_KH_Content.Resize += pnl_KH_Content_Resize;

            pnl_KH_LSDichVu.Anchor = AnchorStyles.None;
            pnl_KH_LSDichVu.Dock = DockStyle.None;

            CenterControlInPanel(pnl_KH_LSDichVu, pnl_KH_Content);
        }

        private void pnl_KH_Content_Resize(object sender, EventArgs e)
        {
            if (currentContentUc != null && pnl_KH_Content.Controls.Contains(currentContentUc))
            {
                CenterControlInPanel(currentContentUc, pnl_KH_Content);
            }
            else if (pnl_KH_Content.Controls.Contains(pnl_KH_LSDichVu))
            {
                CenterControlInPanel(pnl_KH_LSDichVu, pnl_KH_Content);
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

        private void LoadMainContentUserControl(UserControl newUc)
        {
            pnl_KH_LSDichVu.Visible = false;

            if (currentContentUc != null)
            {
                pnl_KH_Content.Controls.Remove(currentContentUc);
                currentContentUc.Dispose();
            }

            // 3. Đăng ký Event QuayVeTrangChu
            // Sử dụng interface chung hoặc kiểm tra kiểu để đăng ký
            if (newUc is IReturnToMainPage ucWithCallback)
            {
                // Sử dụng hàm chung để xử lý quay lại
                ucWithCallback.QuayVeTrangChu += Uc_QuayVeTrangChu_Callback;
            }

            // 4. Thêm UC mới vào Panel và căn giữa
            pnl_KH_Content.Controls.Add(newUc);
            currentContentUc = newUc; // Cập nhật UC hiện tại

            CenterControlInPanel(currentContentUc, pnl_KH_Content);
            currentContentUc.BringToFront();
        }

        private void Uc_QuayVeTrangChu_Callback()
        {
            // 1. Ẩn và dọn dẹp UC con hiện tại
            if (currentContentUc != null)
            {
                if (currentContentUc is IReturnToMainPage ucWithCallback)
                {
                    ucWithCallback.QuayVeTrangChu -= Uc_QuayVeTrangChu_Callback;
                }
                pnl_KH_Content.Controls.Remove(currentContentUc);
                currentContentUc.Dispose();
                currentContentUc = null;
            }

            // 2. Hiển thị lại nội dung chính (Panel chứa các nút)
            pnl_KH_LSDichVu.Visible = true;
            CenterControlInPanel(pnl_KH_LSDichVu, pnl_KH_Content);
            pnl_KH_LSDichVu.BringToFront();
        }

        private void btn_KH_LSTiemPhong_Click(object sender, EventArgs e)
        {
            LoadMainContentUserControl(new UC_KH_LSTiemPhong());
        }

        private void btn_KH_LSKhamBenh_Click(object sender, EventArgs e)
        {
            LoadMainContentUserControl(new UC_KH_LSKhamBenh());
        }

        private void btn_KH_XemHuyDV_Click(object sender, EventArgs e)
        {
            LoadMainContentUserControl(new UC_KH_XemVaHuyDichVu());
        }

    }
}
