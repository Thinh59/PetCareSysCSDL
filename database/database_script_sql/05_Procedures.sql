USE PetCareDB
GO

-------CHỨC NĂNG DÙNG CHUNG--------
---ALL5: Xem danh sách dịch vụ
CREATE PROC sp_ALL5_XemDanhSachDichVu
AS 
BEGIN
	SET NOCOUNT ON

	SELECT MaDichVu, TenDV, GiaTienDV
	FROM DICHVU
	ORDER BY MaDichVu
END
GO

-------PHÂN HỆ TIẾP TÂN--------
--TT1: 
CREATE OR ALTER PROCEDURE sp_TT1_TaoTKKH
    @HoTenKH NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @SDT VARCHAR(11),
    @Email NVARCHAR(50),
    @Username NVARCHAR(20),
    @PasswordHash NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    BEGIN TRANSACTION
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @Username)
        BEGIN
            THROW 50001, N'Tên đăng nhập đã tồn tại.', 1;
        END
        
		DECLARE @MaxMaKH INT, @NewMaKH NVARCHAR(10);
		DECLARE @NewID_TK INT;

		SELECT @MaxMaKH = ISNULL(MAX(TRY_CAST(SUBSTRING(MaKH, 3, 8) AS INT)), 0) + 1
		FROM KHACHHANG
		WHERE MaKH LIKE 'KH[0-9]%'; 

		SET @NewMaKH = 'KH' + RIGHT('00000' + CAST(@MaxMaKH AS NVARCHAR(10)), 5);

        INSERT INTO TAIKHOAN(TenDangNhap, MatKhau, LoaiTK)
        VALUES(@Username, @PasswordHash, N'Khách hàng');
        
        SET @NewID_TK = SCOPE_IDENTITY();
        
        INSERT INTO KHACHHANG(MaKH, HoTen_KH, SDT_KH, NgaySinh_KH, GioiTinh_KH, Email_KH, Loai_KH, ID_TK)
        VALUES(@NewMaKH, @HoTenKH, @SDT, @NgaySinh, @GioiTinh, @Email, N'Thường', @NewID_TK);
        
        COMMIT TRANSACTION
        SELECT @NewMaKH AS MaKH, @NewID_TK AS ID_TK;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

USE PetCareDB
GO

DECLARE @MaxID INT;
SELECT @MaxID = MAX(ID_TK) FROM TAIKHOAN;
PRINT N'ID lớn nhất hiện tại trong bảng là: ' + CAST(@MaxID AS NVARCHAR(20));

DBCC CHECKIDENT ('TAIKHOAN', RESEED, @MaxID);
GO

---TT2: ĐĂNG KÝ HỘI VIÊN CHO KHÁCH HÀNG
CREATE OR ALTER PROCEDURE sp_TT2_DangKyHoiVien
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    DECLARE @CurrentLoaiKH NVARCHAR(50);
    DECLARE @ID_TK INT;
    
    SELECT @CurrentLoaiKH = Loai_KH, @ID_TK = ID_TK 
    FROM KHACHHANG 
    WHERE MaKH = @MaKH;

    IF @CurrentLoaiKH IS NULL
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại.', 16, 1);
        RETURN;
    END

    IF @CurrentLoaiKH = N'Hội viên'
    BEGIN
        RAISERROR(N'Khách hàng này đã là Hội viên.', 16, 1);
        RETURN;
    END
    
    BEGIN TRANSACTION
    
    BEGIN TRY
        UPDATE TAIKHOAN
        SET LoaiTK = N'Hội viên'
        WHERE ID_TK = @ID_TK;

        UPDATE KHACHHANG
        SET Loai_KH = N'Hội viên'
        WHERE MaKH = @MaKH;

        INSERT INTO HOIVIEN (MaKH, CapDo, DiemLoyalty)
        VALUES (@MaKH, N'Cơ bản', 0);

        COMMIT TRANSACTION

        SELECT @MaKH AS MaKH_DangKy;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

----TT2.1: Lấy KHACHHANG chưa là HOIVIEN
CREATE OR ALTER PROCEDURE dbo.sp_TT2_GetAvailableCustomers
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        KH.MaKH,
        KH.HoTen_KH,
        KH.SDT_KH 
    FROM 
        dbo.KHACHHANG KH 
    LEFT JOIN 
        dbo.HOIVIEN HV ON KH.MaKH = HV.MaKH 
    WHERE 
        KH.Loai_KH = N'Thường'
        AND HV.MaKH IS NULL
    ORDER BY 
        KH.MaKH;
END
GO

EXEC sp_TT2_GetAvailableCustomers
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'KHACHHANG')
BEGIN
    PRINT N'CẢNH BÁO: Bảng KHACHHANG chưa tồn tại!'
    RETURN
END
GO

--TT3: Xác nhận lịch sử dịch vụ khám bệnh
-- TT3.1: Lấy danh sách bác sĩ đang rảnh

CREATE PROCEDURE dbo.sp_TT3_GetAvailableDoctors
    @MaCN NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NHANVIEN]') AND type in (N'U'))
    BEGIN
        SELECT TOP 0 CAST('' AS NVARCHAR(10)) AS MaNV, CAST('' AS NVARCHAR(50)) AS HoTenNV
        RETURN;
    END

    SELECT 
        NV.MaNV,
        NV.HoTenNV,
        NV.ChiNhanhLamViec
    FROM 
        dbo.NHANVIEN NV
    WHERE 
        NV.ChucVu = N'Bác sĩ'
        AND NV.TrangThaiNV = N'Rảnh'
        AND (@MaCN IS NULL OR NV.ChiNhanhLamViec = @MaCN)
    ORDER BY 
        NV.HoTenNV;
END
GO

CREATE OR ALTER PROCEDURE sp_TT3_GetPendingMaLSDV
AS
BEGIN
    SELECT DISTINCT LS.MaLSDV 
    FROM LS_DV LS
    INNER JOIN DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE LS.TrangThaiGD = N'Đã đặt trước' 
      AND DV.TenDV LIKE N'%Khám%'
    ORDER BY LS.MaLSDV;
END;
GO

CREATE OR ALTER PROCEDURE sp_TT3_GetPendingMaKH
AS
BEGIN
    SELECT DISTINCT KH.MaKH 
    FROM KHACHHANG KH
    INNER JOIN LS_DV LS ON KH.MaKH = LS.MaKH
	INNER JOIN DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE LS.NgayDatTruoc IS NOT NULL 
    AND LS.TrangThaiGD = N'Đã đặt trước'
	AND DV.TenDV LIKE N'%Khám%'
    ORDER BY KH.MaKH;
END;
GO

CREATE OR ALTER PROCEDURE sp_TT3_GetPendingSDT
AS
BEGIN
    SELECT DISTINCT KH.SDT_KH 
    FROM KHACHHANG KH
    INNER JOIN LS_DV LS ON KH.MaKH = LS.MaKH
    INNER JOIN DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE LS.TrangThaiGD = N'Đã đặt trước'
      AND DV.TenDV LIKE N'%Khám%' 
    ORDER BY KH.SDT_KH;
END;
GO

USE PetCareDB
GO

-- TT3.2: Tìm kiếm lịch sử dịch vụ
DROP PROCEDURE dbo.sp_TT3_SearchLSDV
CREATE PROCEDURE dbo.sp_TT3_SearchLSDV
    @SearchValue NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        LS.MaLSDV,
        LS.MaKH,
        KH.HoTen_KH,
        KH.SDT_KH,
        DV.TenDV,
        LS.NgayDatTruoc,
        LS.TrangThaiGD,
        KB.MaThuCung,
        TC.TenThuCung
    FROM dbo.LS_DV LS
    INNER JOIN dbo.KHACHHANG KH ON LS.MaKH = KH.MaKH
    INNER JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    LEFT JOIN dbo.LS_DVKHAMBENH KB ON LS.MaLSDV = KB.MaLSDVKB
    LEFT JOIN dbo.THUCUNG TC ON KB.MaThuCung = TC.MaThuCung
    WHERE 
        (LS.MaLSDV LIKE '%' + @SearchValue + '%' 
         OR LS.MaKH LIKE '%' + @SearchValue + '%'
         OR KH.SDT_KH LIKE '%' + @SearchValue + '%')
        AND LS.TrangThaiGD = N'Đã đặt trước' 
        AND DV.TenDV LIKE N'%Khám%'
    ORDER BY 
        LS.NgayDatTruoc ASC;
END
GO
-- TT3.3: Lấy thông tin chi tiết LSDV

CREATE PROCEDURE sp_TT3_GetLSDVDetail
    @MaLSDV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        LS.MaLSDV,
        LS.MaKH,
        KH.HoTen_KH,
        KH.SDT_KH,
        LS.MaDichVu,
        DV.TenDV,
        DV.GiaTienDV,
        LS.NgayDatTruoc,
        LS.TrangThaiGD
    FROM 
        dbo.LS_DV LS
    INNER JOIN 
        dbo.KHACHHANG KH ON LS.MaKH = KH.MaKH
    INNER JOIN 
        dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE 
        LS.MaLSDV = @MaLSDV;
END
GO

-- TT3.4: Xác nhận khám bệnh

CREATE PROCEDURE dbo.sp_TT3_XacNhanKhamBenh
    @MaLSDV NVARCHAR(10),
    @MaBacSi NVARCHAR(10),
    @ThoiGianGD DATETIME,
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    DECLARE @TrangThaiHienTai NVARCHAR(50);
    
    BEGIN TRANSACTION
    BEGIN TRY
        SELECT @TrangThaiHienTai = TrangThaiGD FROM dbo.LS_DV WHERE MaLSDV = @MaLSDV;
        
        IF @TrangThaiHienTai IS NULL THROW 50001, N'Mã lịch sử dịch vụ không tồn tại.', 1;
        IF @TrangThaiHienTai = N'Đang sử dụng' THROW 50002, N'Dịch vụ này đã được xác nhận trước đó.', 1;

        UPDATE dbo.LS_DV
        SET TrangThaiGD = N'Đang sử dụng'
        WHERE MaLSDV = @MaLSDV;
        
        UPDATE dbo.NHANVIEN SET TrangThaiNV = N'Bận' WHERE MaNV = @MaBacSi;

        IF EXISTS (SELECT 1 FROM dbo.LS_DVKHAMBENH WHERE MaLSDVKB = @MaLSDV)
        BEGIN
            UPDATE dbo.LS_DVKHAMBENH
            SET BacSiPhuTrach = @MaBacSi,
                MaThuCung = @MaThuCung,
                NgayKham = CAST(@ThoiGianGD AS DATE),
                ThoiGianSD = CAST(@ThoiGianGD AS DATE)
            WHERE MaLSDVKB = @MaLSDV;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.LS_DVKHAMBENH (MaLSDVKB, BacSiPhuTrach, NgayHen, MaThuCung, NgayKham, ThoiGianSD)
            VALUES (@MaLSDV, @MaBacSi, NULL, @MaThuCung, CAST(@ThoiGianGD AS DATE), CAST(@ThoiGianGD AS DATE));
        END
        
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- TT3.5: Lấy danh sách thú cưng của khách hàng

CREATE PROCEDURE dbo.sp_TT3_GetPetsByCustomer
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        MaThuCung,
        TenThuCung,
        LoaiThuCung
    FROM THUCUNG
    WHERE MaKH = @MaKH
    ORDER BY TenThuCung;
END
GO

USE PetCareDBOpt
GO

--TT4:
CREATE OR ALTER PROCEDURE sp_TT4_GetPendingMaLSDV
AS
BEGIN
    SELECT DISTINCT LS.MaLSDV 
    FROM LS_DV LS
    INNER JOIN LS_DVTIEMPHONG TP ON LS.MaLSDV = TP.MaLSDVTP 
    WHERE LS.TrangThaiGD = N'Đã đặt trước' 
    ORDER BY LS.MaLSDV;
END;
GO

CREATE OR ALTER PROCEDURE sp_TT4_GetPendingSDT
AS
BEGIN
    SELECT DISTINCT KH.SDT_KH 
    FROM KHACHHANG KH
    INNER JOIN LS_DV LS ON KH.MaKH = LS.MaKH
    INNER JOIN LS_DVTIEMPHONG TP ON LS.MaLSDV = TP.MaLSDVTP
    WHERE LS.TrangThaiGD = N'Đã đặt trước'
    ORDER BY KH.SDT_KH;
END;
GO

-- TT4.1: Tìm kiếm

CREATE PROCEDURE dbo.sp_TT4_SearchLSDV_TiemPhong
    @SearchValue NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        LS.MaLSDV,
        LS.MaKH,
        TP.MaThuCung, 
        TC.TenThuCung,
        ISNULL(NV.HoTenNV, N'Chưa chỉ định') AS BacSi,
        TP.LoaiVacXin,
        TP.MaGoiTiem,
        LS.NgayDatTruoc AS NgayDat,
        TP.MaLSDVTP,
        DV.TenDV
    FROM LS_DV LS
    INNER JOIN dbo.LS_DVTIEMPHONG TP ON LS.MaLSDV = TP.MaLSDVTP 
    INNER JOIN dbo.KHACHHANG KH ON LS.MaKH = KH.MaKH
    LEFT JOIN dbo.THUCUNG TC ON TP.MaThuCung = TC.MaThuCung
    LEFT JOIN dbo.NHANVIEN NV ON TP.BacSiPhuTrach = NV.MaNV
    INNER JOIN dbo.DICHVU DV ON DV.MaDichVu = LS.MaDichVu
    WHERE 
        LS.TrangThaiGD = N'Đã đặt trước'
        AND DV.TenDV LIKE N'%Tiêm%'
        AND (
            @SearchValue IS NULL OR @SearchValue = '' 
            OR LS.MaLSDV LIKE '%' + @SearchValue + '%'     
            OR KH.SDT_KH LIKE '%' + @SearchValue + '%'      
            OR TP.MaThuCung LIKE '%' + @SearchValue + '%'  
        )
    ORDER BY LS.NgayDatTruoc ASC;
END
GO

SELECT TABLE_SCHEMA, TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%LS%' OR TABLE_NAME LIKE '%DV%'
ORDER BY TABLE_NAME;

USE PetCareDB
GO

-- TT4.2: Kiểm tra lịch sử - GIỮ NGUYÊN
CREATE OR ALTER PROCEDURE dbo.sp_TT4_CheckLichSuTiem
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 5
        VX.TenVacXin,
        TP.NgayTiem,
        TP.LieuLuong
    FROM dbo.LS_DVTIEMPHONG TP
    INNER JOIN dbo.VACXIN VX ON TP.LoaiVacXin = VX.MaVacXin
    WHERE TP.MaThuCung = @MaThuCung 
      AND TP.NgayTiem IS NOT NULL 
    ORDER BY TP.NgayTiem DESC;
END

-- TT4.3: Xác nhận tiêm phòng

CREATE OR ALTER PROCEDURE dbo.sp_TT4_XacNhanTiemPhong_Update
    @MaLSDV NVARCHAR(10),
    @MaLSDVTP NVARCHAR(10),
    @MaBacSi NVARCHAR(10),
    @LoaiVacXin NVARCHAR(8),
    @LieuLuong NVARCHAR(50),
    @ThoiGianGD DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON; 
    
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.LS_DVTIEMPHONG WHERE MaLSDVTP = @MaLSDVTP)
        BEGIN
            THROW 50001, N'Lỗi: Không tìm thấy mã chi tiết tiêm phòng này.', 1;
        END

        DECLARE @TrangThaiHienTai NVARCHAR(50);
        SELECT @TrangThaiHienTai = TrangThaiGD FROM LS_DV WHERE MaLSDV = @MaLSDV;

        IF @TrangThaiHienTai <> N'Đã đặt trước'
        BEGIN
             THROW 50002, N'Lỗi: Đơn này đã được xử lý hoặc hủy bỏ.', 1;
        END

        DECLARE @SlTon INT;
        SELECT @SlTon = SoMuiTonKho FROM VACXIN WHERE MaVacXin = @LoaiVacXin;
        
        IF @SlTon <= 0 
        BEGIN
             THROW 50003, N'Lỗi: Vắc xin này đã hết hàng trong kho.', 1;
        END

        UPDATE dbo.VACXIN 
        SET SoMuiTonKho = SoMuiTonKho - 1 
        WHERE MaVacXin = @LoaiVacXin;

        UPDATE dbo.LS_DVTIEMPHONG
        SET BacSiPhuTrach = @MaBacSi,
            LoaiVacXin = @LoaiVacXin,
            LieuLuong = @LieuLuong,
            NgayTiem = CAST(@ThoiGianGD AS DATE), 
            ThoiGianSD = CAST(@ThoiGianGD AS DATE) 
        WHERE MaLSDVTP = @MaLSDVTP;

        UPDATE dbo.LS_DV
        SET TrangThaiGD = N'Đang sử dụng'
        WHERE MaLSDV = @MaLSDV;

        UPDATE dbo.NHANVIEN 
        SET TrangThaiNV = N'Bận' 
        WHERE MaNV = @MaBacSi;
        
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        THROW 50000, @ErrorMessage, 1;
    END CATCH
END
GO

---TT5:
CREATE OR ALTER PROCEDURE sp_TT5_GetAllCustomersBasic
AS
BEGIN
    SELECT MaKH, HoTen_KH, SDT_KH 
    FROM KHACHHANG 
    ORDER BY MaKH;
END;
GO

-- TT5.1: Lấy danh sách các Gói Tiêm có sẵn
CREATE OR ALTER PROCEDURE dbo.sp_TT5_GetPackages
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        MaGoiTiem,
        TenGoi,
        SoThang,
        UuDai
    FROM dbo.GOITIEM
    ORDER BY TenGoi;
END
GO

-- TT5.2: Đăng ký gói tiêm cho khách hàng 
CREATE OR ALTER PROCEDURE dbo.sp_TT5_RegisterPackage
    @MaGoiTiem NVARCHAR(8),
    @MaKH NVARCHAR(10),
    @NgayDangKy DATE
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.KHACHHANG WHERE MaKH = @MaKH)
            THROW 50001, N'Mã khách hàng không tồn tại.', 1;
            
        IF NOT EXISTS (SELECT 1 FROM dbo.GOITIEM WHERE MaGoiTiem = @MaGoiTiem)
            THROW 50002, N'Mã gói tiêm không tồn tại.', 1;

        IF EXISTS (SELECT 1 FROM dbo.LS_DANGKY 
                   WHERE MaGoiTiem = @MaGoiTiem AND MaKH = @MaKH AND NgayDangKy = @NgayDangKy)
        BEGIN
             THROW 50003, N'Khách hàng đã đăng ký gói này vào ngày đã chọn. Vui lòng kiểm tra lại.', 1;
        END

        INSERT INTO dbo.LS_DANGKY (MaGoiTiem, MaKH, NgayDangKy)
        VALUES (@MaGoiTiem, @MaKH, @NgayDangKy);

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

--TT6: Lập hóa đơn
-- 1. Hàm tính tổng tiền vật tư (Thuốc, Vacxin, Hàng hóa) đi kèm dịch vụ
CREATE OR ALTER FUNCTION dbo.fn_TinhTienVatTu (@MaLSDV NVARCHAR(10))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @TongTien DECIMAL(18, 2) = 0;

    SELECT @TongTien = @TongTien + ISNULL(SUM(MH.ThanhTienMH), 0)
    FROM dbo.CT_MUAHANG MH
    INNER JOIN dbo.LS_DVMUAHANG LS ON MH.MaLSDVMH = LS.MaLSDVMH
    WHERE LS.MaLSDVMH = @MaLSDV;

    SELECT @TongTien = @TongTien + ISNULL(SUM(VX.GiaTien), 0)
    FROM dbo.LS_DVTIEMPHONG TP
    INNER JOIN dbo.VACXIN VX ON TP.LoaiVacXin = VX.MaVacXin
    WHERE TP.MaLSDVTP = @MaLSDV;

    SELECT @TongTien = @TongTien + ISNULL(SUM(TT.ThanhTien), 0)
    FROM dbo.TOA_THUOC TT
    WHERE TT.MaLSDV = @MaLSDV;

    RETURN @TongTien;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_TT6_TaoHoaDon
    @MaKH NVARCHAR(10),
    @MaNV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH)
    BEGIN
        RAISERROR(N'Khách hàng không tồn tại', 16, 1);
        RETURN;
    END

    DECLARE @MaHD NVARCHAR(10);
    SELECT TOP 1 @MaHD = MaHD FROM HOADON WHERE MaKH = @MaKH AND TrangThaiHD = N'Chờ thanh toán';

    IF @MaHD IS NULL
    BEGIN
        SET @MaHD = 'HD' + RIGHT(REPLACE(CONVERT(VARCHAR, GETDATE(), 120), ':', ''), 6) + LEFT(NEWID(), 3);
        
        INSERT INTO HOADON (MaHD, NgayLap, NV_Lap, TienTruocKM, TienThanhToan, HinhThucPay, TrangThaiHD, CongLoyalty, MaKH, MaCN)
        VALUES (@MaHD, CAST(GETDATE() AS DATE), @MaNV, 0, 0, N'Tiền mặt', N'Chờ thanh toán', 0, @MaKH, 'CN01');
    END

    INSERT INTO CT_HOADON (MaHD, MaLSGD, TongPhiDV, Pet)
    SELECT 
        @MaHD,
        LS.MaLSDV,
        DV.GiaTienDV + dbo.fn_TinhTienVatTu(LS.MaLSDV), 
        NULL 
    FROM dbo.LS_DV LS
    INNER JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE LS.MaKH = @MaKH 
      AND LS.TrangThaiGD = N'Đã sử dụng'
      AND LS.MaLSDV NOT IN (SELECT MaLSGD FROM CT_HOADON);

    DECLARE @TongTien DECIMAL(18,2);
    SELECT @TongTien = SUM(TongPhiDV) FROM CT_HOADON WHERE MaHD = @MaHD;

    UPDATE HOADON 
    SET TienTruocKM = ISNULL(@TongTien, 0),
        TienThanhToan = ISNULL(@TongTien, 0)
    WHERE MaHD = @MaHD;

    SELECT HD.*, KH.SDT_KH, KH.HoTen_KH
    FROM HOADON HD
    INNER JOIN KHACHHANG KH ON HD.MaKH = KH.MaKH
    WHERE HD.MaHD = @MaHD;

    SELECT 
        CT.MaLSGD AS MaLSDV,
        DV.TenDV,
        DV.GiaTienDV AS PhiDV,
        dbo.fn_TinhTienVatTu(CT.MaLSGD) AS PhiSanPham,
        CT.TongPhiDV AS ThanhTien
    FROM dbo.CT_HOADON CT
    INNER JOIN dbo.LS_DV LS ON CT.MaLSGD = LS.MaLSDV
    INNER JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE CT.MaHD = @MaHD;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_TT6_GetChiTietVatTu
    @MaLSDV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT T.MaThuoc AS TenSP, N'Thuốc' AS LoaiSP, TT.SoLuongThuoc AS SoLuong, (TT.ThanhTien/NULLIF(TT.SoLuongThuoc,0)) AS Gia, TT.ThanhTien
    FROM TOA_THUOC TT JOIN THUOC T ON TT.MaThuoc = T.MaThuoc WHERE TT.MaLSDV = @MaLSDV
    
    UNION ALL

    SELECT VX.TenVacXin, N'Vacxin', 1, VX.GiaTien, VX.GiaTien
    FROM LS_DVTIEMPHONG TP JOIN VACXIN VX ON TP.LoaiVacXin = VX.MaVacXin WHERE TP.MaLSDVTP = @MaLSDV
    
    UNION ALL

    SELECT SP.TenSP, N'Sản phẩm', CT.SoLuongSP, SP.GiaBan, CT.ThanhTienMH
    FROM CT_MUAHANG CT JOIN SANPHAM SP ON CT.MaSP = SP.MaSP 
    JOIN LS_DVMUAHANG MH ON CT.MaLSDVMH = MH.MaLSDVMH WHERE MH.MaLSDVMH = @MaLSDV;
END
GO

USE PetCareDB
GO

USE PetCareDB
GO

USE PetCareDBOpt
GO

CREATE OR ALTER PROCEDURE dbo.sp_TT6_XemTruocDichVu
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LS.MaLSDV,
        DV.TenDV,
        DV.GiaTienDV AS PhiDV,
        dbo.fn_TinhTienVatTu(LS.MaLSDV) AS PhiSanPham, 
        (DV.GiaTienDV + dbo.fn_TinhTienVatTu(LS.MaLSDV)) AS ThanhTien,
        KH.SDT_KH,
        KH.HoTen_KH
    FROM dbo.LS_DV LS
    INNER JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    INNER JOIN dbo.KHACHHANG KH ON LS.MaKH = KH.MaKH
    WHERE LS.MaKH = @MaKH 
      AND LS.TrangThaiGD IN (N'Chờ lập hóa đơn', N'Chờ thanh toán') 
      AND LS.MaLSDV NOT IN (SELECT MaLSGD FROM CT_HOADON);
END
GO


USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_TT6_LuuHoaDonMoi
    @MaKH NVARCHAR(10),
    @MaNV NVARCHAR(10),
    @TongTien DECIMAL(18,2),
    @NgayLap DATE, 
	@MaCN NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaHD NVARCHAR(10);
    SET @MaHD = 'HD' + RIGHT(REPLACE(CONVERT(VARCHAR, @NgayLap, 120), ':', ''), 6) + LEFT(NEWID(), 3);

    INSERT INTO HOADON (MaHD, NgayLap, NV_Lap, TienTruocKM, TienThanhToan, HinhThucPay, TrangThaiHD, CongLoyalty, MaKH, MaCN)
    VALUES (@MaHD, @NgayLap, @MaNV, @TongTien, @TongTien, N'Tiền mặt', N'Chờ thanh toán', 0, @MaKH, @MaCN);

    INSERT INTO CT_HOADON (MaHD, MaLSGD, TongPhiDV, Pet)
    SELECT 
        @MaHD,
        LS.MaLSDV,
        DV.GiaTienDV + dbo.fn_TinhTienVatTu(LS.MaLSDV),
        NULL 
    FROM dbo.LS_DV LS
    INNER JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE LS.MaKH = @MaKH 
      AND LS.TrangThaiGD IN (N'Đã sử dụng', N'Hoàn thành', N'Chờ thanh toán', N'Chờ lập hóa đơn')
      AND LS.MaLSDV NOT IN (SELECT MaLSGD FROM CT_HOADON);

    UPDATE LS_DV 
    SET TrangThaiGD = N'Chờ thanh toán' 
    WHERE MaKH = @MaKH 
      AND TrangThaiGD IN (N'Đang sử dụng', N'Đã sử dụng', N'Hoàn thành', N'Chờ lập hóa đơn')
      AND MaLSDV IN (SELECT MaLSGD FROM CT_HOADON WHERE MaHD = @MaHD);

    SELECT @MaHD AS NewMaHD;
END
GO

CREATE OR ALTER PROCEDURE sp_TT6_GetKhachHangChoThanhToan
AS
BEGIN
    SELECT DISTINCT 
        KH.MaKH,
        KH.HoTen_KH,
        KH.SDT_KH,
        (KH.HoTen_KH + ' - ' + KH.SDT_KH) AS DisplayText
    FROM KHACHHANG KH
    JOIN LS_DV LS ON KH.MaKH = LS.MaKH
    WHERE 
        LS.TrangThaiGD IN (N'Đang sử dụng', N'Đã sử dụng', N'Hoàn thành', N'Chờ thanh toán', N'Chờ lập hóa đơn')
        AND LS.MaLSDV NOT IN (SELECT MaLSGD FROM CT_HOADON)
    ORDER BY KH.HoTen_KH;
END
GO

USE PetCareDB
GO

EXEC sp_TT6_GetKhachHangChoThanhToan;

--TT7:
--Lấy danh sách Mã HĐ do nhân viên cụ thể lập (Để lọc)
CREATE OR ALTER PROCEDURE sp_TT7_GetMyMaHD
    @MaNV NVARCHAR(20)
AS
BEGIN
    SELECT MaHD 
    FROM HOADON 
    WHERE NV_Lap = @MaNV 
    ORDER BY MaHD DESC;
END;
GO

--Lấy danh sách KH mà nhân viên đã phục vụ
CREATE OR ALTER PROCEDURE sp_TT7_GetMyCustomers
    @MaNV NVARCHAR(20)
AS
BEGIN
    SELECT DISTINCT KH.MaKH, KH.HoTen_KH 
    FROM KHACHHANG KH
    INNER JOIN HOADON HD ON KH.MaKH = HD.MaKH
    WHERE HD.NV_Lap = @MaNV 
    ORDER BY KH.MaKH;
END;
GO

USE PetCareDB
GO

-- TT7: Tìm kiếm danh sách hóa đơn đã lập
CREATE OR ALTER PROCEDURE dbo.sp_TT7_SearchHoaDon
    @TuKhoa NVARCHAR(50),
    @MaNV NVARCHAR(10) 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        HD.MaHD,
        HD.MaKH,
        KH.HoTen_KH,
        HD.NgayLap,
        HD.TienThanhToan AS TongTien,
        HD.NV_Lap,
        NV.HoTenNV AS TenNVLap,
        HD.TrangThaiHD
    FROM dbo.HOADON HD
    LEFT JOIN dbo.KHACHHANG KH ON HD.MaKH = KH.MaKH
    LEFT JOIN dbo.NHANVIEN NV ON HD.NV_Lap = NV.MaNV
    WHERE 
        HD.NV_Lap = @MaNV 
        AND (
            (@TuKhoa IS NULL OR @TuKhoa = '') 
            OR HD.MaHD LIKE '%' + @TuKhoa + '%'
            OR HD.MaKH LIKE '%' + @TuKhoa + '%'
            OR KH.SDT_KH LIKE '%' + @TuKhoa + '%'
        )
    ORDER BY HD.NgayLap DESC;
END
GO

--TT8: Xem lịch sử điều động

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_TT8_GetYears
AS
BEGIN
    SELECT DISTINCT YEAR(NgayDieuDong) as Nam 
    FROM LS_DIEUDONG 
    ORDER BY Nam DESC;
END;
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_TT8_GetLSDieuDong
    @MaNV NVARCHAR(10),      
    @MaCN NVARCHAR(10) = NULL, 
    @Nam INT = NULL            
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LS.STT,
        LS.MaNV,
        NV.HoTenNV,
        LS.NgayDieuDong,
        LS.ChiNhanhCu AS MaCNCu,
        CNC.TenCN AS TenCNCu,
        LS.ChiNhanhMoi AS MaCNMoi,
        CNM.TenCN AS TenCNMoi
    FROM dbo.LS_DIEUDONG LS
    INNER JOIN dbo.NHANVIEN NV ON LS.MaNV = NV.MaNV
    INNER JOIN dbo.CHINHANH CNC ON LS.ChiNhanhCu = CNC.MaCN
    INNER JOIN dbo.CHINHANH CNM ON LS.ChiNhanhMoi = CNM.MaCN
    WHERE 
        LS.MaNV = @MaNV 
        AND (@Nam IS NULL OR YEAR(LS.NgayDieuDong) = @Nam)
        AND (@MaCN IS NULL OR LS.ChiNhanhCu = @MaCN OR LS.ChiNhanhMoi = @MaCN)
    ORDER BY LS.NgayDieuDong DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetListChiNhanh
AS
BEGIN
    SELECT MaCN, TenCN FROM dbo.CHINHANH;
END
GO

--TT9: Đặt lịch cho khách
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.SP_TT_DatLichTaiQuay
    @MaKhachHang NVARCHAR(10), @MaChiNhanh NVARCHAR(10), @TenDichVu NVARCHAR(100),       
    @MaThuCung NVARCHAR(10), @ThoiGianHen DATE, @MaBacSi NVARCHAR(10) = NULL, @MaVacXin NVARCHAR(8) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @MaDichVu NVARCHAR(10);
    DECLARE @NewMaLSDV NVARCHAR(10);    
    DECLARE @NgayDatLich DATE = CAST(GETDATE() AS DATE);

    SELECT @MaDichVu = DV.MaDichVu FROM DICHVU DV
    JOIN CHINHANH_DV CDV ON DV.MaDichVu = CDV.MaDichVu
    WHERE DV.TenDV = @TenDichVu AND CDV.MaCN = @MaChiNhanh;

    IF @MaDichVu IS NULL BEGIN RAISERROR(N'Dịch vụ không tồn tại tại chi nhánh này.', 16, 1); RETURN; END

    DECLARE @NextID INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaLSDV, 5, 6) AS INT)), 0) + 1 FROM LS_DV);
    SET @NewMaLSDV = 'LSDV' + RIGHT('000000' + CAST(@NextID AS NVARCHAR), 6);
    
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO LS_DV (MaLSDV, MaKH, MaDichVu, TrangThaiGD, NgayDatTruoc, MaCN)
        VALUES (@NewMaLSDV, @MaKhachHang, @MaDichVu, N'Chờ thực hiện', @NgayDatLich, @MaChiNhanh); 

        IF @TenDichVu = N'Khám bệnh'
            INSERT INTO LS_DVKHAMBENH (MaLSDVKB, BacSiPhuTrach, NgayHen, MaThuCung, NgayKham)
            VALUES (@NewMaLSDV, @MaBacSi, @ThoiGianHen, @MaThuCung, @ThoiGianHen);
        ELSE IF @TenDichVu = N'Tiêm phòng'
            INSERT INTO LS_DVTIEMPHONG (MaLSDVTP, BacSiPhuTrach, MaGoiTiem, LoaiVacXin, LieuLuong, NgayTiem, MaThuCung)
            VALUES (@NewMaLSDV, @MaBacSi, NULL, @MaVacXin, N'Chờ khám', @ThoiGianHen, @MaThuCung);

        COMMIT TRANSACTION;
        SELECT @NewMaLSDV AS MaLSDV;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_TT_GetBacSiRanh
    @MaCN NVARCHAR(10)
AS
BEGIN
    SELECT MaNV, HoTenNV 
    FROM NHANVIEN 
    WHERE ChiNhanhLamViec = @MaCN 
      AND ChucVu = N'Bác sĩ' 
      AND TrangThaiNV = N'Rảnh' 
END
GO

--TT10: Tra cứu thú cưng
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.SP_TT_TimKiemThuCung
    @TuKhoa NVARCHAR(50)
AS
BEGIN
    SELECT 
        TC.MaThuCung,
        TC.TenThuCung,
        TC.LoaiThuCung,
        TC.Giong_TC,
        TC.GioiTinh_TC,
        TC.NgaySinh_TC,
        KH.HoTen_KH AS TenChu,
        KH.SDT_KH AS SDTChu,
        KH.MaKH
    FROM dbo.THUCUNG TC
    JOIN dbo.KHACHHANG KH ON TC.MaKH = KH.MaKH
    WHERE 
        TC.MaThuCung LIKE '%' + @TuKhoa + '%'
        OR KH.SDT_KH LIKE '%' + @TuKhoa + '%'
        OR TC.TenThuCung LIKE N'%' + @TuKhoa + '%'
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_TT_GetLSTiemPhong_ByPet
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SELECT 
        TP.MaLSDVTP AS MaLSDV,
        TP.NgayTiem,
        VX.TenVacXin AS LoaiVacXin,
        TP.LieuLuong,
        ISNULL(NV.HoTenNV, N'Chưa có') AS BacSi
    FROM dbo.LS_DVTIEMPHONG TP
    LEFT JOIN dbo.VACXIN VX ON TP.LoaiVacXin = VX.MaVacXin
    LEFT JOIN dbo.NHANVIEN NV ON TP.BacSiPhuTrach = NV.MaNV
    WHERE TP.MaThuCung = @MaThuCung
    ORDER BY TP.NgayTiem DESC
END
GO


CREATE OR ALTER PROCEDURE dbo.SP_TT_GetLSKhamBenh_ByPet
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SELECT 
        KB.MaLSDVKB AS MaLSDV,
        KB.NgayKham,
        ISNULL(NV.HoTenNV, N'Chưa có') AS BacSi,
        N'' AS TrieuChung, 
        N'' AS ChuanDoan,
        KB.NgayHen,
        (SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END 
         FROM dbo.TOA_THUOC TT 
         WHERE TT.MaLSDV = KB.MaLSDVKB) AS CoToaThuoc
    FROM dbo.LS_DVKHAMBENH KB
    LEFT JOIN dbo.NHANVIEN NV ON KB.BacSiPhuTrach = NV.MaNV
    WHERE KB.MaThuCung = @MaThuCung
    ORDER BY KB.NgayKham DESC
END
GO
-----PHÂN HỆ QUẢN LÝ CHI NHÁNH-----
--QLCN1: Thống kê doanh thu
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN1_ThongKeDoanhThu
    @MaCN NVARCHAR(10),
    @LoaiLoc NVARCHAR(20), -- 'Ngay', 'Thang', 'Quy', 'Nam'
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DV.MaDichVu,
        DV.TenDV AS TenDichVu,
        COUNT(CT.MaLSGD) AS SoLuongLSDV,
        SUM(CT.TongPhiDV) AS DoanhThu
    FROM dbo.HOADON HD
    JOIN dbo.CT_HOADON CT ON HD.MaHD = CT.MaHD
    JOIN dbo.LS_DV LS ON CT.MaLSGD = LS.MaLSDV
    JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE 
        HD.MaCN = @MaCN 
        AND HD.TrangThaiHD = N'Đã thanh toán'
        AND YEAR(HD.NgayLap) = @Nam
        AND (
            (@LoaiLoc = 'Nam') -- Lấy cả năm
            OR (@LoaiLoc = 'Quy' AND DATEPART(QUARTER, HD.NgayLap) = @Quy)
            OR (@LoaiLoc = 'Thang' AND MONTH(HD.NgayLap) = @Thang)
            OR (@LoaiLoc = 'Ngay' AND MONTH(HD.NgayLap) = @Thang AND DAY(HD.NgayLap) = @Ngay)
        )
    GROUP BY DV.MaDichVu, DV.TenDV
    ORDER BY DoanhThu DESC;
END
GO

--QLCN2:
USE PetCareDB
GO


CREATE OR ALTER PROCEDURE dbo.sp_QLCN2_GetDSNV
    @MaCN NVARCHAR(10)
AS
BEGIN
    SELECT 
        MaNV, HoTenNV, NgaySinhNV, NgayVaoLam, ChucVu, Luong, TrangThaiNV
    FROM NHANVIEN
    WHERE ChiNhanhLamViec = @MaCN
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN2_ThemNV
    @MaNV NVARCHAR(10),
    @HoTen NVARCHAR(50),
    @NgaySinh DATE,
    @NgayVaoLam DATE,
    @ChucVu NVARCHAR(50),
    @Luong DECIMAL(18,2),
    @TrangThai NVARCHAR(50),
    @MaCN NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM NHANVIEN WHERE MaNV = @MaNV)
    BEGIN
        RAISERROR(N'Mã nhân viên đã tồn tại!', 16, 1);
        RETURN;
    END

    DECLARE @NewID_TK INT;

    SELECT @NewID_TK = ID_TK FROM TAIKHOAN WHERE TenDangNhap = @MaNV;

    IF @NewID_TK IS NULL 
    BEGIN
        INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTK)
        VALUES (@MaNV, '123', @ChucVu); -- Pass mặc định 123
        
        SET @NewID_TK = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE TAIKHOAN 
        SET LoaiTK = @ChucVu 
        WHERE ID_TK = @NewID_TK;
    END

    INSERT INTO NHANVIEN (MaNV, HoTenNV, NgaySinhNV, ChucVu, NgayVaoLam, Luong, ChiNhanhLamViec, TrangThaiNV, ID_TK)
    VALUES (@MaNV, @HoTen, @NgaySinh, @ChucVu, @NgayVaoLam, @Luong, @MaCN, @TrangThai, @NewID_TK);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN2_SuaNV
    @MaNV NVARCHAR(10),
    @HoTen NVARCHAR(50),
    @NgaySinh DATE,
    @NgayVaoLam DATE,
    @ChucVu NVARCHAR(50),
    @Luong INT,
    @TrangThai NVARCHAR(50)
AS
BEGIN
    UPDATE NHANVIEN
    SET HoTenNV = @HoTen,
        NgaySinhNV = @NgaySinh,
        NgayVaoLam = @NgayVaoLam,
        ChucVu = @ChucVu,
        Luong = @Luong,
        TrangThaiNV = @TrangThai
    WHERE MaNV = @MaNV;
    
    UPDATE TAIKHOAN
    SET LoaiTK = @ChucVu
    WHERE ID_TK = (SELECT ID_TK FROM NHANVIEN WHERE MaNV = @MaNV);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN2_XoaNV
    @MaNV NVARCHAR(10)
AS
BEGIN
    BEGIN TRY
        DECLARE @ID_TK INT = (SELECT ID_TK FROM NHANVIEN WHERE MaNV = @MaNV);

        DELETE FROM NHANVIEN WHERE MaNV = @MaNV;
        DELETE FROM TAIKHOAN WHERE ID_TK = @ID_TK;
    END TRY
    BEGIN CATCH
        RAISERROR(N'Nhân viên này đã có dữ liệu lịch sử (Hóa đơn/Lịch khám), không thể xóa hẳn. Hãy chuyển trạng thái sang "Đã nghỉ việc".', 16, 1);
    END CATCH
END
GO

--QLCN3:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN3_XemHieuSuat
    @MaNV NVARCHAR(10),
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ChucVu NVARCHAR(50);
    DECLARE @SoLuongGD INT = 0;

    SELECT @ChucVu = ChucVu FROM dbo.NHANVIEN WHERE MaNV = @MaNV;

    IF @ChucVu = N'Tiếp tân' OR @ChucVu = N'Thu ngân' OR @ChucVu = N'Quản lý chi nhánh'
    BEGIN
        SELECT @SoLuongGD = COUNT(*)
        FROM dbo.HOADON
        WHERE NV_Lap = @MaNV
          AND (@Nam = 0 OR YEAR(NgayLap) = @Nam)
          AND (@Quy = 0 OR @Quy IS NULL OR DATEPART(QUARTER, NgayLap) = @Quy)
          AND (@Thang = 0 OR @Thang IS NULL OR MONTH(NgayLap) = @Thang)
          AND (@Ngay = 0 OR @Ngay IS NULL OR DAY(NgayLap) = @Ngay);
    END
    ELSE IF @ChucVu = N'Bác sĩ'
    BEGIN
        DECLARE @SoCaKham INT = 0;
        DECLARE @SoCaTiem INT = 0;

        SELECT @SoCaKham = COUNT(*)
        FROM dbo.LS_DVKHAMBENH
        WHERE BacSiPhuTrach = @MaNV
          AND (@Nam = 0 OR YEAR(NgayKham) = @Nam)
          AND (@Quy = 0 OR @Quy IS NULL OR DATEPART(QUARTER, NgayKham) = @Quy)
          AND (@Thang = 0 OR @Thang IS NULL OR MONTH(NgayKham) = @Thang)
          AND (@Ngay = 0 OR @Ngay IS NULL OR DAY(NgayKham) = @Ngay);

        SELECT @SoCaTiem = COUNT(*)
        FROM dbo.LS_DVTIEMPHONG
        WHERE BacSiPhuTrach = @MaNV
          AND (@Nam = 0 OR YEAR(NgayTiem) = @Nam)
          AND (@Quy = 0 OR @Quy IS NULL OR DATEPART(QUARTER, NgayTiem) = @Quy)
          AND (@Thang = 0 OR @Thang IS NULL OR MONTH(NgayTiem) = @Thang)
          AND (@Ngay = 0 OR @Ngay IS NULL OR DAY(NgayTiem) = @Ngay);

        SET @SoLuongGD = @SoCaKham + @SoCaTiem;
    END

    SELECT @SoLuongGD AS KetQua;
END
GO

--QLCN4:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN4_ThongKeVacxin
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL,
    @LoaiSort VARCHAR(10)
BEGIN
    SELECT 
        VX.MaVacXin,
        VX.TenVacXin,
        COUNT(LS.MaLSDVTP) AS SLDat
    FROM dbo.VACXIN VX
    LEFT JOIN dbo.LS_DVTIEMPHONG LS ON VX.MaVacXin = LS.LoaiVacXin
    WHERE 
        (@Nam = 0 OR YEAR(LS.NgayTiem) = @Nam)
        AND (@Quy IS NULL OR DATEPART(QUARTER, LS.NgayTiem) = @Quy)
        AND (@Thang IS NULL OR MONTH(LS.NgayTiem) = @Thang)
        AND (@Ngay IS NULL OR DAY(LS.NgayTiem) = @Ngay)
    GROUP BY VX.MaVacXin, VX.TenVacXin
    ORDER BY 
        CASE WHEN @LoaiSort = 'MAX' THEN COUNT(LS.MaLSDVTP) END DESC,
        CASE WHEN @LoaiSort = 'MIN' THEN COUNT(LS.MaLSDVTP) END ASC,
        COUNT(LS.MaLSDVTP) DESC 
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN4_ThongKeThuoc
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL,
    @LoaiSort VARCHAR(10)
AS
BEGIN
    SELECT 
        T.MaThuoc,
        SP.TenSP AS TenThuoc,
        T.DonViTinh AS DVTinh,
        T.NgayHetHan AS HanSD,
        ISNULL(SUM(TT.SoLuongThuoc), 0) AS SLBan
    FROM dbo.THUOC T
    JOIN dbo.SANPHAM SP ON T.MaThuoc = SP.MaSP
    LEFT JOIN dbo.TOA_THUOC TT ON T.MaThuoc = TT.MaThuoc
    LEFT JOIN dbo.LS_DVKHAMBENH KB ON TT.MaLSDV = KB.MaLSDVKB
    WHERE 
        (@Nam = 0 OR YEAR(KB.NgayKham) = @Nam)
        AND (@Quy IS NULL OR DATEPART(QUARTER, KB.NgayKham) = @Quy)
        AND (@Thang IS NULL OR MONTH(KB.NgayKham) = @Thang)
        AND (@Ngay IS NULL OR DAY(KB.NgayKham) = @Ngay)
    GROUP BY T.MaThuoc, SP.TenSP, T.DonViTinh, T.NgayHetHan
    ORDER BY 
        CASE WHEN @LoaiSort = 'MAX' THEN SUM(TT.SoLuongThuoc) END DESC,
        CASE WHEN @LoaiSort = 'MIN' THEN SUM(TT.SoLuongThuoc) END ASC,
        SUM(TT.SoLuongThuoc) DESC
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN4_ThongKeSanPham
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL,
    @LoaiSort VARCHAR(10)
AS
BEGIN
    SELECT 
        SP.MaSP,
        SP.TenSP,
        ISNULL(SUM(CT.SoLuongSP), 0) AS SLBan
    FROM dbo.SANPHAM SP
    LEFT JOIN dbo.CT_MUAHANG CT ON SP.MaSP = CT.MaSP
    LEFT JOIN dbo.LS_DVMUAHANG LS ON CT.MaLSDVMH = LS.MaLSDVMH
    LEFT JOIN dbo.LS_DV GOC ON LS.MaLSDVMH = GOC.MaLSDV
    WHERE 
        SP.LoaiSP NOT IN (N'Thuốc')
        AND (@Nam = 0 OR YEAR(GOC.NgayDatTruoc) = @Nam)
        AND (@Quy IS NULL OR DATEPART(QUARTER, GOC.NgayDatTruoc) = @Quy)
        AND (@Thang IS NULL OR MONTH(GOC.NgayDatTruoc) = @Thang)
        AND (@Ngay IS NULL OR DAY(GOC.NgayDatTruoc) = @Ngay)
    GROUP BY SP.MaSP, SP.TenSP
    ORDER BY 
        CASE WHEN @LoaiSort = 'MAX' THEN SUM(CT.SoLuongSP) END DESC,
        CASE WHEN @LoaiSort = 'MIN' THEN SUM(CT.SoLuongSP) END ASC,
        SUM(CT.SoLuongSP) DESC
END
GO

--QLCN5:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN5_GetDSSP
    @MaCN NVARCHAR(10)
AS
BEGIN
    SELECT sp.MaSP, sp.TenSP, sp.LoaiSP, 
                            ISNULL(ct.GiaSPCN, sp.GiaBan) as DonGia, 
                            ISNULL(ct.SLTonKho, 0) as SLTonKho 
                     FROM SANPHAM sp 
                     LEFT JOIN CT_SPCN ct ON sp.MaSP = ct.MaSP AND ct.MaCN = @MaCN
END
GO

CREATE OR ALTER PROCEDURE sp_QLCN5_ThemSP
    @MaSP VARCHAR(20),
    @TenSP NVARCHAR(100),
    @LoaiSP NVARCHAR(50),
    @DonGia INT, 
    @SLTonKho INT,
    @MaCN VARCHAR(10)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = @MaSP)
    BEGIN
        INSERT INTO SANPHAM (MaSP, TenSP, LoaiSP, GiaBan) 
        VALUES (@MaSP, @TenSP, @LoaiSP, @DonGia)
    END

    DECLARE @GiaChot INT = @DonGia;
    IF (@GiaChot IS NULL OR @GiaChot = 0)
    BEGIN
        SELECT @GiaChot = GiaBan FROM SANPHAM WHERE MaSP = @MaSP;
    END

    IF EXISTS (SELECT 1 FROM CT_SPCN WHERE MaSP = @MaSP AND MaCN = @MaCN)
    BEGIN
        UPDATE CT_SPCN 
        SET GiaSPCN = @GiaChot, SLTonKho = @SLTonKho 
        WHERE MaSP = @MaSP AND MaCN = @MaCN
    END
    ELSE
    BEGIN
        INSERT INTO CT_SPCN (MaSP, MaCN, GiaSPCN, SLTonKho)
        VALUES (@MaSP, @MaCN, @GiaChot, @SLTonKho)
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN5_SuaSP
    @MaSP NVARCHAR(10),
    @TenSP NVARCHAR(100),
    @LoaiSP NVARCHAR(20),
    @DonGia DECIMAL(18,2),
    @SLTonKho INT,
    @MaCN NVARCHAR(10)
AS
BEGIN
    UPDATE SANPHAM 
    SET TenSP = @TenSP, LoaiSP = @LoaiSP 
    WHERE MaSP = @MaSP;
    UPDATE CT_SPCN
    SET GiaSPCN = @DonGia, SLTonKho = @SLTonKho
    WHERE MaCN = @MaCN AND MaSP = @MaSP;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN5_XoaSP
    @MaSP NVARCHAR(10),
    @MaCN NVARCHAR(10)
AS
BEGIN
    BEGIN TRY
        DELETE FROM CT_SPCN WHERE MaCN = @MaCN AND MaSP = @MaSP;
    END TRY
    BEGIN CATCH
        RAISERROR(N'Sản phẩm đã có lịch sử giao dịch tại chi nhánh, không thể xóa. Hãy chỉnh số lượng về 0.', 16, 1);
    END CATCH
END
GO

