using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PetCare
{
    public partial class Usc_QLCT8_TKSP : UserControl
    {
        private ServiceDAL dal = new ServiceDAL();
        private BindingList<Class_QLCT8_TKSP_SPBL> ds_SPBL = new BindingList<Class_QLCT8_TKSP_SPBL>();
        private BindingList<Class_QLCT8_TKSP_Thuoc> ds_Thuoc = new BindingList<Class_QLCT8_TKSP_Thuoc>();
        private BindingList<Class_QLCT8_TKSP_Vaccine> ds_Vaccine = new BindingList<Class_QLCT8_TKSP_Vaccine>();

        public Usc_QLCT8_TKSP()
        {
            InitializeComponent();

            dgv_QLCT8_TKSP_SPBL.DataSource = ds_SPBL;
            dgv_QLCT8_TKSP_Thuoc.DataSource = ds_Thuoc;
            dgv_QLCT8_TKSP_Vaccine.DataSource = ds_Vaccine;
        }

        private void cmb_QLCT8_selectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_QLCT8_getMaSPBL.Visible = false;
            txb_QLCT8_getMaThuoc.Visible = false;
            txb_QLCT8_getMaVaccine.Visible = false;

            lbl_QLCT8_MaSPBL.Visible = false;
            lbl_QLCT8_MaThuoc.Visible = false;
            lbl_QLCT8_MaVaccine.Visible = false;

            if (cmb_QLCT8_selectMode.SelectedItem == null)
                return;

            string mode = cmb_QLCT8_selectMode.SelectedItem.ToString();

            switch (mode)
            {
                case "Sản phẩm bán lẻ":
                    txb_QLCT8_getMaSPBL.Visible = true;
                    lbl_QLCT8_MaSPBL.Visible = true;
                    break;

                case "Thuốc":
                    txb_QLCT8_getMaThuoc.Visible = true;
                    lbl_QLCT8_MaThuoc.Visible = true;
                    break;

                case "Vắc-xin":
                    txb_QLCT8_getMaVaccine.Visible = true;
                    lbl_QLCT8_MaVaccine.Visible = true;
                    break;
            }
        }

        private void btn_QLCT8_XacNhan_Click(object sender, EventArgs e)
        {
            if (cmb_QLCT8_selectMode.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại thống kê!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mode = cmb_QLCT8_selectMode.SelectedItem.ToString();

            try
            {
                switch (mode)
                {
                    case "Sản phẩm bán lẻ":
                        dgv_QLCT8_TKSP_SPBL.Visible = true;
                        dgv_QLCT8_TKSP_Thuoc.Visible = false;
                        dgv_QLCT8_TKSP_Vaccine.Visible = false;
                        LoadDataSPBL();
                        break;

                    case "Thuốc":
                        dgv_QLCT8_TKSP_Thuoc.Visible = true;
                        dgv_QLCT8_TKSP_SPBL.Visible = false;
                        dgv_QLCT8_TKSP_Vaccine.Visible = false;
                        LoadDataThuoc();
                        break;

                    case "Vắc-xin":
                        dgv_QLCT8_TKSP_Vaccine.Visible = true;
                        dgv_QLCT8_TKSP_SPBL.Visible = false;
                        dgv_QLCT8_TKSP_Thuoc.Visible = false;
                        LoadDataVaccine();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataSPBL()
        {
            ds_SPBL.Clear();
            DataTable dt = dal.ThongKeSanPhamCongTy();

            string filterMa = txb_QLCT8_getMaSPBL.Text.Trim().ToLower();

            foreach (DataRow row in dt.Rows)
            {
                string maSP = row["MaSP"].ToString();

                if (!string.IsNullOrEmpty(filterMa) && !maSP.ToLower().Contains(filterMa))
                {
                    continue;
                }

                ds_SPBL.Add(new Class_QLCT8_TKSP_SPBL
                {
                    MaSP = maSP,
                    TenSP = row["TenSP"].ToString(),
                    LoaiSP = row["LoaiSP"].ToString(),
                    GiaBan = Convert.ToDecimal(row["GiaBan"]),
                    TonKho = Convert.ToInt32(row["TonKho"]),
                    DaBan = Convert.ToInt32(row["DaBan"])
                });
            }
        }

        private void LoadDataThuoc()
        {
            ds_Thuoc.Clear();
            DataTable dt = dal.ThongKeThuocCongTy();

            string filterMa = txb_QLCT8_getMaThuoc.Text.Trim().ToLower();

            foreach (DataRow row in dt.Rows)
            {
                string maThuoc = row["MaThuoc"].ToString();

                if (!string.IsNullOrEmpty(filterMa) && !maThuoc.ToLower().Contains(filterMa))
                {
                    continue;
                }

                ds_Thuoc.Add(new Class_QLCT8_TKSP_Thuoc
                {
                    MaThuoc = maThuoc,
                    TenThuoc = row["TenThuoc"].ToString(),
                    DonViTinh = row["DonViTinh"].ToString(),
                    GiaBan = Convert.ToDecimal(row["GiaBan"]),
                    HSD = row["HSD"] != DBNull.Value ? Convert.ToDateTime(row["HSD"]) : DateTime.MinValue,
                    TonKho = Convert.ToInt32(row["TonKho"]),
                    DaBan = Convert.ToInt32(row["DaBan"])
                });
            }
        }

        private void LoadDataVaccine()
        {
            ds_Vaccine.Clear();
            DataTable dt = dal.ThongKeVaccineCongTy();

            string filterMa = txb_QLCT8_getMaVaccine.Text.Trim().ToLower();

            foreach (DataRow row in dt.Rows)
            {
                string maVaccine = row["MaVaccine"].ToString();

                if (!string.IsNullOrEmpty(filterMa) && !maVaccine.ToLower().Contains(filterMa))
                {
                    continue;
                }

                ds_Vaccine.Add(new Class_QLCT8_TKSP_Vaccine
                {
                    MaVaccine = maVaccine,
                    TenVaccine = row["TenVaccine"].ToString(),
                    GiaTien = Convert.ToDecimal(row["GiaTien"]),
                    TonKho = Convert.ToInt32(row["TonKho"]),
                    DaBan = Convert.ToInt32(row["DaBan"])
                });
            }
        }
    }
}