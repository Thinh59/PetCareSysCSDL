using PetCare.KhachHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCare.HoiVien
{
    public partial class UC_HV_DiemLoyalty : UserControl
    {
        ServiceDAL dal = new ServiceDAL();
        public event Action QuayVeTrangChu;

        public UC_HV_DiemLoyalty()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadDiem();
        }

        private void LoadDiem()
        {
            try
            {
                DataSet ds = dal.GetDiemLoyalty(SessionData.MaKH);

                // Table 0: Điểm hiện có
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox_HV_DiemHienTai.Text = ds.Tables[0].Rows[0]["DiemHienCo"].ToString();
                }

                // Table 1: Lịch sử giao dịch
                dataGridView_HV_LSDiem.AutoGenerateColumns = false;
                dataGridView_HV_LSDiem.DataSource = ds.Tables[1];

                // Mapping các cột từ SQL
                dataGridView_HV_LSDiem.Columns["NgayGio"].DataPropertyName = "NgayGio";
                dataGridView_HV_LSDiem.Columns["HoaDon"].DataPropertyName = "HoaDon";
                dataGridView_HV_LSDiem.Columns["MoTa"].DataPropertyName = "MoTa";
                dataGridView_HV_LSDiem.Columns["DiemCong"].DataPropertyName = "DiemCong";
                dataGridView_HV_LSDiem.Columns["DiemTru"].DataPropertyName = "DiemTru";
                dataGridView_HV_LSDiem.Columns["DiemConLai"].DataPropertyName = "DiemConLai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu điểm: " + ex.Message);
            }
        }

        private void btn_HV_QuayLai_Click(object sender, EventArgs e)
        {
            QuayVeTrangChu?.Invoke();
        }
    }
}