--QLCN6:
USE PetCareDB
GO
CREATE OR ALTER PROCEDURE sp_QLCN6_GetMaLSDVByPet
    @MaPet NVARCHAR(50)
AS
BEGIN
    SELECT DISTINCT MaLSDV 
    FROM dbo.LS_DV LS
    WHERE 
        EXISTS (SELECT 1 FROM dbo.LS_DVKHAMBENH KB WHERE KB.MaLSDVKB = LS.MaLSDV AND KB.MaThuCung = @MaPet)
        OR 
        EXISTS (SELECT 1 FROM dbo.LS_DVTIEMPHONG TP 
                INNER JOIN dbo.LS_DVKHAMBENH KB ON TP.MaLSDVTP = KB.MaLSDVKB 
                WHERE KB.MaThuCung = @MaPet)
END;
GO

CREATE OR ALTER PROCEDURE sp_QLCN6_GetMaSPByPet
    @MaPet NVARCHAR(50)
AS
BEGIN
    SELECT DISTINCT MaThuoc AS MaSP 
    FROM dbo.TOA_THUOC 
    WHERE MaLSDV IN (SELECT MaLSDVKB FROM dbo.LS_DVKHAMBENH WHERE MaThuCung = @MaPet)

    UNION

    SELECT DISTINCT TP.LoaiVacXin 
    FROM dbo.LS_DVTIEMPHONG TP
    INNER JOIN dbo.LS_DVKHAMBENH KB ON TP.MaLSDVTP = KB.MaLSDVKB
    WHERE KB.MaThuCung = @MaPet
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN6_ThongKePet
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL,
    @LoaiDV NVARCHAR(20) 
AS
BEGIN
    IF @LoaiDV = 'KHAM'
    BEGIN
        SELECT 
            TC.MaThuCung AS MaPet,
            TC.TenThuCung AS TenPet,
            TC.LoaiThuCung AS LoaiPet,
            COUNT(KB.MaLSDVKB) AS SoLan
        FROM dbo.THUCUNG TC
        JOIN dbo.LS_DVKHAMBENH KB ON TC.MaThuCung = KB.MaThuCung
        WHERE 
            (@Nam = 0 OR YEAR(KB.NgayKham) = @Nam)
            AND (@Quy IS NULL OR DATEPART(QUARTER, KB.NgayKham) = @Quy)
            AND (@Thang IS NULL OR MONTH(KB.NgayKham) = @Thang)
            AND (@Ngay IS NULL OR DAY(KB.NgayKham) = @Ngay)
        GROUP BY TC.MaThuCung, TC.TenThuCung, TC.LoaiThuCung
    END
    ELSE
    BEGIN
        SELECT 
            TC.MaThuCung AS MaPet,
            TC.TenThuCung AS TenPet,
            TC.LoaiThuCung AS LoaiPet,
            COUNT(TP.MaLSDVTP) AS SoLan
        FROM dbo.THUCUNG TC
        JOIN dbo.LS_DVTIEMPHONG TP ON TC.MaThuCung = TP.MaThuCung
        WHERE 
            (@Nam = 0 OR YEAR(TP.NgayTiem) = @Nam)
            AND (@Quy IS NULL OR DATEPART(QUARTER, TP.NgayTiem) = @Quy)
            AND (@Thang IS NULL OR MONTH(TP.NgayTiem) = @Thang)
            AND (@Ngay IS NULL OR DAY(TP.NgayTiem) = @Ngay)
        GROUP BY TC.MaThuCung, TC.TenThuCung, TC.LoaiThuCung
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN6_XemLichSuPet
    @MaPet NVARCHAR(10),
    @MaLSDV NVARCHAR(10) = NULL 
AS
BEGIN
    SELECT 
        TP.MaLSDVTP AS MaLSDV,
        NV.HoTenNV AS BacSi,
        TP.NgayTiem AS NgaySD,
        VX.MaVacXin AS MaSP,
        VX.TenVacXin AS TenSP,
        N'Vacxin' AS LoaiSP
    FROM dbo.LS_DVTIEMPHONG TP
    LEFT JOIN dbo.NHANVIEN NV ON TP.BacSiPhuTrach = NV.MaNV
    LEFT JOIN dbo.VACXIN VX ON TP.LoaiVacXin = VX.MaVacXin
    WHERE TP.MaThuCung = @MaPet 
      AND (@MaLSDV IS NULL OR TP.MaLSDVTP LIKE '%' + @MaLSDV + '%')

    UNION ALL

    SELECT 
        KB.MaLSDVKB AS MaLSDV,
        NV.HoTenNV AS BacSi,
        KB.NgayKham AS NgaySD,
        T.MaThuoc AS MaSP,
        SP.TenSP AS TenSP,
        N'Thuốc' AS LoaiSP
    FROM dbo.LS_DVKHAMBENH KB
    LEFT JOIN dbo.NHANVIEN NV ON KB.BacSiPhuTrach = NV.MaNV
    JOIN dbo.TOA_THUOC TT ON KB.MaLSDVKB = TT.MaLSDV
    JOIN dbo.THUOC T ON TT.MaThuoc = T.MaThuoc
    JOIN dbo.SANPHAM SP ON T.MaThuoc = SP.MaSP
    WHERE KB.MaThuCung = @MaPet
      AND (@MaLSDV IS NULL OR KB.MaLSDVKB LIKE '%' + @MaLSDV + '%')

    ORDER BY NgaySD DESC
END
GO

--QLCN7:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN7_ThongKeKhachHang
    @TuNgay DATE,
    @DenNgay DATE,
    @LoaiTK NVARCHAR(20) -- 'ALL', 'NEW', 'OLD', 'VIP', 'LOST'
AS
BEGIN
    SET NOCOUNT ON;

    IF @LoaiTK = 'ALL'
    BEGIN
        SELECT 
            KH.MaKH,
            KH.HoTen_KH AS HoTen,
            KH.SDT_KH AS SDT,
            KH.Loai_KH AS LoaiKH,
            MAX(HD.NgayLap) AS NgayGiaoDichGanNhat,
            SUM(HD.TienThanhToan) AS TongChiTieu
        FROM dbo.KHACHHANG KH
        JOIN dbo.HOADON HD ON KH.MaKH = HD.MaKH
        WHERE HD.NgayLap BETWEEN @TuNgay AND @DenNgay
        GROUP BY KH.MaKH, KH.HoTen_KH, KH.SDT_KH, KH.Loai_KH
        ORDER BY TongChiTieu DESC
    END

    ELSE IF @LoaiTK = 'NEW' 
    BEGIN
        SELECT 
            KH.MaKH,
            KH.HoTen_KH AS HoTen,
            KH.SDT_KH AS SDT,
            KH.Loai_KH AS LoaiKH,
            MIN(HD.NgayLap) AS NgayGiaoDichGanNhat,
            SUM(HD.TienThanhToan) AS TongChiTieu
        FROM dbo.KHACHHANG KH
        JOIN dbo.HOADON HD ON KH.MaKH = HD.MaKH
        GROUP BY KH.MaKH, KH.HoTen_KH, KH.SDT_KH, KH.Loai_KH
        HAVING MIN(HD.NgayLap) BETWEEN @TuNgay AND @DenNgay
    END

    ELSE IF @LoaiTK = 'OLD' 
    BEGIN
        SELECT 
            KH.MaKH,
            KH.HoTen_KH AS HoTen,
            KH.SDT_KH AS SDT,
            KH.Loai_KH AS LoaiKH,
            MAX(HD.NgayLap) AS NgayGiaoDichGanNhat,
            SUM(HD.TienThanhToan) AS TongChiTieu
        FROM dbo.KHACHHANG KH
        JOIN dbo.HOADON HD ON KH.MaKH = HD.MaKH
        GROUP BY KH.MaKH, KH.HoTen_KH, KH.SDT_KH, KH.Loai_KH
        HAVING MIN(HD.NgayLap) < @TuNgay
           AND MAX(HD.NgayLap) BETWEEN @TuNgay AND @DenNgay 
    END

    ELSE IF @LoaiTK = 'VIP' 
    BEGIN
        SELECT 
            KH.MaKH,
            KH.HoTen_KH AS HoTen,
            KH.SDT_KH AS SDT,
            (KH.Loai_KH + ISNULL(' (' + HV.CapDo + ')', '')) AS LoaiKH, 
            MAX(HD.NgayLap) AS NgayGiaoDichGanNhat,
            SUM(HD.TienThanhToan) AS TongChiTieu
        FROM dbo.KHACHHANG KH
        JOIN dbo.HOADON HD ON KH.MaKH = HD.MaKH
        LEFT JOIN dbo.HOIVIEN HV ON KH.MaKH = HV.MaKH
        WHERE HD.NgayLap BETWEEN @TuNgay AND @DenNgay
          AND (KH.Loai_KH = N'Hội viên' OR HV.MaKH IS NOT NULL) 
        GROUP BY KH.MaKH, KH.HoTen_KH, KH.SDT_KH, KH.Loai_KH, HV.CapDo
        ORDER BY TongChiTieu DESC
    END

    ELSE IF @LoaiTK = 'LOST' 
    BEGIN
        SELECT 
            KH.MaKH,
            KH.HoTen_KH AS HoTen,
            KH.SDT_KH AS SDT,
            KH.Loai_KH AS LoaiKH,
            MAX(HD.NgayLap) AS NgayGiaoDichGanNhat,
            SUM(HD.TienThanhToan) AS TongChiTieu
        FROM dbo.KHACHHANG KH
        JOIN dbo.HOADON HD ON KH.MaKH = HD.MaKH
        GROUP BY KH.MaKH, KH.HoTen_KH, KH.SDT_KH, KH.Loai_KH
        HAVING MAX(HD.NgayLap) < @TuNgay
    END
END
GO
--QLCN8:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN9_GetDSDV
    @MaCN NVARCHAR(10)
AS
BEGIN
    SELECT 
        DV.MaDichVu,
        DV.TenDV,
        ISNULL(CD.GiaDV_CN, DV.GiaTienDV) AS GiaTien, 
        ISNULL(CD.TrangThaiDV, N'Ngừng kinh doanh') AS TrangThai
    FROM dbo.DICHVU DV
    LEFT JOIN dbo.CHINHANH_DV CD ON DV.MaDichVu = CD.MaDichVu AND CD.MaCN = @MaCN
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN9_ThemSuaDV
    @MaDV NVARCHAR(10),
    @TenDV NVARCHAR(100),
    @GiaTien INT,
    @TrangThai NVARCHAR(50),
    @MaCN NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM DICHVU WHERE MaDichVu = @MaDV)
    BEGIN
        INSERT INTO DICHVU (MaDichVu, TenDV, GiaTienDV)
        VALUES (@MaDV, @TenDV, @GiaTien);
    END
    ELSE
    BEGIN
        UPDATE DICHVU SET TenDV = @TenDV WHERE MaDichVu = @MaDV;
    END

    MERGE dbo.CHINHANH_DV AS T
    USING (SELECT @MaCN AS MaCN, @MaDV AS MaDV) AS S
    ON (T.MaCN = S.MaCN AND T.MaDichVu = S.MaDV)
    WHEN MATCHED THEN
        UPDATE SET GiaDV_CN = @GiaTien, TrangThaiDV = @TrangThai
    WHEN NOT MATCHED THEN
        INSERT (MaCN, MaDichVu, GiaDV_CN, TrangThaiDV)
        VALUES (@MaCN, @MaDV, @GiaTien, @TrangThai);
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN9_XoaDV
    @MaDV NVARCHAR(10),
    @MaCN NVARCHAR(10)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM LS_DV WHERE MaDichVu = @MaDV)
    BEGIN
        UPDATE CHINHANH_DV 
        SET TrangThaiDV = N'Ngừng kinh doanh' 
        WHERE MaCN = @MaCN AND MaDichVu = @MaDV;
    END
    ELSE
    BEGIN
        DELETE FROM CHINHANH_DV WHERE MaCN = @MaCN AND MaDichVu = @MaDV;
    END
END
GO

--QLCN9:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN10_GetDSHoaDon
    @MaCN NVARCHAR(10),
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL,
    @MaHD NVARCHAR(10) = NULL
AS
BEGIN
    SELECT 
        HD.MaHD,
        HD.MaKH,
        KH.HoTen_KH AS TenKH,
        HD.NgayLap,
        HD.TienThanhToan AS TongTien,
        HD.TrangThaiHD
    FROM dbo.HOADON HD
    LEFT JOIN dbo.KHACHHANG KH ON HD.MaKH = KH.MaKH
    WHERE HD.MaCN = @MaCN
      AND (@Nam = 0 OR YEAR(HD.NgayLap) = @Nam)
      AND (@Quy IS NULL OR DATEPART(QUARTER, HD.NgayLap) = @Quy)
      AND (@Thang IS NULL OR MONTH(HD.NgayLap) = @Thang)
      AND (@Ngay IS NULL OR DAY(HD.NgayLap) = @Ngay)
      AND (@MaHD IS NULL OR HD.MaHD LIKE '%' + @MaHD + '%')
    ORDER BY HD.NgayLap DESC
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN10_GetChiTietHD
    @MaHD NVARCHAR(10)
AS
BEGIN
    SELECT 
        DV.TenDV AS TenMuc,
        1 AS SoLuong,
        CT.TongPhiDV AS ThanhTien
    FROM CT_HOADON CT
    JOIN LS_DV LS ON CT.MaLSGD = LS.MaLSDV
    JOIN DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    WHERE CT.MaHD = @MaHD
    
    UNION ALL
    
    SELECT 
        KM.LoaiKM + N' (' + CAST(KM.GiaKM AS NVARCHAR) + N'%)' AS TenMuc,
        CTKM.SoLuongDung AS SoLuong,
        -CTKM.TienKM AS ThanhTien -- Tiền giảm giá hiển thị âm
    FROM CT_KHUYENMAI CTKM
    JOIN KHUYENMAI KM ON CTKM.MaKM = KM.MaKM
    WHERE CTKM.MaHD = @MaHD
END
GO

--QLCN10:
USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN11_GetLSDV
    @MaCN NVARCHAR(10), 
    @Nam INT,
    @Quy INT = NULL,
    @Thang INT = NULL,
    @Ngay INT = NULL,
    @MaDV NVARCHAR(10) = NULL
AS
BEGIN
    SELECT 
        LS.MaLSDV,
        LS.MaDichVu AS MaDV,
        DV.TenDV,
        LS.NgayDatTruoc AS NgayDat,
        ISNULL(LS.NgayDatTruoc, GETDATE()) AS NgaySD, 
        ISNULL(CT.TongPhiDV, DV.GiaTienDV) AS ThanhTien, 
        LS.TrangThaiGD AS TrangThai
    FROM dbo.LS_DV LS
    JOIN dbo.DICHVU DV ON LS.MaDichVu = DV.MaDichVu
    LEFT JOIN dbo.CT_HOADON CT ON LS.MaLSDV = CT.MaLSGD
    LEFT JOIN dbo.HOADON HD ON CT.MaHD = HD.MaHD
    WHERE 
        (@MaCN IS NULL OR HD.MaCN = @MaCN OR HD.MaCN IS NULL)
        AND (@Nam = 0 OR YEAR(LS.NgayDatTruoc) = @Nam)
        AND (@Quy IS NULL OR DATEPART(QUARTER, LS.NgayDatTruoc) = @Quy)
        AND (@Thang IS NULL OR MONTH(LS.NgayDatTruoc) = @Thang)
        AND (@Ngay IS NULL OR DAY(LS.NgayDatTruoc) = @Ngay)
        AND (@MaDV IS NULL OR LS.MaDichVu = @MaDV)
    ORDER BY LS.NgayDatTruoc DESC
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_QLCN11_GetDiemDanhGia
    @MaDV NVARCHAR(10)
AS
BEGIN
    SELECT ISNULL(AVG(CAST(DiemDV AS FLOAT)), 0) AS DiemTB
    FROM dbo.DANHGIA
    WHERE (@MaDV IS NULL OR MaDV = @MaDV)
END
GO

-----BÁC SĨ-----

---BS: Tạo Hồ Sơ

USE PetCareDB
GO

CREATE PROCEDURE sp_TaoHoSoKhamMoi
    @MaLSKB NVARCHAR(10),
    @MaNV NVARCHAR(10),
    @MaTC NVARCHAR(10),
    @NgayKham DATE,
    @NgayHen DATE,
    @TrieuChung NVARCHAR(100),
    @ChuanDoan NVARCHAR(100)  
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LS_DVKHAMBENH (MaLSDVKB, BacSiPhuTrach, NgayHen, MaThuCung, NgayKham)
    VALUES (@MaLSKB, @MaNV, @NgayHen, @MaTC, @NgayKham);

    INSERT INTO TRIEUCHUNG (MaLSKB, TrieuChung) 
    VALUES (@MaLSKB, @TrieuChung);

    INSERT INTO CHUANDOAN (MaLSKB, ChuanDoan) 
    VALUES (@MaLSKB, @ChuanDoan);
END;
GO

CREATE OR ALTER PROCEDURE sp_BacSiCapNhatKham
    @MaLSKB NVARCHAR(10),
    @MaNV NVARCHAR(10),
    @TrieuChung NVARCHAR(100),
    @ChuanDoan NVARCHAR(100)
AS
BEGIN
    UPDATE LS_DVKHAMBENH
    SET BacSiPhuTrach = @MaNV,
        NgayKham = GETDATE() 
    WHERE MaLSDVKB = @MaLSKB;

    DELETE FROM TRIEUCHUNG WHERE MaLSKB = @MaLSKB;
    DELETE FROM CHUANDOAN WHERE MaLSKB = @MaLSKB;

    INSERT INTO TRIEUCHUNG (MaLSKB, TrieuChung) VALUES (@MaLSKB, @TrieuChung);
    INSERT INTO CHUANDOAN (MaLSKB, ChuanDoan) VALUES (@MaLSKB, @ChuanDoan);
END
GO

USE PetCareDBOpt
GO
---BS: Tạo toa thuốc

CREATE PROCEDURE sp_GetThongTinThuoc
    @MaThuoc NVARCHAR(10),
    @MaLSDV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaCN NVARCHAR(10);
    SELECT @MaCN = ChiNhanhLamViec 
    FROM NHANVIEN 
    WHERE MaNV = (SELECT BacSiPhuTrach FROM LS_DVKHAMBENH WHERE MaLSDVKB = @MaLSDV);

    SELECT 
        s.TenSP, 
        ct.GiaSPCN AS GiaBan,
        ct.SLTonKho
    FROM SANPHAM s
    INNER JOIN CT_SPCN ct ON s.MaSP = ct.MaSP
    WHERE s.MaSP = @MaThuoc AND ct.MaCN = @MaCN;
END
GO

CREATE PROCEDURE sp_LuuChiTietToa
    @MaThuoc NVARCHAR(10),
    @MaLSDV NVARCHAR(10),
    @SL INT,
    @LieuDung NVARCHAR(50),
    @ThanhTien DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaCN NVARCHAR(10);
    SELECT @MaCN = ChiNhanhLamViec 
    FROM NHANVIEN 
    WHERE MaNV = (SELECT BacSiPhuTrach FROM LS_DVKHAMBENH WHERE MaLSDVKB = @MaLSDV);

    UPDATE CT_SPCN 
    SET SLTonKho = SLTonKho - @SL 
    WHERE MaSP = @MaThuoc AND MaCN = @MaCN;

    IF EXISTS (SELECT 1 FROM TOA_THUOC WHERE MaThuoc = @MaThuoc AND MaLSDV = @MaLSDV)
        UPDATE TOA_THUOC SET SoLuongThuoc += @SL, ThanhTien += @ThanhTien, LieuDung = @LieuDung
        WHERE MaThuoc = @MaThuoc AND MaLSDV = @MaLSDV;
    ELSE
        INSERT INTO TOA_THUOC (MaThuoc, MaLSDV, SoLuongThuoc, LieuDung, ThanhTien)
        VALUES (@MaThuoc, @MaLSDV, @SL, @LieuDung, @ThanhTien);
END
GO

USE PetCareDB;
GO

CREATE OR ALTER PROCEDURE sp_GetChiTietHoSo
    @MaLSKB NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        kb.MaLSDVKB,
        kb.MaThuCung,
        tc.TenThuCung,
        tc.LoaiThuCung,
        tc.TinhTrangSK,
        kh.HoTen_KH,
        DATEDIFF(YEAR, tc.NgaySinh_TC, GETDATE()) AS Tuoi 
    FROM LS_DVKHAMBENH kb
    JOIN THUCUNG tc ON kb.MaThuCung = tc.MaThuCung
    JOIN KHACHHANG kh ON tc.MaKH = kh.MaKH
    WHERE kb.MaLSDVKB = @MaLSKB;
END
GO
---BS: Quản lý gói tiêm
CREATE OR ALTER PROCEDURE sp_GetGoiTiemDaMua
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SELECT gt.MaGoiTiem, gt.TenGoi
    FROM LS_DANGKY dk
    JOIN GOITIEM gt ON dk.MaGoiTiem = gt.MaGoiTiem
    JOIN KHACHHANG kh ON dk.MaKH = kh.MaKH
    JOIN THUCUNG tc ON kh.MaKH = tc.MaKH
    WHERE tc.MaThuCung = @MaThuCung
END
GO

CREATE OR ALTER PROCEDURE sp_GetChiTietGoiTiemVoiTrangThai
    @MaGoiTiem NVARCHAR(8),
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SELECT 
        nd.MaVacXin AS MaVC,
        vx.TenVacXin AS [Tên Vaccine],
        nd.SoMui AS [Số Mũi],
        (SELECT MAX(NgayTiem) FROM LS_DVTIEMPHONG tp 
         WHERE tp.MaGoiTiem = @MaGoiTiem AND tp.MaThuCung = @MaThuCung 
         AND tp.LoaiVacXin = nd.MaVacXin) AS [Ngày Tiêm]
    FROM ND_GOITIEM nd
    JOIN VACXIN vx ON nd.MaVacXin = vx.MaVacXin
    WHERE nd.MaGoiTiem = @MaGoiTiem
END
GO

CREATE OR ALTER PROCEDURE sp_LuuTiemPhongTheoGoi
    @MaLSDVTP NVARCHAR(8),
    @BacSiPhuTrach NVARCHAR(10),
    @MaGoiTiem NVARCHAR(8),
    @LoaiVacXin NVARCHAR(8),
    @NgayTiem DATE,
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    INSERT INTO LS_DVTIEMPHONG (MaLSDVTP, BacSiPhuTrach, MaGoiTiem, LoaiVacXin, LieuLuong, NgayTiem, MaThuCung)
    VALUES (@MaLSDVTP, @BacSiPhuTrach, @MaGoiTiem, @LoaiVacXin, N'Đúng liều', @NgayTiem, @MaThuCung);
END
GO

