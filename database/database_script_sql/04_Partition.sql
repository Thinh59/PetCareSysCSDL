USE PetCareDBOpt
GO

USE PetCareDB
GO

--KỊCH BẢN 07:
--QLCN10:
SELECT * FROM LS_DV WHERE NgayDatTruoc >= '2025-01-01' AND NgayDatTruoc < '2026-01-01' AND MaCN = 'CN06'
GO

-- 1. Tạo Filegroups và Files
ALTER DATABASE PetCareDBOpt ADD FILEGROUP FG_LSDV_History;
ALTER DATABASE PetCareDBOpt ADD FILEGROUP FG_LSDV_2024;
ALTER DATABASE PetCareDBOpt ADD FILEGROUP FG_LSDV_2025;
GO
ALTER DATABASE PetCareDBOpt ADD FILE (NAME = N'F_History', FILENAME = N'D:\NA\Kì 5\Cơ Sở Dữ Liệu Nâng Cao\FinalProject\QL_ThuCung\PetCareSystem\database\Partition\QLCN10_Parition\LSDV_History.ndf') TO FILEGROUP FG_LSDV_History;
ALTER DATABASE PetCareDBOpt ADD FILE (NAME = N'F_2024', FILENAME = N'D:\NA\Kì 5\Cơ Sở Dữ Liệu Nâng Cao\FinalProject\QL_ThuCung\PetCareSystem\database\Partition\QLCN10_Parition\LSDV_2024.ndf') TO FILEGROUP FG_LSDV_2024;
ALTER DATABASE PetCareDBOpt ADD FILE (NAME = N'F_2025', FILENAME = N'D:\NA\Kì 5\Cơ Sở Dữ Liệu Nâng Cao\FinalProject\QL_ThuCung\PetCareSystem\database\Partition\QLCN10_Parition\LSDV_2025.ndf') TO FILEGROUP FG_LSDV_2025;
GO

IF EXISTS (SELECT * FROM sys.partition_schemes WHERE name = 'scheme_PhanManhTheoNam')
	DROP PARTITION SCHEME scheme_QLCN10_LSDV_PhanManhTheoNam;
GO

IF EXISTS (SELECT * FROM sys.partition_functions WHERE name = 'func_PhanManhTheoNam')
	DROP PARTITION FUNCTION func_QLCN10_LSDV_PhanManhTheoNam;
GO

-- 2. Tạo Partition Function
CREATE PARTITION FUNCTION func_QLCN10_LSDV_PhanManhTheoNam (DATE)
AS RANGE RIGHT FOR VALUES ('2024-01-01', '2025-01-01');
GO

-- 3. Tạo Partition Scheme
CREATE PARTITION SCHEME scheme_QLCN10_LSDV_PhanManhTheoNam
AS PARTITION func_QLCN10_LSDV_PhanManhTheoNam
TO (FG_LSDV_History, FG_LSDV_2024, FG_LSDV_2025);
GO

-- 1. Xóa các khóa ngoại
ALTER TABLE [dbo].[CT_HOADON] DROP CONSTRAINT [FK__CT_HOADON__MaLSG__08211BE3];
ALTER TABLE [dbo].[LS_DVTIEMPHONG] DROP CONSTRAINT [FK_LSDVTP_LSDV];
ALTER TABLE [dbo].[LS_DVKHAMBENH] DROP CONSTRAINT [FK_LSDVKB_LSDV];
ALTER TABLE [dbo].[LS_DVMUAHANG] DROP CONSTRAINT [FK_LSDVMH_LSDV];
GO

-- 2. Xóa khóa chính hiện tại của bảng LS_DV
DECLARE @pkName NVARCHAR(MAX);
SELECT TOP 1 @pkName = name FROM sys.key_constraints 
WHERE type = 'PK' AND parent_object_id = OBJECT_ID('LS_DV');

IF @pkName IS NOT NULL
BEGIN
    EXEC('ALTER TABLE LS_DV DROP CONSTRAINT ' + @pkName);
END
GO

-- 1. Tạo lại Khóa chính
ALTER TABLE LS_DV
ADD CONSTRAINT PK_LS_DV_New PRIMARY KEY NONCLUSTERED (MaLSDV) -- Giữ nguyên cột khóa chính cũ của bạn
ON [PRIMARY]; 
GO

-- 2. Tạo Clustered Index để "Đẩy" dữ liệu về Partition
CREATE CLUSTERED INDEX QLCN10_CI_PhanManh_LSDV
ON LS_DV (NgayDatTruoc)
ON scheme_QLCN10_LSDV_PhanManhTheoNam(NgayDatTruoc);
GO

-- Chạy lại đoạn code tạo khóa ngoại
ALTER TABLE [dbo].[CT_HOADON] WITH CHECK ADD CONSTRAINT [FK__CT_HOADON__MaLSG__08211BE3] 
FOREIGN KEY ([MaLSGD]) REFERENCES [dbo].[LS_DV]([MaLSDV]);

ALTER TABLE [dbo].[LS_DVTIEMPHONG] WITH CHECK ADD CONSTRAINT [FK_LSDVTP_LSDV] 
FOREIGN KEY ([MaLSDVTP]) REFERENCES [dbo].[LS_DV]([MaLSDV]);

ALTER TABLE [dbo].[LS_DVKHAMBENH] WITH CHECK ADD CONSTRAINT [FK_LSDVKB_LSDV] 
FOREIGN KEY ([MaLSDVKB]) REFERENCES [dbo].[LS_DV]([MaLSDV]);

ALTER TABLE [dbo].[LS_DVMUAHANG] WITH CHECK ADD CONSTRAINT [FK_LSDVMH_LSDV] 
FOREIGN KEY ([MaLSDVMH]) REFERENCES [dbo].[LS_DV]([MaLSDV]);
GO

