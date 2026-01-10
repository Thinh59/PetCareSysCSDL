using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCare
{
    public interface IReturnToMainPage
    {
        event Action QuayVeTrangChu;
    }
    public static class SessionData
    {
        public static int ID_TK { get; set; }          
        public static string Quyen { get; set; }        
        public static string TenHienThi { get; set; }   

        public static string MaNV { get; set; }         
        public static string MaKH { get; set; }         

        public static string MaCN { get; set; } 

        public static string MaCN_DangChon { get; set; }
        public static string TenCN { get; set; }

        public static void Clear()
        {
            ID_TK = 0;
            Quyen = "";
            TenHienThi = "";
            MaNV = "";
            MaKH = "";
            MaCN = "";
            TenCN = "";
        }
    }

    public class ThuCungView
    {
        public string MaThuCung { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public string Giong { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string TinhTrangSucKhoe { get; set; }
    }

    public class TiemPhongView
    {
        public string MaLSDV { get; set; }
        public string MaThuCung { get; set; }
        public string TenThuCung { get; set; }
        public DateTime NgayTiem { get; set; }
        public string TenVacXin { get; set; }
        public string LieuLuong { get; set; }
        public string BacSiPhuTrach { get; set; }
        public string MaGoiTiem { get; set; }
    }

    public class KhamBenhView
    {
        public string MaLichSuDichVu { get; set; }
        public string ThuCung { get; set; }
        public DateTime? NgayKham { get; set; }
        public string BacSi { get; set; }
        public string TrieuChung { get; set; }
        public string ChuanDoan { get; set; }
        public DateTime? NgayHen { get; set; }
        public int CoToaThuoc { get; set; } 
    }

    public class ToaThuocView
    {
        public string MaThuoc { get; set; }
        public int SoLuongThuoc { get; set; }
        public string DonViTinh { get; set; }
        public string LieuDung { get; set; }
        public decimal ThanhTien { get; set; }
        public string ThanhTienFormat => ThanhTien.ToString("N0") + " VNĐ";
    }
}