---BS: Lịch sử khám
CREATE OR ALTER PROCEDURE sp_GetLichSuKhamBenh
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SELECT 
        kb.MaLSDVKB AS [Mã LSDV],
        kb.MaThuCung AS [Mã Thú Cưng], 
        cd.ChuanDoan AS [Chuẩn Đoán],
        tr.TrieuChung AS [Triệu Chứng],
        STUFF((
            SELECT CHAR(13) + '• ' + s.TenSP + ' (SL:' + CAST(tt.SoLuongThuoc AS NVARCHAR(5)) + ' | ' + tt.LieuDung + ')'
            FROM TOA_THUOC tt
            JOIN SANPHAM s ON tt.MaThuoc = s.MaSP
            WHERE tt.MaLSDV = kb.MaLSDVKB
            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS [Toa Thuốc],
        kb.NgayHen AS [Ngày Hẹn],
        nv.HoTenNV AS [BS Phụ Trách]
    FROM LS_DVKHAMBENH kb
    LEFT JOIN CHUANDOAN cd ON kb.MaLSDVKB = cd.MaLSKB
    LEFT JOIN TRIEUCHUNG tr ON kb.MaLSDVKB = tr.MaLSKB
    LEFT JOIN NHANVIEN nv ON kb.BacSiPhuTrach = nv.MaNV
    WHERE kb.MaThuCung = @MaThuCung
    ORDER BY kb.NgayKham DESC
END
GO

CREATE OR ALTER PROCEDURE sp_GetLichSuTiemPhong
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        tp.MaLSDVTP AS [Mã LSDV], 
        tp.MaThuCung AS [Mã Thú Cưng],
        tp.MaGoiTiem AS [Mã Gói Tiêm], 
        tp.NgayTiem AS [Ngày SD], 
        tp.LieuLuong AS [Liều Lượng], 
        COALESCE(v.TenVacXin, tp.LoaiVacXin, N'Chưa xác định') AS [Loại Vaccine], 
        nv.HoTenNV AS [BS Phụ Trách]
    FROM LS_DVTIEMPHONG tp
    LEFT JOIN VACXIN v ON tp.LoaiVacXin = v.MaVacXin 
    LEFT JOIN NHANVIEN nv ON tp.BacSiPhuTrach = nv.MaNV
    WHERE tp.MaThuCung = @MaThuCung;
END
GO

CREATE OR ALTER PROCEDURE sp_TraCuuVatTu
    @TuKhoa NVARCHAR(100) = '', 
    @Loai NVARCHAR(10), 
    @MaNV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @MaCN NVARCHAR(10);
    SELECT @MaCN = ChiNhanhLamViec FROM NHANVIEN WHERE MaNV = @MaNV;

    IF @Loai = 'Thuoc'
    BEGIN
        SELECT 
            s.MaSP AS [Mã Thuốc], 
            s.TenSP AS [Tên Thuốc], 
            t.DonViTinh AS [Đơn Vị Tính], 
            ct.GiaSPCN AS [Giá Bán],
            ct.SLTonKho AS [Số Lượng Tồn Kho],
            t.NgayHetHan AS [Ngày Hết Hạn]
        FROM SANPHAM s
        JOIN THUOC t ON s.MaSP = t.MaThuoc
        JOIN CT_SPCN ct ON s.MaSP = ct.MaSP
        WHERE ct.MaCN = @MaCN 
        AND (s.MaSP LIKE '%' + @TuKhoa + '%' OR s.TenSP LIKE '%' + @TuKhoa + '%');
    END
    ELSE IF @Loai = 'Vaccine'
    BEGIN
        SELECT 
            MaVacXin AS [Mã Vaccine], 
            TenVacXin AS [Tên Vaccine], 
            GiaTien AS [Giá Tiền], 
            SoMuiTonKho AS [Số Lượng Tồn Kho]
        FROM VACXIN
        WHERE (@TuKhoa = '' OR MaVacXin LIKE '%' + @TuKhoa + '%' OR TenVacXin LIKE '%' + @TuKhoa + '%');
    END
END
GO

---BS: Tiêm phòng lẻ

CREATE OR ALTER PROCEDURE sp_GetThongTinThuCung
    @MaTC NVARCHAR(10)
AS
BEGIN
    SELECT MaKH, LoaiThuCung, DATEDIFF(YEAR, NgaySinh_TC, GETDATE()) as Tuoi, TinhTrangSK 
    FROM THUCUNG WHERE MaThuCung = @MaTC;
END
GO

CREATE OR ALTER PROCEDURE sp_LuuTiemPhongLe
    @MaLSDVTP NVARCHAR(8),
    @MaNV NVARCHAR(10),
    @MaVacXin NVARCHAR(8),
    @LieuLuong NVARCHAR(50),
    @NgayTiem DATE,
    @MaTC NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
   
    DECLARE @TonKho INT;
    SELECT @TonKho = SoMuiTonKho FROM VACXIN WHERE MaVacXin = @MaVacXin;

    IF @TonKho IS NULL
    BEGIN
        RAISERROR(N'Lỗi: Mã Vaccine không tồn tại!', 16, 1);
        RETURN;
    END

    IF @TonKho <= 0
    BEGIN
        RAISERROR(N'Lỗi: Vaccine này đã hết hàng trong kho!', 16, 1);
        RETURN;
    END

    INSERT INTO LS_DVTIEMPHONG (MaLSDVTP, BacSiPhuTrach, MaGoiTiem, LoaiVacXin, LieuLuong, NgayTiem, MaThuCung)
    VALUES (@MaLSDVTP, @MaNV, NULL, @MaVacXin, @LieuLuong, @NgayTiem, @MaTC);

    UPDATE VACXIN 
    SET SoMuiTonKho = SoMuiTonKho - 1 
    WHERE MaVacXin = @MaVacXin;

    PRINT N'Lưu thành công và đã trừ 1 mũi vaccine.';
END;
GO

---BS: Xem đánh giá
CREATE OR ALTER PROCEDURE sp_GetDanhGiaBacSi
    @MaKH NVARCHAR(10) = '',       
    @LoaiDichVu NVARCHAR(100) = '' ,
	@PageNumber INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        dg.MaDV AS [Mã Dịch Vụ],
        dg.MaKH AS [Mã Khách Hàng],
        dg.DiemDV AS [Điểm Dịch Vụ],
        dg.DiemNV AS [Điểm Nhân Viên],
        dg.MucDoHaiLong AS [Hài Lòng],
        dg.BinhLuan AS [Bình Luận]
    FROM DANHGIA dg
    JOIN DICHVU dv ON dg.MaDV = dv.MaDichVu
    JOIN KHACHHANG kh ON dg.MaKH = kh.MaKH
    WHERE (@MaKH = '' OR kh.MaKH LIKE '%' + @MaKH + '%')
      AND (@LoaiDichVu = '' OR dv.TenDV LIKE '%' + @LoaiDichVu + '%')
    ORDER BY dg.MaDG DESC
	OFFSET (@PageNumber - 1) * @PageSize ROWS -- Phân trang tại đây
    FETCH NEXT @PageSize ROWS ONLY;
END
GO

EXEC sp_GetDanhGiaBacSi;


-----BÁN HÀNG-----

---BH3: Hàng Tồn Kho
CREATE OR ALTER PROCEDURE sp_GetHangTonKho
    @MaNV NVARCHAR(10),
    @LoaiSP NVARCHAR(20) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaCN NVARCHAR(10);
    SELECT @MaCN = ChiNhanhLamViec FROM NHANVIEN WHERE MaNV = @MaNV;

    SELECT 
        s.MaSP AS [Mã SP], 
        s.TenSP AS [Tên SP], 
        s.LoaiSP AS [Loại], 
        ISNULL(ct.GiaSPCN, s.GiaBan) AS [Giá Bán], 
        ISNULL(ct.SLTonKho, 0) AS [Số Lượng]
    FROM SANPHAM s
    LEFT JOIN CT_SPCN ct ON s.MaSP = ct.MaSP AND ct.MaCN = @MaCN
    WHERE (@LoaiSP = '' OR s.LoaiSP = @LoaiSP)
    ORDER BY s.MaSP;
END
GO

---BH: Bán hàng trực tiếp

CREATE TYPE dbo.TVP_SaleItems AS TABLE (
    MaSP NVARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL
);
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_LapHoaDonTaiQuay
    @MaKH NVARCHAR(10),
    @MaNV NVARCHAR(10),
    @MaCN NVARCHAR(10),
    @Items dbo.TVP_SaleItems READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @MaHD NVARCHAR(10) = 'HD' + FORMAT(GETDATE(), 'ssmmHHddM');
        DECLARE @MaLSDV NVARCHAR(10) = 'MH' + FORMAT(GETDATE(), 'ssmmHHddM');

        INSERT INTO LS_DV (MaLSDV, MaKH, MaDichVu, TrangThaiGD, MaCN, NgayDatTruoc)
        VALUES (@MaLSDV, @MaKH, N'DV003', N'Hoàn thành', @MaCN, GETDATE());

        INSERT INTO LS_DVMUAHANG (MaLSDVMH, HinhThucMH) 
        VALUES (@MaLSDV, N'Trực tiếp');

        INSERT INTO CT_MUAHANG (MaLSDVMH, MaSP, SoLuongSP, ThanhTienMH)
        SELECT @MaLSDV, it.MaSP, it.SoLuong, (it.SoLuong * ISNULL(ct.GiaSPCN, s.GiaBan))
        FROM @Items it
        JOIN SANPHAM s ON it.MaSP = s.MaSP
        LEFT JOIN CT_SPCN ct ON s.MaSP = ct.MaSP AND ct.MaCN = @MaCN;

        UPDATE ct
        SET ct.SLTonKho = ct.SLTonKho - it.SoLuong
        FROM CT_SPCN ct
        JOIN @Items it ON ct.MaSP = it.MaSP
        WHERE ct.MaCN = @MaCN;

        DECLARE @TongTien INT;
        SELECT @TongTien = SUM(ThanhTienMH) FROM CT_MUAHANG WHERE MaLSDVMH = @MaLSDV;

        INSERT INTO HOADON (MaHD, NgayLap, NV_Lap, TienTruocKM, TienThanhToan, HinhThucPay, TrangThaiHD, CongLoyalty, MaKH, MaCN)
        VALUES (@MaHD, CAST(GETDATE() AS DATE), @MaNV, @TongTien, @TongTien, N'Tiền mặt', N'Chờ thanh toán', 0, @MaKH, @MaCN);
        INSERT INTO CT_HOADON (MaHD, MaLSGD, TongPhiDV) 
        VALUES (@MaHD, @MaLSDV, @TongTien);

        COMMIT;
        SELECT @MaHD AS GeneratedMaHD;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW;
    END CATCH
END;
GO

---BH: Thanh toán HD

CREATE OR ALTER PROCEDURE sp_ThanhToanHoaDon
    @MaHD NVARCHAR(10),
    @HinhThucPay NVARCHAR(50),
    @SuDungLoyalty INT = 0 
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @MaKH NVARCHAR(10), @TienGoc INT, @TienGiam INT = 0;
        SELECT @MaKH = MaKH, @TienGoc = TienTruocKM FROM HOADON WHERE MaHD = @MaHD;
        DECLARE @CapDo NVARCHAR(20), @PhanTramGiam FLOAT = 0;
        SELECT @CapDo = CapDo FROM HOIVIEN WHERE MaKH = @MaKH;

        IF @CapDo = N'Cơ bản' SET @PhanTramGiam = 0.03;
        ELSE IF @CapDo = N'Thân thiết' SET @PhanTramGiam = 0.05;
        ELSE IF @CapDo = N'VIP' SET @PhanTramGiam = 0.1;

        SET @TienGiam = @TienGoc * @PhanTramGiam;

        IF @SuDungLoyalty > 0
        BEGIN
            DECLARE @DiemHienTai INT;
            SELECT @DiemHienTai = DiemLoyalty FROM HOIVIEN WHERE MaKH = @MaKH;
            IF @DiemHienTai >= @SuDungLoyalty
            BEGIN
                SET @TienGiam = @TienGiam + (@SuDungLoyalty * 1000);
                UPDATE HOIVIEN SET DiemLoyalty = DiemLoyalty - @SuDungLoyalty WHERE MaKH = @MaKH;

                INSERT INTO CT_KHUYENMAI (MaHD, MaKM, SoLuongDung, TienKM) 
                VALUES (@MaHD, 'LOYALTY', @SuDungLoyalty, @SuDungLoyalty * 1000);
            END
        END

        DECLARE @TienCuoi INT = @TienGoc - @TienGiam;
        DECLARE @DiemCong INT = FLOOR(@TienCuoi / 50000); -- Tích 1 điểm cho mỗi 50k

        UPDATE HOADON SET 
            TienThanhToan = @TienCuoi,
            HinhThucPay = @HinhThucPay,
            TrangThaiHD = N'Đã thanh toán',
            CongLoyalty = @DiemCong
        WHERE MaHD = @MaHD;

        UPDATE HOIVIEN SET DiemLoyalty = DiemLoyalty + @DiemCong WHERE MaKH = @MaKH;
        
        IF EXISTS (SELECT 1 FROM CHITIEUNAM WHERE MaKH = @MaKH AND Nam = YEAR(GETDATE()))
            UPDATE CHITIEUNAM SET ChiTieu = ChiTieu + @TienCuoi WHERE MaKH = @MaKH AND Nam = YEAR(GETDATE());
        ELSE
            INSERT INTO CHITIEUNAM (MaKH, Nam, ChiTieu) VALUES (@MaKH, YEAR(GETDATE()), @TienCuoi);

        COMMIT;
        SELECT @TienGiam AS TienGiam, @TienCuoi AS TienCuoi;
    END TRY
    BEGIN CATCH
        ROLLBACK; THROW;
    END CATCH
END;
GO

---BH: Quản lý đơn đặt hàng
---
CREATE OR ALTER PROCEDURE sp_GetDSDonHang_Paging
    @MaKH NVARCHAR(50) = NULL,
    @TrangThai NVARCHAR(50) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ls.MaLSDV, 
        ls.MaKH, 
        hd.TienThanhToan, 
        ls.TrangThaiGD, 
        mh.HinhThucMH
    FROM LS_DV ls
    JOIN LS_DVMUAHANG mh ON ls.MaLSDV = mh.MaLSDVMH
    LEFT JOIN CT_HOADON ct ON ls.MaLSDV = ct.MaLSGD
    LEFT JOIN HOADON hd ON ct.MaHD = hd.MaHD
    WHERE (@MaKH IS NULL OR ls.MaKH LIKE '%' + @MaKH + '%')
      AND (@TrangThai IS NULL OR @TrangThai = N'---Tất cả---' OR ls.TrangThaiGD = @TrangThai)
      AND mh.HinhThucMH <> N'Tại quầy' -- LOẠI BỎ ĐƠN TẠI QUẦY
    ORDER BY ls.MaLSDV DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO

CREATE OR ALTER PROCEDURE sp_XacNhanVaTruKho
    @MaLSDV NVARCHAR(10),
    @MaCN NVARCHAR(10)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            UPDATE LS_DV SET TrangThaiGD = N'Đã hoàn tất' WHERE MaLSDV = @MaLSDV;

            UPDATE HOADON SET TrangThaiHD = N'Đã hoàn tất'
            FROM HOADON hd
            JOIN CT_HOADON ct ON hd.MaHD = ct.MaHD
            WHERE ct.MaLSGD = @MaLSDV;

            UPDATE CT_SPCN
            SET SLTonKho = SLTonKho - ct.SoLuongSP
            FROM CT_SPCN spcn
            JOIN CT_MUAHANG ct ON spcn.MaSP = ct.MaSP
            WHERE ct.MaLSDVMH = @MaLSDV AND spcn.MaCN = @MaCN;

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- KH2: Quản lý thú cưng
CREATE PROCEDURE dbo.sp_KH_QLTC_GetDSTC
    @MaKH nvarchar(10)
AS
BEGIN
    SELECT 
        MaThuCung,
        TenThuCung, 
        LoaiThuCung, 
        Giong_TC, 
        NgaySinh_TC, 
        GioiTinh_TC,
        TinhTrangSK
    FROM THUCUNG
    WHERE MaKH = @MaKH
    ORDER BY MaThuCung
END
GO


CREATE PROC SP_KH_ThemThuCung
    @TenThuCung NVARCHAR(100),
    @LoaiThuCung NVARCHAR(100),
    @Giong_TC NVARCHAR(100),
    @NgaySinh_TC DATE,
    @GioiTinh_TC NVARCHAR(10),
    @TinhTrangSK NVARCHAR(255) = NULL,
    @MaKH NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

    DECLARE @NewMaThuCung NVARCHAR(10);
    DECLARE @MaxID INT;

    SELECT @MaxID = ISNULL(MAX(CAST(SUBSTRING(MaThuCung, 4, LEN(MaThuCung) - 3) AS INT)), 0)
    FROM THUCUNG

    SET @MaxID = @MaxID + 1;
    SET @NewMaThuCung = 'PET' + RIGHT('00000' + CAST(@MaxID AS NVARCHAR(10)), 5);

    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH)
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại.', 16, 1);
        RETURN;
    END

    IF @TenThuCung IS NULL OR LTRIM(@TenThuCung) = '' 
        OR @LoaiThuCung IS NULL OR LTRIM(@LoaiThuCung) = '' 
        OR @Giong_TC IS NULL OR LTRIM(@Giong_TC) = ''
    BEGIN
        RAISERROR(N'Tên, Loài, và Giống thú cưng không được để trống.', 16, 1);
        RETURN;
    END

    IF @NgaySinh_TC > GETDATE()
    BEGIN
        RAISERROR(N'Ngày sinh thú cưng không hợp lệ (Không thể lớn hơn ngày hiện tại).', 16, 1);
        RETURN;
    END

    IF @GioiTinh_TC NOT IN (N'Đực', N'Cái', N'Khác')
    BEGIN
        RAISERROR(N'Giới tính thú cưng phải là "Đực", "Cái", hoặc "Khác".', 16, 1);
        RETURN;
    END

    INSERT INTO THUCUNG (MaThuCung, TenThuCung, LoaiThuCung, Giong_TC, NgaySinh_TC, GioiTinh_TC, TinhTrangSK, MaKH)
    VALUES (@NewMaThuCung, @TenThuCung, @LoaiThuCung, @Giong_TC, @NgaySinh_TC, @GioiTinh_TC, @TinhTrangSK, @MaKH);

    SELECT @NewMaThuCung AS MaThuCungMoi;

END
GO

-- KH2: Sửa thú cưng

CREATE PROC SP_KH_SuaThongTinThuCung
    @MaThuCung NVARCHAR(10),
    @TenThuCung NVARCHAR(100),
    @LoaiThuCung NVARCHAR(100),
    @Giong_TC NVARCHAR(100),
    @NgaySinh_TC DATE,
    @GioiTinh_TC NVARCHAR(10),
    @TinhTrangSK NVARCHAR(255),
    @MaKH_ChuSoHuu NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

    IF NOT EXISTS (SELECT 1 FROM THUCUNG WHERE MaThuCung = @MaThuCung AND MaKH = @MaKH_ChuSoHuu)
    BEGIN
        RAISERROR(N'Mã thú cưng không tồn tại hoặc không thuộc sở hữu của khách hàng này.', 16, 1);
        RETURN;
    END

    IF @TenThuCung IS NULL OR @LoaiThuCung IS NULL OR @Giong_TC IS NULL
    BEGIN
        RAISERROR(N'Tên, Loài, và Giống thú cưng không được để trống.', 16, 1);
        RETURN;
    END

    IF @NgaySinh_TC > GETDATE()
    BEGIN
        RAISERROR(N'Ngày sinh thú cưng không hợp lệ (Không thể lớn hơn ngày hiện tại).', 16, 1);
        RETURN;
    END

    IF @GioiTinh_TC NOT IN (N'Đực', N'Cái', N'Khác')
    BEGIN
        RAISERROR(N'Giới tính thú cưng phải là "Đực", "Cái", hoặc "Khác".', 16, 1);
        RETURN;
    END

    UPDATE THUCUNG
    SET
        TenThuCung = @TenThuCung,
        LoaiThuCung = @LoaiThuCung,
        Giong_TC = @Giong_TC,
        NgaySinh_TC = @NgaySinh_TC,
        GioiTinh_TC = @GioiTinh_TC,
        TinhTrangSK = @TinhTrangSK
    WHERE
        MaThuCung = @MaThuCung AND MaKH = @MaKH_ChuSoHuu; 

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- KH2: Xóa thú cưng

CREATE PROC SP_KH_XoaThuCung
    @MaThuCung NVARCHAR(10),
    @MaKH_ChuSoHuu NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON
    DECLARE @ErrorMessage NVARCHAR(500);

    IF NOT EXISTS (SELECT 1 FROM THUCUNG WHERE MaThuCung = @MaThuCung AND MaKH = @MaKH_ChuSoHuu)
    BEGIN
        SET @ErrorMessage = N'Mã thú cưng không tồn tại hoặc không thuộc sở hữu của khách hàng này.';
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM LS_DVKHAMBENH WHERE MaThuCung = @MaThuCung) OR
       EXISTS (SELECT 1 FROM LS_DVTIEMPHONG WHERE MaThuCung = @MaThuCung)
    BEGIN
        SET @ErrorMessage = N'Không thể xóa thú cưng này vì đã có lịch sử dịch vụ (khám bệnh hoặc tiêm phòng).';
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END

    DELETE FROM THUCUNG
    WHERE 
        MaThuCung = @MaThuCung AND MaKH = @MaKH_ChuSoHuu; 
    IF @@ROWCOUNT = 0
    BEGIN
        SET @ErrorMessage = N'Lỗi: Không tìm thấy thú cưng để xóa, hoặc không có quyền sở hữu.';
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END

    SELECT 1 AS RowsAffected; 
	
END
GO

-- KH3: Xem lịch sử tiêm phòng

CREATE PROC SP_KH_XemLichSuTiemPhong
	@MaKH NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH)
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại.', 16, 1);
        RETURN;
    END

    SELECT
        LSTP.MaLSDVTP AS MaLSDV, 
        TC.TenThuCung,            
        LSTP.NgayTiem,
        VX.TenVacXin,             
        LSTP.LieuLuong,
        NV.HoTenNV AS BacSiPhuTrach, 
        LSTP.MaGoiTiem,
        LSTP.MaThuCung
    FROM 
        LS_DVTIEMPHONG LSTP
    JOIN 
        THUCUNG TC ON LSTP.MaThuCung = TC.MaThuCung
    JOIN 
        VACXIN VX ON LSTP.LoaiVacXin = VX.MaVacXin   
    JOIN 
        NHANVIEN NV ON LSTP.BacSiPhuTrach = NV.MaNV 
    WHERE 
        TC.MaKH = @MaKH 
    ORDER BY 
        LSTP.NgayTiem DESC;
END
GO

-- KH4: Xem lịch sử khám bệnh
IF OBJECT_ID('dbo.SP_KH_XemLichSuKhamBenh', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_XemLichSuKhamBenh
GO

CREATE PROC SP_KH_XemLichSuKhamBenh
    @MaKH NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

    -- 1. Kiểm tra MaKH có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH)
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại.', 16, 1);
        RETURN;
    END

    SELECT
        LSKB.MaLSDVKB AS MaLichSuDichVu,
        TC.TenThuCung AS ThuCung,
        LSKB.NgayKham AS NgayKham, 
        NV.HoTenNV AS BacSi,

        ISNULL(STUFF((
            SELECT N', ' + T.TrieuChung
            FROM TRIEUCHUNG T
            WHERE T.MaLSKB = LSKB.MaLSDVKB
            FOR XML PATH('')
        ), 1, 2, ''), N'') AS TrieuChung,

        ISNULL(STUFF((
            SELECT N', ' + CD.ChuanDoan
            FROM CHUANDOAN CD
            WHERE CD.MaLSKB = LSKB.MaLSDVKB
            FOR XML PATH('')
        ), 1, 2, ''), N'') AS ChuanDoan,

        LSKB.NgayHen, 
        CASE WHEN EXISTS (SELECT 1 FROM TOA_THUOC TT WHERE TT.MaLSDV = LSKB.MaLSDVKB) THEN 1 ELSE 0 END AS CoToaThuoc
    FROM 
        LS_DVKHAMBENH LSKB
    JOIN 
        THUCUNG TC ON LSKB.MaThuCung = TC.MaThuCung  
    JOIN 
        NHANVIEN NV ON LSKB.BacSiPhuTrach = NV.MaNV  
    WHERE 
        TC.MaKH = @MaKH  
    ORDER BY 
        LSKB.NgayKham DESC;

END
GO

-- KH4: Xem toa thuốc
IF OBJECT_ID('dbo.SP_KH_XemToaThuoc', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_XemToaThuoc
GO

CREATE PROC SP_KH_XemToaThuoc
	@MaLSDVKB NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

    IF NOT EXISTS (SELECT 1 FROM LS_DVKHAMBENH WHERE MaLSDVKB = @MaLSDVKB)
    BEGIN
        RAISERROR(N'Mã lịch sử khám bệnh không tồn tại.', 16, 1);
        RETURN;
    END

    SELECT
        TT.MaThuoc,
        TT.SoLuongThuoc,
        T.DonViTinh,
        TT.LieuDung,
        TT.ThanhTien 
    FROM 
        TOA_THUOC TT
    JOIN
        THUOC T ON TT.MaThuoc = T.MaThuoc
    WHERE 
        TT.MaLSDV = @MaLSDVKB 
    ORDER BY 
        T.MaThuoc ASC;
END
GO

-- KH5: Đặt dịch vụ

IF OBJECT_ID('dbo.SP_KH_KiemTraCungCap', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_KiemTraCungCap
GO

CREATE PROC SP_KH_KiemTraCungCap
    @MaCN NVARCHAR(10),
    @TenDV NVARCHAR(100)
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM CHINHANH_DV CDV 
        JOIN DICHVU DV ON CDV.MaDichVu = DV.MaDichVu
        WHERE CDV.MaCN = @MaCN AND DV.TenDV = @TenDV AND CDV.TrangThaiDV = N'Có cung cấp'
    )
        SELECT 1 AS Result;
    ELSE
        SELECT 0 AS Result;
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.SP_KH_DatDichVu
    @MaKhachHang NVARCHAR(10), 
	@MaChiNhanh NVARCHAR(10), 
	@TenDichVu NVARCHAR(100),       
    @MaThuCung NVARCHAR(10), 
	@ThoiGianHen DATE, 
	@MaVacXin NVARCHAR(8) = NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @MaDichVu NVARCHAR(10);
    DECLARE @NewMaLSDV NVARCHAR(10);    
    DECLARE @NgayDatLich DATE = CAST(GETDATE() AS DATE);

    IF @MaThuCung IS NULL OR @ThoiGianHen IS NULL BEGIN RAISERROR(N'Thiếu thông tin.', 16, 1); RETURN; END
    IF @ThoiGianHen < @NgayDatLich BEGIN RAISERROR(N'Ngày hẹn không hợp lệ.', 16, 1); RETURN; END

    SELECT @MaDichVu = DV.MaDichVu FROM DICHVU DV
    JOIN CHINHANH_DV CDV ON DV.MaDichVu = CDV.MaDichVu
    WHERE DV.TenDV = @TenDichVu AND CDV.MaCN = @MaChiNhanh AND CDV.TrangThaiDV IN (N'Có cung cấp', N'Đang hoạt động');

    IF @MaDichVu IS NULL BEGIN RAISERROR(N'Dịch vụ không khả dụng.', 16, 1); RETURN; END

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @NextID INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaLSDV, 5, 6) AS INT)), 0) + 1 FROM LS_DV);
        SET @NewMaLSDV = 'LSDV' + RIGHT('000000' + CAST(@NextID AS NVARCHAR), 6);
        
        INSERT INTO LS_DV (MaLSDV, MaKH, MaDichVu, TrangThaiGD, NgayDatTruoc, MaCN)
        VALUES (@NewMaLSDV, @MaKhachHang, @MaDichVu, N'Đã đặt trước', @NgayDatLich, @MaChiNhanh);

        IF @TenDichVu = N'Khám bệnh'
            INSERT INTO LS_DVKHAMBENH (MaLSDVKB, BacSiPhuTrach, NgayHen, MaThuCung, NgayKham, ThoiGianSD)
            VALUES (@NewMaLSDV, NULL, @ThoiGianHen, @MaThuCung, @NgayDatLich, @NgayDatLich);
        ELSE IF @TenDichVu = N'Tiêm phòng'
        BEGIN
            IF @MaVacXin IS NULL OR NOT EXISTS (SELECT 1 FROM VACXIN WHERE MaVacXin = @MaVacXin) THROW 50000, N'Vắc xin sai.', 1;
            INSERT INTO LS_DVTIEMPHONG (MaLSDVTP, BacSiPhuTrach, MaGoiTiem, LoaiVacXin, LieuLuong, NgayTiem, MaThuCung, ThoiGianSD)
            VALUES (@NewMaLSDV, NULL, NULL, @MaVacXin, N'Chờ khám lẻ', @ThoiGianHen, @MaThuCung, @ThoiGianHen);
        END
        COMMIT TRANSACTION;
        SELECT @NewMaLSDV AS MaLSDVCreated;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SP_GetDanhSachVacXin', 'P') IS NOT NULL
	DROP PROC dbo.SP_GetDanhSachVacXin