--CHẠY LẠI SAU KHI CÀI PARTITION
SELECT * FROM LS_DV WHERE NgayDatTruoc >= '2025-01-01' AND NgayDatTruoc < '2026-01-01' AND MaCN = 'CN06'
GO

--KỊCH BẢN 06:
USE PetCareDBOpt1
GO

USE PetCareDB
GO

SELECT 
        HD.MaHD,
        HD.MaKH,
        KH.HoTen_KH AS TenKH,
        HD.NgayLap,
        HD.TienThanhToan AS TongTien,
        HD.TrangThaiHD
    FROM HOADON HD
    LEFT JOIN KHACHHANG KH ON HD.MaKH = KH.MaKH
    WHERE HD.MaCN = 'CN07'
      AND YEAR(HD.NgayLap) = 2024
    ORDER BY HD.NgayLap DESC

IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE name = 'FG_HOADON_History') ALTER DATABASE PetCareDBOpt ADD FILEGROUP FG_HOADON_History;
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE name = 'FG_HOADON_2024') ALTER DATABASE PetCareDBOpt ADD FILEGROUP FG_HOADON_2024;
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE name = 'FG_HOADON_2025') ALTER DATABASE PetCareDBOpt ADD FILEGROUP FG_HOADON_2025;
GO

IF NOT EXISTS (SELECT name FROM sys.database_files WHERE name = 'F_HD_History')
    ALTER DATABASE PetCareDBOpt ADD FILE (NAME = N'F_HD_History', FILENAME = N'D:\NA\Kì 5\Cơ Sở Dữ Liệu Nâng Cao\FinalProject\QL_ThuCung\PetCareSystem\database\Partition\HoaDon_Partition\HOADON_History.ndf') TO FILEGROUP FG_HOADON_History;

IF NOT EXISTS (SELECT name FROM sys.database_files WHERE name = 'F_HD_2024')
    ALTER DATABASE PetCareDBOpt ADD FILE (NAME = N'F_HD_2024', FILENAME = N'D:\NA\Kì 5\Cơ Sở Dữ Liệu Nâng Cao\FinalProject\QL_ThuCung\PetCareSystem\database\Partition\HoaDon_Partition\HOADON_2024.ndf') TO FILEGROUP FG_HOADON_2024;

IF NOT EXISTS (SELECT name FROM sys.database_files WHERE name = 'F_HD_2025')
    ALTER DATABASE PetCareDBOpt ADD FILE (NAME = N'F_HD_2025', FILENAME = N'D:\NA\Kì 5\Cơ Sở Dữ Liệu Nâng Cao\FinalProject\QL_ThuCung\PetCareSystem\database\Partition\HoaDon_Partition\HOADON_2025.ndf') TO FILEGROUP FG_HOADON_2025;
GO

IF EXISTS (SELECT * FROM sys.partition_schemes WHERE name = 'scheme_HOADON_PhanManhTheoNam') DROP PARTITION SCHEME scheme_HOADON_PhanManhTheoNam;
IF EXISTS (SELECT * FROM sys.partition_functions WHERE name = 'func_HOADON_PhanManhTheoNam') DROP PARTITION FUNCTION func_HOADON_PhanManhTheoNam;
GO

CREATE PARTITION FUNCTION func_HOADON_PhanManhTheoNam (DATE)
AS RANGE RIGHT FOR VALUES ('2024-01-01', '2025-01-01');
GO

CREATE PARTITION SCHEME scheme_HOADON_PhanManhTheoNam
AS PARTITION func_HOADON_PhanManhTheoNam
TO (FG_HOADON_History, FG_HOADON_2024, FG_HOADON_2025);
GO

-- Xóa chính xác các FK trỏ về bảng HOADON từ CT_HOADON
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
               N' DROP CONSTRAINT ' + QUOTENAME(name) + N'; '
FROM sys.foreign_keys
WHERE referenced_object_id = OBJECT_ID('HOADON');

EXEC sp_executesql @sql;
GO

-- Xóa Primary Key cũ của HOADON
DECLARE @pkName NVARCHAR(MAX);
SELECT TOP 1 @pkName = name 
FROM sys.key_constraints 
WHERE type = 'PK' AND parent_object_id = OBJECT_ID('HOADON');

IF @pkName IS NOT NULL
BEGIN
    DECLARE @sqlPK NVARCHAR(MAX);
    SET @sqlPK = 'ALTER TABLE HOADON DROP CONSTRAINT ' + @pkName;
    EXEC sp_executesql @sqlPK;
END
GO

ALTER TABLE HOADON
ADD CONSTRAINT PK_HOADON_New PRIMARY KEY NONCLUSTERED (MaHD)
ON [PRIMARY];
GO

CREATE CLUSTERED INDEX CI_HOADON_Composite_PhanManh
ON HOADON (MaCN, NgayLap DESC)
ON scheme_HOADON_PhanManhTheoNam(NgayLap);
GO

ALTER TABLE [dbo].[CT_HOADON] WITH CHECK ADD CONSTRAINT [FK_CTHOADON_HOADON] 
FOREIGN KEY ([MaHD]) REFERENCES [dbo].[HOADON]([MaHD]);

IF OBJECT_ID('CT_KHUYENMAI') IS NOT NULL
BEGIN
    ALTER TABLE [dbo].[CT_KHUYENMAI] WITH CHECK ADD CONSTRAINT [FK_CTKHUYENMAI_HOADON] 
    FOREIGN KEY ([MaHD]) REFERENCES [dbo].[HOADON]([MaHD]);
END
GO