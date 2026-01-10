using System;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel; 
namespace PetCare
{
    public partial class Usc_TT10_LSKham : UserControl
    {
        private ServiceDAL dal;
        public event Action OnBack;

        public Usc_TT10_LSKham()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            dal = new ServiceDAL();
            dataGridView_KH_LSKhamBenh.AutoGenerateColumns = false;

            SetupGridColumns();
            dataGridView_KH_LSKhamBenh.CellContentClick += DataGridView_CellContentClick;
        }

        private void SetupGridColumns()
        {
            dataGridView_KH_LSKhamBenh.Columns.Clear();

            dataGridView_KH_LSKhamBenh.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MaLSDV",
                HeaderText = "Mã HS",
                DataPropertyName = "MaLSDV",
                Width = 80
            });

            dataGridView_KH_LSKhamBenh.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TenThuCung",
                HeaderText = "Thú Cưng",
                DataPropertyName = "TenThuCung",
                Width = 100
            });

            var colNgay = new DataGridViewTextBoxColumn()
            {
                Name = "NgayKham",
                HeaderText = "Ngày Khám",
                DataPropertyName = "NgayKham",
                Width = 100
            };
            colNgay.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView_KH_LSKhamBenh.Columns.Add(colNgay);

            dataGridView_KH_LSKhamBenh.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BacSi",
                HeaderText = "Bác Sĩ",
                DataPropertyName = "BacSi",
                Width = 120
            });

            dataGridView_KH_LSKhamBenh.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TrieuChung",
                HeaderText = "Triệu Chứng",
                DataPropertyName = "TrieuChung",
                Width = 150
            });

            dataGridView_KH_LSKhamBenh.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ChuanDoan",
                HeaderText = "Chẩn Đoán",
                DataPropertyName = "ChuanDoan",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var colHen = new DataGridViewTextBoxColumn()
            {
                Name = "NgayHen",
                HeaderText = "Tái Khám",
                DataPropertyName = "NgayHen",
                Width = 100
            };
            colHen.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView_KH_LSKhamBenh.Columns.Add(colHen);

            var btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "ToaThuoc";
            btnCol.HeaderText = "Toa Thuốc";
            btnCol.Text = "Xem chi tiết";
            btnCol.UseColumnTextForButtonValue = true; 
            btnCol.Width = 100;
            dataGridView_KH_LSKhamBenh.Columns.Add(btnCol);

            dataGridView_KH_LSKhamBenh.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CoToaThuoc",
                DataPropertyName = "CoToaThuoc",
                Visible = false
            });
        }

        public void LoadData(string maTC, string tenTC)
        {
            try
            {
                DataTable dt = dal.GetLSKhamBenh_ByPet(maTC);

                if (!dt.Columns.Contains("TenThuCung"))
                    dt.Columns.Add("TenThuCung", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    row["TenThuCung"] = tenTC;
                }

                dataGridView_KH_LSKhamBenh.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }

        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_KH_LSKhamBenh.Columns[e.ColumnIndex].Name == "ToaThuoc")
            {
                try
                {
  
                    string maLSDV = dataGridView_KH_LSKhamBenh.Rows[e.RowIndex].Cells["MaLSDV"].Value.ToString();

                    var cellCoToa = dataGridView_KH_LSKhamBenh.Rows[e.RowIndex].Cells["CoToaThuoc"].Value;
                    int coToa = (cellCoToa != null && cellCoToa != DBNull.Value) ? Convert.ToInt32(cellCoToa) : 0;

                    if (coToa == 1)
                    {
                        DataTable dtChiTiet = dal.GetChiTietVatTu(maLSDV);
                        Frm_TT6_ChiTiet frm = new Frm_TT6_ChiTiet("Chi tiết Toa thuốc - " + maLSDV, dtChiTiet);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Lần khám này bác sĩ không kê toa thuốc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xem chi tiết: " + ex.Message);
                }
            }
            
        }

        private void btn_TT10_QuayLaiLST_Click(object sender, EventArgs e)
        {
            OnBack?.Invoke();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}