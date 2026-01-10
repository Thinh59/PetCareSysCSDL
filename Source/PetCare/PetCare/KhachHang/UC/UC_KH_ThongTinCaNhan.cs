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
    public partial class UC_KH_ThongTinCaNhan : UserControl
    {
        public UC_KH_ThongTinCaNhan()
        {
            InitializeComponent();
        }

        private void btn_KH_DoiMatKhau_Click(object sender, EventArgs e)
        {
            pnl_KH_Content.Controls.Clear();
            UC_KH_DoiMatKhau UC_DoiMK = new UC_KH_DoiMatKhau();
            pnl_KH_Content.Controls.Add(UC_DoiMK);


            UC_DoiMK.Visible = true;
            UC_DoiMK.BringToFront();

            UC_DoiMK.Left = (pnl_KH_Content.Width - UC_DoiMK.Width) / 2;
            UC_DoiMK.Top = (pnl_KH_Content.Height - UC_DoiMK.Height) / 2;

            UC_DoiMK.Anchor = AnchorStyles.None;
            UC_DoiMK.Dock = DockStyle.None;
        }

        private void btn_KH_CapNhat_Click(object sender, EventArgs e)
        {

        }
    }
}
