using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PetCare
{

    public partial class Usc_TT3_XNKhamBenh: UserControl
    {
        private readonly ServiceDAL serviceDAL = new ServiceDAL();

        private BindingList<Class_TT3_DSKhamBenh> danhSachKB = new BindingList<Class_TT3_DSKhamBenh>();
        private void LoadSearchOptions()
        {
            cmb_TT3_TraCuu.Items.Add("MaLSDV");
            cmb_TT3_TraCuu.Items.Add("MaKH");
            cmb_TT3_TraCuu.Items.Add("SDT_KH");
            cmb_TT3_TraCuu.SelectedIndex = 0;
        }
        public Usc_TT3_XNKhamBenh()
        {
            InitializeComponent();

            dgv_TT3_DsKB.AllowUserToAddRows = false;

            dgv_TT3_DsKB.ReadOnly = false; 
            dgv_TT3_DsKB.DataSource = danhSachKB;
            LoadSearchOptions();
            LoadDoctorComboBox();
        }
        private void LoadDoctorComboBox(string maCN = null)
        {
            ServiceDAL dal = new ServiceDAL();
            try
            {
                DataTable dt = dal.GetAvailableDoctors(maCN);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmb_TT3_MaBS.DataSource = dt;
                    cmb_TT3_MaBS.DisplayMember = "HoTenNV";
                    cmb_TT3_MaBS.ValueMember = "MaNV";
                    cmb_TT3_MaBS.SelectedIndex = -1;
                }
                else
                {
                    cmb_TT3_MaBS.DataSource = null;
                    cmb_TT3_MaBS.Text = "-- Không có bác sĩ rảnh --";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Bác sĩ: " + ex.Message + "\n\nChi tiết: " + ex.InnerException?.Message, "Lỗi");
                cmb_TT3_MaBS.DataSource = null;
            }
        }

        private void pic_TT1_BG_Click(object sender, EventArgs e)
        {

        }

        private void LoadPetComboBox(string maKH)
        {
            try
            {
                cmb_TT3_MaThuCung.DataSource = null;
                cmb_TT3_MaThuCung.Items.Clear();

                DataTable dt = serviceDAL.GetPetsByCustomer(maKH);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmb_TT3_MaThuCung.DataSource = dt;
                    cmb_TT3_MaThuCung.DisplayMember = "TenThuCung"; 
                    cmb_TT3_MaThuCung.ValueMember = "MaThuCung";   
                    cmb_TT3_MaThuCung.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thú cưng: " + ex.Message);
            }
        }

        private void btn_TT3_QuayLai_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Visible = false;

                this.Parent.Controls.Remove(this);

                this.Dispose();
            }
        }

        private void btn_TT3_Tim_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            string searchType = cmb_TT3_TraCuu.SelectedItem?.ToString();

            string searchValue = cmb_TT3_NhapMa.Text.Trim();

            if (string.IsNullOrEmpty(searchType))
            {
                MessageBox.Show("Vui lòng chọn loại tra cứu.", "Thiếu thông tin");
                return;
            }

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Vui lòng nhập giá trị để tìm kiếm.", "Thiếu thông tin");
                return;
            }

            try
            {
                DataTable dt = null;

                if (searchType == "MaLSDV" || searchType == "MaKH" || searchType == "SDT_KH")
                {
                    dt = serviceDAL.SearchLSDV(searchValue);
                }
                else
                {
                    MessageBox.Show("Loại tra cứu không hợp lệ.", "Lỗi");
                    return;
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv_TT3_DsKB.DataSource = dt; 

                    if (dgv_TT3_DsKB.Rows.Count > 0)
                    {
                        string maKH = dgv_TT3_DsKB.Rows[0].Cells["MaKH"].Value.ToString();
                        LoadPetComboBox(maKH);
                    }

                    if (dgv_TT3_DsKB.Columns.Contains("MaLSDV"))
                        dgv_TT3_DsKB.Columns["MaLSDV"].HeaderText = "Mã LSDV";
                    if (dgv_TT3_DsKB.Columns.Contains("MaKH"))
                        dgv_TT3_DsKB.Columns["MaKH"].HeaderText = "Mã KH";
                    if (dgv_TT3_DsKB.Columns.Contains("HoTen_KH"))
                        dgv_TT3_DsKB.Columns["HoTen_KH"].HeaderText = "Khách hàng";
                    if (dgv_TT3_DsKB.Columns.Contains("SDT_KH"))
                        dgv_TT3_DsKB.Columns["SDT_KH"].HeaderText = "Số điện thoại";
                    if (dgv_TT3_DsKB.Columns.Contains("TenDV"))
                        dgv_TT3_DsKB.Columns["TenDV"].HeaderText = "Dịch vụ";
                    if (dgv_TT3_DsKB.Columns.Contains("NgayDatTruoc"))
                        dgv_TT3_DsKB.Columns["NgayDatTruoc"].HeaderText = "Ngày đặt";
                    if (dgv_TT3_DsKB.Columns.Contains("TrangThaiGD"))
                        dgv_TT3_DsKB.Columns["TrangThaiGD"].HeaderText = "Trạng thái";

                }
                else
                {
                    dgv_TT3_DsKB.DataSource = null;
                    MessageBox.Show("Không tìm thấy lịch đặt khám bệnh nào phù hợp.", "Thông báo");
                }

                timer.Stop();
                long thoiGianChay = timer.ElapsedMilliseconds;

                if (txb_TT3_Time != null)
                {
                    txb_TT3_Time.Text = thoiGianChay.ToString() + " ms";

                    if (thoiGianChay < 50)
                    {
                        txb_TT3_Time.ForeColor = Color.Green; 
                        txb_TT3_Time.BackColor = Color.FromArgb(220, 255, 220);
                    }
                    else
                    {
                        txb_TT3_Time.ForeColor = Color.Red;   
                        txb_TT3_Time.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", "Lỗi CSDL");
            }
        }

        private void btn_TT3_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_TT3_MaBS.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ phụ trách.", "Thiếu thông tin");
                return;
            }
            string maBacSi = cmb_TT3_MaBS.SelectedValue.ToString();

            if (cmb_TT3_MaThuCung.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng cần khám.", "Thiếu thông tin");
                return;
            }
            string maThuCung = cmb_TT3_MaThuCung.SelectedValue.ToString(); 
            if (dgv_TT3_DsKB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng lịch khám cần xác nhận.", "Thông báo");
                return;
            }

            DataGridViewRow row = dgv_TT3_DsKB.SelectedRows[0];
            string maLSDV = row.Cells["MaLSDV"].Value?.ToString();
            string tenThuCung = cmb_TT3_MaThuCung.Text; 

            if (string.IsNullOrEmpty(maLSDV))
            {
                MessageBox.Show("Mã lịch sử dịch vụ không hợp lệ.", "Lỗi");
                return;
            }

            if (MessageBox.Show($"Xác nhận khám cho bé {tenThuCung}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    serviceDAL.XacNhanKhamBenh(maLSDV, maBacSi, DateTime.Now, maThuCung);

                    MessageBox.Show("Xác nhận thành công!");

                    cmb_TT3_NhapMa.Text = "";
                    btn_TT3_Tim_Click(null, null); 

                    LoadDoctorComboBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void btn_TT3_Huy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_TT3_TraCuu.Items.Count > 0)
                {
                    cmb_TT3_TraCuu.SelectedIndex = 0;
                }
                cmb_TT3_NhapMa.Items.Clear();
                cmb_TT3_NhapMa.Text = "";
                cmb_TT3_NhapMa.DataSource = null;

                if (cmb_TT3_MaBS.Items.Count > 0)
                {
                    cmb_TT3_MaBS.SelectedIndex = -1;
                }


                if (dgv_TT3_DsKB.DataSource != null)
                {
                    dgv_TT3_DsKB.DataSource = null;
                }


                danhSachKB.Clear();
                dgv_TT3_DsKB.DataSource = danhSachKB;

                LoadDoctorComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset form: " + ex.Message, "Lỗi");
            }
        }

        private void cmb_TT3_TraCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchType = cmb_TT3_TraCuu.SelectedItem?.ToString();

            cmb_TT3_NhapMa.Items.Clear();
            cmb_TT3_NhapMa.Text = "";
            cmb_TT3_NhapMa.DataSource = null;
            cmb_TT3_NhapMa.DropDownStyle = ComboBoxStyle.DropDown;

            try
            {
                DataTable dt = null;

                if (searchType == "MaLSDV")
                {
                    dt = serviceDAL.GetAllMaLSDV();
                }
                else if (searchType == "MaKH")
                {
                    dt = serviceDAL.GetAllMaKH();
                }
                else if (searchType == "SDT_KH")
                {
                    dt = serviceDAL.GetAllSDT();
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmb_TT3_NhapMa.Items.Add(row[0].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void cmb_TT3_NhapMa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_TT3_MaBS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_TT3_MaThuCung_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_TT3_DsKB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgv_TT3_DsKB.Rows[e.RowIndex];

                if (!dgv_TT3_DsKB.Columns.Contains("MaKH"))
                {
                    MessageBox.Show("Lỗi: Không tìm thấy cột tên là 'MaKH' trên lưới.\nHãy kiểm tra lại Property Name của cột.", "Lỗi Code");
                    return;
                }

                string maKH = row.Cells["MaKH"].Value?.ToString();

                if (!string.IsNullOrEmpty(maKH))
                {

                    LoadPetComboBox(maKH);


                    if (dgv_TT3_DsKB.Columns.Contains("MaThuCung") && row.Cells["MaThuCung"].Value != null)
                    {
                        string maThuCungTrenGrid = row.Cells["MaThuCung"].Value.ToString();

                        cmb_TT3_MaThuCung.SelectedValue = maThuCungTrenGrid;
                    }
                    else
                    {
                        if (cmb_TT3_MaThuCung.Items.Count > 0)
                            cmb_TT3_MaThuCung.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn dòng: " + ex.Message);
            }
        }

        private void dgv_TT3_DsKB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_TT3_DsKB.Rows.Count) return;

            try
            {
                DataGridViewRow row = dgv_TT3_DsKB.Rows[e.RowIndex];

                string maKH = row.Cells["MaKH"].Value?.ToString();

                if (!string.IsNullOrEmpty(maKH))
                {
                    LoadPetComboBox(maKH); 
                    string maThuCungTrenGrid = row.Cells["MaThuCung"].Value?.ToString();

                    if (!string.IsNullOrEmpty(maThuCungTrenGrid))
                    {
                        cmb_TT3_MaThuCung.SelectedValue = maThuCungTrenGrid;
                    }
                    else
                    {
                        if (cmb_TT3_MaThuCung.Items.Count > 0)
                            cmb_TT3_MaThuCung.SelectedIndex = 0;
                        else
                            cmb_TT3_MaThuCung.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi binding thú cưng: " + ex.Message);
            }
        }

        private void txb_TT3_Time_TextChanged(object sender, EventArgs e)
        {

        }

        private void lab_ALL_TDN_Click(object sender, EventArgs e)
        {

        }
    }
}
