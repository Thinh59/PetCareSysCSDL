using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT15_LSDD : UserControl
    {
        private BindingList<Class_QLCT15_LSDD> danhSach = new BindingList<Class_QLCT15_LSDD>();
        private ServiceDAL serviceDAL = new ServiceDAL();

        public Usc_QLCT15_LSDD()
        {
            InitializeComponent();
            dgv_QLCT15_LSDD.DataSource = danhSach;
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                var chiNhanhData = serviceDAL.GetThongTinChiNhanhDonGian();

                cmb_QLCT15_selectFrom.Items.Clear();
                cmb_QLCT15_selectTo.Items.Clear();

                cmb_QLCT15_selectFrom.Items.Add("Bất kỳ");
                cmb_QLCT15_selectTo.Items.Add("Bất kỳ");

                foreach (DataRow row in chiNhanhData.Rows)
                {
                    string maCN = row["MaCN"].ToString();
                    cmb_QLCT15_selectFrom.Items.Add(maCN);
                    cmb_QLCT15_selectTo.Items.Add(maCN);
                }

                cmb_QLCT15_selectFrom.SelectedIndex = 0;
                cmb_QLCT15_selectTo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu chi nhánh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnl_QTCT15_Paint(object sender, PaintEventArgs e) { }

        private void btn_QLCT15_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string maNV = string.IsNullOrWhiteSpace(txb_QLCT15_getMaNV.Text) ? null : txb_QLCT15_getMaNV.Text.Trim();
                DateTime? ngayDieuDong = null;

                if (dtp_QLCT15_NgayDD.Value > new DateTime(1900, 1, 1))
                {
                    ngayDieuDong = dtp_QLCT15_NgayDD.Value.Date;
                }

                string chiNhanhFrom = cmb_QLCT15_selectFrom.SelectedItem?.ToString();
                string chiNhanhTo = cmb_QLCT15_selectTo.SelectedItem?.ToString();

                DataTable resultData = null;

                if (chiNhanhFrom == "Bất kỳ" && chiNhanhTo == "Bất kỳ")
                {
                    resultData = serviceDAL.XemLSDDToanCongTy(maNV, ngayDieuDong);
                }
                else if (chiNhanhFrom != "Bất kỳ" && chiNhanhTo == "Bất kỳ")
                {
                    resultData = serviceDAL.XemLSDDTuChiNhanh(chiNhanhFrom, maNV, ngayDieuDong);
                }
                else if (chiNhanhFrom == "Bất kỳ" && chiNhanhTo != "Bất kỳ")
                {
                    resultData = serviceDAL.XemLSDDDenChiNhanh(chiNhanhTo, maNV, ngayDieuDong);
                }
                else if (chiNhanhFrom != "Bất kỳ" && chiNhanhTo != "Bất kỳ")
                {
                    resultData = serviceDAL.XemLSDDToanCongTy(maNV, ngayDieuDong);
                    DataView dv = new DataView(resultData);
                    dv.RowFilter = $"ChiNhanhCu = '{chiNhanhFrom}' AND ChiNhanhMoi = '{chiNhanhTo}'";
                    resultData = dv.ToTable();
                }

                danhSach.Clear();

                if (resultData != null && resultData.Rows.Count > 0)
                {
                    foreach (DataRow row in resultData.Rows)
                    {
                        var item = new Class_QLCT15_LSDD
                        {
                            STT = Convert.ToInt32(row["STT"]),
                            MaNV = row["MaNV"].ToString(),
                            NgayDieuDong = Convert.ToDateTime(row["NgayDieuDong"]),
                            ChiNhanhCu = row["ChiNhanhCu"].ToString(),
                            ChiNhanhMoi = row["ChiNhanhMoi"].ToString()
                        };
                        danhSach.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp với điều kiện tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}