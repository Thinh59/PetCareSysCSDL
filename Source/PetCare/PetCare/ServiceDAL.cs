using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace PetCare
{
    public class DataConnection
    {
        protected static string connectionString =
        ConfigurationManager.ConnectionStrings["PetCareDB"].ConnectionString;

        public static void ChuyenCheDoKetNoi(bool isToiUu)
        {
            string key = isToiUu ? "PetCareDBOpt" : "PetCareDB";
            connectionString =
                ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static class LuuMaHoaDon
        {
            public static string MaHDVuaLap = "";
        }

        public SqlConnection getConnect()
        {
            return new SqlConnection(connectionString);
        }
        public DataTable ExecuteProcedure(string procName, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = getConnect())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public DataTable ExecuteQuery(string sql, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = getConnect())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandTimeout = 600;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public int ExecuteNonQueryProc(string procName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = getConnect())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = getConnect())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable ExecuteQuery_Text(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = getConnect())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch { }
            }
            return dt;
        }
    }
    public class ServiceDAL : DataConnection
    {
        // PHẦN A: TÀI KHOẢN & HỆ THỐNG (Đăng nhập/Đăng ký)

        public DataTable GetAvailableCustomers()
        {
            try { return ExecuteProcedure("dbo.sp_TT2_GetAvailableCustomers", null); }
            catch (Exception ex) { throw new Exception("Lỗi: " + ex.Message); }
        }

        public DataTable GetCustomerInfo(string maKH)
        {
            return ExecuteProcedure("dbo.sp_GetCustomerInfo", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public Dictionary<string, object> TaoTaiKhoanKhachHang(string hoTen, DateTime ngaySinh, string gioiTinh, string sdt, string email, string username, string passwordHash)
        {
            var result = new Dictionary<string, object>();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_TT1_TaoTKKH", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HoTenKH", hoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Date);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.Add("MaKH", reader["MaKH"].ToString());
                            result.Add("ID_TK", Convert.ToInt32(reader["ID_TK"]));
                        }
                    }
                }
                catch (SqlException ex) { throw new Exception(ex.Message); }
            }
            return result;
        }

        // PHẦN B: TIẾP TÂN - BÁC SĨ - BÁN HÀNG (Nghiệp vụ nội bộ)

        public DataTable GetAvailableDoctors(string maCN = null)
        {
            SqlParameter[] p = { new SqlParameter("@MaCN", (object)maCN ?? DBNull.Value) };
            return ExecuteProcedure("dbo.sp_TT3_GetAvailableDoctors", p);
        }

        public bool TruDiemLoyalty(string maKH, int diemTru)
        {
            try
            {
                string query = "UPDATE HOIVIEN SET DiemLoyalty = DiemLoyalty - @DiemTru WHERE MaKH = @MaKH";

                int result = ExecuteNonQuery(query, new SqlParameter[] {
            new SqlParameter("@MaKH", maKH),
            new SqlParameter("@DiemTru", diemTru)
        });

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi trừ điểm Loyalty: " + ex.Message);
            }
        }

        public DataTable SearchLSDV(string s)
        {
            return ExecuteProcedure("dbo.sp_TT3_SearchLSDV", new SqlParameter[] { new SqlParameter("@SearchValue", s) });
        }

        public DataTable GetLSDVDetail(string id)
        {
            return ExecuteProcedure("dbo.sp_TT3_GetLSDVDetail", new SqlParameter[] { new SqlParameter("@MaLSDV", id) });
        }

        public DataTable GetPetsByCustomer(string maKH)
        {
            return ExecuteProcedure("dbo.sp_TT3_GetPetsByCustomer", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public void XacNhanKhamBenh(string maLSDV, string maBacSi, DateTime thoiGianGD, string maThuCungChon)
        {
            SqlParameter[] p = {
                new SqlParameter("@MaLSDV", maLSDV),
                new SqlParameter("@MaBacSi", maBacSi),
                new SqlParameter("@ThoiGianGD", thoiGianGD),
                new SqlParameter("@MaThuCung", maThuCungChon)
            };
            ExecuteNonQueryProc("dbo.sp_TT3_XacNhanKhamBenh", p);
        }

        // --- Danh sách chờ & Tiêm phòng ---
        public DataTable GetAllMaLSDV() { return ExecuteProcedure("dbo.sp_TT3_GetPendingMaLSDV", null); }
        public DataTable GetAllMaKH() { return ExecuteProcedure("dbo.sp_TT3_GetPendingMaKH", null); }
        public DataTable GetAllSDT() { return ExecuteProcedure("dbo.sp_TT3_GetPendingSDT", null); }
        public DataTable GetAllMaLSDV_TiemPhong() { return ExecuteProcedure("dbo.sp_TT4_GetPendingMaLSDV", null); }
        public DataTable GetAllSDT_TiemPhong() { return ExecuteProcedure("dbo.sp_TT4_GetPendingSDT", null); }

        public DataTable SearchLSDV_TiemPhong(string s)
        {
            return ExecuteProcedure("dbo.sp_TT4_SearchLSDV_TiemPhong", new SqlParameter[] { new SqlParameter("@SearchValue", string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim()) });
        }

        public DataTable CheckVaccineHistory(string id)
        {
            return ExecuteProcedure("dbo.sp_TT4_CheckLichSuTiem", new SqlParameter[] { new SqlParameter("@MaThuCung", id) });
        }

        public void XacNhanTiemPhong_Update(string maLSDV, string maLSDVTP, string maBS, string loaiVX, string lieuLuong, DateTime thoiGian)
        {
            SqlParameter[] p = {
                new SqlParameter("@MaLSDV", maLSDV),
                new SqlParameter("@MaLSDVTP", maLSDVTP),
                new SqlParameter("@MaBacSi", maBS),
                new SqlParameter("@LoaiVacXin", loaiVX),
                new SqlParameter("@LieuLuong", lieuLuong),
                new SqlParameter("@ThoiGianGD", thoiGian)
            };
            ExecuteNonQueryProc("dbo.sp_TT4_XacNhanTiemPhong_Update", p);
        }

        // --- Gói tiêm ---
        public DataTable GetPackages() { return ExecuteProcedure("dbo.sp_TT5_GetPackages", null); }
        public void RegisterPackage(string maGoi, string maKH, DateTime ngay)
        {
            ExecuteNonQueryProc("dbo.sp_TT5_RegisterPackage", new SqlParameter[] {
                new SqlParameter("@MaGoiTiem", maGoi),
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@NgayDangKy", ngay)
            });
        }
        public DataTable GetAllCustomersForService() { return ExecuteProcedure("dbo.sp_TT5_GetAllCustomersBasic", null); }

        // --- Hóa đơn & Bán hàng (Nội bộ) ---
        public DataSet TaoHoaDon(string maKH, string maNV)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_TT6_TaoHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                try { conn.Open(); new SqlDataAdapter(cmd).Fill(ds); }
                catch (Exception ex) { throw new Exception("Lỗi tạo hóa đơn: " + ex.Message); }
            }
            return ds;
        }

        public DataTable GetChiTietVatTu(string maLSDV)
        {
            return ExecuteProcedure("dbo.sp_TT6_GetChiTietVatTu", new SqlParameter[] { new SqlParameter("@MaLSDV", maLSDV) });
        }

        public DataTable XemTruocDichVu(string maKH)
        {
            return ExecuteProcedure("dbo.sp_TT6_XemTruocDichVu", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public string LuuHoaDonMoi(string maKH, string maNV, decimal tongTien, DateTime ngayLap, string maCN)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_TT6_LuuHoaDonMoi", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                cmd.Parameters.AddWithValue("@NgayLap", ngayLap);

                cmd.Parameters.AddWithValue("@MaCN", maCN);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    string newMaHD = result != null ? result.ToString() : null;
                    if (!string.IsNullOrEmpty(newMaHD)) LuuMaHoaDon.MaHDVuaLap = newMaHD;
                    return newMaHD;
                }
                catch (Exception ex) { throw new Exception("Lỗi lưu hóa đơn: " + ex.Message); }
            }
        }
        public string GetNextMaHD()
        {
            DataTable dt = ExecuteQuery("sp_GetNextMaHD", null);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["NextMaHD"].ToString();
            return "HD00000001"; // Fallback
        }

        public DataTable GetThongTinKhuyenMaiKH(string maKH)
        {
            return ExecuteProcedure("sp_GetThongTinKhuyenMaiKH", new SqlParameter[] {
            new SqlParameter("@MaKH", maKH)
            });
        }

        public DataTable GetThongTinNhanVienLap(string maNV)
        {
            return ExecuteQuery("sp_GetThongTinNhanVienLap", new SqlParameter[] {
        new SqlParameter("@MaNV", maNV)
    });
        }

        public DataTable GetKhachHangChoThanhToan()
        {
            return ExecuteQuery("dbo.sp_TT6_GetKhachHangChoThanhToan", null);
        }

        // Trong ServiceDAL.cs

        // Trong ServiceDAL.cs

        public string ThanhToanGioHang(string maKH, string maLSGD, string hinhThuc, int tongTien, string maCN)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.SP_KH_ThanhToanGioHang", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.Parameters.AddWithValue("@MaLSGD", maLSGD);
                cmd.Parameters.AddWithValue("@HinhThucPay", hinhThuc);
                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                cmd.Parameters.AddWithValue("@MaCN", maCN);

                // Lưu ý: Không truyền tham số điểm vào đây vì SP không hỗ trợ.
                // Việc trừ điểm sẽ được thực hiện thủ công ở tầng Giao diện (UC_KH_ThanhToan) bằng hàm CapNhatDiem_HoiVien.

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi thanh toán giỏ hàng: " + ex.Message);
                }
            }
        }

        public void CapNhatDiemTrucTiep(string maKH, int diemMoi)
        {
            string sql = "UPDATE KHACHHANG SET DiemTichLuy = @Diem WHERE MaKH = @MaKH";

            SqlParameter[] p = {
        new SqlParameter("@Diem", diemMoi),
        new SqlParameter("@MaKH", maKH)
    };
            ExecuteNonQuery(sql, p);
        }
        public DataTable GetLichSuTuHoaDon(string maKH)
        {
            string sql = @"SELECT MaHD, NgayLap, TongTien 
                   FROM HOADON 
                   WHERE MaKH = @MaKH AND TrangThai = N'Đã thanh toán' 
                   ORDER BY NgayLap DESC";

            return ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }
        public DataSet GetDiemLoyalty(string maKH)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_HV_XemDiemLoyalty", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                new SqlDataAdapter(cmd).Fill(ds);
            }
            return ds;
        }
        public int GetDiemHienTai_HoiVien(string maKH)
        {
            string sql = "SELECT DiemLoyalty FROM HOIVIEN WHERE MaKH = @MaKH";

            DataTable dt = ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                return Convert.ToInt32(dt.Rows[0][0]);

            return 0;
        }

        public void CapNhatDiem_HoiVien(string maKH, int diemMoi)
        {
            string sql = "UPDATE HOIVIEN SET DiemLoyalty = @Diem WHERE MaKH = @MaKH";

            ExecuteNonQuery(sql, new SqlParameter[] {
        new SqlParameter("@Diem", diemMoi),
        new SqlParameter("@MaKH", maKH)
    });
        }
        public int GetDiemHienTai_Simple(string maKH)
        {
            string sql = "SELECT DiemTichLuy FROM KHACHHANG WHERE MaKH = @MaKH";
            DataTable dt = ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                return Convert.ToInt32(dt.Rows[0][0]);
            return 0;
        }
        public DataTable TimKiemHoaDon(string tuKhoa, string maNV)
        {
            return ExecuteProcedure("dbo.sp_TT7_SearchHoaDon", new SqlParameter[] {
                new SqlParameter("@TuKhoa", string.IsNullOrEmpty(tuKhoa) ? (object)DBNull.Value : tuKhoa),
                new SqlParameter("@MaNV", maNV)
            });
        }
        public DataTable GetMyMaHD(string maNV) { return ExecuteProcedure("dbo.sp_TT7_GetMyMaHD", new SqlParameter[] { new SqlParameter("@MaNV", maNV) }); }
        public DataTable GetMyCustomers(string maNV) { return ExecuteProcedure("dbo.sp_TT7_GetMyCustomers", new SqlParameter[] { new SqlParameter("@MaNV", maNV) }); }
        public DataTable GetLSDieuDong(string maNV, string maCN, int nam)
        {
            return ExecuteProcedure("dbo.sp_TT8_GetLSDieuDong", new SqlParameter[] {
                new SqlParameter("@MaNV", string.IsNullOrEmpty(maNV) ? (object)DBNull.Value : maNV),
                new SqlParameter("@MaCN", string.IsNullOrEmpty(maCN) ? (object)DBNull.Value : maCN),
                new SqlParameter("@Nam", nam <= 0 ? (object)DBNull.Value : nam)
            });
        }
        public DataTable GetListChiNhanh() { return ExecuteProcedure("dbo.sp_GetListChiNhanh", null); }
        public DataTable GetYearsInLSDieuDong() { return ExecuteProcedure("dbo.sp_TT8_GetYears", null); }

        public DataTable GetAllKhachHang()
        {
            return ExecuteQuery("SELECT MaKH, HoTen_KH, SDT_KH FROM KHACHHANG", null);
        }

        public DataTable GetDSBacSiRanh(string maCN)
        {
            if (string.IsNullOrEmpty(maCN)) maCN = "CN01";
            return ExecuteProcedure("dbo.SP_TT_GetBacSiRanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) });
        }

        public string DatLichTaiQuay(string maKH, string maCN, string tenDV, string maTC, DateTime thoiGian, string maBS, string maVX)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.SP_TT_DatLichTaiQuay", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKhachHang", maKH);
                cmd.Parameters.AddWithValue("@MaChiNhanh", maCN);
                cmd.Parameters.AddWithValue("@TenDichVu", tenDV);
                cmd.Parameters.AddWithValue("@MaThuCung", maTC);
                cmd.Parameters.AddWithValue("@ThoiGianHen", thoiGian);
                cmd.Parameters.AddWithValue("@MaBacSi", string.IsNullOrEmpty(maBS) ? (object)DBNull.Value : maBS);
                cmd.Parameters.AddWithValue("@MaVacXin", string.IsNullOrEmpty(maVX) ? (object)DBNull.Value : maVX);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        public DataTable TimKiemThuCung(string tuKhoa)
        {
            return ExecuteProcedure("dbo.SP_TT_TimKiemThuCung", new SqlParameter[] { new SqlParameter("@TuKhoa", tuKhoa) });
        }

        public DataTable GetLSTiemPhong_ByPet(string maThuCung)
        {
            return ExecuteProcedure("dbo.SP_TT_GetLSTiemPhong_ByPet", new SqlParameter[] { new SqlParameter("@MaThuCung", maThuCung) });
        }

        public DataTable GetLSKhamBenh_ByPet(string maThuCung)
        {
            return ExecuteProcedure("dbo.SP_TT_GetLSKhamBenh_ByPet", new SqlParameter[] { new SqlParameter("@MaThuCung", maThuCung) });
        }

        public DataTable GetAllMaThuCung()
        {
            string sql = "SELECT MaThuCung FROM ThuCung";
            return ExecuteQuery(sql, null);
        }

        // PHẦN C: QUẢN LÝ CHI NHÁNH

        public DataTable GetDoanhThu(string maCN, string loaiLoc, int nam, int quy, int thang, int ngay)
        {
            return ExecuteProcedure("dbo.sp_QLCN1_ThongKeDoanhThu", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN), new SqlParameter("@LoaiLoc", loaiLoc),
                new SqlParameter("@Nam", nam), new SqlParameter("@Quy", quy > 0 ? (object)quy : DBNull.Value),
                new SqlParameter("@Thang", thang > 0 ? (object)thang : DBNull.Value), new SqlParameter("@Ngay", ngay > 0 ? (object)ngay : DBNull.Value)
            });
        }

        public DataTable GetDSNV(string maCN)
        {
            return ExecuteProcedure("dbo.sp_QLCN2_GetDSNV", new SqlParameter[] { new SqlParameter("@MaCN", maCN) });
        }

        public void ThemNV(string maNV, string hoTen, DateTime ngSinh, DateTime ngVaoLam, string chucVu, int luong, string trangThai, string maCN)
        {
            ExecuteNonQueryProc("dbo.sp_QLCN2_ThemNV", new SqlParameter[] {
                new SqlParameter("@MaNV", maNV), new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", ngSinh), new SqlParameter("@NgayVaoLam", ngVaoLam),
                new SqlParameter("@ChucVu", chucVu), new SqlParameter("@Luong", luong),
                new SqlParameter("@TrangThai", trangThai), new SqlParameter("@MaCN", maCN)
            });
        }

        public void SuaNV(string maNV, string hoTen, DateTime ngSinh, DateTime ngVaoLam, string chucVu, int luong, string trangThai)
        {
            ExecuteNonQueryProc("dbo.sp_QLCN2_SuaNV", new SqlParameter[] {
                new SqlParameter("@MaNV", maNV), new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", ngSinh), new SqlParameter("@NgayVaoLam", ngVaoLam),
                new SqlParameter("@ChucVu", chucVu), new SqlParameter("@Luong", luong),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public void XoaNV(string maNV)
        {
            ExecuteNonQueryProc("dbo.sp_QLCN2_XoaNV", new SqlParameter[] { new SqlParameter("@MaNV", maNV) });
        }

        public int GetHieuSuatNV(string maNV, int nam, int quy, int thang, int ngay)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_QLCN3_XemHieuSuat", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@Quy", quy > 0 ? (object)quy : DBNull.Value);
                cmd.Parameters.AddWithValue("@Thang", thang > 0 ? (object)thang : DBNull.Value);
                cmd.Parameters.AddWithValue("@Ngay", ngay > 0 ? (object)ngay : DBNull.Value);
                try { conn.Open(); object res = cmd.ExecuteScalar(); return res != null ? Convert.ToInt32(res) : 0; }
                catch { return 0; }
            }
        }
        private DataTable GetStatsCommon(string proc, int nam, int quy, int thang, int ngay, string sort)
        {
            return ExecuteProcedure(proc, new SqlParameter[] {
                new SqlParameter("@Nam", nam), new SqlParameter("@Quy", quy > 0 ? (object)quy : DBNull.Value),
                new SqlParameter("@Thang", thang > 0 ? (object)thang : DBNull.Value), new SqlParameter("@Ngay", ngay > 0 ? (object)ngay : DBNull.Value),
                new SqlParameter("@LoaiSort", sort)
            });
        }
        public DataTable ThongKeVacxin(int nam, int quy, int thang, int ngay, string sort) { return GetStatsCommon("dbo.sp_QLCN4_ThongKeVacxin", nam, quy, thang, ngay, sort); }
        public DataTable ThongKeThuoc(int nam, int quy, int thang, int ngay, string sort) { return GetStatsCommon("dbo.sp_QLCN4_ThongKeThuoc", nam, quy, thang, ngay, sort); }
        public DataTable ThongKeSanPham(int nam, int quy, int thang, int ngay, string sort) { return GetStatsCommon("dbo.sp_QLCN4_ThongKeSanPham", nam, quy, thang, ngay, sort); }

        public DataTable GetDSSP(string maCN)
        {
            return ExecuteProcedure("dbo.sp_QLCN5_GetDSSP", new SqlParameter[] { new SqlParameter("@MaCN", maCN) });
        }

        public void ThemSP(string maSP, string tenSP, string loaiSP, int donGia, int slTon, string maCN)
        {
            ExecuteNonQueryProc("dbo.sp_QLCN5_ThemSP", new SqlParameter[] {
                new SqlParameter("@MaSP", maSP), new SqlParameter("@TenSP", tenSP),
                new SqlParameter("@LoaiSP", loaiSP), new SqlParameter("@DonGia", donGia),
                new SqlParameter("@SLTonKho", slTon), new SqlParameter("@MaCN", maCN)
            });
        }

        public void SuaSP(string maSP, string tenSP, string loaiSP, int donGia, int slTon, string maCN)
        {
            string query = "UPDATE CT_SPCN SET GiaSPCN = @Gia, SLTonKho = @SL WHERE MaSP = @MaSP AND MaCN = @MaCN";
            ExecuteNonQuery(query, new SqlParameter[] {
                new SqlParameter("@Gia", donGia), new SqlParameter("@SL", slTon),
                new SqlParameter("@MaSP", maSP), new SqlParameter("@MaCN", maCN)
            });
        }

        public void XoaSP(string maSP, string maCN)
        {
            string query = "DELETE FROM CT_SPCN WHERE MaSP = @MaSP AND MaCN = @MaCN";
            ExecuteNonQuery(query, new SqlParameter[] { new SqlParameter("@MaSP", maSP), new SqlParameter("@MaCN", maCN) });
        }

        public DataTable GetThongKePet(int nam, int quy, int thang, int ngay, string loaiDV)
        {
            return ExecuteProcedure("dbo.sp_QLCN6_ThongKePet", new SqlParameter[] {
                new SqlParameter("@Nam", nam), new SqlParameter("@Quy", quy > 0 ? (object)quy : DBNull.Value),
                new SqlParameter("@Thang", thang > 0 ? (object)thang : DBNull.Value), new SqlParameter("@Ngay", ngay > 0 ? (object)ngay : DBNull.Value),
                new SqlParameter("@LoaiDV", loaiDV)
            });
        }

        public DataTable GetLichSuPet(string maPet, string maLSDV)
        {
            return ExecuteProcedure("dbo.sp_QLCN6_XemLichSuPet", new SqlParameter[] { new SqlParameter("@MaPet", maPet), new SqlParameter("@MaLSDV", string.IsNullOrEmpty(maLSDV) ? (object)DBNull.Value : maLSDV) });
        }
        public DataTable GetMaLSDVByPet(string maPet) { return ExecuteProcedure("dbo.sp_QLCN6_GetMaLSDVByPet", new SqlParameter[] { new SqlParameter("@MaPet", maPet) }); }
        public DataTable GetMaSPByPet(string maPet) { return ExecuteProcedure("dbo.sp_QLCN6_GetMaSPByPet", new SqlParameter[] { new SqlParameter("@MaPet", maPet) }); }

        public DataTable GetThongKeKhachHang(DateTime tu, DateTime den, string loai)
        {
            return ExecuteProcedure("dbo.sp_QLCN7_ThongKeKhachHang", new SqlParameter[] { new SqlParameter("@TuNgay", tu), new SqlParameter("@DenNgay", den), new SqlParameter("@LoaiTK", loai) });
        }

        public DataTable GetDSDV(string maCN) { return ExecuteProcedure("dbo.sp_QLCN9_GetDSDV", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }); }

        public void ThemSuaDV(string maDV, string tenDV, int giaTien, string trangThai, string maCN)
        {
            ExecuteNonQueryProc("dbo.sp_QLCN9_ThemSuaDV", new SqlParameter[] {
                new SqlParameter("@MaDV", maDV), new SqlParameter("@TenDV", tenDV),
                new SqlParameter("@GiaTien", giaTien), new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@MaCN", maCN)
            });
        }

        public void XoaDV(string maDV, string maCN)
        {
            ExecuteNonQueryProc("dbo.sp_QLCN9_XoaDV", new SqlParameter[] { new SqlParameter("@MaDV", maDV), new SqlParameter("@MaCN", maCN) });
        }

        public DataTable GetDSHoaDon(string maCN, int nam, int quy, int thang, int ngay, string maHD)
        {
            return ExecuteProcedure("dbo.sp_QLCN10_GetDSHoaDon", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN), new SqlParameter("@Nam", nam),
                new SqlParameter("@Quy", quy > 0 ? (object)quy : DBNull.Value), new SqlParameter("@Thang", thang > 0 ? (object)thang : DBNull.Value),
                new SqlParameter("@Ngay", ngay > 0 ? (object)ngay : DBNull.Value), new SqlParameter("@MaHD", string.IsNullOrEmpty(maHD) ? (object)DBNull.Value : maHD)
            });
        }
        public DataTable GetChiTietHD(string maHD) { return ExecuteProcedure("dbo.sp_QLCN10_GetChiTietHD", new SqlParameter[] { new SqlParameter("@MaHD", maHD) }); }

        public DataTable GetThongKeLSDV(string maCN, int nam, int quy, int thang, int ngay, string maDV)
        {
            return ExecuteProcedure("dbo.sp_QLCN11_GetLSDV", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN), new SqlParameter("@Nam", nam),
                new SqlParameter("@Quy", quy > 0 ? (object)quy : DBNull.Value), new SqlParameter("@Thang", thang > 0 ? (object)thang : DBNull.Value),
                new SqlParameter("@Ngay", ngay > 0 ? (object)ngay : DBNull.Value), new SqlParameter("@MaDV", string.IsNullOrEmpty(maDV) || maDV == "Tất cả" ? (object)DBNull.Value : maDV)
            });
        }

        public double GetDiemDanhGiaTB(string maDV)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_QLCN11_GetDiemDanhGia", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaDV", string.IsNullOrEmpty(maDV) || maDV == "Tất cả" ? (object)DBNull.Value : maDV);
                try { conn.Open(); object res = cmd.ExecuteScalar(); return res != DBNull.Value ? Convert.ToDouble(res) : 0.0; }
                catch { return 0.0; }
            }
        }

        // Thêm vào class ServiceDAL

        public void LuuChiTietKhuyenMai(string maHD, string maKM, int soLuong, decimal tienKM)
        {
            // Lưu ý: maKM cho điểm Loyalty thường là 'KM004' hoặc 'LOYALTY' tùy dữ liệu của bạn.
            // Dưới đây tôi dùng câu lệnh INSERT trực tiếp
            string sql = "INSERT INTO CT_KHUYENMAI (MaHD, MaKM, SoLuongDung, TienKM) VALUES (@MaHD, @MaKM, @SoLuong, @TienKM)";

            ExecuteNonQuery(sql, new SqlParameter[] {
        new SqlParameter("@MaHD", maHD),
        new SqlParameter("@MaKM", maKM),
        new SqlParameter("@SoLuong", soLuong),
        new SqlParameter("@TienKM", tienKM)
            });
        }

        public int GetDiemDaSuDungChoGiaoDich(string maLSGD)
        {
            string sql = @"
            SELECT ISNULL(SUM(SoLuongDung), 0) 
            FROM CT_KHUYENMAI KM
            JOIN CT_HOADON CT ON KM.MaHD = CT.MaHD
            WHERE CT.MaLSGD = @MaLSGD 
              AND (KM.MaKM = 'KM004' OR KM.MaKM = 'LOYALTY')";

            try
            {
                object result = ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@MaLSGD", maLSGD) });
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch { return 0; }
        }
        public object ExecuteScalar(string sql, SqlParameter[] parameters)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        // PHẦN D: PHÂN HỆ KHÁCH HÀNG

        public DataTable GetDSTC(string maKH)
        {
            return ExecuteProcedure("dbo.sp_KH_QLTC_GetDSTC", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public void ThemThuCung(string ten, string loai, string giong, DateTime ngSinh, string gioiTinh, string tinhTrangSK, string maKH)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("dbo.SP_KH_ThemThuCung", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenThuCung", ten);
                cmd.Parameters.AddWithValue("@LoaiThuCung", loai);
                cmd.Parameters.AddWithValue("@Giong_TC", giong);
                cmd.Parameters.AddWithValue("@NgaySinh_TC", ngSinh);
                cmd.Parameters.AddWithValue("@GioiTinh_TC", gioiTinh);
                cmd.Parameters.AddWithValue("@TinhTrangSK", tinhTrangSK);
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                try { conn.Open(); cmd.ExecuteNonQuery(); }
                catch (SqlException ex) { throw new Exception(ex.Message); }
            }
        }

        public ThuCungView GetThuCungChiTiet(string maTC)
        {
            string query = "SELECT MaThuCung, TenThuCung, LoaiThuCung, Giong_TC, NgaySinh_TC, GioiTinh_TC, TinhTrangSK FROM THUCUNG WHERE MaThuCung = @MaTC";
            ThuCungView pet = null;
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaTC", maTC);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pet = new ThuCungView
                        {
                            MaThuCung = reader["MaThuCung"].ToString(),
                            Ten = reader["TenThuCung"].ToString(),
                            Loai = reader["LoaiThuCung"].ToString(),
                            Giong = reader["Giong_TC"].ToString(),
                            NgaySinh = Convert.ToDateTime(reader["NgaySinh_TC"]),
                            GioiTinh = reader["GioiTinh_TC"].ToString(),
                            TinhTrangSucKhoe = reader["TinhTrangSK"].ToString()
                        };
                    }
                }
            }
            return pet;
        }

        public bool SuaThuCung(ThuCungView pet, string maKH_ChuSoHuu)
        {
            try
            {
                ExecuteNonQueryProc("SP_KH_SuaThongTinThuCung", new SqlParameter[] {
                    new SqlParameter("@MaThuCung", pet.MaThuCung),
                    new SqlParameter("@TenThuCung", pet.Ten),
                    new SqlParameter("@LoaiThuCung", pet.Loai),
                    new SqlParameter("@Giong_TC", pet.Giong),
                    new SqlParameter("@NgaySinh_TC", pet.NgaySinh),
                    new SqlParameter("@GioiTinh_TC", pet.GioiTinh),
                    new SqlParameter("@TinhTrangSK", pet.TinhTrangSucKhoe),
                    new SqlParameter("@MaKH_ChuSoHuu", maKH_ChuSoHuu)
                });
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi khi sửa thú cưng: " + ex.Message); }
        }

        public void XoaThuCung(string maTC, string maKH_ChuSoHuu)
        {
            try
            {
                ExecuteNonQueryProc("dbo.SP_KH_XoaThuCung", new SqlParameter[] {
                    new SqlParameter("@MaThuCung", maTC),
                    new SqlParameter("@MaKH_ChuSoHuu", maKH_ChuSoHuu)
                });
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<TiemPhongView> GetLichSuTiemPhong(string maKH)
        {
            List<TiemPhongView> list = new List<TiemPhongView>();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_KH_XemLichSuTiemPhong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TiemPhongView
                        {
                            MaLSDV = reader["MaLSDV"].ToString(),
                            MaThuCung = reader["MaThuCung"].ToString(),
                            TenThuCung = reader["TenThuCung"].ToString(),
                            NgayTiem = Convert.ToDateTime(reader["NgayTiem"]),
                            TenVacXin = reader["TenVacXin"].ToString(),
                            LieuLuong = reader["LieuLuong"].ToString(),
                            BacSiPhuTrach = reader["BacSiPhuTrach"].ToString(),
                            MaGoiTiem = reader["MaGoiTiem"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<KhamBenhView> GetLichSuKhamBenh(string maKH)
        {
            List<KhamBenhView> list = new List<KhamBenhView>();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_KH_XemLichSuKhamBenh", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        KhamBenhView item = new KhamBenhView();
                        item.MaLichSuDichVu = dr["MaLichSuDichVu"].ToString();
                        item.ThuCung = dr["ThuCung"].ToString();
                        item.BacSi = dr["BacSi"].ToString();
                        item.TrieuChung = dr["TrieuChung"].ToString();
                        item.ChuanDoan = dr["ChuanDoan"].ToString();
                        item.CoToaThuoc = Convert.ToInt32(dr["CoToaThuoc"]);
                        if (dr["NgayKham"] != DBNull.Value) item.NgayKham = Convert.ToDateTime(dr["NgayKham"]);
                        if (dr["NgayHen"] != DBNull.Value) item.NgayHen = Convert.ToDateTime(dr["NgayHen"]);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<ToaThuocView> GetChiTietToaThuoc(string maLSDVKB)
        {
            List<ToaThuocView> list = new List<ToaThuocView>();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_KH_XemToaThuoc", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaLSDVKB", maLSDVKB);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ToaThuocView
                        {
                            MaThuoc = reader["MaThuoc"].ToString(),
                            SoLuongThuoc = Convert.ToInt32(reader["SoLuongThuoc"]),
                            DonViTinh = reader["DonViTinh"].ToString(),
                            LieuDung = reader["LieuDung"].ToString(),
                            ThanhTien = Convert.ToDecimal(reader["ThanhTien"])
                        });
                    }
                }
            }
            return list;
        }
        public DataTable GetDanhSachChiNhanh()
        {
            return ExecuteQuery("SELECT MaCN, TenCN FROM CHINHANH", null);
        }

        public DataTable GetDanhSachDichVu()
        {
            return ExecuteQuery("SELECT MaDichVu, TenDV FROM DICHVU WHERE TenDV IN (N'Khám bệnh', N'Tiêm phòng')", null);
        }

        public DataTable GetDanhSachVacXin()
        {
            return ExecuteProcedure("SP_GetDanhSachVacXin", null);
        }

        public string DatDichVu(string maKH, string maCN, string tenDV, string maTC, DateTime thoiGianHen, string maVX = null)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_KH_DatDichVu", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKhachHang", maKH);
                cmd.Parameters.AddWithValue("@MaChiNhanh", maCN);
                cmd.Parameters.AddWithValue("@TenDichVu", tenDV);
                cmd.Parameters.AddWithValue("@MaThuCung", maTC);
                cmd.Parameters.AddWithValue("@ThoiGianHen", thoiGianHen);
                cmd.Parameters.AddWithValue("@MaVacXin", (object)maVX ?? DBNull.Value);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
                catch (SqlException ex) { throw new Exception(ex.Message); }
            }
        }

        public bool KiemTraDichVuTaiChiNhanh(string maCN, string tenDV)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_KH_KiemTraCungCap", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaCN", maCN);
                cmd.Parameters.AddWithValue("@TenDV", tenDV);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result.ToString() == "1";
            }
        }
        public bool DatGoiTiemPhong(string maKH, string maGoi, string maTC, string maCN, string maVX, DateTime ngayHen)
        {
            ExecuteNonQueryProc("SP_KH_DatGoiTiemPhong", new SqlParameter[] {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@MaGoiDuocChon", maGoi),
                new SqlParameter("@MaThuCung", maTC),
                new SqlParameter("@MaChiNhanh", maCN),
                new SqlParameter("@MaVacXinDuocChon", maVX),
                new SqlParameter("@NgayHenTiem", ngayHen)
            });
            return true;
        }

        public DataTable GetVacXinTheoGoi(string maGoi)
        {
            return ExecuteProcedure("SP_KH_GetVacXinTheoGoi", new SqlParameter[] { new SqlParameter("@MaGoiTiem", maGoi) });
        }
        public DataTable GetDanhSachGoiTiem() { return ExecuteProcedure("SP_KH_GetDSGoiTiemChiTiet", null); }
        public DataTable GetDanhSachGoiTiemRutGon() { return ExecuteQuery("SELECT MaGoiTiem, TenGoi FROM GOITIEM", null); }

        public DataTable TaoGoiTiemMoi(string tenGoi, int soThang, DataTable noiDung)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_TaoGoiTiemMoi", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenGoiTiem", tenGoi);
                cmd.Parameters.AddWithValue("@SoThang", soThang);
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@NoiDungGoi", noiDung);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.UDT_NoiDungGoiTiem";
                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }
        public DataTable TimKiemThongMinh(string maCN, string searchValue)
        {
            return ExecuteProcedure("SP_KH_TimKiemThongMinh", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN),
                new SqlParameter("@SearchValue", (object)searchValue ?? DBNull.Value)
            });
        }

        public DataTable LocSanPham(string maCN, string maSP, string tenSP, string loaiSP, decimal? giaMin, decimal? giaMax)
        {
            return ExecuteProcedure("SP_KH_XemVaLocSanPham", new SqlParameter[] {
                new SqlParameter("@MaCN_Filter", maCN),
                new SqlParameter("@MaSP_Filter", (object)maSP ?? DBNull.Value),
                new SqlParameter("@TenSP_Filter", (object)tenSP ?? DBNull.Value),
                new SqlParameter("@LoaiSP_Filter", (object)loaiSP ?? DBNull.Value),
                new SqlParameter("@GiaMin_Filter", (object)giaMin ?? DBNull.Value),
                new SqlParameter("@GiaMax_Filter", (object)giaMax ?? DBNull.Value)
            });
        }

        public bool ThemVaoGioHang(string maKH, string maSP, string maCN)
        {
            ExecuteNonQueryProc("SP_KH_ThemSPVaoGioHang", new SqlParameter[] {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@MaCN_SuDung", maCN)
            });
            return true;
        }

        public DataTable GetChiTietGioHang(string maKH, string maCN)
        {
            return ExecuteProcedure("SP_KH_LayChiTietGioHang", new SqlParameter[] { new SqlParameter("@MaKH", maKH), new SqlParameter("@MaCN", maCN) });
        }

        public bool CapNhatGioHang(string maLSDV, string maSP, int soLuongMoi, string maCN)
        {
            ExecuteNonQueryProc("SP_KH_CapNhatGioHang", new SqlParameter[] {
                new SqlParameter("@MaLSDV_GioHang", maLSDV),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuongMoi", soLuongMoi),
                new SqlParameter("@MaCN_SuDung", maCN)
            });
            return true;
        }
        public DataTable GetDichVuDaDat(string maKH)
        {
            return ExecuteProcedure("SP_XemDichVuDaDat", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public bool HuyDichVu(string maLSDV)
        {
            try { ExecuteNonQueryProc("SP_HuyDichVuDaDat", new SqlParameter[] { new SqlParameter("@MaLSDV", maLSDV) }); return true; }
            catch (Exception ex) { throw new Exception("Lỗi hủy dịch vụ: " + ex.Message); }
        }

        public DataTable GetDichVuChuaThanhToan(string maKH)
        {
            return ExecuteProcedure("SP_KH_LayDichVuChoThanhToan", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public string GetLoaiKhachHang(string maKH)
        {
            using (SqlConnection conn = getConnect())
            {
                SqlCommand cmd = new SqlCommand("SELECT Loai_KH FROM KHACHHANG WHERE MaKH = @MaKH", conn);
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Khách hàng";
            }
        }

        public string ThanhToanHoaDon(string maKH, string maLSGD, string hinhThucPay, int diemDung, string maCN, string maKM)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_KH_ThanhToanHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.Parameters.AddWithValue("@MaLSGD", maLSGD);
                cmd.Parameters.AddWithValue("@HinhThucPay", hinhThucPay);
                cmd.Parameters.AddWithValue("@SoDiemLoyaltyDung", diemDung);
                cmd.Parameters.AddWithValue("@MaCN", maCN);
                cmd.Parameters.AddWithValue("@MaKM", string.IsNullOrEmpty(maKM) ? (object)DBNull.Value : maKM);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }
        public DataTable GetLichSuHoaDon(string maKH, string maHD = null, DateTime? ngayLap = null, string trangThai = null)
        {
            return ExecuteProcedure("SP_LS_HoaDon", new SqlParameter[] {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@MaHoaDonFilter", (object)maHD ?? DBNull.Value),
                new SqlParameter("@NgayLapFilter", (object)ngayLap?.Date ?? DBNull.Value),
                new SqlParameter("@TrangThaiFilter", (object)trangThai ?? DBNull.Value)
            });
        }

        public DataSet GetChiTietHoaDon(string maHD)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_LS_HoaDon_XemChiTiet", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                new SqlDataAdapter(cmd).Fill(ds);
            }
            return ds;
        }

        public DataTable GetDichVuChoDanhGia(string maKH)
        {
            return ExecuteProcedure("SP_DanhGia_LayDS_DV_HoanTat", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public bool LuuDanhGia(string maKH, string maDV, int diemDV, int diemNV, string binhLuan)
        {
            ExecuteNonQueryProc("SP_DanhGia_Luu", new SqlParameter[] {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@MaDichVu", maDV),
                new SqlParameter("@DiemDV", diemDV),
                new SqlParameter("@DiemNV", diemNV),
                new SqlParameter("@BinhLuan", (object)binhLuan ?? DBNull.Value)
            });
            return true;
        }

        public DataTable GetLichSuTuHoaDon_Simple(string maKH)
        {
            string sql = @"
        SELECT MaHD, NgayLap, TienThanhToan, TienTruocKM, TrangThaiHD 
        FROM HOADON 
        WHERE MaKH = @MaKH 
        ORDER BY NgayLap DESC, MaHD DESC";

            return ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }

        public int GetDiemDaDungTrongHoaDon(string maHD)
        {
            string sql = @"
        SELECT ISNULL(SUM(SoLuongDung), 0)
        FROM CT_KHUYENMAI
        WHERE MaHD = @MaHD AND (MaKM = 'KM004' OR MaKM = 'LOYALTY')";

            try
            {
                using (SqlConnection conn = getConnect())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch { return 0; }
        }
        public DataTable GetThongTinChiNhanh() { return ExecuteProcedure("SP_XemThongTinChiNhanh", null); }
        public DataTable GetDanhSachKhuyenMai(string maKH) { return ExecuteProcedure("SP_KH_XemKhuyenMai", new SqlParameter[] { new SqlParameter("@MaKH", maKH) }); }

        public string DangKyHoiVien(string maKH)
        {
            ExecuteNonQueryProc("SP_KH_DangKyHoiVien", new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
            return maKH;
        }

        public DataTable GetLichBacSi(string maCN, DateTime ngayChon)
        {
            return ExecuteProcedure("SP_KH_TraCuuLichBacSi", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN),
                new SqlParameter("@NgayChon", ngayChon.Date)
            });
        }

        public DataTable GetThongTinHoiVien(string maKH) { return ExecuteProcedure("SP_HV_XemThongTin", new SqlParameter[] { new SqlParameter("@MaKH", maKH) }); }

        public DataSet GetThongKeChiTieu(string maKH)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("SP_HV_ThongKeChiTieuNam", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                new SqlDataAdapter(cmd).Fill(ds);
            }
            return ds;
        }

        // PHẦN E: QUẢN LÝ CẤP CAO & THỐNG KÊ TOÀN HỆ THỐNG (Admin / Manager)
        public int GetDoanhThuCongTyTheoNgay(int? ngay = null, int? thang = null, int? nam = null)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_DoanhThu_CongTy_TheoNgay", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ngay", ngay ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Thang", thang ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nam", nam ?? (object)DBNull.Value);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }

        public int GetDoanhThuChiNhanhTheoNgay(string maCN, int? ngay = null, int? thang = null, int? nam = null)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_DoanhThu_ChiNhanh_TheoNgay", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaCN", maCN);
                cmd.Parameters.AddWithValue("@Ngay", ngay ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Thang", thang ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nam", nam ?? (object)DBNull.Value);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }

        public int GetDoanhThuCongTyTheoQuy(int? quy = null, int? nam = null)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_DoanhThu_CongTy_TheoQuy", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Quy", quy ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nam", nam ?? (object)DBNull.Value);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }

        public int GetDoanhThuChiNhanhTheoQuy(string maCN, int? quy = null, int? nam = null)
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_DoanhThu_ChiNhanh_TheoQuy", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaCN", maCN);
                cmd.Parameters.AddWithValue("@Quy", quy ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nam", nam ?? (object)DBNull.Value);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }
        public bool ThemChiNhanh(string maCN, string tenCN, string diaChiCN, string sdtCN, TimeSpan timeMoCua, TimeSpan timeDongCua)
        {
            return ExecuteNonQueryProc("sp_ThemChiNhanh", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN), new SqlParameter("@TenCN", tenCN),
                new SqlParameter("@DiaChiCN", diaChiCN), new SqlParameter("@SDT_CN", sdtCN),
                new SqlParameter("@TimeMoCua", timeMoCua), new SqlParameter("@TimeDongCua", timeDongCua)
            }) > 0;
        }

        public bool SuaChiNhanh(string maCN, string tenCN, string diaChiCN, string sdtCN, TimeSpan timeMoCua, TimeSpan timeDongCua)
        {
            return ExecuteNonQueryProc("sp_SuaChiNhanh", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN), new SqlParameter("@TenCN", tenCN),
                new SqlParameter("@DiaChiCN", diaChiCN), new SqlParameter("@SDT_CN", sdtCN),
                new SqlParameter("@TimeMoCua", timeMoCua), new SqlParameter("@TimeDongCua", timeDongCua)
            }) > 0;
        }

        public bool XoaChiNhanh(string maCN)
        {
            return ExecuteNonQueryProc("sp_XoaChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }) > 0;
        }

        public DataTable XemChiNhanh(string maCN = null, string tenCN = null, string diaChiCN = null, string sdtCN = null, TimeSpan? timeMoCua = null, TimeSpan? timeDongCua = null)
        {
            return ExecuteProcedure("sp_XemChiNhanh", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN ?? (object)DBNull.Value),
                new SqlParameter("@TenCN", tenCN ?? (object)DBNull.Value),
                new SqlParameter("@DiaChiCN", diaChiCN ?? (object)DBNull.Value),
                new SqlParameter("@SDT_CN", sdtCN ?? (object)DBNull.Value),
                new SqlParameter("@TimeMoCua", timeMoCua ?? (object)DBNull.Value),
                new SqlParameter("@TimeDongCua", timeDongCua ?? (object)DBNull.Value)
            });
        }

        public DataTable GetThongKeHieuSuatCongTy() { return ExecuteProcedure("sp_ThongKe_HieuSuat_CongTy", null); }
        public DataTable GetThongKeHieuSuatChiNhanh(string maCN) { return ExecuteProcedure("sp_ThongKe_HieuSuat_ChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }); }

        public bool ThemNhanVien(string maNV, string hoTen, DateTime ngSinh, string chucVu, DateTime ngVaoLam, int luong, string chiNhanh, string trangThai, string tenDangNhap)
        {
            try
            {
                using (SqlConnection conn = getConnect())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThemNhanVien", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@HoTenNV", hoTen);
                    cmd.Parameters.AddWithValue("@NgaySinhNV", ngSinh);
                    cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", ngVaoLam);
                    cmd.Parameters.AddWithValue("@Luong", luong);

                    if (string.IsNullOrEmpty(chiNhanh) || chiNhanh == "Trụ sở chính")
                        cmd.Parameters.AddWithValue("@ChiNhanhLV", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ChiNhanhLV", chiNhanh);

                    cmd.Parameters.AddWithValue("@TrangThaiNV", trangThai);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    cmd.ExecuteNonQuery();
                    return true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL Thêm: " + ex.Message);
                return false;
            }
        }

        public bool SuaNhanVien(string maNV, string hoTen, DateTime ngSinh, string chucVu, DateTime ngVaoLam, int luong, string chiNhanh, string trangThai, string tenDangNhap)
        {
            try
            {
                using (SqlConnection conn = getConnect())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SuaNhanVien", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@HoTenNV", hoTen);
                    cmd.Parameters.AddWithValue("@NgaySinhNV", ngSinh);
                    cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", ngVaoLam);
                    cmd.Parameters.AddWithValue("@Luong", luong);

                    if (string.IsNullOrEmpty(chiNhanh) || chiNhanh == "Trụ sở chính")
                        cmd.Parameters.AddWithValue("@ChiNhanhLV", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ChiNhanhLV", chiNhanh);

                    cmd.Parameters.AddWithValue("@TrangThaiNV", trangThai);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    cmd.ExecuteNonQuery();
                    return true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL Sửa: " + ex.Message);
                return false;
            }
        }

        public bool XoaNhanVien(string maNV)
        {
            try
            {
                using (SqlConnection conn = getConnect())
                {
                    conn.Open();
                    string query = "UPDATE NHANVIEN SET TrangThaiNV = N'Đã nghỉ việc' WHERE MaNV = @MaNV";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text; // Dùng Text query cho nhanh
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL Xóa: " + ex.Message);
                return false;
            }
        }

        public DataTable XemNhanVien(string maNV = null, string hoTenNV = null, string chiNhanhLV = null, string chucVu = null)
        {
            return ExecuteProcedure("sp_XemNhanVien", new SqlParameter[] {
                new SqlParameter("@MaNV", maNV ?? (object)DBNull.Value),
                new SqlParameter("@HoTenNV", hoTenNV ?? (object)DBNull.Value),
                new SqlParameter("@ChiNhanhLV", chiNhanhLV ?? (object)DBNull.Value),
                new SqlParameter("@ChucVu", chucVu ?? (object)DBNull.Value)
            });
        }
        public bool TaoKhuyenMai(string maKM, string loaiKM, int giaKM)
        {
            return ExecuteNonQueryProc("sp_TaoKhuyenMai", new SqlParameter[] {
                new SqlParameter("@MaKM", maKM), new SqlParameter("@LoaiKM", loaiKM), new SqlParameter("@GiaKM", giaKM)
            }) > 0;
        }

        public bool SuaKhuyenMai(string maKM, string loaiKM, int giaKM)
        {
            return ExecuteNonQueryProc("sp_SuaKhuyenMai", new SqlParameter[] {
                new SqlParameter("@MaKM", maKM), new SqlParameter("@LoaiKM", loaiKM), new SqlParameter("@GiaKM", giaKM)
            }) > 0;
        }

        public bool XoaKhuyenMai(string maKM)
        {
            return ExecuteNonQueryProc("sp_XoaKhuyenMai", new SqlParameter[] { new SqlParameter("@MaKM", maKM) }) > 0;
        }

        public DataTable XemKhuyenMai(string maKM = null, string loaiKM = null, int? giaKM = null)
        {
            return ExecuteProcedure("sp_XemKhuyenMai", new SqlParameter[] {
                new SqlParameter("@MaKM", maKM ?? (object)DBNull.Value),
                new SqlParameter("@LoaiKM", loaiKM ?? (object)DBNull.Value),
                new SqlParameter("@GiaKM", giaKM ?? (object)DBNull.Value)
            });
        }
        public bool ThemDichVu(string maDichVu, string tenDV, int giaTienDV)
        {
            return ExecuteNonQueryProc("sp_ThemDichVu", new SqlParameter[] {
                new SqlParameter("@MaDichVu", maDichVu), new SqlParameter("@TenDV", tenDV), new SqlParameter("@GiaTienDV", giaTienDV)
            }) > 0;
        }

        public bool SuaDichVu(string maDichVu, string tenDV, int giaTienDV)
        {
            return ExecuteNonQueryProc("sp_SuaDichVu", new SqlParameter[] {
                new SqlParameter("@MaDichVu", maDichVu), new SqlParameter("@TenDV", tenDV), new SqlParameter("@GiaTienDV", giaTienDV)
            }) > 0;
        }

        public bool XoaDichVu(string maDichVu)
        {
            return ExecuteNonQueryProc("sp_XoaDichVu", new SqlParameter[] { new SqlParameter("@MaDichVu", maDichVu) }) > 0;
        }

        public DataTable XemDichVu(string maDichVu = null, string tenDV = null, int? giaTienDV = null)
        {
            return ExecuteProcedure("sp_XemDichVu", new SqlParameter[] {
                new SqlParameter("@MaDichVu", maDichVu ?? (object)DBNull.Value),
                new SqlParameter("@TenDV", tenDV ?? (object)DBNull.Value),
                new SqlParameter("@GiaTienDV", giaTienDV ?? (object)DBNull.Value)
            });
        }
        public DataTable ThongKeSanPhamCongTy() { return ExecuteProcedure("sp_ThongKeSanPham_CongTy", null); }
        public DataTable ThongKeSanPhamChiNhanh(string maCN) { return ExecuteProcedure("sp_ThongKeSanPham_ChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }); }

        public DataTable ThongKeThuocCongTy() { return ExecuteProcedure("sp_ThongKeThuoc_CongTy", null); }
        public DataTable ThongKeThuocChiNhanh(string maCN) { return ExecuteProcedure("sp_ThongKeThuoc_ChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }); }

        public DataTable ThongKeVaccineCongTy() { return ExecuteProcedure("sp_ThongKeVaccine_CongTy", null); }
        public DataTable ThongKeVaccineChiNhanh(string maCN) { return ExecuteProcedure("sp_ThongKeVaccine_ChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }); }

        public DataTable ThongKeKhachHangCongTy() { return ExecuteProcedure("sp_ThongKeKhachHang_CongTy", null); }
        public DataTable ThongKeKhachHangChiNhanh(string maCN) { return ExecuteProcedure("sp_ThongKeKhachHang_ChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN) }); }

        public DataTable XemDanhSachThuCung(string maThuCung = null, string tenThuCung = null, string maKH = null, string loaiThuCung = null)
        {
            return ExecuteProcedure("sp_XemDanhSachThuCung", new SqlParameter[] {
                new SqlParameter("@MaThuCung", maThuCung ?? (object)DBNull.Value),
                new SqlParameter("@TenThuCung", tenThuCung ?? (object)DBNull.Value),
                new SqlParameter("@MaKH", maKH ?? (object)DBNull.Value),
                new SqlParameter("@LoaiThuCung", loaiThuCung ?? (object)DBNull.Value)
            });
        }

        public bool ThemThuCung(string maThuCung, string tenThuCung, string loaiThuCung, string giongTC, DateTime ngaySinhTC, string gioiTinhTC, string tinhTrangSK, string maKH)
        {
            return ExecuteNonQueryProc("sp_ThemThuCung", new SqlParameter[] {
                new SqlParameter("@MaThuCung", maThuCung), new SqlParameter("@TenThuCung", tenThuCung),
                new SqlParameter("@LoaiThuCung", loaiThuCung), new SqlParameter("@Giong_TC", giongTC),
                new SqlParameter("@NgaySinh_TC", ngaySinhTC), new SqlParameter("@GioiTinh_TC", gioiTinhTC),
                new SqlParameter("@TinhTrangSK", tinhTrangSK), new SqlParameter("@MaKH", maKH)
            }) > 0;
        }

        public bool CapNhatThuCung(string maThuCung, string tenThuCung, string loaiThuCung, string giongTC, DateTime ngaySinhTC, string gioiTinhTC, string tinhTrangSK, string maKH)
        {
            return ExecuteNonQueryProc("sp_CapNhatThuCung", new SqlParameter[] {
                new SqlParameter("@MaThuCung", maThuCung), new SqlParameter("@TenThuCung", tenThuCung),
                new SqlParameter("@LoaiThuCung", loaiThuCung), new SqlParameter("@Giong_TC", giongTC),
                new SqlParameter("@NgaySinh_TC", ngaySinhTC), new SqlParameter("@GioiTinh_TC", gioiTinhTC),
                new SqlParameter("@TinhTrangSK", tinhTrangSK), new SqlParameter("@MaKH", maKH)
            }) > 0;
        }

        public bool XoaThuCung(string maThuCung)
        {
            return ExecuteNonQueryProc("sp_XoaThuCung", new SqlParameter[] { new SqlParameter("@MaThuCung", maThuCung) }) > 0;
        }

        public DataTable GetLSTiemPhongByPet(string maThuCung, DateTime? ngayTiem = null)
        {
            return ExecuteProcedure("sp_GetLSTiemPhong_ByPet_DateOptional", new SqlParameter[] {
                new SqlParameter("@MaThuCung", maThuCung),
                new SqlParameter("@NgayTiem", ngayTiem ?? (object)DBNull.Value)
            });
        }

        public DataTable XemDSHoaDonCongTyNgayThangNam(int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemDSHoaDon_CongTy_NgayThangNam", new SqlParameter[] {
                new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value)
            });
        }
        public DataTable XemDSHoaDonChiNhanhNgayThangNam(string maCN, int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemDSHoaDon_ChiNhanh_NgayThangNam", new SqlParameter[] {
                new SqlParameter("@MaCN", maCN), new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value)
            });
        }
        public DataTable XemDSHoaDonCongTyQuyNam(int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemDSHoaDon_CongTy_QuyNam", new SqlParameter[] { new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemDSHoaDonChiNhanhQuyNam(string maCN, int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemDSHoaDon_ChiNhanh_QuyNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }

        public DataTable XemLSDVMuaHangCongTyQuyNam(int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVMuaHang_CongTy_QuyNam", new SqlParameter[] { new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVMuaHangChiNhanhQuyNam(string maCN, int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVMuaHang_ChiNhanh_QuyNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVMuaHangCongTyNgayThangNam(int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVMuaHang_CongTy_NgayThangNam", new SqlParameter[] { new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVMuaHangChiNhanhNgayThangNam(string maCN, int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVMuaHang_ChiNhanh_NgayThangNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }

        public DataTable XemLSDVKhamBenhCongTyNgayThangNam(int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVKhamBenh_CongTy_NgayThangNam", new SqlParameter[] { new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVKhamBenhChiNhanhNgayThangNam(string maCN, int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVKhamBenh_ChiNhanh_NgayThangNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVKhamBenhCongTyQuyNam(int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVKhamBenh_CongTy_QuyNam", new SqlParameter[] { new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVKhamBenhChiNhanhQuyNam(string maCN, int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVKhamBenh_ChiNhanh_QuyNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }

        public DataTable XemLSDVTiemPhongCongTyNgayThangNam(int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVTiemPhong_CongTy_NgayThangNam", new SqlParameter[] { new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVTiemPhongChiNhanhNgayThangNam(string maCN, int? ngay = null, int? thang = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVTiemPhong_ChiNhanh_NgayThangNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Ngay", ngay ?? (object)DBNull.Value), new SqlParameter("@Thang", thang ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVTiemPhongCongTyQuyNam(int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVTiemPhong_CongTy_QuyNam", new SqlParameter[] { new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDVTiemPhongChiNhanhQuyNam(string maCN, int? quy = null, int? nam = null)
        {
            return ExecuteProcedure("sp_XemLSDVTiemPhong_ChiNhanh_QuyNam", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@Quy", quy ?? (object)DBNull.Value), new SqlParameter("@Nam", nam ?? (object)DBNull.Value) });
        }

        public DataTable ThongKeHoiVien() { return ExecuteProcedure("sp_ThongKe_HoiVien_LietKe", null); }
        public int GetTongSoLuongHoiVien()
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_ThongKe_HoiVien_TongSoLuong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                object res = cmd.ExecuteScalar();
                return res != null ? Convert.ToInt32(res) : 0;
            }
        }
        public int GetChiTieuTrungBinhHoiVien()
        {
            using (SqlConnection conn = getConnect())
            using (SqlCommand cmd = new SqlCommand("sp_ThongKe_HoiVien_ChiTieuTrungBinh", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                object res = cmd.ExecuteScalar();
                return res != null ? Convert.ToInt32(res) : 0;
            }
        }

        public DataTable XemLSDDToanCongTy(string maNV = null, DateTime? ngayDieuDong = null)
        {
            return ExecuteProcedure("sp_XemLSDD_ToanCongTy", new SqlParameter[] { new SqlParameter("@MaNV", maNV ?? (object)DBNull.Value), new SqlParameter("@NgayDieuDong", ngayDieuDong ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDDTuChiNhanh(string maCN, string maNV = null, DateTime? ngayDieuDong = null)
        {
            return ExecuteProcedure("sp_XemLSDD_TuChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@MaNV", maNV ?? (object)DBNull.Value), new SqlParameter("@NgayDieuDong", ngayDieuDong ?? (object)DBNull.Value) });
        }
        public DataTable XemLSDDDenChiNhanh(string maCN, string maNV = null, DateTime? ngayDieuDong = null)
        {
            return ExecuteProcedure("sp_XemLSDD_DenChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN), new SqlParameter("@MaNV", maNV ?? (object)DBNull.Value), new SqlParameter("@NgayDieuDong", ngayDieuDong ?? (object)DBNull.Value) });
        }

        public DataTable GetAllDichVuByChiNhanh(string maCN = null)
        {
            return ExecuteProcedure("GetAllDichVuByChiNhanh", new SqlParameter[] { new SqlParameter("@MaCN", maCN ?? (object)DBNull.Value) });
        }

        public List<string> GetDanhSachMaChiNhanh()
        {
            List<string> list = new List<string>();
            DataTable dt = ExecuteQuery("SELECT MaCN FROM CHINHANH", null);
            foreach (DataRow row in dt.Rows) list.Add(row["MaCN"].ToString());
            return list;
        }

        public List<string> GetDanhSachMaNhanVien()
        {
            List<string> list = new List<string>();
            DataTable dt = ExecuteQuery("SELECT MaNV FROM NHANVIEN", null);
            foreach (DataRow row in dt.Rows) list.Add(row["MaNV"].ToString());
            return list;
        }

        public DataTable GetThongTinChiNhanhDonGian()
        {
            return ExecuteQuery("SELECT MaCN, TenCN FROM CHINHANH ORDER BY TenCN", null);
        }

        // PHẦN QUẢN TRỊ VIÊN (QTV)

        public void ThemTaiKhoan(string tenDangNhap, string matKhau, string loaiTK)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThemTaiKhoan", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@LoaiTK", loaiTK);

                cmd.ExecuteNonQuery();
            }
        }

        public void XoaTaiKhoan(string tenDangNhap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_XoaTaiKhoan", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                cmd.ExecuteNonQuery();
            }
        }

        public void DoiLoaiTaiKhoan(string tenDangNhap, string loaiTKMoi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DoiLoaiTaiKhoan", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@LoaiTKMoi", loaiTKMoi);

                cmd.ExecuteNonQuery();
            }
        }
        public void DoiMatKhau(string tenDangNhap, string matKhauMoi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DoiMatKhau", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);

                cmd.ExecuteNonQuery();
            }
        }
    }
}