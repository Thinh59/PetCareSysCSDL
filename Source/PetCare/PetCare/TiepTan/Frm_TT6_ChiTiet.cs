using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Frm_TT6_ChiTiet: Form
    {
        public Frm_TT6_ChiTiet(string title, object dataSource)
        {
            InitializeComponent();
            this.Text = title;
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            DataGridView dgv = new DataGridView();
            dgv.Parent = this;
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DataSource = dataSource;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
        }

        private void Frm_TT6_ChiTiet_Load(object sender, EventArgs e)
        {

        }
    }
}