GO

CREATE PROCEDURE SP_GetDanhSachVacXin
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MaVacXin, 
        TenVacXin, 
        GiaTien,
        SoMuiTonKho 
    FROM VACXIN 
    WHERE SoMuiTonKho > 0 
    ORDER BY TenVacXin ASC;
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_GetDanhSachHoSoKB
    @MaTC NVARCHAR(10) = NULL,
	@PageNumber INT = 1,      
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kb.MaThuCung, 
        tc.MaKH, 
        kb.NgayKham, 
        tr.TrieuChung, 
        cd.ChuanDoan,
        kb.NgayHen, 
        tc.LoaiThuCung, 
        tc.NgaySinh_TC,
        kb.BacSiPhuTrach
    FROM LS_DVKHAMBENH kb
    JOIN THUCUNG tc ON kb.MaThuCung = tc.MaThuCung
    LEFT JOIN TRIEUCHUNG tr ON kb.MaLSDVKB = tr.MaLSKB
    LEFT JOIN CHUANDOAN cd ON kb.MaLSDVKB = cd.MaLSKB
    WHERE (@MaTC IS NULL OR kb.MaThuCung LIKE '%' + @MaTC + '%')
    ORDER BY kb.NgayKham DESC

	OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

END
GO

-- KH6: Đặt gói tiêm

IF OBJECT_ID('dbo.SP_KH_GetDSGoiTiemChiTiet', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_GetDSGoiTiemChiTiet
GO

CREATE PROCEDURE SP_KH_GetDSGoiTiemChiTiet
AS
BEGIN
    SELECT 
        DISTINCT G.MaGoiTiem AS MaGT, 
        G.TenGoi AS TenGT, 
        G.SoThang, 
        V.TenVacXin AS Vacxin, 
        ND.SoMui, 
        G.UuDai,
        CAST((V.GiaTien * ND.SoMui) * (1 - G.UuDai/100) AS INT) AS GiaTien
    FROM GOITIEM G
    INNER JOIN ND_GOITIEM ND ON G.MaGoiTiem = ND.MaGoiTiem
    INNER JOIN VACXIN V ON ND.MaVacXin = V.MaVacXin;
END;
GO

IF OBJECT_ID('dbo.SP_KH_DatGoiTiemPhong', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_DatGoiTiemPhong
GO

CREATE PROC dbo.SP_KH_DatGoiTiemPhong
    @MaKH NVARCHAR(10), @MaGoiDuocChon NVARCHAR(8), @MaThuCung NVARCHAR(10),        
    @MaChiNhanh NVARCHAR(10), @MaVacXinDuocChon NVARCHAR(8), @NgayHenTiem DATE              
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(500);
    DECLARE @NgayDatLich DATE = CAST(GETDATE() AS DATE);
    DECLARE @MaDichVu_TP NVARCHAR(10);
    DECLARE @NewMaLSDV NVARCHAR(10);
    
    BEGIN TRANSACTION;
    SELECT @MaDichVu_TP = MaDichVu FROM DICHVU WHERE TenDV = N'Tiêm phòng';
  
    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH) BEGIN SET @ErrorMessage = N'KH không tồn tại.'; GOTO ErrorHandler; END

    INSERT INTO LS_DANGKY (MaGoiTiem, MaKH, NgayDangKy) VALUES (@MaGoiDuocChon, @MaKH, @NgayDatLich);

    SELECT @NewMaLSDV = 'LSDV' + RIGHT('000000' + CAST(ISNULL((SELECT MAX(CAST(SUBSTRING(MaLSDV, 5, 6) AS INT)) FROM LS_DV WHERE MaLSDV LIKE 'LSDV%'), 0) + 1 AS NVARCHAR(6)), 6);

    INSERT INTO LS_DV (MaLSDV, MaKH, MaDichVu, TrangThaiGD, NgayDatTruoc, MaCN)
    VALUES (@NewMaLSDV, @MaKH, @MaDichVu_TP, N'Đã đặt trước', @NgayDatLich, @MaChiNhanh);

    INSERT INTO LS_DVTIEMPHONG (MaLSDVTP, BacSiPhuTrach, MaGoiTiem, LoaiVacXin, LieuLuong, NgayTiem, MaThuCung)
    VALUES (@NewMaLSDV, NULL, @MaGoiDuocChon, @MaVacXinDuocChon, N'Chờ khám', @NgayHenTiem, @MaThuCung);
    
    COMMIT TRANSACTION;
    SELECT N'Đặt thành công. Mã: ' + @NewMaLSDV AS ThongBao;
    RETURN;

    ErrorHandler:
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
END
GO

IF OBJECT_ID('dbo.SP_KH_GetVacXinTheoGoi', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_GetVacXinTheoGoi
GO
CREATE PROCEDURE SP_KH_GetVacXinTheoGoi
    @MaGoiTiem NVARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.MaVacXin, 
        v.TenVacXin 
    FROM ND_GOITIEM nd
    INNER JOIN VACXIN v ON nd.MaVacXin = v.MaVacXin
    WHERE nd.MaGoiTiem = @MaGoiTiem;
END
GO

-- KH6: Tạo gói tiêm mới
IF OBJECT_ID('dbo.SP_TaoGoiTiemMoi', 'P') IS NOT NULL
	DROP PROC dbo.SP_TaoGoiTiemMoi
GO

IF EXISTS (SELECT 1 FROM sys.types WHERE name = 'UDT_NoiDungGoiTiem' AND is_table_type = 1)
    DROP TYPE dbo.UDT_NoiDungGoiTiem;
GO

CREATE TYPE dbo.UDT_NoiDungGoiTiem AS TABLE
(
    MaVacXin NVARCHAR(8) NOT NULL,
    SoMui INT NOT NULL
);

GO

CREATE PROC SP_TaoGoiTiemMoi
    @TenGoiTiem NVARCHAR(100),
    @SoThang INT,
    @NoiDungGoi AS dbo.UDT_NoiDungGoiTiem READONLY 
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewMaGoi NVARCHAR(8), @ErrorMessage NVARCHAR(500);
    DECLARE @TongChiPhi DECIMAL(10,2), @UuDai_PhanTram FLOAT, @TongTien DECIMAL(10,2);
    
    BEGIN TRANSACTION;
    BEGIN TRY
        IF @TenGoiTiem IS NULL OR @SoThang <= 0
        BEGIN
            RAISERROR(N'Tên gói hoặc số tháng không hợp lệ.', 16, 1);
        END

        SELECT @TongChiPhi = ISNULL(SUM(ND.SoMui * VX.GiaTien), 0)
        FROM @NoiDungGoi ND JOIN VACXIN VX ON ND.MaVacXin = VX.MaVacXin;

        SET @UuDai_PhanTram = FLOOR(RAND()*(15-5+1)+5);
        SET @TongTien = @TongChiPhi * (1 - @UuDai_PhanTram / 100.0);

        DECLARE @NextID INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaGoiTiem, 3, 3) AS INT)), 0) + 1 FROM GOITIEM);
        SET @NewMaGoi = 'GT' + RIGHT('000' + CAST(@NextID AS NVARCHAR), 3);

        INSERT INTO GOITIEM (MaGoiTiem, TenGoi, SoThang, UuDai)
        VALUES (@NewMaGoi, @TenGoiTiem, @SoThang, @UuDai_PhanTram);

        INSERT INTO ND_GOITIEM (MaGoiTiem, MaVacXin, SoMui)
        SELECT @NewMaGoi, MaVacXin, SoMui FROM @NoiDungGoi;

        COMMIT TRANSACTION;

        SELECT 'Success' AS Status, 
           @NewMaGoi AS MaGoiMoi, 
           @TongChiPhi AS ChiPhiBanDau,
           @UuDai_PhanTram AS UuDai,
           @TongTien AS TongTien;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Msg NVARCHAR(4000) = ERROR_MESSAGE();
        SELECT 'Error' AS Status, @Msg AS Message;
    END CATCH
END
GO

-- KH7: Xem và lọc sản phẩm
IF OBJECT_ID('dbo.SP_KH_XemVaLocSanPham', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_XemVaLocSanPham
GO

CREATE PROC SP_KH_XemVaLocSanPham
    @MaCN_Filter NVARCHAR(10),
    @MaSP_Filter NVARCHAR(10) = NULL,
    @TenSP_Filter NVARCHAR(100) = NULL,
    @LoaiSP_Filter NVARCHAR(50) = NULL,
    @GiaMin_Filter DECIMAL(18, 2) = NULL,
    @GiaMax_Filter DECIMAL(18, 2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        SP.MaSP,
        SP.TenSP,
        SP.LoaiSP,
        CN.GiaSPCN AS GiaBan, 
        CN.SLTonKho
    FROM SANPHAM SP
    INNER JOIN CT_SPCN CN ON SP.MaSP = CN.MaSP
    WHERE CN.MaCN = @MaCN_Filter
        AND (@MaSP_Filter IS NULL OR SP.MaSP LIKE '%' + @MaSP_Filter + '%')
        AND (@TenSP_Filter IS NULL OR SP.TenSP LIKE N'%' + @TenSP_Filter + N'%')
        AND (@LoaiSP_Filter IS NULL OR SP.LoaiSP = @LoaiSP_Filter)
        AND (@GiaMin_Filter IS NULL OR CN.GiaSPCN >= @GiaMin_Filter)
        AND (@GiaMax_Filter IS NULL OR CN.GiaSPCN <= @GiaMax_Filter)
    ORDER BY SP.TenSP;
END
GO

-- KH7: ThemSPVaoGioHang
IF OBJECT_ID('dbo.SP_KH_TimKiemThongMinh', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_TimKiemThongMinh
GO

CREATE PROC SP_KH_TimKiemThongMinh
    @MaCN NVARCHAR(10),
    @SearchValue NVARCHAR(100) = NULL 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        SP.MaSP,
        SP.TenSP,
        SP.LoaiSP,
        CN.GiaSPCN AS GiaBan, 
        CN.SLTonKho
    FROM SANPHAM SP
    INNER JOIN CT_SPCN CN ON SP.MaSP = CN.MaSP
    WHERE CN.MaCN = @MaCN
      AND (
          @SearchValue IS NULL OR @SearchValue = '' 
          OR SP.MaSP LIKE '%' + @SearchValue + '%'   -- So sánh với Mã
          OR SP.TenSP LIKE N'%' + @SearchValue + N'%' -- So sánh với Tên
          OR SP.LoaiSP LIKE N'%' + @SearchValue + N'%' -- So sánh với Loại
      )
    ORDER BY SP.TenSP;
END
GO

IF OBJECT_ID('dbo.SP_KH_ThemSPVaoGioHang', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_ThemSPVaoGioHang
GO

USE PetCareDBOpt
GO

CREATE OR ALTER PROCEDURE SP_KH_ThemSPVaoGioHang
    @MaKH NVARCHAR(10),
    @MaSP NVARCHAR(10),
    @MaCN_SuDung NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @MaLSDV_GioHang NVARCHAR(10), @GiaBan DECIMAL(18,2), @SLTonKho INT;
    DECLARE @MaDV_MuaHang NVARCHAR(10) = 'DV003'; -- Giả sử DV003 là Mua hàng

    -- 1. Kiểm tra tồn kho
    SELECT @GiaBan = GiaSPCN, @SLTonKho = SLTonKho 
    FROM CT_SPCN WHERE MaSP = @MaSP AND MaCN = @MaCN_SuDung;

    -- 2. Kiểm tra xem khách đã có Giỏ hàng chưa
    SELECT @MaLSDV_GioHang = MaLSDV 
    FROM LS_DV 
    WHERE MaKH = @MaKH AND MaDichVu = @MaDV_MuaHang AND TrangThaiGD = N'Giỏ hàng';

    BEGIN TRANSACTION;
    BEGIN TRY
        -- 3. Nếu chưa có giỏ hàng, tạo mới
        IF @MaLSDV_GioHang IS NULL
        BEGIN
            DECLARE @NextID INT;

            -- [SỬA LỖI TẠI ĐÂY] ---------------------------------------------
            -- Chỉ lấy MAX của các mã bắt đầu bằng 'MH', bỏ qua 'LSDV'
            SELECT @NextID = ISNULL(MAX(CAST(SUBSTRING(MaLSDV, 3, 7) AS INT)), 0) + 1
            FROM LS_DV
            WHERE MaLSDV LIKE 'MH[0-9]%'; -- Chỉ lọc các mã MH chuẩn
            ------------------------------------------------------------------
            
            -- Tạo mã dạng MH0000001
            SET @MaLSDV_GioHang = 'MH' + RIGHT('0000000' + CAST(@NextID AS NVARCHAR), 7);

            INSERT INTO LS_DV (MaLSDV, MaKH, MaDichVu, TrangThaiGD, NgayDatTruoc, MaCN)
            VALUES (@MaLSDV_GioHang, @MaKH, @MaDV_MuaHang, N'Giỏ hàng', GETDATE(), @MaCN_SuDung);

            INSERT INTO LS_DVMUAHANG (MaLSDVMH, HinhThucMH)
            VALUES (@MaLSDV_GioHang, N'Trực tuyến');
        END

        -- 4. Kiểm tra tồn kho
        IF @SLTonKho <= 0
        BEGIN
            RAISERROR(N'Sản phẩm này hiện đã hết hàng tại chi nhánh.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 5. Thêm sản phẩm vào chi tiết
        IF EXISTS (SELECT 1 FROM CT_MUAHANG WHERE MaLSDVMH = @MaLSDV_GioHang AND MaSP = @MaSP)
            UPDATE CT_MUAHANG 
            SET SoLuongSP = SoLuongSP + 1, 
                ThanhTienMH = (SoLuongSP + 1) * @GiaBan
            WHERE MaLSDVMH = @MaLSDV_GioHang AND MaSP = @MaSP;
        ELSE
            INSERT INTO CT_MUAHANG (MaLSDVMH, MaSP, SoLuongSP, ThanhTienMH)
            VALUES (@MaLSDV_GioHang, @MaSP, 1, @GiaBan);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

-- KH7: Cập nhật và xóa sản phẩm trong giỏ hàng
IF OBJECT_ID('dbo.SP_KH_LayChiTietGioHang', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_LayChiTietGioHang
GO

CREATE PROC SP_KH_LayChiTietGioHang
    @MaKH NVARCHAR(10),
    @MaCN NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        CT.MaSP,
        SP.TenSP AS [SanPham],
        CASE 
            WHEN @MaCN IS NOT NULL THEN ISNULL(CN.GiaSPCN, SP.GiaBan)
            ELSE SP.GiaBan 
        END AS [DonGia], 
        CT.SoLuongSP AS [SoLuong],
        CT.ThanhTienMH AS [ThanhTien],
        L.MaLSDV AS [MaLSDV_GioHang]
    FROM LS_DV L
    JOIN CT_MUAHANG CT ON L.MaLSDV = CT.MaLSDVMH
    JOIN SANPHAM SP ON CT.MaSP = SP.MaSP
    LEFT JOIN CT_SPCN CN ON SP.MaSP = CN.MaSP AND CN.MaCN = @MaCN
    WHERE L.MaKH = @MaKH 
      AND L.TrangThaiGD = N'Giỏ hàng';
END
GO

IF OBJECT_ID('dbo.SP_KH_CapNhatGioHang', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_CapNhatGioHang
GO

CREATE PROC SP_KH_CapNhatGioHang
    @MaLSDV_GioHang NVARCHAR(10),
    @MaSP NVARCHAR(10),
    @SoLuongMoi INT, 
    @MaCN_SuDung NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @GiaBan DECIMAL(10, 2), @SLTonKho INT;

    IF @SoLuongMoi < 0
    BEGIN
        RAISERROR(N'Số lượng không thể là số âm.', 16, 1);
        RETURN;
    END

    SELECT 
        @GiaBan = ISNULL(CN.GiaSPCN, SP.GiaBan),
        @SLTonKho = CN.SLTonKho
    FROM SANPHAM SP
    INNER JOIN CT_SPCN CN ON SP.MaSP = CN.MaSP
    WHERE CN.MaSP = @MaSP AND CN.MaCN = @MaCN_SuDung;

    IF @GiaBan IS NULL
    BEGIN
        RAISERROR(N'Sản phẩm không tồn tại hoặc không được bán tại chi nhánh này.', 16, 1);
        RETURN;
    END

    IF @SoLuongMoi > 0 AND @SoLuongMoi > @SLTonKho
    BEGIN
        RAISERROR(N'Số lượng yêu cầu vượt quá tồn kho hiện có tại chi nhánh.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM CT_MUAHANG WHERE MaLSDVMH = @MaLSDV_GioHang AND MaSP = @MaSP)
        BEGIN
            RAISERROR(N'Sản phẩm không có trong giỏ hàng hiện tại.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @SoLuongMoi = 0
        BEGIN
            DELETE FROM CT_MUAHANG 
            WHERE MaLSDVMH = @MaLSDV_GioHang AND MaSP = @MaSP;
        END
        ELSE
        BEGIN
            UPDATE CT_MUAHANG 
            SET SoLuongSP = @SoLuongMoi, 
                ThanhTienMH = @SoLuongMoi * @GiaBan
            WHERE MaLSDVMH = @MaLSDV_GioHang AND MaSP = @MaSP;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

-- KH8: Xem Dịch vụ đã đặt
IF OBJECT_ID('dbo.SP_XemDichVuDaDat', 'P') IS NOT NULL
	DROP PROC dbo.SP_XemDichVuDaDat
GO

CREATE PROCEDURE SP_XemDichVuDaDat
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH)
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại.', 16, 1);
        RETURN;
    END

    SELECT 
		L.NgayDatTruoc AS Ngay,
        L.MaLSDV AS MaLSDV,
        L.MaDichVu AS MaDV,
        D.TenDV AS TenDV,
        L.TrangThaiGD AS TrangThai
    FROM 
        LS_DV L
    INNER JOIN 
        DICHVU D ON L.MaDichVu = D.MaDichVu
    WHERE 
        L.MaKH = @MaKH
        AND L.TrangThaiGD = N'Đã đặt trước'
    ORDER BY 
        L.NgayDatTruoc ASC;
END
GO

-- KH8: Hủy Dịch vụ đã đặt

IF OBJECT_ID('dbo.SP_HuyDichVuDaDat', 'P') IS NOT NULL
	DROP PROC dbo.SP_HuyDichVuDaDat
GO

CREATE PROCEDURE SP_HuyDichVuDaDat
    @MaLSDV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CurrentStatus NVARCHAR(50);
    
    SELECT @CurrentStatus = TrangThaiGD FROM LS_DV WHERE MaLSDV = @MaLSDV;

    IF @CurrentStatus IS NULL
    BEGIN
        RAISERROR(N'Mã dịch vụ không tồn tại trong hệ thống.', 16, 1);
        RETURN;
    END

    IF @CurrentStatus <> N'Đã đặt trước'
    BEGIN
        DECLARE @Msg NVARCHAR(200) = N'Dịch vụ đang ở trạng thái "' + @CurrentStatus + N'", không thể hủy.';
        RAISERROR(@Msg, 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM CT_HOADON WHERE MaLSGD = @MaLSDV)
    BEGIN
        RAISERROR(N'Dịch vụ này đã được xuất hóa đơn, không thể thực hiện hủy.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY

        IF EXISTS (SELECT 1 FROM LS_DVKHAMBENH WHERE MaLSDVKB = @MaLSDV)
        BEGIN
            DELETE FROM TRIEUCHUNG WHERE MaLSKB = @MaLSDV;
            DELETE FROM CHUANDOAN WHERE MaLSKB = @MaLSDV;
            DELETE FROM TOA_THUOC WHERE MaLSDV = @MaLSDV;
            DELETE FROM LS_DVKHAMBENH WHERE MaLSDVKB = @MaLSDV;
        END
        
        ELSE IF EXISTS (SELECT 1 FROM LS_DVTIEMPHONG WHERE MaLSDVTP = @MaLSDV)
        BEGIN
            DELETE FROM LS_DVTIEMPHONG WHERE MaLSDVTP = @MaLSDV;
        END
        
        ELSE IF EXISTS (SELECT 1 FROM LS_DVMUAHANG WHERE MaLSDVMH = @MaLSDV)
        BEGIN
            DELETE FROM CT_MUAHANG WHERE MaLSDVMH = @MaLSDV;
            DELETE FROM LS_DVMUAHANG WHERE MaLSDVMH = @MaLSDV;
        END

        DELETE FROM LS_DV WHERE MaLSDV = @MaLSDV;

        COMMIT TRANSACTION;
        SELECT N'Hủy dịch vụ thành công.' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @Err NVARCHAR(MAX) = ERROR_MESSAGE();
        RAISERROR(@Err, 16, 1);
    END CATCH
END
GO

-- KH9: Thanh toán hóa đơn

IF OBJECT_ID('dbo.SP_KH_LayDichVuChoThanhToan', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_LayDichVuChoThanhToan
GO

CREATE OR ALTER PROCEDURE SP_KH_LayDichVuChoThanhToan
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        L.MaLSDV AS MaLSDV,
        ISNULL(L.NgayDatTruoc, GETDATE()) AS NgayLap,
        
        CASE 
            WHEN HD.MaHD IS NOT NULL THEN HD.TienTruocKM 
            ELSE D.GiaTienDV 
        END AS TongTien,

        D.TenDV AS LoaiDichVu
    FROM LS_DV L
    INNER JOIN DICHVU D ON L.MaDichVu = D.MaDichVu
    LEFT JOIN CT_HOADON CTHD ON L.MaLSDV = CTHD.MaLSGD
    LEFT JOIN HOADON HD ON CTHD.MaHD = HD.MaHD
    WHERE L.MaKH = @MaKH
      AND L.TrangThaiGD = N'Chờ thanh toán'
      AND (
          CTHD.MaLSGD IS NULL 
          OR
          HD.TrangThaiHD = N'Chờ thanh toán' 
      )
    ORDER BY L.MaLSDV DESC;
END
GO

SELECT MaLSDV, TrangThaiGD FROM LS_DV WHERE MaKH = 'KH00001'

EXEC SP_KH_LayDichVuChoThanhToan @MaKH = 'KH00001'
GO

USE PetCareDBOpt
GO

CREATE OR ALTER PROCEDURE dbo.SP_KH_ThanhToanHoaDon
    @MaKH NVARCHAR(10),
    @MaLSGD NVARCHAR(10),     
    @HinhThucPay NVARCHAR(50),
    @SoDiemLoyaltyDung INT = 0,
    @MaCN NVARCHAR(10) = NULL, 
    @NV_Lap NVARCHAR(10) = NULL,
    @MaKM NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaHD NVARCHAR(10);
    DECLARE @TienTruocKM DECIMAL(18,0) = 0;
    DECLARE @TienKM_Loyalty DECIMAL(18,0) = 0, @TienKM_Voucher DECIMAL(18,0) = 0;
    DECLARE @TienThanhToan DECIMAL(18,0), @CongLoyalty INT;
    DECLARE @LoaiKM_Voucher NVARCHAR(20), @GiaTriKM_Voucher INT;
    DECLARE @LoaiKH NVARCHAR(20), @NamHienTai INT = YEAR(GETDATE());
    
    DECLARE @IsExistingInvoice BIT = 0;

    BEGIN TRANSACTION;
    BEGIN TRY
        SELECT TOP 1 
            @MaHD = HD.MaHD, 
            @IsExistingInvoice = 1, 
            @MaCN = HD.MaCN 
        FROM CT_HOADON CT
        INNER JOIN HOADON HD ON CT.MaHD = HD.MaHD
        WHERE CT.MaLSGD = @MaLSGD 
          AND HD.TrangThaiHD = N'Chờ thanh toán';

        IF @MaCN IS NULL SET @MaCN = 'CN01'; 

        SELECT @LoaiKH = Loai_KH FROM dbo.KHACHHANG WHERE MaKH = @MaKH;

        SELECT @TienTruocKM = ISNULL(SUM(ThanhTienMH), 0) FROM dbo.CT_MUAHANG WHERE MaLSDVMH = @MaLSGD;
        IF @TienTruocKM = 0 
        BEGIN
             SELECT @TienTruocKM = D.GiaTienDV 
             FROM LS_DV L JOIN DICHVU D ON L.MaDichVu = D.MaDichVu 
             WHERE L.MaLSDV = @MaLSGD;
        END

        IF @MaKM IS NOT NULL AND @MaKM <> 'KM004'
        BEGIN
            SELECT @LoaiKM_Voucher = RTRIM(LoaiKM), @GiaTriKM_Voucher = GiaKM FROM dbo.KHUYENMAI WHERE MaKM = @MaKM;
            IF @LoaiKM_Voucher LIKE 'HV%' OR @LoaiKM_Voucher LIKE '%%'
                SET @TienKM_Voucher = CAST((@TienTruocKM * @GiaTriKM_Voucher) / 100.0 AS INT);
            ELSE
                SET @TienKM_Voucher = @GiaTriKM_Voucher;
        END

        IF @SoDiemLoyaltyDung > 0
        BEGIN
            IF @LoaiKH <> N'Hội viên' THROW 50000, N'Chỉ Hội viên mới được sử dụng điểm!', 1;
            IF (SELECT DiemLoyalty FROM dbo.HOIVIEN WHERE MaKH = @MaKH) < @SoDiemLoyaltyDung
                THROW 50000, N'Số dư điểm Loyalty không đủ!', 1;
            
            SET @TienKM_Loyalty = @SoDiemLoyaltyDung * 1000;
            IF @TienKM_Loyalty > (@TienTruocKM - @TienKM_Voucher) 
                SET @TienKM_Loyalty = (@TienTruocKM - @TienKM_Voucher);
        END

        SET @TienThanhToan = @TienTruocKM - @TienKM_Voucher - @TienKM_Loyalty;
        IF @TienThanhToan < 0 SET @TienThanhToan = 0;
        SET @CongLoyalty = CAST(@TienThanhToan / 50000 AS INT);

        IF @IsExistingInvoice = 1
        BEGIN
            UPDATE dbo.HOADON
            SET NgayLap = GETDATE(),
                TienThanhToan = @TienThanhToan,
                HinhThucPay = @HinhThucPay,
                TrangThaiHD = N'Đã thanh toán',
                CongLoyalty = @CongLoyalty,
                TienTruocKM = @TienTruocKM 
            WHERE MaHD = @MaHD;
            
            DELETE FROM dbo.CT_KHUYENMAI WHERE MaHD = @MaHD;
        END
        ELSE
        BEGIN
            DECLARE @NextID INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaHD, PATINDEX('%[0-9]%', MaHD), LEN(MaHD)) AS INT)), 0) + 1 FROM dbo.HOADON);
            SET @MaHD = 'HD' + RIGHT('00000000' + CAST(@NextID AS NVARCHAR), 8);

            INSERT INTO dbo.HOADON (MaHD, NgayLap, NV_Lap, TienTruocKM, TienThanhToan, HinhThucPay, TrangThaiHD, CongLoyalty, MaKH, MaCN)
            VALUES (@MaHD, GETDATE(), @NV_Lap, @TienTruocKM, @TienThanhToan, @HinhThucPay, N'Đã thanh toán', @CongLoyalty, @MaKH, @MaCN);
            
            INSERT INTO dbo.CT_HOADON (MaHD, MaLSGD, TongPhiDV) VALUES (@MaHD, @MaLSGD, @TienTruocKM);
        END
        IF @MaKM IS NOT NULL AND @MaKM <> 'KM004'
            INSERT INTO dbo.CT_KHUYENMAI (MaHD, MaKM, SoLuongDung, TienKM) VALUES (@MaHD, @MaKM, 1, @TienKM_Voucher);

        IF @SoDiemLoyaltyDung > 0
            INSERT INTO dbo.CT_KHUYENMAI (MaHD, MaKM, SoLuongDung, TienKM) 
            VALUES (@MaHD, N'KM004', @SoDiemLoyaltyDung, @TienKM_Loyalty);

        IF EXISTS (SELECT 1 FROM dbo.CT_MUAHANG WHERE MaLSDVMH = @MaLSGD)
        BEGIN
            UPDATE dbo.CT_SPCN SET SLTonKho = SLTonKho - CT.SoLuongSP
            FROM dbo.CT_SPCN INNER JOIN dbo.CT_MUAHANG CT ON dbo.CT_SPCN.MaSP = CT.MaSP
            WHERE CT.MaLSDVMH = @MaLSGD AND dbo.CT_SPCN.MaCN = @MaCN;
        END

        IF @LoaiKH = N'Hội viên'
        BEGIN
            UPDATE dbo.HOIVIEN SET DiemLoyalty = DiemLoyalty - @SoDiemLoyaltyDung + @CongLoyalty WHERE MaKH = @MaKH;

            IF EXISTS (SELECT 1 FROM dbo.CHITIEUNAM WHERE MaKH = @MaKH AND Nam = @NamHienTai)
                UPDATE dbo.CHITIEUNAM SET ChiTieu = ChiTieu + @TienThanhToan WHERE MaKH = @MaKH AND Nam = @NamHienTai;
            ELSE
                INSERT INTO dbo.CHITIEUNAM (MaKH, Nam, ChiTieu) VALUES (@MaKH, @NamHienTai, @TienThanhToan);

            DECLARE @TongChiTieuNam DECIMAL(18,2);
            SELECT @TongChiTieuNam = ChiTieu FROM dbo.CHITIEUNAM WHERE MaKH = @MaKH AND Nam = @NamHienTai;

            IF @TongChiTieuNam >= 12000000 UPDATE dbo.HOIVIEN SET CapDo = N'VIP' WHERE MaKH = @MaKH;
            ELSE IF @TongChiTieuNam >= 5000000 UPDATE dbo.HOIVIEN SET CapDo = N'Thân thiết' WHERE MaKH = @MaKH AND CapDo = N'Cơ bản';
        END

        UPDATE dbo.LS_DV SET TrangThaiGD = N'Hoàn thành' WHERE MaLSDV = @MaLSGD;

        COMMIT TRANSACTION;
        SELECT @MaHD AS MaHoaDonMoi;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO


-- KH10: Xem Lịch sử hóa đơn
IF OBJECT_ID('dbo.SP_LS_HoaDon', 'P') IS NOT NULL
	DROP PROC dbo.SP_LS_HoaDon
GO

CREATE PROCEDURE SP_LS_HoaDon
    @MaKH NVARCHAR(10),
    @MaHoaDonFilter NVARCHAR(10) = NULL,
    @NgayLapFilter DATE = NULL,
    @TrangThaiFilter NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        H.MaHD,
        CAST(H.NgayLap AS DATE) AS NgayLap,
        H.NV_Lap AS NV_Lap,
        H.TienThanhToan AS TongTien,
        H.TrangThaiHD AS TrangThai 
    FROM HOADON H
    WHERE H.MaKH = @MaKH
      AND (@TrangThaiFilter IS NULL OR H.TrangThaiHD = @TrangThaiFilter)
      AND (@MaHoaDonFilter IS NULL OR H.MaHD LIKE '%' + @MaHoaDonFilter + '%')
      AND (@NgayLapFilter IS NULL OR CAST(H.NgayLap AS DATE) = @NgayLapFilter)
    ORDER BY H.NgayLap DESC;
END
GO

-- KH10: Xem Chi tiết hóa đơn
IF OBJECT_ID('dbo.SP_LS_HoaDon_XemChiTiet', 'P') IS NOT NULL
	DROP PROC dbo.SP_LS_HoaDon_XemChiTiet
GO

CREATE PROCEDURE SP_LS_HoaDon_XemChiTiet
    @MaHD NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM HOADON WHERE MaHD = @MaHD)
    BEGIN
        RAISERROR(N'Mã Hóa đơn không tồn tại.', 16, 1);
        RETURN;
    END

    SELECT
        H.MaHD,
        CAST(H.NgayLap AS DATE) AS NgayLap,
        H.TienThanhToan AS TongTien,
        H.HinhThucPay,
 
        STUFF((
            SELECT DISTINCT N', ' + DV.TenDV
            FROM CT_HOADON CTHD
            JOIN LS_DV L ON CTHD.MaLSGD = L.MaLSDV
            JOIN DICHVU DV ON L.MaDichVu = DV.MaDichVu
            WHERE CTHD.MaHD = @MaHD
            FOR XML PATH('')
        ), 1, 2, '') AS TenDichVu
    FROM HOADON H
    WHERE H.MaHD = @MaHD;

    SELECT 
        DV.TenDV AS SanPham, 
        DV.GiaTienDV AS DonGia, 
        1 AS SoLuong, 
        DV.GiaTienDV AS ThanhTien
    FROM CT_HOADON CTHD
    JOIN LS_DV L ON CTHD.MaLSGD = L.MaLSDV
    JOIN DICHVU DV ON L.MaDichVu = DV.MaDichVu
    WHERE CTHD.MaHD = @MaHD

    UNION ALL

    SELECT SP.TenSP, SP.GiaBan, CMH.SoLuongSP, CMH.ThanhTienMH
    FROM CT_MUAHANG CMH
    JOIN SANPHAM SP ON CMH.MaSP = SP.MaSP
    WHERE EXISTS (SELECT 1 FROM CT_HOADON WHERE MaHD = @MaHD AND MaLSGD = CMH.MaLSDVMH)

    UNION ALL

    SELECT T.MaThuoc, SP.GiaBan, TT.SoLuongThuoc, TT.ThanhTien
    FROM TOA_THUOC TT
    JOIN SANPHAM SP ON TT.MaThuoc = SP.MaSP
    JOIN THUOC T ON SP.MaSP = T.MaThuoc
    WHERE EXISTS (SELECT 1 FROM CT_HOADON WHERE MaHD = @MaHD AND MaLSGD = TT.MaLSDV);

    SELECT
        ISNULL(STUFF((
            SELECT N', ' + KM.LoaiKM + N' (-' + CAST(CTKM.TienKM AS NVARCHAR) + N' VND)'
            FROM CT_KHUYENMAI CTKM
            JOIN KHUYENMAI KM ON CTKM.MaKM = KM.MaKM
            WHERE CTKM.MaHD = @MaHD
            FOR XML PATH('')
        ), 1, 2, ''), N'Không có') AS ChuoiKhuyenMai;
END
GO

-- KH11: Đánh giá dịch vụ
IF OBJECT_ID('dbo.SP_DanhGia_LayDS_DV_HoanTat', 'P') IS NOT NULL
	DROP PROC dbo.SP_DanhGia_LayDS_DV_HoanTat
GO

CREATE PROC SP_DanhGia_LayDS_DV_HoanTat
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT
        DV.MaDichVu,
        DV.TenDV
    FROM HOADON H
    JOIN CT_HOADON CTHD ON H.MaHD = CTHD.MaHD
    JOIN LS_DV L ON CTHD.MaLSGD = L.MaLSDV
    JOIN DICHVU DV ON L.MaDichVu = DV.MaDichVu
    WHERE H.MaKH = @MaKH 
      AND H.TrangThaiHD = N'Đã thanh toán' 
      AND NOT EXISTS (
        SELECT 1 FROM DANHGIA DG 
        WHERE DG.MaKH = @MaKH AND DG.MaDV = DV.MaDichVu
    );
END
GO

IF OBJECT_ID('dbo.SP_DanhGia_Luu', 'P') IS NOT NULL
	DROP PROC dbo.SP_DanhGia_Luu
GO

CREATE PROC SP_DanhGia_Luu
    @MaKH NVARCHAR(10),
    @MaDichVu NVARCHAR(10),
    @DiemDV TINYINT,      -- 1-5
    @DiemNV TINYINT,      -- 1-5
    @BinhLuan NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @MaDG NVARCHAR(10);

    IF @DiemDV < 1 OR @DiemDV > 5 OR @DiemNV < 1 OR @DiemNV > 5
    BEGIN
        RAISERROR(N'Điểm đánh giá phải từ 1 đến 5.', 16, 1);
        RETURN;
    END
    DECLARE @NextID INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaDG, 3, 7) AS INT)), 0) + 1 FROM DANHGIA);
    SET @MaDG = 'DG' + RIGHT('0000000' + CAST(@NextID AS NVARCHAR), 7);

    INSERT INTO DANHGIA (MaDG, DiemDV, DiemNV, MucDoHaiLong, BinhLuan, MaDV, MaKH)
    VALUES (@MaDG, @DiemDV, @DiemNV, @DiemDV, @BinhLuan, @MaDichVu, @MaKH);
    
    SELECT N'Thành công' AS Result;
