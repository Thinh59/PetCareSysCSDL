# 🐾 PetCare Management System

**Hệ thống Quản lý Phòng khám Thú y & Cửa hàng Thú cưng**

![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/Platform-.NET_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

---

## 📖 Giới thiệu (Introduction)

**PetCare** là giải pháp phần mềm toàn diện được xây dựng trên nền tảng **Windows Forms (C#)**, phục vụ cho các **phòng khám thú y tích hợp cửa hàng bán lẻ**.

Hệ thống giải quyết bài toán quản lý quy trình khép kín:
- Đặt lịch khám
- Khám chữa bệnh / tiêm phòng
- Mua sắm sản phẩm
- Thanh toán & tích điểm
- Quản lý chi nhánh, công ty

Dự án áp dụng:
- Kiến trúc **3-Layer Architecture**
- **SQL Server + Stored Procedures**
---

## 🌟 Chức năng hệ thống (System Features)

Các chức năng dưới đây được xây dựng dựa trên **đề bài môn CSC12002 – Cơ Sở Dữ Liệu Nâng Cao**, đảm bảo đáp ứng **yêu cầu cài đặt ứng dụng tối thiểu** và mở rộng thêm các chức năng nâng cao phục vụ phân tích – thiết kế CSDL ở mức vật lý.

---

## ✅ I. Chức năng tối thiểu (Theo yêu cầu đề bài)

### 👤 1. Khách hàng (Customer)
- Tìm kiếm sản phẩm theo tên, loại, giá.
- Đặt mua sản phẩm (thức ăn, thuốc, phụ kiện).
- Đặt lịch khám cho thú cưng tại chi nhánh.
- Tra cứu lịch làm việc của bác sĩ.
- Xem lịch sử mua hàng của khách hàng.
- Xem lịch sử khám chữa bệnh / tiêm phòng của từng thú cưng.

---

### 🩺 2. Bác sĩ (Doctor)
- Tra cứu hồ sơ thú cưng (thông tin cơ bản, tình trạng sức khỏe).
- Xem lịch sử khám bệnh của thú cưng.
- Tra cứu thuốc / vắc-xin.
- Tạo bệnh án mới cho thú cưng.
- Kê toa thuốc, ghi chẩn đoán và chỉ định tái khám.
- Ghi nhận thông tin tiêm phòng (loại vắc-xin, liều lượng, ngày tiêm).

---

### 🧾 3. Nhân viên (Staff / Lễ tân)
- Tạo lịch khám trực tiếp cho khách hàng đến tại quầy.
- Tra cứu thú cưng để xác định khách hàng mới / khách hàng cũ.
- Lập hóa đơn cho dịch vụ và sản phẩm.
- Hỗ trợ khách hàng trong quá trình thanh toán.

---

### 📊 4. Quản lý (Manager)
- Thống kê doanh thu phòng khám theo thời gian.
- Thống kê doanh thu theo bác sĩ.
- Thống kê số lượt khám theo bác sĩ / chi nhánh.
- Thống kê doanh thu bán sản phẩm.
- Thống kê doanh thu toàn hệ thống và từng chi nhánh.

---

## ⭐ II. Chức năng nổi bật (Mở rộng – nâng cao)

### 🛒 1. Quản lý giỏ hàng & thanh toán
- Giỏ hàng thông minh: tự động tính tổng tiền khi thêm/xóa sản phẩm.
- Kiểm tra tồn kho theo từng chi nhánh (`MaCN`).
- Thanh toán giỏ hàng và thanh toán dịch vụ nợ cũ.
- Hỗ trợ nhiều hình thức thanh toán.
- Áp dụng khuyến mãi, voucher và điểm tích lũy.

---

### 🎯 2. Loyalty & Hội viên
- Quản lý cấp độ thành viên: Cơ bản – Thân thiết – VIP.
- Tự động cộng điểm loyalty khi thanh toán  
  (1 điểm = 50.000 VNĐ).
- Tự động xét nâng / giữ hạng thành viên theo tổng chi tiêu năm.

---

### 💉 3. Quản lý tiêm phòng nâng cao
- Quản lý tiêm phòng lẻ và gói tiêm (6 tháng, 12 tháng).
- Cho phép khách hàng chọn các mũi tiêm trong gói.
- Áp dụng ưu đãi giảm giá theo chính sách gói tiêm.
- Tra cứu lịch sử tiêm chủng theo thú cưng.

---

### 🏥 4. Quản lý nhân sự & hiệu suất
- Quản lý nhân viên theo chi nhánh.
- Lưu lịch sử điều động nhân sự giữa các chi nhánh.
- Thống kê hiệu suất nhân viên:
  - Số lượt khám / đơn hàng xử lý.
  - Điểm đánh giá từ khách hàng.

---

### 📦 5. Thống kê & truy vấn nâng cao
- Doanh thu theo ngày / tháng / quý / năm.
- Thống kê vắc-xin được sử dụng nhiều nhất.
- Thống kê số lượng thú cưng theo loài, giống.
- Tra cứu khách hàng lâu chưa quay lại.
- Quản lý tồn kho sản phẩm bán lẻ.

---

### 🔐 6. An toàn dữ liệu & hiệu năng
- Xử lý thanh toán bằng Transaction đảm bảo toàn vẹn dữ liệu:
  - Trừ tiền
  - Trừ kho
  - Cập nhật hóa đơn
  - Cộng điểm khách hàng
- Áp dụng Stored Procedure, Index, Partition để tối ưu truy vấn.
- Phù hợp minh họa phân tích tần suất truy vấn ở mức vật lý.

---

📌 **Các chức năng trên được lựa chọn để phục vụ:**
- Thiết kế CSDL mức quan niệm – logic – vật lý  
- Phân tích tần suất truy vấn  
- Minh họa hiệu quả của Index / Partition / Stored Procedure  
- Cài đặt ứng dụng WinForms mô phỏng hệ thống thực tế
---

## 🛠️ Cài đặt & Hướng dẫn (Installation)

### 1. Clone dự án
~~~bash
git clone https://github.com/YourUsername/PetCare-Project.git
~~~

### 2. Cấu hình Database
- Mở **SQL Server Management Studio**
- Chạy file:
~~~sql
Database/PetCareDB.sql
~~~

### 3. Kết nối Database
- Mở `App.config`
- Cập nhật `ConnectionString` phù hợp với máy của bạn

### 4. Chạy ứng dụng
- Mở Solution bằng **Visual Studio**
- Nhấn **F5**
---

## 📂 Cấu trúc thư mục

## 📂 Cấu trúc thư mục (Project Structure)

Dự án được tổ chức theo hướng tách biệt rõ ràng giữa **Source Code** và **Database Script**, phục vụ cho việc phân tích – thiết kế – tối ưu CSDL theo yêu cầu môn **CSC12002 – Cơ Sở Dữ Liệu Nâng Cao**.

---

### 📁 Source Code

Source/
└── PetCare/
├── PetCare.sln # Solution chính của dự án
└── PetCare/ # Project WinForms (C#)
├── DAO/ # Data Access Layer
│ ├── DataProvider.cs # Kết nối & thực thi SQL
│ └── ServiceDAL.cs # Giao tiếp Stored Procedure
├── DTO/ # Data Transfer Objects
├── BUS/ # Business Logic Layer
├── GUI/ # Presentation Layer (WinForms)
│ ├── KhachHang/ # Giao diện Khách hàng
│ │ └── UC/ # User Controls (Giỏ hàng, Thanh toán…)
│ ├── BacSi/ # Giao diện Bác sĩ
│ ├── NhanVien/ # Giao diện Nhân viên
│ └── QuanLy/ # Giao diện Quản lý
├── Resources/ # Hình ảnh, icon
└── App.config # Cấu hình chuỗi kết nối CSDL

---

### 🗄️ Database

database/
├── 03_GenData/ # Script phát sinh dữ liệu kiểm thử
└── database_script_sql/ # Script thiết kế CSDL mức vật lý
├── 00_CreateDatabase.sql # Tạo Database
├── 01_CreateTables.sql # Tạo bảng & ràng buộc
├── 02_CreateIndexs.sql # Chỉ mục tối ưu truy vấn
├── 04_Partition.sql # Partition dữ liệu (theo thời gian/chi nhánh)
├── 05_Procedures.sql # Stored Procedures
└── 06_Query.sql # Các truy vấn thống kê & báo cáo

---

## 👨‍💻 Tác giả

23122019 Phan Huỳnh Châu Thịnh
23122029 Nguyễn Trọng Hòa
23120079 Phạm Thúy Quy
23120080 Nguyễn Ngọc Như Quỳnh

---

⭐ Nếu bạn thấy dự án hữu ích, hãy cho repository một **Star**!
