USE PetCareDBOpt
GO

--KỊCH BẢN 01:
--TT3:
USE PetCareDB
GO

EXEC dbo.sp_TT3_SearchLSDV '0350004540'
GO

USE PetCareDBOpt
GO

CREATE NONCLUSTERED INDEX IX_LSDV_TrangThai 
ON [dbo].[LS_DV] ([TrangThaiGD]) 
INCLUDE ([MaKH], [MaDichVu], [NgayDatTruoc]);

-- Tạo Index cho số điện thoại để tìm khách nhanh
CREATE NONCLUSTERED INDEX IX_KHACHHANG_SDT 
ON [dbo].[KHACHHANG] ([SDT_KH])
INCLUDE ([HoTen_KH]); 
GO

-- Tạo thêm Index cho các khóa ngoại để JOIN nhanh hơn (giảm chi phí Join)
CREATE NONCLUSTERED INDEX IX_LSDVKHAM_MaThuCung 
ON [dbo].[LS_DVKHAMBENH] ([MaThuCung]);
GO

CREATE NONCLUSTERED INDEX IX_THUCUNG_MaKH
ON [dbo].[THUCUNG] ([MaKH]);
GO

-- Tạo Index tối ưu cho bảng Lịch Sử Dịch Vụ
CREATE NONCLUSTERED INDEX IX_LSDV_GoiYTuSQL
ON [dbo].[LS_DV] ([MaDichVu], [TrangThaiGD])
INCLUDE ([MaKH], [NgayDatTruoc]);
GO

--KỊCH BẢN 02:
-- KH4
USE PetCareDBOpt
GO

EXEC dbo.SP_KH_XemLichSuKhamBenh 'KH00001';
GO

-- 1. Tối ưu bảng Toa thuốc
CREATE NONCLUSTERED INDEX IX_TOATHUOC_MaLSDV 
ON [dbo].[TOA_THUOC] ([MaLSDV]);
GO

-- 2. Tối ưu bảng Lịch sử khám: Đánh vào khóa ngoại và sắp xếp ngày khám
CREATE NONCLUSTERED INDEX IX_LSDVKHAM_MaThuCung_NgayKham 
ON [dbo].[LS_DVKHAMBENH] ([MaThuCung], [NgayKham] DESC) 
INCLUDE ([BacSiPhuTrach], [NgayHen], [MaLSDVKB]);
GO

-- 3. Tối ưu bảng Thú cưng: Tăng tốc lọc theo chủ sở hữu (MaKH)
/*CREATE NONCLUSTERED INDEX IX_THUCUNG_MaKH 
ON [dbo].[THUCUNG] ([MaKH]) 
INCLUDE ([TenThuCung]);
GO*/ --Đã tạo cho TT3

--KỊCH BẢN 04:
-- QLCN1
-- Thực thi thống kê doanh thu năm 2024 cho chi nhánh CN01

USE PetCareDBOpt
GO

EXEC dbo.sp_QLCN1_ThongKeDoanhThu @MaCN = 'CN01', @LoaiLoc = 'Nam', @Nam = 2024;
GO

-- 1. Tối ưu bảng HOADON: Lọc nhanh các hóa đơn "Đã thanh toán" của từng Chi nhánh
CREATE NONCLUSTERED INDEX IX_HOADON_ThongKeDoanhThu
ON [dbo].[HOADON] ([TrangThaiHD], [MaCN])
INCLUDE ([NgayLap], [MaHD]); -- Include MaHD để join với chi tiết hóa đơn
GO

-- 2. Tối ưu bảng CT_HOADON: Tìm kiếm nhanh chi tiết dịch vụ từ một mã hóa đơn đã lọc
CREATE NONCLUSTERED INDEX IX_CTHOADON_MaHD 
ON [dbo].[CT_HOADON] ([MaHD]) 
INCLUDE ([MaLSGD], [TongPhiDV]);
GO

-- 3. Tối ưu bảng LS_DV: Tăng tốc lấy MaDichVu
CREATE NONCLUSTERED INDEX IX_LSDV_Lookup 
ON [dbo].[LS_DV] ([MaLSDV]) 
INCLUDE ([MaDichVu]);
GO

--KỊCH BẢN 03:
---BS8
EXEC dbo.sp_GetDanhSachHoSoKB @MaTC = 'PET04313'

USE PetCareDBOpt
GO

-- Tạo chỉ mục trên bảng LS_DVKHAMBENH
-- Tối ưu việc tìm kiếm theo MaThuCung và loại bỏ toán tử Sort bằng cách sắp xếp sẵn theo NgayKham
CREATE NONCLUSTERED INDEX IX_LS_DVKHAMBENH_MaThuCung_NgayKham
ON [dbo].[LS_DVKHAMBENH] ([MaThuCung], [NgayKham] DESC)
INCLUDE ([MaLSDVKB], [BacSiPhuTrach], [NgayHen]);
GO

-- Tạo thêm chỉ mục trên các khóa ngoại của bảng triệu chứng và chẩn đoán để tối ưu phép JOIN
CREATE NONCLUSTERED INDEX IX_TRIEUCHUNG_MaLSKB ON [dbo].[TRIEUCHUNG] ([MaLSKB]);
CREATE NONCLUSTERED INDEX IX_CHUANDOAN_MaLSKB ON [dbo].[CHUANDOAN] ([MaLSKB]);
GO

-- Index cho bảng THUCUNG để hỗ trợ
CREATE NONCLUSTERED INDEX IX_THUCUNG_Covering
ON [dbo].[THUCUNG] ([MaThuCung])
INCLUDE ([MaKH], [LoaiThuCung], [NgaySinh_TC]);
GO


EXEC dbo.sp_GetDanhSachHoSoKB @MaTC = 'PET04313'


--KỊCH BẢN 05: 
---BS: Xem danh gia

EXEC dbo.sp_GetDanhGiaBacSi @LoaiDichVu = N'Khám bệnh', @MaKH = N'KH01384'
GO

USE PetCareDBOpt;
GO

-- 1. Tạo Index bao phủ cho bảng DANHGIA
CREATE NONCLUSTERED INDEX IX_DANHGIA_MaKH_MaDV
ON [dbo].[DANHGIA] ([MaKH], [MaDV], [MaDG] DESC)
INCLUDE ([DiemDV], [DiemNV], [MucDoHaiLong], [BinhLuan]);
GO

-- 2. Tạo Index cho bảng KHACHHANG
CREATE NONCLUSTERED INDEX IX_KHACHHANG_MaKH_Search
ON [dbo].[KHACHHANG] ([MaKH]);
GO

-- 3. Tạo Index cho bảng DICHVU
CREATE NONCLUSTERED INDEX IX_DICHVU_TenDV
ON [dbo].[DICHVU] ([TenDV])
INCLUDE ([MaDichVu]);
GO


