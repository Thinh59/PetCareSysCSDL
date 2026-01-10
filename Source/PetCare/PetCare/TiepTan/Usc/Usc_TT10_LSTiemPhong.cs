using System;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT10_LSTiemPhong : UserControl
    {
        private ServiceDAL dal = new ServiceDAL();
        public event Action OnBack; 

        public Usc_TT10_LSTiemPhong()
        {
            InitializeComponent();

            dataGridView_KH_LSTiemPhong.AutoGenerateColumns = false;

            SetupGrid();
        }

        private void SetupGrid()
        {

            if (dataGridView_KH_LSTiemPhong.Columns.Count > 0)
            {

                dataGridView_KH_LSTiemPhong.Columns[0].DataPropertyName = "MaLSDV";


                if (dataGridView_KH_LSTiemPhong.Columns.Count > 1)
                    dataGridView_KH_LSTiemPhong.Columns[1].DataPropertyName = "MaThuCung";


                if (dataGridView_KH_LSTiemPhong.Columns.Count > 2)
                    dataGridView_KH_LSTiemPhong.Columns[2].DataPropertyName = "TenThuCung";


                if (dataGridView_KH_LSTiemPhong.Columns.Count > 3)
                    dataGridView_KH_LSTiemPhong.Columns[3].DataPropertyName = "NgayTiem";


                if (dataGridView_KH_LSTiemPhong.Columns.Count > 4)
                    dataGridView_KH_LSTiemPhong.Columns[4].DataPropertyName = "LoaiVacXin";
                if (dataGridView_KH_LSTiemPhong.Columns.Count > 5)
                    dataGridView_KH_LSTiemPhong.Columns[5].DataPropertyName = "LieuLuong";
                if (dataGridView_KH_LSTiemPhong.Columns.Count > 6)
                    dataGridView_KH_LSTiemPhong.Columns[6].DataPropertyName = "BacSi";

                if (dataGridView_KH_LSTiemPhong.Columns.Count > 7)
                    dataGridView_KH_LSTiemPhong.Columns[7].DataPropertyName = "MaGoiTiem";
            }
        }

        public void LoadData(string maTC, string tenTC)
        {
            try
            {
                DataTable dt = dal.GetLSTiemPhong_ByPet(maTC);

                if (!dt.Columns.Contains("MaThuCung"))
                    dt.Columns.Add("MaThuCung", typeof(string));

                if (!dt.Columns.Contains("TenThuCung"))
                    dt.Columns.Add("TenThuCung", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    row["MaThuCung"] = maTC;
                    row["TenThuCung"] = tenTC;
                }

                dataGridView_KH_LSTiemPhong.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử tiêm: " + ex.Message);
            }
        }

        private void btn_TT10_QuayLaiLSK_Click(object sender, EventArgs e)
        {
            OnBack?.Invoke();
        }
        private void btn_QuayLai_Click(object sender, EventArgs e) { OnBack?.Invoke(); } // Phòng hờ bạn đặt tên nút khác
        private void dataGridView_KH_LSTiemPhong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}