END
GO

-- KH12: Xem thông tin Chi Nhánh
IF OBJECT_ID('dbo.SP_XemThongTinChiNhanh', 'P') IS NOT NULL
	DROP PROC dbo.SP_XemThongTinChiNhanh
GO

CREATE PROCEDURE SP_XemThongTinChiNhanh
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
        CN.TenCN AS [TenCN],
        CN.TimeMoCua AS [TGMo],
        CN.TimeDongCua AS [TGDong],
        CN.DiaChiCN AS [DiaChi],
        CN.SDT_CN AS [SDT],
        
        ISNULL(STUFF((
            SELECT N', ' + DV.TenDV
            FROM CHINHANH_DV CDV
            JOIN DICHVU DV ON CDV.MaDichVu = DV.MaDichVu
            WHERE CDV.MaCN = CN.MaCN 
              AND CDV.TrangThaiDV = N'Có cung cấp'
            FOR XML PATH('')
        ), 1, 2, ''), N'Đang cập nhật') AS [DVCungCap]
        
    FROM 
        CHINHANH CN
    ORDER BY 
        CN.TenCN;
END
GO

-- KH13: Xem Ưu đãi / Khuyến mãi
IF OBJECT_ID('dbo.SP_KH_XemKhuyenMai', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_XemKhuyenMai
GO

CREATE PROCEDURE SP_KH_XemKhuyenMai
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @LoaiKH NVARCHAR(20);
    SELECT @LoaiKH = Loai_KH FROM KHACHHANG WHERE MaKH = @MaKH;

    SELECT 
        MaKM,
        LoaiKM,
        GiaKM AS [GiaTriGoc], 
        CASE 
            WHEN LoaiKM IN (N'HVCoBan', N'HVThanThiet', N'HVVIP') THEN CAST(GiaKM AS NVARCHAR) + N' %'
            ELSE FORMAT(GiaKM, 'N0') + N' VNĐ'
        END AS [HienThi]
    FROM KHUYENMAI 
    WHERE 
        (@LoaiKH = N'Hội viên') 
        OR 
        (@LoaiKH <> N'Hội viên' AND LoaiKM NOT IN (N'HVCoBan', N'HVThanThiet', N'HVVIP', N'Loyalty')) 
    ORDER BY MaKM;
END
GO

-- KH14: Đăng ký hội viên
IF OBJECT_ID('dbo.SP_KH_DangKyHoiVien', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_DangKyHoiVien
GO

CREATE PROCEDURE SP_KH_DangKyHoiVien
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(500);
    
    BEGIN TRANSACTION;

    IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = @MaKH)
    BEGIN
        SET @ErrorMessage = N'Mã khách hàng không tồn tại.';
        GOTO ErrorHandler;
    END

    IF EXISTS (SELECT 1 FROM HOIVIEN WHERE MaKH = @MaKH)
    BEGIN
        SET @ErrorMessage = N'Khách hàng này đã là Hội viên rồi.';
        GOTO ErrorHandler;
    END

    UPDATE KHACHHANG
    SET Loai_KH = N'Hội viên'
    WHERE MaKH = @MaKH;

    UPDATE TK
    SET TK.LoaiTK = N'Hội viên'
    FROM TAIKHOAN TK
    JOIN KHACHHANG KH ON TK.ID_TK = KH.ID_TK
    WHERE KH.MaKH = @MaKH;

    INSERT INTO HOIVIEN (MaKH, CapDo, DiemLoyalty)
    VALUES (@MaKH, N'Cơ bản', 0);
    
    COMMIT TRANSACTION;

    SELECT N'Thành công' AS Result;
    RETURN;

    ErrorHandler:
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
END
GO

-- KH15: Tra cứu lịch bác sĩ
IF OBJECT_ID('dbo.SP_KH_TraCuuLichBacSi', 'P') IS NOT NULL
	DROP PROC dbo.SP_KH_TraCuuLichBacSi
GO

CREATE PROCEDURE SP_KH_TraCuuLichBacSi
    @MaCN NVARCHAR(10),
    @NgayChon DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @NgayChon IS NULL SET @NgayChon = CAST(GETDATE() AS DATE);

    SELECT 
        NV.MaNV AS [MaBS],
        NV.HoTenNV AS [TenBS],
        @NgayChon AS [Ngay],
        ISNULL(CaKham.SL, 0) + ISNULL(CaTiem.SL, 0) AS [SoLuongHen],
        N'Đã có ' + CAST(ISNULL(CaKham.SL, 0) + ISNULL(CaTiem.SL, 0) AS NVARCHAR) + N' lịch hẹn' AS [SoLichHen],
        CASE 
            WHEN (ISNULL(CaKham.SL, 0) + ISNULL(CaTiem.SL, 0)) < 5 THEN N'Còn trống'
            WHEN (ISNULL(CaKham.SL, 0) + ISNULL(CaTiem.SL, 0)) <= 10 THEN N'Khá bận'
            ELSE N'Rất bận'
        END AS [TrangThai]
    FROM NHANVIEN NV
    LEFT JOIN (
        SELECT BacSiPhuTrach, COUNT(*) AS SL FROM LS_DVKHAMBENH 
        WHERE CAST(NgayHen AS DATE) = @NgayChon GROUP BY BacSiPhuTrach
    ) CaKham ON NV.MaNV = CaKham.BacSiPhuTrach
    LEFT JOIN (
        SELECT BacSiPhuTrach, COUNT(*) AS SL FROM LS_DVTIEMPHONG 
        WHERE CAST(NgayTiem AS DATE) = @NgayChon GROUP BY BacSiPhuTrach
    ) CaTiem ON NV.MaNV = CaTiem.BacSiPhuTrach
    WHERE NV.ChucVu = N'Bác sĩ'
      AND NV.ChiNhanhLamViec = @MaCN
      AND NV.TrangThaiNV = N'Rảnh'
    ORDER BY [SoLuongHen] ASC;
END
GO

-- HV1: Xem thông tin hội viên
IF OBJECT_ID('dbo.SP_HV_XemThongTin', 'P') IS NOT NULL
	DROP PROC dbo.SP_HV_XemThongTin
GO

CREATE PROCEDURE SP_HV_XemThongTin
    @MaKH NVARCHAR(10) 
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CapDoHienTai NVARCHAR(20);
    DECLARE @LoaiKH NVARCHAR(50);

    SELECT @LoaiKH = Loai_KH FROM KHACHHANG WHERE MaKH = @MaKH;

	IF @LoaiKH IS NULL
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại trong hệ thống.', 16, 1);
        RETURN;
    END

    SELECT @CapDoHienTai = CapDo FROM HOIVIEN WHERE MaKH = @MaKH;

    IF @CapDoHienTai IS NULL AND @LoaiKH = N'Hội viên'
    BEGIN
        INSERT INTO HOIVIEN (MaKH, CapDo, DiemLoyalty)
        VALUES (@MaKH, N'Cơ bản', 0);
    END
    ELSE IF @CapDoHienTai IS NULL AND @LoaiKH <> N'Hội viên'
    BEGIN
        RAISERROR(N'Khách hàng này chưa đăng ký làm Hội viên.', 16, 1);
        RETURN;
    END

    SELECT 
        H.CapDo AS [Cấp độ hiện tại],
        H.DiemLoyalty AS [Điểm Loyalty hiện tại],
        
        CASE H.CapDo
            WHEN N'VIP' THEN N'Chi tiêu năm >= 8.000.000 VNĐ'
            WHEN N'Thân thiết' THEN N'Chi tiêu năm >= 3.000.000 VNĐ'
            WHEN N'Cơ bản' THEN N'Không áp dụng (Mặc định Cơ bản)'
            ELSE N'Không xác định.'
        END AS [Điều kiện duy trì],

        CASE H.CapDo
            WHEN N'VIP' THEN N'Không áp dụng (Cấp độ cao nhất)'
            WHEN N'Thân thiết' THEN N'Chi tiêu năm >= 12.000.000 VNĐ'
            WHEN N'Cơ bản' THEN N'Chi tiêu năm >= 5.000.000 VNĐ'
            ELSE N'Không xác định.'
        END AS [Điều kiện thăng hạng],

        CASE H.CapDo
            WHEN N'VIP' THEN N'Giảm 10% phí dịch vụ.'
            WHEN N'Thân thiết' THEN N'Giảm 5% phí dịch vụ.'
            WHEN N'Cơ bản' THEN N'Giảm 3% phí dịch vụ.'
            ELSE N'Không có quyền lợi đặc biệt.'
        END AS [Quyền lợi]
    
    FROM HOIVIEN H
    WHERE H.MaKH = @MaKH;
    
END
GO


-- HV2: Quản lý điểm Loyalty
IF OBJECT_ID('dbo.SP_HV_XemDiemLoyalty', 'P') IS NOT NULL
	DROP PROC dbo.SP_HV_XemDiemLoyalty
GO

CREATE PROCEDURE dbo.SP_HV_XemDiemLoyalty
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @DiemThucTe INT;
    DECLARE @HistoryTable TABLE (
        NgayGio DATETIME,
        MaHD NVARCHAR(10),
        MoTa NVARCHAR(255),
        DiemCong INT,
        DiemTru INT
    );

    INSERT INTO @HistoryTable (NgayGio, MaHD, MoTa, DiemCong, DiemTru)
    SELECT 
        MAX(NgayGio) AS NgayGio, 
        MaHD, 
        CASE 
            WHEN SUM(DiemCong) > 0 AND SUM(DiemTru) > 0 THEN N'Thanh toán (Tích & Sử dụng điểm)'
            WHEN SUM(DiemCong) > 0 THEN N'Tích điểm thanh toán'
            ELSE N'Sử dụng điểm Loyalty'
        END AS MoTa,
        SUM(DiemCong) AS DiemCong,
        SUM(DiemTru) AS DiemTru
    FROM (
        SELECT NgayLap AS NgayGio, MaHD, CongLoyalty AS DiemCong, 0 AS DiemTru
        FROM dbo.HOADON 
        WHERE MaKH = @MaKH AND CongLoyalty > 0
        
        UNION ALL

        SELECT H.NgayLap AS NgayGio, H.MaHD, 0 AS DiemCong, CTKM.SoLuongDung AS DiemTru
        FROM dbo.CT_KHUYENMAI CTKM
        JOIN dbo.HOADON H ON CTKM.MaHD = H.MaHD
        JOIN dbo.KHUYENMAI KM ON CTKM.MaKM = KM.MaKM
        WHERE H.MaKH = @MaKH AND KM.LoaiKM = N'Loyalty'
    ) AS RawData
    GROUP BY MaHD; 
    SELECT @DiemThucTe = ISNULL(SUM(DiemCong - DiemTru), 0) FROM @HistoryTable;
    IF @DiemThucTe < 0 SET @DiemThucTe = 0;
    UPDATE dbo.HOIVIEN SET DiemLoyalty = @DiemThucTe WHERE MaKH = @MaKH;
    SELECT @DiemThucTe AS [DiemHienCo];

    SELECT 
        NgayGio, 
        MaHD AS HoaDon, 
        MoTa, 
        DiemCong, 
        DiemTru,
        SUM(DiemCong - DiemTru) OVER (ORDER BY NgayGio ASC, MaHD ASC) AS DiemConLai
    FROM @HistoryTable
    ORDER BY NgayGio DESC;
END
GO


-- HV4: Thông kê chi tiêu năm
IF OBJECT_ID('dbo.SP_HV_ThongKeChiTieuNam', 'P') IS NOT NULL
	DROP PROC dbo.SP_HV_ThongKeChiTieuNam
GO

CREATE PROCEDURE SP_HV_ThongKeChiTieuNam
    @MaKH NVARCHAR(10) 
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NamHienTai INT = YEAR(GETDATE());

    IF NOT EXISTS (SELECT 1 FROM HOIVIEN WHERE MaKH = @MaKH)
    BEGIN
        RAISERROR(N'Khách hàng không phải là Hội viên.', 16, 1);
        RETURN;
    END
    SELECT
        ISNULL((SELECT ChiTieu FROM CHITIEUNAM WHERE MaKH = @MaKH AND Nam = @NamHienTai), 0) AS [TongChiTieuHienTai];

    SELECT
        Nam AS [Nam],
        ChiTieu AS [ChiTieu]
    FROM CHITIEUNAM
    WHERE MaKH = @MaKH
    ORDER BY Nam DESC;
END
GO

USE PetCareDB
GO

-- QLCT1 - Revenue Procedures
CREATE PROCEDURE sp_DoanhThu_CongTy_TheoNgay
    @Ngay INT = NULL,
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(SUM(CAST(TienThanhToan AS BIGINT)), 0) AS DoanhThu
    FROM HOADON
    WHERE (@Ngay IS NULL OR DAY(NgayLap) = @Ngay)
      AND (@Thang IS NULL OR MONTH(NgayLap) = @Thang)
      AND (@Nam IS NULL OR YEAR(NgayLap) = @Nam);
END;
GO

CREATE PROCEDURE sp_DoanhThu_ChiNhanh_TheoNgay
    @MaCN NVARCHAR(10),
    @Ngay INT = NULL,
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(SUM(CAST(H.TienThanhToan AS BIGINT)), 0) AS DoanhThu
    FROM HOADON H
    JOIN NHANVIEN NV ON H.NV_Lap = NV.MaNV
    WHERE NV.ChiNhanhLamViec = @MaCN
      AND (@Ngay IS NULL OR DAY(H.NgayLap) = @Ngay)
      AND (@Thang IS NULL OR MONTH(H.NgayLap) = @Thang)
      AND (@Nam IS NULL OR YEAR(H.NgayLap) = @Nam);
END;
GO

CREATE PROCEDURE sp_DoanhThu_CongTy_TheoQuy
    @Quy INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(SUM(CAST(TienThanhToan AS BIGINT)), 0) AS DoanhThu
    FROM HOADON
    WHERE (@Quy IS NULL OR DATEPART(QUARTER, NgayLap) = @Quy)
      AND (@Nam IS NULL OR YEAR(NgayLap) = @Nam);
END;
GO

CREATE PROCEDURE sp_DoanhThu_ChiNhanh_TheoQuy
    @MaCN NVARCHAR(10),
    @Quy INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(SUM(CAST(H.TienThanhToan AS BIGINT)), 0) AS DoanhThu
    FROM HOADON H
    JOIN NHANVIEN NV ON H.NV_Lap = NV.MaNV
    WHERE NV.ChiNhanhLamViec = @MaCN
      AND (@Quy IS NULL OR DATEPART(QUARTER, H.NgayLap) = @Quy)
      AND (@Nam IS NULL OR YEAR(H.NgayLap) = @Nam);
END;
GO

-- QLCT2 - Branch Management
CREATE PROCEDURE sp_ThemChiNhanh
    @MaCN NVARCHAR(10),
    @TenCN NVARCHAR(50),
    @DiaChiCN NVARCHAR(100),
    @SDT_CN VARCHAR(11),
    @TimeMoCua TIME,
    @TimeDongCua TIME
AS
BEGIN
    INSERT INTO CHINHANH (MaCN, TenCN, DiaChiCN, SDT_CN, TimeMoCua, TimeDongCua)
    VALUES (@MaCN, @TenCN, @DiaChiCN, @SDT_CN, @TimeMoCua, @TimeDongCua);
END;
GO

CREATE PROCEDURE sp_SuaChiNhanh
    @MaCN NVARCHAR(10),
    @TenCN NVARCHAR(50),
    @DiaChiCN NVARCHAR(100),
    @SDT_CN VARCHAR(11),
    @TimeMoCua TIME,
    @TimeDongCua TIME
AS
BEGIN
    UPDATE CHINHANH
    SET TenCN = @TenCN,
        DiaChiCN = @DiaChiCN,
        SDT_CN = @SDT_CN,
        TimeMoCua = @TimeMoCua,
        TimeDongCua = @TimeDongCua
    WHERE MaCN = @MaCN;
END;
GO

CREATE PROCEDURE sp_XoaChiNhanh
    @MaCN NVARCHAR(10)
AS
BEGIN
    DELETE FROM CHINHANH
    WHERE MaCN = @MaCN;
END;
GO

CREATE PROCEDURE sp_XemChiNhanh
    @MaCN NVARCHAR(10) = NULL,
    @TenCN NVARCHAR(50) = NULL,
    @DiaChiCN NVARCHAR(100) = NULL,
    @SDT_CN VARCHAR(11) = NULL,
    @TimeMoCua TIME = NULL,
    @TimeDongCua TIME = NULL
AS
BEGIN
    SELECT MaCN, TenCN, DiaChiCN, SDT_CN, TimeMoCua, TimeDongCua
    FROM CHINHANH
    WHERE (@MaCN IS NULL OR MaCN = @MaCN)
      AND (@TenCN IS NULL OR TenCN LIKE '%' + @TenCN + '%')
      AND (@DiaChiCN IS NULL OR DiaChiCN LIKE '%' + @DiaChiCN + '%')
      AND (@SDT_CN IS NULL OR SDT_CN = @SDT_CN)
      AND (@TimeMoCua IS NULL OR TimeMoCua = @TimeMoCua)
      AND (@TimeDongCua IS NULL OR TimeDongCua = @TimeDongCua);
END;
GO

-- QLCT3 - Performance Statistics
CREATE PROCEDURE sp_ThongKe_HieuSuat_CongTy
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM HOADON) AS TongSoHoaDon,
        (SELECT COUNT(*) FROM CT_HOADON) AS TongSoLuongDichVu,
        (SELECT ISNULL(AVG((CAST(DiemDV AS FLOAT) + CAST(DiemNV AS FLOAT)) / 2), 0) 
         FROM DANHGIA) AS DiemDanhGiaTB
