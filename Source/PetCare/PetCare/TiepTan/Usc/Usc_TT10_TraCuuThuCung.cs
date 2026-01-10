using System;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_TT10_TraCuuThuCung : UserControl
    {
        ServiceDAL dal = new ServiceDAL();

        private string _maTC_DangChon = "";
        private string _tenTC_DangChon = "";

        public Usc_TT10_TraCuuThuCung()
        {
            InitializeComponent();

            dataGridView_QLThuCung.AutoGenerateColumns = false;
            dataGridView_QLThuCung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_QLThuCung.MultiSelect = false;

            SetupGrid();
            cmb_TT10_TraCuu.Items.Clear();
            cmb_TT10_TraCuu.Items.Add("SĐT Chủ");
            cmb_TT10_TraCuu.Items.Add("Mã Thú Cưng");

            this.Load += Usc_TT10_TraCuuThuCung_Load;
        }

        private void Usc_TT10_TraCuuThuCung_Load(object sender, EventArgs e)
        {
            if (cmb_TT10_TraCuu.Items.Count > 0)
                cmb_TT10_TraCuu.SelectedIndex = 0;

            _maTC_DangChon = "";
            _tenTC_DangChon = "";
            cmb_TT10_PetChon.Text = "";
            cmb_TT10_PetChon.Items.Clear();
        }
        private void SetupGrid()
        {
            if (dataGridView_QLThuCung.Columns.Count > 0)
            {
                dataGridView_QLThuCung.Columns[0].DataPropertyName = "MaThuCung";
                if (dataGridView_QLThuCung.Columns.Count > 1) dataGridView_QLThuCung.Columns[1].DataPropertyName = "TenThuCung";
                if (dataGridView_QLThuCung.Columns.Count > 2) dataGridView_QLThuCung.Columns[2].DataPropertyName = "LoaiThuCung";
                if (dataGridView_QLThuCung.Columns.Count > 3) dataGridView_QLThuCung.Columns[3].DataPropertyName = "Giong_TC";
                if (dataGridView_QLThuCung.Columns.Count > 4) dataGridView_QLThuCung.Columns[4].DataPropertyName = "NgaySinh_TC";
                if (dataGridView_QLThuCung.Columns.Count > 5) dataGridView_QLThuCung.Columns[5].DataPropertyName = "GioiTinh_TC";
                if (dataGridView_QLThuCung.Columns.Count > 6) dataGridView_QLThuCung.Columns[6].DataPropertyName = "TinhTrangSK";
            }

            foreach (DataGridViewColumn col in dataGridView_QLThuCung.Columns)
            {
                if (col.HeaderText == "Sửa" || col.HeaderText == "Xóa" ||
                    col.Name == "Sua" || col.Name == "Xoa")
                {
                    col.Visible = false; 
                }
            }
        }

        private void btn_TT10_Tim_Click(object sender, EventArgs e)
        {
            string tuKhoa = cmb_TT10_MaTenSDT.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                return;
            }

            try
            {
                DataTable dt = dal.TimKiemThuCung(tuKhoa);
                dataGridView_QLThuCung.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thú cưng nào với thông tin: " + tuKhoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dataGridView_QLThuCung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView_QLThuCung.Rows[e.RowIndex];


                    var cellMa = row.Cells[0].Value;
                    var cellTen = row.Cells[1].Value;

                    _maTC_DangChon = (cellMa != null) ? cellMa.ToString() : "";
                    _tenTC_DangChon = (cellTen != null) ? cellTen.ToString() : "";

                    if (!string.IsNullOrEmpty(_tenTC_DangChon))
                    {
                        cmb_TT10_PetChon.Items.Clear();
                        cmb_TT10_PetChon.Items.Add(_tenTC_DangChon);
                        cmb_TT10_PetChon.SelectedIndex = 0;
                    }
                }
                catch {}
            }
        }
        private void btn_TT10_LSKham_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maTC_DangChon))
            {
                MessageBox.Show("Vui lòng BẤM VÀO MỘT DÒNG thú cưng trong danh sách trước!");
                return;
            }

            Usc_TT10_LSKham usc = new Usc_TT10_LSKham();

            usc.LoadData(_maTC_DangChon, _tenTC_DangChon);

            ShowChildForm(usc);
        }
        private void btn_TT10_LSTiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maTC_DangChon))
            {
                MessageBox.Show("Vui lòng BẤM VÀO MỘT DÒNG thú cưng trong danh sách trước!");
                return;
            }

            Usc_TT10_LSTiemPhong usc = new Usc_TT10_LSTiemPhong();
            usc.LoadData(_maTC_DangChon, _tenTC_DangChon);
            ShowChildForm(usc);
        }
        private void ShowChildForm(UserControl usc)
        {
            dataGridView_QLThuCung.Visible = false;
            btn_TT10_Tim.Visible = false;
            var eventInfo = usc.GetType().GetEvent("OnBack");
            if (eventInfo != null)
            {
                if (usc is Usc_TT10_LSKham) ((Usc_TT10_LSKham)usc).OnBack += () => CloseChildForm(usc);
                if (usc is Usc_TT10_LSTiemPhong) ((Usc_TT10_LSTiemPhong)usc).OnBack += () => CloseChildForm(usc);
            }
            usc.Dock = DockStyle.Fill;
            this.Controls.Add(usc);
            usc.BringToFront();
        }

        private void CloseChildForm(UserControl usc)
        {
            this.Controls.Remove(usc);
            usc.Dispose();

            dataGridView_QLThuCung.Visible = true;
            btn_TT10_Tim.Visible = true;
        }
        private void cmb_TT10_TraCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_TT10_MaTenSDT.DataSource = null;
            cmb_TT10_MaTenSDT.Items.Clear();

            DataTable dt = new DataTable();
            string kieuTraCuu = cmb_TT10_TraCuu.Text;

            if (kieuTraCuu == "SĐT Chủ")
                dt = dal.GetAllSDT();
            else if (kieuTraCuu == "Mã Thú Cưng")
                dt = dal.GetAllMaThuCung();

            if (dt != null && dt.Rows.Count > 0)
            {
                string tenCot = dt.Columns[0].ColumnName;

                cmb_TT10_MaTenSDT.DataSource = dt;
                cmb_TT10_MaTenSDT.DisplayMember = tenCot;
                cmb_TT10_MaTenSDT.ValueMember = tenCot;

                cmb_TT10_MaTenSDT.AutoCompleteMode = AutoCompleteMode.None;

                cmb_TT10_MaTenSDT.SelectedIndex = -1;
            }
        }
        private void cmb_TT10_MaTenSDT_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmb_TT10_PetChon_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dataGridView_QLThuCung_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}