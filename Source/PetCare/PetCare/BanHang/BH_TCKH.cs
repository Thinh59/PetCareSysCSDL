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

namespace PetCare
{
    public partial class BH_TCKH : UserControl
    {
        int currentPage = 1;
        int pageSize = 20;

        DataConnection dc = new DataConnection();

        public BH_TCKH()
        {
            InitializeComponent();
            data_BH_TCKH.AllowUserToAddRows = false;
            data_BH_TCKH.AutoGenerateColumns = false;
            this.VisibleChanged += new EventHandler(TraCuuKhachHang_VisibleChanged);
        }

        private void pnl_BH_TCKH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TraCuuKhachHang_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                currentPage = 1;
                LoadAllKhachHang();
            }
        }

        private void LoadAllKhachHang(string condition = "", SqlParameter[] p = null)
        {
            string sql = @"
        SELECT MaKH, HoTen_KH, SDT_KH, Loai_KH
        FROM KHACHHANG
        " + condition + @"
        ORDER BY MaKH
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY
    ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (p != null)
                parameters.AddRange(p);

            parameters.Add(new SqlParameter("@PageNumber", currentPage));
            parameters.Add(new SqlParameter("@PageSize", pageSize));

            data_BH_TCKH.DataSource = dc.ExecuteQuery(sql, parameters.ToArray());
        }


        private void tbox_BH_TCKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_BH_TCKH_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadKhachHangWithCurrentCondition();
        }

        private void data_BH_TCKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BH_TCKH_Load(object sender, EventArgs e)
        {
            LoadAllKhachHang();
        }

        private void LoadKhachHangWithCurrentCondition()
        {
            string searchVal = tbox_BH_TCKH.Text.Trim();

            if (string.IsNullOrEmpty(searchVal))
            {
                LoadAllKhachHang();
            }
            else
            {
                string condition = " WHERE MaKH LIKE @search OR HoTen_KH LIKE @search";
                SqlParameter[] p = {
            new SqlParameter("@search", "%" + searchVal + "%")
        };

                LoadAllKhachHang(condition, p);
            }
        }

        private void btn_BH_TCKH_Tr_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadKhachHangWithCurrentCondition();
            }
        }

        private void btn_BH_TCKH_Next_Click(object sender, EventArgs e)
        {
            if (data_BH_TCKH.Rows.Count == pageSize)
            {
                currentPage++;
                LoadKhachHangWithCurrentCondition();
            }
        }

    }
}
