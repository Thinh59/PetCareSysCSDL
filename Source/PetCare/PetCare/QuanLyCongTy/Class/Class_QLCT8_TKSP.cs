using System;

namespace PetCare
{
    class Class_QLCT8_TKSP_SPBL
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSP { get; set; }
        public decimal GiaBan { get; set; }

        public int TonKho { get; set; }
        public int DaBan { get; set; }
    }

    class Class_QLCT8_TKSP_Thuoc
    {
        public string MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DonViTinh { get; set; }
        public decimal GiaBan { get; set; }

        public DateTime HSD { get; set; }

        public int TonKho { get; set; }
        public int DaBan { get; set; }
    }

    class Class_QLCT8_TKSP_Vaccine
    {
        public string MaVaccine { get; set; }
        public string TenVaccine { get; set; }
        public decimal GiaTien { get; set; }
        public int TonKho { get; set; }

        public int DaBan { get; set; }
    }
}
