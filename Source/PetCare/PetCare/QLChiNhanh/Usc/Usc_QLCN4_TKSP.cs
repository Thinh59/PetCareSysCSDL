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
    public partial class Usc_QLCN4_TKSP: UserControl
    {
        public Usc_QLCN4_TKSP()
        {
            InitializeComponent();
        }

        private void btn_QLCN4_TiepTuc_Click(object sender, EventArgs e)
        {
            pnl_QLCN4_UC.Controls.Clear();
            UserControl ucMoi = null;

            if (rbn_QLCN4_Thuoc.Checked)
            {
                ucMoi = new Usc_QLCN4_TKThuoc();
            }
            else if (rbn_QLCN4_Vacxin.Checked)
            {
                ucMoi = new Usc_QLCN4_TKVacxin();
            }
            else if (rbn_QLCN4_SanPham.Checked)
            {
                ucMoi = new Usc_QLCN4_TKSPKhac();
            }

            if (ucMoi != null)
            {
                ucMoi.Dock = DockStyle.Fill;
                pnl_QLCN4_UC.Controls.Add(ucMoi);
                ucMoi.BringToFront(); 
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần thống kê.", "Thông báo");
            }
        }

        private void btn_QLCN4_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }

        private void rbn_QLCN4_Thuoc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbn_QLCN4_Vacxin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbn_QLCN4_SanPham_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pnl_QLCN4_UC_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
