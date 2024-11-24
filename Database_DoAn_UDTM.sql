﻿CREATE DATABASE [DoAnKetMon_UDTM]
use DoAnKetMon_UDTM
CREATE TABLE NhomNguoiDung (
	MaNhomNguoiDung INT PRIMARY KEY IDENTITY(1,1),
	TenNhomNguoiDung NVARCHAR(255) NOT NULL,
)
CREATE TABLE NguoiDung (
    NguoiDungID INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    SoDienThoai NVARCHAR(15),
    DiaChi NVARCHAR(255),
	NgaySinh Date,
    MaNhomNguoiDung INT,
    NgayTao DATETIME DEFAULT GETDATE(),
	GioiTinh NVARCHAR(10) NULL,              
    KichHoat BIT DEFAULT 1,
    FOREIGN KEY (MaNhomNguoiDung) REFERENCES NhomNguoiDung(MaNhomNguoiDung)
);    

CREATE TABLE ManHinh (
    MaManHinh NVARCHAR(50) PRIMARY KEY NOT NULL, -- Mã màn hình để dùng trong ứng dụng
    TenManHinh NVARCHAR(100) NOT NULL
);
CREATE TABLE PhanQuyen (
    MaNhomNguoiDung INT NOT NULL,
    MaManHinh NVARCHAR(50) NOT NULL,
    FOREIGN KEY (MaManHinh) REFERENCES ManHinh(MaManHinh),
	FOREIGN KEY (MaNhomNguoiDung) REFERENCES NhomNguoiDung(MaNhomNguoiDung),
    PRIMARY KEY (MaNhomNguoiDung, MaManHinh)
);
CREATE TABLE ThongTinGiaoHang (
    DiaChiID INT PRIMARY KEY IDENTITY(1,1),
    NguoiDungID INT NOT NULL,
    TenNguoiNhan NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15) NOT NULL,
    DiaChiGiaoHang NVARCHAR(255) NOT NULL,
	DiaChiMacDinh BIT DEFAULT 0, -- Địa chỉ mặc định 1:mặc định/ 0:không phải mặc định
    FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID)
);

CREATE TABLE DanhMuc (
    DanhMucID INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc NVARCHAR(100) NOT NULL
);

CREATE TABLE NhaCungCap (
    NhaCungCapID INT PRIMARY KEY IDENTITY(1,1),
    TenNhaCungCap NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    NgayHopTac DATETIME DEFAULT GETDATE()
);
CREATE TABLE SanPham ( --1 Sản Phẩm Khi Vừa Được Tạo ra bắc buột ít nhất phải có 1 CHI TIẾT SẢN PHẨM được tạo ra cùng
    SanPhamID INT PRIMARY KEY IDENTITY(1,1),
    TenSanPham NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX),
	SoSaoTB int Default 0, --Số sao của sản phẩm, nếu chưa có đánh giá nào thì mặc định là 0
    DanhMucID INT,
	SoLuongDaBan INT DEFAULT 0,
    KichHoat BIT DEFAULT 1, --Sản phẩm còn bán trên web hay không (1:Còn,0:Không)
    FOREIGN KEY (DanhMucID) REFERENCES DanhMuc(DanhMucID)
);
CREATE TABLE NhaCungCapSanPham(
	NhaCungCapID INT,
	SanPhamID INT,
	Primary key (NhaCungCapID,SanPhamID),
    FOREIGN KEY (NhaCungCapID) REFERENCES NhaCungCap(NhaCungCapID),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
);
CREATE TABLE Mau (
    MauID INT PRIMARY KEY IDENTITY(1,1),
    TenMau NVARCHAR(50) NOT NULL
);
CREATE TABLE Size (
    SizeID INT PRIMARY KEY IDENTITY(1,1),
    TenSize NVARCHAR(50) NOT NULL
);
CREATE TABLE ChiTietSanPham (
	ChiTietID INT PRIMARY KEY IDENTITY(1,1),
    SanPhamID INT NOT NULL,
    MauID INT NOT NULL,
    SizeID INT NOT NULL,
	Gia DECIMAL(18, 2) NOT NULL,
	HinhAnhUrl NVARCHAR(255),
    SoLuongTonKho INT NOT NULL,
	KichHoat BIT DEFAULT 1, --Chi tiết của 1 sản phẩm còn bán hay không: 1:có - 0:không
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID),
    FOREIGN KEY (MauID) REFERENCES Mau(MauID),
    FOREIGN KEY (SizeID) REFERENCES Size(SizeID),
    UNIQUE (SanPhamID, MauID, SizeID)
);
CREATE TABLE DonHang (
    DonHangID INT PRIMARY KEY IDENTITY(1,1),
	DiaChiID INT,
	NhanVienID INT,
    NguoiDungID INT NOT NULL,
    TongTien DECIMAL(18, 2) NOT NULL,
    TinhTrangDonHang NVARCHAR(50) NOT NULL, --5 trạng thái: Đang xử lý,Đã Xác Nhận, Đang Vận Chuyển,Hoàn Thành,Đã huỷ:Phía web khi người dùng đặt hàng bên web thì đơn hàng sẽ được tạo với trạng thái [Đang xử lý],khi nhân viên xác nhận đơn thì chuyển thành [Đã Xác Nhận] hoặc [Đã Huỷ nếu] nhân viên Huỷ Đơn,[Đã Xác Nhận] --> [Đang Vận Chuyển] khi nhân viên bấm nút vận chuyển(hàm ý là đã giao cho nhân viên vận chuyển).Còn lại bên web xử lý
    NgayDatHang DATETIME DEFAULT GETDATE(),
	HinhThucThanhToan NVARCHAR(50) NOT NULL,
    TinhTrangThanhToan NVARCHAR(50) NOT NULL,-- Đã Thanh Toán/Chưa Thanh Toán
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID),
    FOREIGN KEY (NhanVienID) REFERENCES NguoiDung(NguoiDungID),
    FOREIGN KEY (DiaChiID) REFERENCES ThongTinGiaoHang(DiaChiID),
);
CREATE TABLE ChiTietDonHang (
	ChiTietDonHangID INT PRIMARY KEY IDENTITY(1,1),
    DonHangID INT NOT NULL,
    SanPhamID INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
	TinhTrangDanhGia int Default 0, --Kiểm Tra Xem Chi Tiết Sản Phẩm trong đơn hàng được đánh giá chưa 0:chưa/1:rồi
    FOREIGN KEY (DonHangID) REFERENCES DonHang(DonHangID),
    FOREIGN KEY (SanPhamID) REFERENCES ChiTietSanPham(ChiTietID)
);
CREATE TABLE PhanHoi (
    PhanHoiID INT PRIMARY KEY IDENTITY(1,1),
    SanPhamID INT NOT NULL,
    NguoiDungID INT NOT NULL,
    NoiDung NVARCHAR(MAX),
    DanhGia INT CHECK(DanhGia BETWEEN 1 AND 5),
    NgayPhanHoi DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID),
    FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID)
);
CREATE TABLE GioHang (
    GioHangID INT PRIMARY KEY IDENTITY(1,1),
    NguoiDungID INT NOT NULL,
    SanPhamID INT NOT NULL,
    SoLuong INT NOT NULL,
    FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID),
    FOREIGN KEY (SanPhamID) REFERENCES ChiTietSanPham(ChiTietID)
);
-- Thêm dữ liệu vào bảng DanhMuc
INSERT INTO DanhMuc (TenDanhMuc)
VALUES 
    (N'Áo Thun'),
    (N'Quần Jean');

-- Thêm dữ liệu vào bảng NhaCungCap
INSERT INTO NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, MoTa)
VALUES
    (N'Công ty ABC', N'123 Đường A, Quận 1, TP.HCM', N'0901234567', N'abc@company.com', N'Nhà cung cấp quần áo thời trang'),
    (N'Công ty XYZ', N'456 Đường B, Quận 2, TP.HCM', N'0902345678', N'xyz@company.com', N'Nhà cung cấp phụ kiện thời trang');

-- Thêm dữ liệu vào bảng SanPham
INSERT INTO SanPham (TenSanPham, MoTa, DanhMucID, KichHoat)
VALUES
    (N'Áo thun', N'Áo thun cổ tròn', 1, 1),
    (N'Quần jean', N'Quần jean nam', 2, 1)
-- Thêm dữ liệu vào bảng NhomNguoiDung
INSERT INTO NhomNguoiDung (TenNhomNguoiDung)
VALUES 
    (N'Khách hàng'),
    (N'Nhân viên'),
    (N'Quản lý');

-- Thêm dữ liệu vào bảng Mau
INSERT INTO Mau (TenMau)
VALUES 
    (N'Đỏ'),
    (N'Xanh dương'),
    (N'Xanh lá'),
    (N'Vàng'),
    (N'Tím'),
    (N'Cam'),
    (N'Hồng'),
    (N'Nâu'),
    (N'Xám'),
    (N'Trắng'),
    (N'Đen');

-- Thêm dữ liệu vào bảng Size
INSERT INTO Size (TenSize)
VALUES 
    (N'S'),
    (N'M'),
    (N'L'),
    (N'XL'),
    (N'XXL');

-- Thêm dữ liệu vào bảng ManHinh
INSERT INTO ManHinh (MaManHinh, TenManHinh)
VALUES 
    (N'MH001', N'Màn hình chính'),
    (N'MH002', N'Màn hình loại sản phẩm'),
    (N'MH003', N'Màn hình sản phẩm'),
    (N'MH004', N'Màn hình nhà cung cấp'),
	(N'MH005', N'Màn hình đơn hàng'),
    (N'MH006', N'Màn hình tài khoản'),
    (N'MH007', N'Màn hình thống kê');

-- Thêm dữ liệu vào bảng PhanQuyen
INSERT INTO PhanQuyen (MaNhomNguoiDung, MaManHinh)
VALUES 
    (3, N'MH001'),
    (3, N'MH002'),
    (3, N'MH003'),
    (3, N'MH004'),
    (3, N'MH005'),
    (3, N'MH006'),
	(3, N'MH007'),
    (2, N'MH001'),
    (2, N'MH002'),
	(2, N'MH003'),
    (2, N'MH004'),
    (2, N'MH005');

INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, DiaChi, NgaySinh, MaNhomNguoiDung, GioiTinh, KichHoat)
VALUES
    (N'khachhang1', N'password1', N'Nguyen Van A', N'khachhang1@example.com', N'0912345678', N'123 Street A', '1990-01-01', 1, N'Nam', 1),
    (N'khachhang2', N'password2', N'Le Thi B', N'khachhang2@example.com', N'0922345678', N'456 Street B', '1991-02-02', 1, N'Nữ', 1),
    (N'nhanvien1', N'password3', N'Tran Van C', N'nhanvien1@example.com', N'0932345678', N'789 Street C', '1988-03-03', 2, N'Nam', 1),
    (N'nhanvien2', N'password4', N'Pham Thi D', N'nhanvien2@example.com', N'0942345678', N'101 Street D', '1992-04-04', 2, N'Nữ', 1),
    (N'nhanvien3', N'password5', N'Hoang Van E', N'nhanvien3@example.com', N'0952345678', N'202 Street E', '1993-05-05', 2, N'Nam', 1),
    (N'quanly1', N'password6', N'Vo Thi F', N'quanly1@example.com', N'0962345678', N'303 Street F', '1985-06-06', 3, N'Nữ', 1),
    (N'quanly2', N'password7', N'Dang Van G', N'quanly2@example.com', N'0972345678', N'404 Street G', '1987-07-07', 3, N'Nam', 1);

	-- Thêm dữ liệu vào bảng ChiTietSanPham
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, HinhAnhUrl, SoLuongTonKho)
VALUES
    (1, 1, 1, 150000, N'hinh1.jpg', 100),  -- Áo thun , Màu Đỏ, Size S
    (1, 2, 2, 160000, N'hinh2.jpg', 200),  -- Áo thun , Màu Xanh dương, Size M
    (2, 7, 2, 220000, N'hinh3.jpg', 90),  -- Quần jean, Màu Hồng, Size M
    (2, 8, 1, 230000, N'hinh4.jpg', 80)  -- Quần jean, Màu Nâu, Size S
    

-- Insert vào đơn hàng.

INSERT [dbo].[DonHang] ([DiaChiID], [NhanVienID], [NguoiDungID], [TongTien], [TinhTrangDonHang], [NgayDatHang], [HinhThucThanhToan], [TinhTrangThanhToan], [NgayThanhToan]) VALUES (NULL, NULL, 6, CAST(1510000.00 AS Decimal(18, 2)), N'Hoàn Thành', CAST(N'2024-11-24 16:04:59.783' AS DateTime), N'Tiền mặt', N'Đã thanh toán', CAST(N'2024-11-24 16:04:59.783' AS DateTime))
INSERT [dbo].[DonHang] ([DiaChiID], [NhanVienID], [NguoiDungID], [TongTien], [TinhTrangDonHang], [NgayDatHang], [HinhThucThanhToan], [TinhTrangThanhToan], [NgayThanhToan]) VALUES (NULL, NULL, 6, CAST(690000.00 AS Decimal(18, 2)), N'Hoàn Thành', CAST(N'2024-11-24 16:05:08.440' AS DateTime), N'Tiền mặt', N'Đã thanh toán', CAST(N'2024-11-24 16:05:08.440' AS DateTime))
INSERT [dbo].[DonHang] ([DiaChiID], [NhanVienID], [NguoiDungID], [TongTien], [TinhTrangDonHang], [NgayDatHang], [HinhThucThanhToan], [TinhTrangThanhToan], [NgayThanhToan]) VALUES (NULL, NULL, 6, CAST(1830000.00 AS Decimal(18, 2)), N'Hoàn Thành', CAST(N'2024-11-24 16:05:45.167' AS DateTime), N'Tiền mặt', N'Đã thanh toán', CAST(N'2024-11-24 16:05:45.167' AS DateTime))

-- Vừa insert vào 3 đơn hàng, xem coi 3 đơn hàng đó có id phải là 61, 62, 63 không, nếu không đúng sửa lại
-- Đơn Số 1 có 2 Chi Tiết
-- Đơn Số 2 có 1 Chi Tiết
-- Đơn Số 3 có 3 Chi Tiết

INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (61, 1, 7, CAST(150000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (61, 4, 2, CAST(230000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (62, 4, 3, CAST(230000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (63, 4, 3, CAST(230000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (63, 3, 3, CAST(220000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (63, 2, 3, CAST(160000.00 AS Decimal(18, 2)), 0)


-- Dữ liệu TEST bảng DonHang
 INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES ( 6, 390000, N'Hoàn Thành', '2024-11-17 15:26:03', N'Tiền mặt', N'Đã thanh toán', '2024-11-17 15:26:03');

-- Đang Xử Lý, Đã thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 500000, N'Đang Xử Lý', '2024-11-18 10:00:00', N'Tiền mặt', N'Đã thanh toán', '2024-11-18 12:30:00');

-- Đã Xác Nhận, Đã thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 600000, N'Đã Xác Nhận', '2024-11-18 11:15:00', N'Chuyển khoản', N'Đã thanh toán', '2024-11-18 14:00:00');

-- Đang Vận Chuyển, Đã thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 750000, N'Đang Vận Chuyển', '2024-11-19 09:00:00', N'Tiền mặt', N'Đã thanh toán', '2024-11-19 12:15:00');

-- Hoàn Thành, Đã thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 900000, N'Hoàn Thành', '2024-11-19 13:00:00', N'Chuyển khoản', N'Đã thanh toán', '2024-11-19 15:45:00');

-- Đã Hủy, Chưa thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 0, N'Đã Hủy', '2024-11-20 10:00:00', N'Tiền mặt', N'Chưa thanh toán', NULL);

-- Đang Xử Lý, Chưa thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 450000, N'Đang Xử Lý', '2024-11-20 11:30:00', N'Tiền mặt', N'Chưa thanh toán', NULL);

-- Đã Xác Nhận, Chưa thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 550000, N'Đã Xác Nhận', '2024-11-20 12:15:00', N'Chuyển khoản', N'Chưa thanh toán', NULL);

-- Đang Vận Chuyển, Chưa thanh toán
INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
VALUES (6, 300000, N'Đang Vận Chuyển', '2024-11-20 14:00:00', N'Tiền mặt', N'Chưa thanh toán', NULL);
