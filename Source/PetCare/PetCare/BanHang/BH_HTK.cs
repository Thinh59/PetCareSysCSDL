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
using static System.Collections.Specialized.BitVector32;

namespace PetCare
{
    public partial class BH_HTK : UserControl
    {
        DataConnection dc = new DataConnection();
        public BH_HTK()
        {
            InitializeComponent();
            data_BH_HTK.AutoGenerateColumns = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbBox_BH_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void data_BH_HTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BH_HTK_Load(object sender, EventArgs e)
        {
            cbBox_BH_Loai.SelectedIndex = 0;
            LoadInventory();
        }

        private void LoadInventory()
        {
            if (cbBox_BH_Loai.SelectedItem == null) return;

            try
            {
                string loai = cbBox_BH_Loai.SelectedItem.ToString();

                if (loai == "---Tất Cả---") loai = "";

                SqlParameter[] p = {
            new SqlParameter("@MaNV", SessionData.MaNV),
            new SqlParameter("@LoaiSP", loai)
        };

                DataTable dt = dc.ExecuteProcedure("sp_GetHangTonKho", p);
                data_BH_HTK.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải kho hàng: " + ex.Message);
            }
        }
    }
}