END;
GO

CREATE PROCEDURE sp_ThongKe_HieuSuat_ChiNhanh
    @MaCN NVARCHAR(10)
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) 
         FROM HOADON H
         JOIN NHANVIEN NV ON H.NV_Lap = NV.MaNV
         WHERE NV.ChiNhanhLamViec = @MaCN) AS TongSoHoaDon,
        (SELECT COUNT(CT.MaHD) 
         FROM HOADON H
         JOIN NHANVIEN NV ON H.NV_Lap = NV.MaNV
         JOIN CT_HOADON CT ON H.MaHD = CT.MaHD
         WHERE NV.ChiNhanhLamViec = @MaCN) AS TongSoLuongDichVu,
        (SELECT ISNULL(AVG((CAST(DiemDV AS FLOAT) + CAST(DiemNV AS FLOAT)) / 2), 0) 
         FROM DANHGIA) AS DiemDanhGiaTB
END;
GO

USE PetCareDB
GO

-- QLCT4 - Employee Management
-- Thêm Nhân Viên (Kèm tạo Tài khoản)
CREATE PROCEDURE sp_ThemNhanVien
    @MaNV NVARCHAR(10),
    @HoTenNV NVARCHAR(50),
    @NgaySinhNV DATE,
    @ChucVu NVARCHAR(50),
    @NgayVaoLam DATE,
    @Luong DECIMAL(18,2),
    @ChiNhanhLV NVARCHAR(10),
    @TrangThaiNV NVARCHAR(20),
    @TenDangNhap NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON; 
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @MatKhauMacDinh NVARCHAR(50) = @MaNV + 'password';

        IF EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            THROW 50001, N'Tên đăng nhập đã tồn tại.', 1;
        END

        INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTK)
        VALUES (@TenDangNhap, @MatKhauMacDinh, @ChucVu);

        DECLARE @NewID_TK INT = SCOPE_IDENTITY();

        INSERT INTO NHANVIEN (MaNV, HoTenNV, NgaySinhNV, ChucVu, NgayVaoLam, Luong, ChiNhanhLamViec, TrangThaiNV, ID_TK)
        VALUES (@MaNV, @HoTenNV, @NgaySinhNV, @ChucVu, @NgayVaoLam, @Luong, @ChiNhanhLV, @TrangThaiNV, @NewID_TK);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_SuaNhanVien
    @MaNV NVARCHAR(10),
    @HoTenNV NVARCHAR(50),
    @NgaySinhNV DATE,
    @ChucVu NVARCHAR(50),
    @NgayVaoLam DATE,
    @Luong DECIMAL(18,2),
    @ChiNhanhLV NVARCHAR(10),
    @TrangThaiNV NVARCHAR(20),
    @TenDangNhap NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @ChiNhanhCu NVARCHAR(10);
        SELECT @ChiNhanhCu = ChiNhanhLamViec FROM NHANVIEN WHERE MaNV = @MaNV;

        IF @ChiNhanhCu IS NOT NULL AND @ChiNhanhCu <> @ChiNhanhLV
        BEGIN
            DECLARE @NewSTT INT;
            SELECT @NewSTT = ISNULL(MAX(STT), 0) + 1 FROM LS_DIEUDONG WHERE MaNV = @MaNV;
            
            INSERT INTO LS_DIEUDONG (STT, MaNV, NgayDieuDong, ChiNhanhCu, ChiNhanhMoi)
            VALUES (@NewSTT, @MaNV, GETDATE(), @ChiNhanhCu, @ChiNhanhLV);
        END

        UPDATE NHANVIEN
        SET HoTenNV = @HoTenNV,
            NgaySinhNV = @NgaySinhNV,
            ChucVu = @ChucVu,
            NgayVaoLam = @NgayVaoLam,
            Luong = @Luong,
            ChiNhanhLamViec = @ChiNhanhLV,
            TrangThaiNV = @TrangThaiNV
        WHERE MaNV = @MaNV;

        DECLARE @ID_TK INT;
        SELECT @ID_TK = ID_TK FROM NHANVIEN WHERE MaNV = @MaNV;

        UPDATE TAIKHOAN
        SET TenDangNhap = @TenDangNhap,
            LoaiTK = @ChucVu
        WHERE ID_TK = @ID_TK;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
Go

USE PetCareDB
GO

CREATE PROCEDURE sp_XoaNhanVien
    @MaNV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE NHANVIEN
    SET TrangThaiNV = N'Đã nghỉ việc'
    WHERE MaNV = @MaNV;

    DECLARE @ID_TK INT;
    SELECT @ID_TK = ID_TK FROM NHANVIEN WHERE MaNV = @MaNV;
END
GO

-- QLCT5 - Promotion Management
CREATE PROCEDURE sp_TaoKhuyenMai
    @MaKM nvarchar(10),
    @LoaiKM nvarchar(20),
    @GiaKM int
AS
BEGIN
    INSERT INTO KHUYENMAI (MaKM, LoaiKM, GiaKM)
    VALUES (@MaKM, @LoaiKM, @GiaKM);
END;
GO

CREATE PROCEDURE sp_SuaKhuyenMai
    @MaKM nvarchar(10),
    @LoaiKM nvarchar(20),
    @GiaKM int
AS
BEGIN
    UPDATE KHUYENMAI
    SET LoaiKM = @LoaiKM,
        GiaKM = @GiaKM
    WHERE MaKM = @MaKM;
END;
GO

CREATE PROCEDURE sp_XoaKhuyenMai
    @MaKM nvarchar(10)
AS
BEGIN
    DELETE FROM KHUYENMAI
    WHERE MaKM = @MaKM;
END;
GO

CREATE PROCEDURE sp_XemKhuyenMai_KhachHang
    @MaKH NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @LoaiKH NVARCHAR(20);
    SELECT @LoaiKH = Loai_KH FROM KHACHHANG WHERE MaKH = @MaKH;

    SELECT 
        MaKM,
        LoaiKM,
        GiaKM AS [GiaTriGoc], 
        CASE 
            WHEN LoaiKM IN (N'Cơ bản', N'Thân thiết', N'VIP', N'Phần trăm') THEN CAST(GiaKM AS NVARCHAR) + N' %'
            ELSE FORMAT(GiaKM, 'N0') + N' VNĐ'
        END AS [HienThi]
    FROM KHUYENMAI 
    WHERE 
        (@LoaiKH = N'Hội viên') 
        OR 
        (@LoaiKH <> N'Hội viên' AND LoaiKM NOT IN (N'Cơ bản', N'Thân thiết', N'VIP')) 
    ORDER BY MaKM;
END
GO

CREATE OR ALTER PROCEDURE sp_XemKhuyenMai
    @MaKM nvarchar(10) = NULL,
    @LoaiKM nvarchar(20) = NULL,
    @GiaKM int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM KHUYENMAI
    WHERE (@MaKM IS NULL OR MaKM LIKE '%' + @MaKM + '%')
      AND (@LoaiKM IS NULL OR LoaiKM LIKE '%' + @LoaiKM + '%')
      AND (@GiaKM IS NULL OR GiaKM = @GiaKM);
END;
GO

-- QLCT6 - Service Management
CREATE PROCEDURE sp_ThemDichVu
    @MaDichVu nvarchar(10),
    @TenDV nvarchar(100),
    @GiaTienDV int
AS
BEGIN
    INSERT INTO DICHVU (MaDichVu, TenDV, GiaTienDV)
    VALUES (@MaDichVu, @TenDV, @GiaTienDV);
END;
GO

CREATE PROCEDURE sp_SuaDichVu
    @MaDichVu nvarchar(10),
    @TenDV nvarchar(100),
    @GiaTienDV int
AS
BEGIN
    UPDATE DICHVU
    SET TenDV = @TenDV,
        GiaTienDV = @GiaTienDV
    WHERE MaDichVu = @MaDichVu;
END;
GO

CREATE PROCEDURE sp_XoaDichVu
    @MaDichVu nvarchar(10)
AS
BEGIN
    DELETE FROM DICHVU
    WHERE MaDichVu = @MaDichVu;
END;
GO

CREATE PROCEDURE sp_XemDichVu
    @MaDichVu nvarchar(10) = NULL,
    @TenDV nvarchar(100) = NULL,
    @GiaTienDV int = NULL
AS
BEGIN
    SELECT * FROM DICHVU
    WHERE (@MaDichVu IS NULL OR MaDichVu = @MaDichVu)
      AND (@TenDV IS NULL OR TenDV = @TenDV)
      AND (@GiaTienDV IS NULL OR GiaTienDV = @GiaTienDV);
END;
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_ThongKe_HieuSuatNV_CongTy
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        NV.ChiNhanhLamViec AS MaCN, 
        NV.MaNV,
        NV.HoTenNV,
        ISNULL((SELECT AVG(CAST(DiemNV AS DECIMAL(10,2))) 
		FROM DANHGIA WHERE MaKH IN (SELECT MaKH FROM LS_DV WHERE MaLSDV IN 
		(SELECT MaLSDVKB FROM LS_DVKHAMBENH WHERE BacSiPhuTrach = NV.MaNV 
		UNION SELECT MaLSDVTP FROM LS_DVTIEMPHONG WHERE BacSiPhuTrach = NV.MaNV))), 0) AS DanhGiaNV
    FROM NHANVIEN NV
    WHERE NV.ChucVu IN (N'Bác sĩ', N'Tiếp tân', N'Bán hàng')
    ORDER BY NV.ChiNhanhLamViec, NV.MaNV;
END
GO


CREATE OR ALTER PROCEDURE sp_ThongKe_HieuSuatNV_ChiNhanh
    @MaCN NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        
        NV.ChiNhanhLamViec AS MaCN,
        NV.MaNV,
        NV.HoTenNV,
        ISNULL(
            (SELECT AVG(CAST(DiemNV AS DECIMAL(10,2))) 
             FROM DANHGIA DG 
             WHERE DG.MaDV IN (SELECT MaDichVu FROM LS_DV WHERE MaKH = DG.MaKH)
            ), 0
        ) AS DanhGiaNV
    FROM NHANVIEN NV
    WHERE NV.ChiNhanhLamViec = @MaCN
      AND NV.ChucVu IN (N'Bác sĩ', N'Tiếp tân', N'Bán hàng')
    ORDER BY NV.MaNV;
END
GO

-- QLCT8 - Product Statistics
CREATE PROCEDURE sp_ThongKeSanPham_CongTy
AS
BEGIN
    SELECT 
        SP.MaSP AS MaSP,
        SP.TenSP AS TenSP,
        SP.LoaiSP AS LoaiSP,
        SP.GiaBan AS GiaBan,
        ISNULL(SUM(CT_SPCN.SLTonKho), 0) AS TonKho,
        ISNULL(SUM(CT_MUAHANG.SoLuongSP), 0) AS DaBan
    FROM SANPHAM SP
    LEFT JOIN CT_SPCN ON SP.MaSP = CT_SPCN.MaSP
    LEFT JOIN CT_MUAHANG ON SP.MaSP = CT_MUAHANG.MaSP
    WHERE SP.LoaiSP IN (N'Thức ăn', N'Phụ kiện')
    GROUP BY SP.MaSP, SP.TenSP, SP.LoaiSP, SP.GiaBan
    ORDER BY SP.MaSP;
END;
GO

CREATE PROCEDURE sp_ThongKeSanPham_ChiNhanh
    @MaCN nvarchar(10)
AS
BEGIN
    SELECT 
        SP.MaSP AS MaSP,
        SP.TenSP AS TenSP,
        SP.LoaiSP AS LoaiSP,
        SP.GiaBan AS GiaBan,
        ISNULL(CT_SPCN.SLTonKho, 0) AS TonKho,
        ISNULL(SUM(CT_MUAHANG.SoLuongSP), 0) AS DaBan
    FROM SANPHAM SP
    LEFT JOIN CT_SPCN ON SP.MaSP = CT_SPCN.MaSP AND CT_SPCN.MaCN = @MaCN
    LEFT JOIN CT_MUAHANG ON SP.MaSP = CT_MUAHANG.MaSP
    LEFT JOIN LS_DVMUAHANG LS ON CT_MUAHANG.MaLSDVMH = LS.MaLSDVMH
    LEFT JOIN LS_DV DV ON LS.MaLSDVMH = DV.MaLSDV
    WHERE SP.LoaiSP IN (N'Thức ăn', N'Phụ kiện') 
      AND (DV.MaCN = @MaCN OR DV.MaCN IS NULL)
    GROUP BY SP.MaSP, SP.TenSP, SP.LoaiSP, SP.GiaBan, CT_SPCN.SLTonKho
    ORDER BY SP.MaSP;
END;
GO

CREATE PROCEDURE sp_ThongKeThuoc_CongTy
AS
BEGIN
    SELECT 
        TH.MaThuoc AS MaThuoc,
        SP.TenSP AS TenThuoc,
        TH.DonViTinh AS DonViTinh,
        SP.GiaBan AS GiaBan,
        TH.NgayHetHan AS HSD,
        ISNULL(SUM(CT_SPCN.SLTonKho), 0) AS TonKho,
        (ISNULL(SUM(CT_MUAHANG.SoLuongSP), 0) + ISNULL(SUM(TOA_THUOC.SoLuongThuoc), 0)) AS DaBan
    FROM SANPHAM SP
    INNER JOIN THUOC TH ON SP.MaSP = TH.MaThuoc
    LEFT JOIN CT_SPCN ON SP.MaSP = CT_SPCN.MaSP
    LEFT JOIN CT_MUAHANG ON SP.MaSP = CT_MUAHANG.MaSP
    LEFT JOIN TOA_THUOC ON SP.MaSP = TOA_THUOC.MaThuoc
    WHERE SP.LoaiSP = N'Thuốc'
    GROUP BY TH.MaThuoc, SP.TenSP, TH.DonViTinh, SP.GiaBan, TH.NgayHetHan
    ORDER BY TH.MaThuoc;
END;
GO

CREATE PROCEDURE sp_ThongKeThuoc_ChiNhanh
    @MaCN nvarchar(10)
AS
BEGIN
    SELECT 
        TH.MaThuoc AS MaThuoc,
        SP.TenSP AS TenThuoc,
        TH.DonViTinh AS DonViTinh,
        SP.GiaBan AS GiaBan,
        TH.NgayHetHan AS HSD,
        ISNULL(CT_SPCN.SLTonKho, 0) AS TonKho,
        ISNULL(Sold.Qty, 0) AS DaBan
    FROM SANPHAM SP
    INNER JOIN THUOC TH ON SP.MaSP = TH.MaThuoc
    LEFT JOIN CT_SPCN ON SP.MaSP = CT_SPCN.MaSP AND CT_SPCN.MaCN = @MaCN
    LEFT JOIN (
        SELECT 
            COALESCE(M.MaSP, T.MaThuoc) AS MaSP,
            SUM(COALESCE(M.SoLuongSP, 0) + COALESCE(T.SoLuongThuoc, 0)) AS Qty
        FROM (
            SELECT CM.MaSP, SUM(CM.SoLuongSP) AS SoLuongSP
            FROM CT_MUAHANG CM
            JOIN LS_DVMUAHANG LS ON CM.MaLSDVMH = LS.MaLSDVMH
            JOIN LS_DV DV ON LS.MaLSDVMH = DV.MaLSDV
            WHERE DV.MaCN = @MaCN
            GROUP BY CM.MaSP
            
            UNION ALL
            
            SELECT TT.MaThuoc, SUM(TT.SoLuongThuoc) AS SoLuongThuoc
            FROM TOA_THUOC TT
            JOIN LS_DV DV ON TT.MaLSDV = DV.MaLSDV
            WHERE DV.MaCN = @MaCN
            GROUP BY TT.MaThuoc
        ) AS M
        FULL OUTER JOIN (
            SELECT TT.MaThuoc, SUM(TT.SoLuongThuoc) AS SoLuongThuoc
            FROM TOA_THUOC TT
            JOIN LS_DV DV ON TT.MaLSDV = DV.MaLSDV
            WHERE DV.MaCN = @MaCN
            GROUP BY TT.MaThuoc
        ) AS T ON M.MaSP = T.MaThuoc
        GROUP BY COALESCE(M.MaSP, T.MaThuoc)
    ) AS Sold ON SP.MaSP = Sold.MaSP
    WHERE SP.LoaiSP = N'Thuốc'
    ORDER BY TH.MaThuoc;
END;
GO

CREATE PROCEDURE sp_ThongKeVaccine_CongTy
AS
BEGIN
    SELECT 
        V.MaVacXin AS MaVaccine,
        V.TenVacXin AS TenVaccine,
        V.GiaTien AS GiaTien,
        ISNULL(V.SoMuiTonKho, 0) AS TonKho,
        COUNT(TP.MaLSDVTP) AS DaBan
    FROM VACXIN V
    LEFT JOIN LS_DVTIEMPHONG TP ON V.MaVacXin = TP.LoaiVacXin
    GROUP BY V.MaVacXin, V.TenVacXin, V.GiaTien, V.SoMuiTonKho
    ORDER BY V.MaVacXin;
END;
GO

CREATE PROCEDURE sp_ThongKeVaccine_ChiNhanh
    @MaCN nvarchar(10)
AS
BEGIN
    SELECT 
        V.MaVacXin AS MaVaccine,
        V.TenVacXin AS TenVaccine,
        V.GiaTien AS GiaTien,
        0 AS TonKho,
        COUNT(TP.MaLSDVTP) AS DaBan
    FROM VACXIN V
    LEFT JOIN LS_DVTIEMPHONG TP ON V.MaVacXin = TP.LoaiVacXin
    LEFT JOIN LS_DV DV ON TP.MaLSDVTP = DV.MaLSDV
    WHERE DV.MaCN = @MaCN
    GROUP BY V.MaVacXin, V.TenVacXin, V.GiaTien
    ORDER BY V.MaVacXin;
END;
GO

-- QLCT9 - Customer Statistics
CREATE OR ALTER PROCEDURE sp_ThongKeKhachHang_CongTy
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CurrentDate DATE = GETDATE();
    DECLARE @OneMonthAgo DATE = DATEADD(MONTH, -1, @CurrentDate);
    DECLARE @ThreeMonthsAgo DATE = DATEADD(MONTH, -3, @CurrentDate);

    WITH CustomerHistory AS (
        SELECT MaKH, MIN(NgayDatTruoc) AS FirstServiceDate, MAX(NgayDatTruoc) AS LastServiceDate
        FROM LS_DV GROUP BY MaKH
    )
    SELECT 
        NULL AS MaCN, NULL AS TenCN,
        (SELECT COUNT(DISTINCT MaKH) FROM LS_DV) AS SoLuongKhach,
        (SELECT COUNT(MaKH) FROM CustomerHistory WHERE FirstServiceDate >= @OneMonthAgo) AS SoLuongKhachMoi,
        (SELECT COUNT(MaKH) FROM CustomerHistory WHERE LastServiceDate <= @ThreeMonthsAgo) AS SoLuongKhachLauKhongTroLai;
END;
GO

CREATE OR ALTER PROCEDURE sp_ThongKeKhachHang_ChiNhanh
    @MaCN NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CurrentDate DATE = GETDATE();
    DECLARE @OneMonthAgo DATE = DATEADD(MONTH, -1, @CurrentDate);
    DECLARE @ThreeMonthsAgo DATE = DATEADD(MONTH, -3, @CurrentDate);
    DECLARE @TenCN NVARCHAR(50);
    SELECT @TenCN = TenCN FROM CHINHANH WHERE MaCN = @MaCN;

    WITH BranchCustomerHistory AS (
        SELECT MaKH, MIN(NgayDatTruoc) AS FirstServiceDate, MAX(NgayDatTruoc) AS LastServiceDate
        FROM LS_DV WHERE MaCN = @MaCN GROUP BY MaKH
    )
    SELECT 
        @MaCN AS MaCN, @TenCN AS TenCN,
        (SELECT COUNT(MaKH) FROM BranchCustomerHistory) AS SoLuongKhach,
        (SELECT COUNT(MaKH) FROM BranchCustomerHistory WHERE FirstServiceDate >= @OneMonthAgo) AS SoLuongKhachMoi,
        (SELECT COUNT(MaKH) FROM BranchCustomerHistory WHERE LastServiceDate <= @ThreeMonthsAgo) AS SoLuongKhachLauKhongTroLai;
END;
GO

-- QLCT10 - Pet Management
CREATE PROCEDURE sp_XemDanhSachThuCung
    @MaThuCung NVARCHAR(10) = NULL,
    @TenThuCung NVARCHAR(100) = NULL,
    @MaKH NVARCHAR(10) = NULL,
    @LoaiThuCung NVARCHAR(100) = NULL
AS
BEGIN
    SELECT 
        TC.MaThuCung,
        TC.TenThuCung,
        TC.LoaiThuCung,
        TC.Giong_TC,
        TC.NgaySinh_TC,
        TC.GioiTinh_TC,
        TC.TinhTrangSK,
        TC.MaKH,
        KH.HoTen_KH
    FROM THUCUNG TC
    LEFT JOIN KHACHHANG KH ON TC.MaKH = KH.MaKH
    WHERE (@MaThuCung IS NULL OR TC.MaThuCung = @MaThuCung)
      AND (@TenThuCung IS NULL OR TC.TenThuCung LIKE N'%' + @TenThuCung + N'%')
      AND (@MaKH IS NULL OR TC.MaKH = @MaKH)
      AND (@LoaiThuCung IS NULL OR TC.LoaiThuCung LIKE N'%' + @LoaiThuCung + N'%');
END;
GO

