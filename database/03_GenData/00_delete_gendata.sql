USE PetCareDB
GO

-- 1. Tắt tất cả các ràng buộc khóa ngoại (Foreign Keys)
-- Giúp xóa bảng nào trước cũng được, không bị lỗi
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
GO

-- 2. Xóa dữ liệu (DELETE)
PRINT 'Đang xóa dữ liệu...'

-- Nhóm Giao dịch & Chi tiết (Xóa trước cho gọn)
DELETE FROM CT_KHUYENMAI
DELETE FROM CT_HOADON
DELETE FROM HOADON
DELETE FROM DANHGIA
DELETE FROM CT_MUAHANG
DELETE FROM TOA_THUOC
DELETE FROM TRIEUCHUNG
DELETE FROM CHUANDOAN
DELETE FROM LS_DVMUAHANG
DELETE FROM LS_DVTIEMPHONG
DELETE FROM LS_DVKHAMBENH
DELETE FROM LS_DV
DELETE FROM LS_DANGKY
DELETE FROM ND_GOITIEM

-- Nhóm Nghiệp vụ & Kho
DELETE FROM LS_DIEUDONG
DELETE FROM CHITIEUNAM
DELETE FROM CT_SPCN
DELETE FROM CHINHANH_DV

-- Nhóm Con người & Đối tượng
DELETE FROM THUCUNG
DELETE FROM HOIVIEN
DELETE FROM KHACHHANG
DELETE FROM NHANVIEN

-- Nhóm Danh mục (Master Data)
DELETE FROM THUOC
DELETE FROM SANPHAM
DELETE FROM VACXIN
DELETE FROM GOITIEM
DELETE FROM KHUYENMAI
DELETE FROM CHINHANH
DELETE FROM TAIKHOAN
DELETE FROM DICHVU -- (Nếu muốn giữ lại 3 dịch vụ cơ bản thì comment dòng này)

-- 3. Reset bộ đếm ID tự động (IDENTITY) về 0
-- Bắt buộc phải có để TAIKHOAN bắt đầu lại từ ID 1 khớp với code Python
PRINT 'Đang reset Identity...'
IF OBJECT_ID('TAIKHOAN', 'U') IS NOT NULL
    DBCC CHECKIDENT ('TAIKHOAN', RESEED, 0);

-- Các bảng khác nếu có Identity cũng reset tương tự (Ví dụ LS_DIEUDONG STT nếu có Identity)
-- Nếu bạn dùng code sinh mã thủ công (như HD0001, KH0001) thì không cần reset các bảng đó.

-- 4. Bật lại tất cả ràng buộc khóa ngoại
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'
GO

PRINT '--- HOÀN TẤT: DATABASE ĐÃ SẠCH SẼ ---'