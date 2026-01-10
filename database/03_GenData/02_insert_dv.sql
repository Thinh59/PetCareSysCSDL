USE PetCareDB
GO

-- Xóa dữ liệu cũ nếu cần (chỉ chạy khi chưa có ràng buộc khóa ngoại)
DELETE FROM DICHVU;

INSERT INTO DICHVU (MaDichVu, TenDV, GiaTienDV)
VALUES 
('DV001', N'Khám bệnh', 50000),
('DV002', N'Tiêm phòng', 30000),
('DV003', N'Mua hàng', 50000); -- Phí dịch vụ mua hàng (thường là phí vận chuyển hoặc xử lý đơn)
GO