CREATE PROCEDURE sp_ThemThuCung
    @MaThuCung NVARCHAR(10),
    @TenThuCung NVARCHAR(100),
    @LoaiThuCung NVARCHAR(100),
    @Giong_TC NVARCHAR(100),
    @NgaySinh_TC DATE,
    @GioiTinh_TC NVARCHAR(10),
    @TinhTrangSK NVARCHAR(255),
    @MaKH NVARCHAR(10)
AS
BEGIN
    INSERT INTO THUCUNG (MaThuCung, TenThuCung, LoaiThuCung, Giong_TC, NgaySinh_TC, GioiTinh_TC, TinhTrangSK, MaKH)
    VALUES (@MaThuCung, @TenThuCung, @LoaiThuCung, @Giong_TC, @NgaySinh_TC, @GioiTinh_TC, @TinhTrangSK, @MaKH);
END;
GO

CREATE PROCEDURE sp_CapNhatThuCung
    @MaThuCung NVARCHAR(10),
    @TenThuCung NVARCHAR(100),
    @LoaiThuCung NVARCHAR(100),
    @Giong_TC NVARCHAR(100),
    @NgaySinh_TC DATE,
    @GioiTinh_TC NVARCHAR(10),
    @TinhTrangSK NVARCHAR(255),
    @MaKH NVARCHAR(10)
AS
BEGIN
    UPDATE THUCUNG
    SET TenThuCung = @TenThuCung,
        LoaiThuCung = @LoaiThuCung,
        Giong_TC = @Giong_TC,
        NgaySinh_TC = @NgaySinh_TC,
        GioiTinh_TC = @GioiTinh_TC,
        TinhTrangSK = @TinhTrangSK,
        MaKH = @MaKH
    WHERE MaThuCung = @MaThuCung;
END;
GO

CREATE PROCEDURE sp_XoaThuCung
    @MaThuCung NVARCHAR(10)
AS
BEGIN
    DELETE FROM THUCUNG WHERE MaThuCung = @MaThuCung;
END;
GO

-- QLCT11 - Vaccination History
CREATE PROCEDURE sp_GetLSTiemPhong_ByPet_DateOptional
    @MaThuCung NVARCHAR(10),
    @NgayTiem DATE = NULL
AS
BEGIN
    SELECT 
        MaLSDVTP AS MaLSTP,
        BacSiPhuTrach,
        MaGoiTiem,
        LoaiVacXin AS MaVacXin,
        LieuLuong,
        NgayTiem,
        MaThuCung
    FROM LS_DVTIEMPHONG
    WHERE MaThuCung = @MaThuCung
      AND (@NgayTiem IS NULL OR NgayTiem = @NgayTiem)
    ORDER BY NgayTiem DESC
END
GO

-- QLCT12 - Invoice Viewing
CREATE PROCEDURE sp_XemDSHoaDon_CongTy_NgayThangNam
    @Ngay INT = NULL,
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SELECT 
        MaHD, 
        NgayLap, 
        NV_Lap, 
        TienTruocKM, 
        TienThanhToan, 
        HinhThucPay, 
        TrangThaiHD, 
        CongLoyalty, 
        MaKH
    FROM HOADON
    WHERE 
        (@Ngay IS NULL OR DAY(NgayLap) = @Ngay)
        AND (@Thang IS NULL OR MONTH(NgayLap) = @Thang)
        AND (@Nam IS NULL OR YEAR(NgayLap) = @Nam)
END
GO

CREATE PROCEDURE sp_XemDSHoaDon_ChiNhanh_NgayThangNam
    @MaCN NVARCHAR(10),
    @Ngay INT = NULL,
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SELECT 
        H.MaHD, 
        H.NgayLap, 
        H.NV_Lap, 
        H.TienTruocKM, 
        H.TienThanhToan, 
        H.HinhThucPay, 
        H.TrangThaiHD, 
        H.CongLoyalty, 
        H.MaKH
    FROM HOADON H
    JOIN NHANVIEN NV ON H.NV_Lap = NV.MaNV
    WHERE 
        NV.ChiNhanhLamViec = @MaCN
        AND (@Ngay IS NULL OR DAY(H.NgayLap) = @Ngay)
        AND (@Thang IS NULL OR MONTH(H.NgayLap) = @Thang)
        AND (@Nam IS NULL OR YEAR(H.NgayLap) = @Nam)
END
GO

CREATE PROCEDURE sp_XemDSHoaDon_CongTy_QuyNam
    @Quy INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SELECT 
        MaHD, 
        NgayLap, 
        NV_Lap, 
        TienTruocKM, 
        TienThanhToan, 
        HinhThucPay, 
        TrangThaiHD, 
        CongLoyalty, 
        MaKH
    FROM HOADON
    WHERE 
        (@Quy IS NULL OR DATEPART(QUARTER, NgayLap) = @Quy)
        AND (@Nam IS NULL OR YEAR(NgayLap) = @Nam)
END
GO

CREATE PROCEDURE sp_XemDSHoaDon_ChiNhanh_QuyNam
    @MaCN NVARCHAR(10),
    @Quy INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SELECT 
        H.MaHD, 
        H.NgayLap, 
        H.NV_Lap, 
        H.TienTruocKM, 
        H.TienThanhToan, 
        H.HinhThucPay, 
        H.TrangThaiHD, 
        H.CongLoyalty, 
        H.MaKH
    FROM HOADON H
    JOIN NHANVIEN NV ON H.NV_Lap = NV.MaNV
    WHERE 
        NV.ChiNhanhLamViec = @MaCN
        AND (@Quy IS NULL OR DATEPART(QUARTER, H.NgayLap) = @Quy)
        AND (@Nam IS NULL OR YEAR(H.NgayLap) = @Nam)
END
GO

USE PetCareDB
GO

-- QLCT13 - Service History Viewing
CREATE OR ALTER PROCEDURE sp_XemLSDVMuaHang_CongTy_NgayThangNam
    @Ngay INT = NULL, @Thang INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, M.HinhThucMH
    FROM LS_DV L JOIN LS_DVMUAHANG M ON L.MaLSDV = M.MaLSDVMH
    WHERE (@Ngay IS NULL OR DAY(L.NgayDatTruoc) = @Ngay)
      AND (@Thang IS NULL OR MONTH(L.NgayDatTruoc) = @Thang)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVMuaHang_ChiNhanh_NgayThangNam
    @MaCN NVARCHAR(10), @Ngay INT = NULL, @Thang INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, M.HinhThucMH
    FROM LS_DV L JOIN LS_DVMUAHANG M ON L.MaLSDV = M.MaLSDVMH
    WHERE L.MaCN = @MaCN
      AND (@Ngay IS NULL OR DAY(L.NgayDatTruoc) = @Ngay)
      AND (@Thang IS NULL OR MONTH(L.NgayDatTruoc) = @Thang)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVMuaHang_CongTy_QuyNam
    @Quy INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, M.HinhThucMH
    FROM LS_DV L JOIN LS_DVMUAHANG M ON L.MaLSDV = M.MaLSDVMH
    WHERE (@Quy IS NULL OR DATEPART(QUARTER, L.NgayDatTruoc) = @Quy)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVMuaHang_ChiNhanh_QuyNam
    @MaCN NVARCHAR(10), @Quy INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, M.HinhThucMH
    FROM LS_DV L JOIN LS_DVMUAHANG M ON L.MaLSDV = M.MaLSDVMH
    WHERE L.MaCN = @MaCN
      AND (@Quy IS NULL OR DATEPART(QUARTER, L.NgayDatTruoc) = @Quy)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVKhamBenh_CongTy_NgayThangNam
    @Ngay INT = NULL, @Thang INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, K.BacSiPhuTrach, K.NgayHen, K.MaThuCung
    FROM LS_DV L JOIN LS_DVKHAMBENH K ON L.MaLSDV = K.MaLSDVKB
    WHERE (@Ngay IS NULL OR DAY(L.NgayDatTruoc) = @Ngay)
      AND (@Thang IS NULL OR MONTH(L.NgayDatTruoc) = @Thang)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVKhamBenh_ChiNhanh_NgayThangNam
    @MaCN NVARCHAR(10), @Ngay INT = NULL, @Thang INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, K.BacSiPhuTrach, K.NgayHen, K.MaThuCung
    FROM LS_DV L JOIN LS_DVKHAMBENH K ON L.MaLSDV = K.MaLSDVKB
    WHERE L.MaCN = @MaCN
      AND (@Ngay IS NULL OR DAY(L.NgayDatTruoc) = @Ngay)
      AND (@Thang IS NULL OR MONTH(L.NgayDatTruoc) = @Thang)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVKhamBenh_CongTy_QuyNam
    @Quy INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, K.BacSiPhuTrach, K.NgayHen, K.MaThuCung
    FROM LS_DV L JOIN LS_DVKHAMBENH K ON L.MaLSDV = K.MaLSDVKB
    WHERE (@Quy IS NULL OR DATEPART(QUARTER, L.NgayDatTruoc) = @Quy)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVKhamBenh_ChiNhanh_QuyNam
    @MaCN NVARCHAR(10), @Quy INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, K.BacSiPhuTrach, K.NgayHen, K.MaThuCung
    FROM LS_DV L JOIN LS_DVKHAMBENH K ON L.MaLSDV = K.MaLSDVKB
    WHERE L.MaCN = @MaCN
      AND (@Quy IS NULL OR DATEPART(QUARTER, L.NgayDatTruoc) = @Quy)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVTiemPhong_CongTy_NgayThangNam
    @Ngay INT = NULL, @Thang INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, 
           T.BacSiPhuTrach, T.MaGoiTiem, 
           T.LoaiVacXin AS MaVacXin, 
           T.LieuLuong, T.NgayTiem, T.MaThuCung
    FROM LS_DV L JOIN LS_DVTIEMPHONG T ON L.MaLSDV = T.MaLSDVTP
    WHERE (@Ngay IS NULL OR DAY(L.NgayDatTruoc) = @Ngay)
      AND (@Thang IS NULL OR MONTH(L.NgayDatTruoc) = @Thang)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVTiemPhong_ChiNhanh_NgayThangNam
    @MaCN NVARCHAR(10), @Ngay INT = NULL, @Thang INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, 
           T.BacSiPhuTrach, T.MaGoiTiem, 
           T.LoaiVacXin AS MaVacXin,
           T.LieuLuong, T.NgayTiem, T.MaThuCung
    FROM LS_DV L JOIN LS_DVTIEMPHONG T ON L.MaLSDV = T.MaLSDVTP
    WHERE L.MaCN = @MaCN
      AND (@Ngay IS NULL OR DAY(L.NgayDatTruoc) = @Ngay)
      AND (@Thang IS NULL OR MONTH(L.NgayDatTruoc) = @Thang)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

-- 2. Xem theo Quý/Năm
CREATE OR ALTER PROCEDURE sp_XemLSDVTiemPhong_CongTy_QuyNam
    @Quy INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, 
           T.BacSiPhuTrach, T.MaGoiTiem, 
           T.LoaiVacXin AS MaVacXin, 
           T.LieuLuong, T.NgayTiem, T.MaThuCung
    FROM LS_DV L JOIN LS_DVTIEMPHONG T ON L.MaLSDV = T.MaLSDVTP
    WHERE (@Quy IS NULL OR DATEPART(QUARTER, L.NgayDatTruoc) = @Quy)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

CREATE OR ALTER PROCEDURE sp_XemLSDVTiemPhong_ChiNhanh_QuyNam
    @MaCN NVARCHAR(10), @Quy INT = NULL, @Nam INT = NULL
AS
BEGIN
    SELECT L.MaLSDV, L.MaKH, L.MaDichVu, L.TrangThaiGD, L.NgayDatTruoc, 
           T.BacSiPhuTrach, T.MaGoiTiem, 
           T.LoaiVacXin AS MaVacXin, 
           T.LieuLuong, T.NgayTiem, T.MaThuCung
    FROM LS_DV L JOIN LS_DVTIEMPHONG T ON L.MaLSDV = T.MaLSDVTP
    WHERE L.MaCN = @MaCN
      AND (@Quy IS NULL OR DATEPART(QUARTER, L.NgayDatTruoc) = @Quy)
      AND (@Nam IS NULL OR YEAR(L.NgayDatTruoc) = @Nam);
END
GO

-- QLCT14 - Member Statistics
CREATE OR ALTER PROCEDURE sp_ThongKe_HoiVien_LietKe
AS
BEGIN
    SELECT 
        KH.MaKH,
        KH.HoTen_KH,
        KH.SDT_KH,
        KH.NgaySinh_KH,
        KH.GioiTinh_KH,
        KH.Email_KH,
        KH.Loai_KH,
        TK.TenDangNhap,
        HV.CapDo,
        HV.DiemLoyalty,
        ISNULL(SUM(CTN.ChiTieu), 0) AS TongChiTieu
    FROM KHACHHANG KH
    INNER JOIN HOIVIEN HV ON KH.MaKH = HV.MaKH
    INNER JOIN TAIKHOAN TK ON KH.ID_TK = TK.ID_TK 
    LEFT JOIN CHITIEUNAM CTN ON KH.MaKH = CTN.MaKH
    WHERE KH.Loai_KH = N'Hội viên'
    GROUP BY 
        KH.MaKH, KH.HoTen_KH, KH.SDT_KH, KH.NgaySinh_KH,
        KH.GioiTinh_KH, KH.Email_KH, KH.Loai_KH, TK.TenDangNhap,
        HV.CapDo, HV.DiemLoyalty
    ORDER BY KH.MaKH
END;
GO

CREATE OR ALTER PROCEDURE sp_ThongKe_HoiVien_TongSoLuong
AS
BEGIN
    SELECT COUNT(*) AS TongSoLuongHoiVien
    FROM KHACHHANG
    WHERE Loai_KH = N'Hội vien'
END;
GO

CREATE OR ALTER PROCEDURE sp_ThongKe_HoiVien_ChiTieuTrungBinh
AS
BEGIN
    SELECT 
        CASE 
            WHEN COUNT(DISTINCT CTN.MaKH) > 0 
            THEN ISNULL(SUM(CTN.ChiTieu), 0) / COUNT(DISTINCT CTN.MaKH)
            ELSE 0
        END AS ChiTieuTrungBinh
    FROM CHITIEUNAM CTN
    INNER JOIN KHACHHANG KH ON CTN.MaKH = KH.MaKH
    WHERE KH.Loai_KH = N'Hội viên'
END;
GO

-- QLCT15 - Transfer History
CREATE PROCEDURE sp_XemLSDD_ToanCongTy
    @MaNV NVARCHAR(50) = NULL,
    @NgayDieuDong DATETIME = NULL
AS
BEGIN
    SELECT 
        STT,
        MaNV,
        NgayDieuDong,
        ChiNhanhCu,
        ChiNhanhMoi
    FROM LS_DIEUDONG
    WHERE 
        (@MaNV IS NULL OR MaNV = @MaNV)
        AND (@NgayDieuDong IS NULL OR CAST(NgayDieuDong AS DATE) = CAST(@NgayDieuDong AS DATE))
    ORDER BY NgayDieuDong DESC
END
GO

CREATE PROCEDURE sp_XemLSDD_TuChiNhanh
    @MaCN NVARCHAR(50),
    @MaNV NVARCHAR(50) = NULL,
    @NgayDieuDong DATETIME = NULL
AS
BEGIN
    SELECT 
        STT,
        MaNV,
        NgayDieuDong,
        ChiNhanhCu,
        ChiNhanhMoi
    FROM LS_DIEUDONG
    WHERE 
        ChiNhanhCu = @MaCN
        AND (@MaNV IS NULL OR MaNV = @MaNV)
        AND (@NgayDieuDong IS NULL OR CAST(NgayDieuDong AS DATE) = CAST(@NgayDieuDong AS DATE))
    ORDER BY NgayDieuDong DESC
END
GO

CREATE PROCEDURE sp_XemLSDD_DenChiNhanh
    @MaCN NVARCHAR(50),
    @MaNV NVARCHAR(50) = NULL,
    @NgayDieuDong DATETIME = NULL
AS
BEGIN
    SELECT 
        STT,
        MaNV,
        NgayDieuDong,
        ChiNhanhCu,
        ChiNhanhMoi
    FROM LS_DIEUDONG
    WHERE 
        ChiNhanhMoi = @MaCN
        AND (@MaNV IS NULL OR MaNV = @MaNV)
        AND (@NgayDieuDong IS NULL OR CAST(NgayDieuDong AS DATE) = CAST(@NgayDieuDong AS DATE))
    ORDER BY NgayDieuDong DESC
END
GO

-- QTV1 - Account Management
CREATE PROCEDURE sp_ThemTaiKhoan
    @TenDangNhap NVARCHAR(20),
    @MatKhau NVARCHAR(50),
    @LoaiTK NVARCHAR(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        THROW 50001, N'Tên đăng nhập đã tồn tại!', 1;
    END

    IF @LoaiTK NOT IN (N'Tiếp tân', N'Quản lý chi nhánh', N'Bác sĩ', 
                       N'Khách hàng', N'Hội viên', N'Quản lý công ty', 
                       N'Quản trị viên', N'Bán hàng')
    BEGIN
        THROW 50002, N'Loại tài khoản không hợp lệ!', 1;
    END

    INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTK)
    VALUES (@TenDangNhap, @MatKhau, @LoaiTK);
END
GO

CREATE PROCEDURE sp_XoaTaiKhoan
    @TenDangNhap NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            THROW 50003, N'Tên đăng nhập không tồn tại!', 1;
        END

        DELETE FROM TAIKHOAN 
        WHERE TenDangNhap = @TenDangNhap;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        THROW 50004, N'Không thể xóa: Tài khoản này đang được sử dụng bởi một Nhân viên hoặc Khách hàng.', 1;
    END CATCH
END
GO

CREATE PROCEDURE sp_DoiLoaiTaiKhoan
    @TenDangNhap NVARCHAR(20),
    @LoaiTKMoi NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        THROW 50005, N'Tên đăng nhập không tồn tại!', 1;
    END

    IF @LoaiTKMoi NOT IN (N'Tiếp tân', N'Quản lý chi nhánh', N'Bác sĩ', 
                          N'Khách hàng', N'Hội viên', N'Quản lý công ty', 
                          N'Quản trị viên', N'Bán hàng')
    BEGIN
        THROW 50006, N'Loại tài khoản mới không hợp lệ!', 1;
    END

    UPDATE TAIKHOAN
    SET LoaiTK = @LoaiTKMoi
    WHERE TenDangNhap = @TenDangNhap;
END
GO

CREATE PROCEDURE sp_DoiMatKhau
    @TenDangNhap NVARCHAR(20),
    @MatKhauMoi NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        THROW 50007, N'Tên đăng nhập không tồn tại!', 1;
    END

    UPDATE TAIKHOAN
    SET MatKhau = @MatKhauMoi
    WHERE TenDangNhap = @TenDangNhap;
END
GO

-- ALL_TraCuuDichVu
CREATE PROCEDURE GetAllDichVuByChiNhanh
    @MaCN NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cd.MaCN,
        cd.MaDichVu,
        ISNULL(cd.GiaDV_CN, dv.GiaTienDV) AS GiaDV_CN,
        cd.TrangThaiDV,
        dv.TenDV
    FROM CHINHANH_DV cd
    INNER JOIN DICHVU dv ON cd.MaDichVu = dv.MaDichVu
    WHERE (@MaCN IS NULL OR cd.MaCN = @MaCN)
    ORDER BY cd.MaCN, cd.MaDichVu;
END

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_XemNhanVien
    @MaNV NVARCHAR(10) = NULL,
    @HoTenNV NVARCHAR(50) = NULL,
    @ChiNhanhLV NVARCHAR(10) = NULL,
    @ChucVu NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        NV.MaNV,
        NV.HoTenNV,
        NV.NgaySinhNV,
        NV.ChucVu,
        NV.NgayVaoLam,
        NV.Luong,
        NV.ChiNhanhLamViec,
        NV.TrangThaiNV,
        TK.TenDangNhap 
    FROM NHANVIEN NV
    LEFT JOIN TAIKHOAN TK ON NV.ID_TK = TK.ID_TK
    WHERE 
        (@MaNV IS NULL OR NV.MaNV LIKE '%' + @MaNV + '%')
        AND (@HoTenNV IS NULL OR NV.HoTenNV LIKE N'%' + @HoTenNV + N'%')
        AND (@ChiNhanhLV IS NULL OR NV.ChiNhanhLamViec = @ChiNhanhLV) -- [ĐÃ SỬA]
        AND (@ChucVu IS NULL OR NV.ChucVu = @ChucVu)
    ORDER BY NV.MaNV;
END
GO

USE PetCareDBOpt
GO

CREATE OR ALTER PROCEDURE sp_BS_GetInfoByMaLSDV
    @MaLSDV NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM LS_DV WHERE MaLSDV = @MaLSDV)
    BEGIN
        THROW 50001, N'Mã Lịch sử dịch vụ không tồn tại.', 1;
    END

    SELECT TOP 1
        L.MaLSDV,
        L.MaKH,
        KH.HoTen_KH,
        TP.MaThuCung,
        TC.TenThuCung,
        TC.LoaiThuCung,
        DATEDIFF(YEAR, TC.NgaySinh_TC, GETDATE()) AS Tuoi,
        TC.TinhTrangSK,
        TP.LoaiVacXin,
        L.TrangThaiGD
    FROM LS_DV L
    LEFT JOIN LS_DVTIEMPHONG TP ON L.MaLSDV = TP.MaLSDVTP
    LEFT JOIN KHACHHANG KH ON L.MaKH = KH.MaKH
    LEFT JOIN THUCUNG TC ON TP.MaThuCung = TC.MaThuCung
    WHERE L.MaLSDV = @MaLSDV
      AND L.TrangThaiGD IN (N'Đã đặt trước', N'Đang sử dụng', N'Chờ thực hiện')
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_BS_HoanTatKham
    @MaLSDV NVARCHAR(10),
    @MaNV NVARCHAR(10)
AS
BEGIN
    UPDATE LS_DVKHAMBENH
    SET BacSiPhuTrach = @MaNV 
    WHERE MaLSDVKB = @MaLSDV

	UPDATE LS_DV
	SET TrangThaiGD = N'Chờ lập hóa đơn'
	WHERE MaLSDV = @MaLSDV

	UPDATE NHANVIEN
	SET TrangThaiNV = N'Rảnh'
	WHERE MaNV = @MaNV
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_HoanTatTiem]
    @MaPhieuTiem INT,  
    @MaNhanVien INT    
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE PHIEUTIEM 
        SET TrangThaiGD = N'Chờ lập hóa đơn'
        WHERE MaPhieuTiem = @MaPhieuTiem;

        UPDATE NHANVIEN
        SET TrangThaiNV = N'Rảnh'
        WHERE MaNV = @MaNhanVien;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE sp_BS_LuuKetQuaTiem
    @MaLSDV NVARCHAR(10),
    @MaBacSi NVARCHAR(10),
    @LoaiVacXin NVARCHAR(8),
    @LieuLuong NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE LS_DVTIEMPHONG
        SET LoaiVacXin = @LoaiVacXin,
            LieuLuong = @LieuLuong
        WHERE MaLSDVTP = @MaLSDV;

        UPDATE LS_DV
        SET TrangThaiGD = N'Chờ lập hóa đơn'
        WHERE MaLSDV = @MaLSDV;

        UPDATE NHANVIEN
        SET TrangThaiNV = N'Rảnh'
        WHERE MaNV = @MaBacSi;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

USE PetCareDB
GO

CREATE OR ALTER PROCEDURE dbo.SP_KH_ThanhToanGioHang
    @MaKH NVARCHAR(10),
    @MaLSGD NVARCHAR(10),
    @HinhThucPay NVARCHAR(50),
    @TongTien INT,
    @MaCN NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM LS_DV WHERE MaLSDV = @MaLSGD)
        BEGIN
            THROW 50001, N'Mã giao dịch giỏ hàng không tồn tại.', 1;
        END

        DECLARE @MaHD NVARCHAR(10);
        DECLARE @NextID INT;

        SELECT @NextID = ISNULL(MAX(TRY_CAST(RIGHT(MaHD, 8) AS INT)), 0) + 1
        FROM HOADON
        WHERE MaHD LIKE 'HD[0-9]%'; 

        SET @MaHD = 'HD' + RIGHT('00000000' + CAST(@NextID AS NVARCHAR), 8);

        INSERT INTO HOADON (MaHD, NgayLap, NV_Lap, TienTruocKM, TienThanhToan, HinhThucPay, TrangThaiHD, CongLoyalty, MaKH, MaCN)
        VALUES (@MaHD, GETDATE(), NULL, @TongTien, @TongTien, @HinhThucPay, N'Đã thanh toán', 0, @MaKH, @MaCN);

        INSERT INTO CT_HOADON (MaHD, MaLSGD, TongPhiDV)
        VALUES (@MaHD, @MaLSGD, @TongTien);

        UPDATE LS_DV 
        SET TrangThaiGD = N'Hoàn thành',
            NgayDatTruoc = GETDATE()
        WHERE MaLSDV = @MaLSGD;

        UPDATE S
        SET S.SLTonKho = S.SLTonKho - CT.SoLuongSP
        FROM CT_SPCN S
        INNER JOIN CT_MUAHANG CT ON S.MaSP = CT.MaSP
        WHERE CT.MaLSDVMH = @MaLSGD AND S.MaCN = @MaCN;

        COMMIT TRANSACTION;
        SELECT @MaHD AS MaHoaDonMoi;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO