CREATE DATABASE [DoAnKetMon_UDTM]
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

-- Thêm dữ liệu vào bảng NhaCungCap
INSERT INTO NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, MoTa)
VALUES
    (N'Công ty ABC', N'123 Đường A, Quận 1, TP.HCM', N'0901234567', N'abc@company.com', N'Nhà cung cấp quần áo thời trang'),
    (N'Công ty XYZ', N'456 Đường B, Quận 2, TP.HCM', N'0902345678', N'xyz@company.com', N'Nhà cung cấp phụ kiện thời trang');

-- Thêm dữ liệu vào bảng NhomNguoiDung
INSERT INTO NhomNguoiDung (TenNhomNguoiDung)
VALUES 
    (N'Khách hàng'),
    (N'Nhân viên'),
    (N'Quản lý');

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


-- Thêm dữ liệu vào bảng DanhMuc
INSERT INTO DanhMuc (TenDanhMuc)
VALUES 
    (N'Áo Thun'),
    (N'Quần Jean'),
    (N'Áo Khoác'),
    (N'Váy'),
    (N'Giày'),
    (N'Túi Xách'),
    (N'Nón'),
    (N'Kính Mát'),
    (N'Phụ Kiện'),
    (N'Đồng Hồ');

	-- Thêm dữ liệu vào bảng SanPham
INSERT INTO SanPham (TenSanPham, MoTa, DanhMucID, KichHoat)
VALUES
    (N'Áo thun cổ tròn', N'Áo thun cổ tròn chất liệu cotton', 1, 1),
    (N'Áo thun cổ tim', N'Áo thun cổ tim chất liệu cotton', 1, 1),
    (N'Quần jean nam', N'Quần jean nam phong cách', 2, 1),
    (N'Quần jean nữ', N'Quần jean nữ thời trang', 2, 1),
    (N'Áo khoác da', N'Áo khoác da cao cấp', 3, 1),
    (N'Áo khoác nỉ', N'Áo khoác nỉ ấm áp', 3, 1),
    (N'Váy dạ hội', N'Váy dạ hội sang trọng', 4, 1),
    (N'Váy công sở', N'Váy công sở thanh lịch', 4, 1),
    (N'Giày thể thao', N'Giày thể thao năng động', 5, 1),
    (N'Giày cao gót', N'Giày cao gót quyến rũ', 5, 1),
    (N'Túi xách da', N'Túi xách da thời trang', 6, 1),
    (N'Túi xách vải', N'Túi xách vải tiện dụng', 6, 1),
    (N'Nón lưỡi trai', N'Nón lưỡi trai cá tính', 7, 1),
    (N'Nón rộng vành', N'Nón rộng vành đi biển', 7, 1),
    (N'Kính mát thời trang', N'Kính mát thời trang', 8, 1),
    (N'Kính mát thể thao', N'Kính mát thể thao', 8, 1),
    (N'Dây chuyền', N'Dây chuyền bạc', 9, 1),
    (N'Bông tai', N'Bông tai vàng', 9, 1),
    (N'Đồng hồ nam', N'Đồng hồ nam cao cấp', 10, 1),
    (N'Đồng hồ nữ', N'Đồng hồ nữ thời trang', 10, 1);

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

-- Thêm dữ liệu vào bảng ChiTietSanPham
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, HinhAnhUrl, SoLuongTonKho)
VALUES
    (1, 1, 1, 150000, N'hinh1.jpg', 100),  -- Áo thun cổ tròn, Màu Đỏ, Size S
    (1, 2, 2, 160000, N'hinh2.jpg', 200),  -- Áo thun cổ tròn, Màu Xanh dương, Size M
    (2, 3, 3, 170000, N'hinh3.jpg', 150),  -- Áo thun cổ tim, Màu Xanh lá, Size L
    (2, 4, 4, 180000, N'hinh4.jpg', 120),  -- Áo thun cổ tim, Màu Vàng, Size XL
    (3, 5, 1, 220000, N'hinh1.jpg', 90),   -- Quần jean nam, Màu Tím, Size S
    (3, 6, 2, 230000, N'hinh2.jpg', 80),   -- Quần jean nam, Màu Cam, Size M
    (4, 7, 3, 240000, N'hinh3.jpg', 70),   -- Quần jean nữ, Màu Hồng, Size L
    (4, 8, 4, 250000, N'hinh4.jpg', 60),   -- Quần jean nữ, Màu Nâu, Size XL
    (5, 9, 1, 300000, N'hinh1.jpg', 50),   -- Áo khoác da, Màu Xám, Size S
    (5, 10, 2, 310000, N'hinh2.jpg', 40),  -- Áo khoác da, Màu Trắng, Size M
    (6, 11, 3, 320000, N'hinh3.jpg', 30),  -- Áo khoác nỉ, Màu Đen, Size L
    (6, 1, 4, 330000, N'hinh4.jpg', 20),   -- Áo khoác nỉ, Màu Đỏ, Size XL
    (7, 2, 1, 400000, N'hinh1.jpg', 10),   -- Váy dạ hội, Màu Xanh dương, Size S
    (7, 3, 2, 410000, N'hinh2.jpg', 15),   -- Váy dạ hội, Màu Xanh lá, Size M
    (8, 4, 3, 420000, N'hinh3.jpg', 25),   -- Váy công sở, Màu Vàng, Size L
    (8, 5, 4, 430000, N'hinh4.jpg', 35),   -- Váy công sở, Màu Tím, Size XL
    (9, 6, 1, 500000, N'hinh1.jpg', 45),   -- Giày thể thao, Màu Cam, Size S
    (9, 7, 2, 510000, N'hinh2.jpg', 55),   -- Giày thể thao, Màu Hồng, Size M
    (10, 8, 3, 520000, N'hinh3.jpg', 65),  -- Giày cao gót, Màu Nâu, Size L
    (10, 9, 4, 530000, N'hinh4.jpg', 75),  -- Giày cao gót, Màu Xám, Size XL
    (11, 10, 1, 600000, N'hinh1.jpg', 85), -- Túi xách da, Màu Trắng, Size S
    (11, 11, 2, 610000, N'hinh2.jpg', 95), -- Túi xách da, Màu Đen, Size M
    (12, 1, 3, 620000, N'hinh3.jpg', 105), -- Túi xách vải, Màu Đỏ, Size L
    (12, 2, 4, 630000, N'hinh4.jpg', 115), -- Túi xách vải, Màu Xanh dương, Size XL
    (13, 3, 1, 700000, N'hinh1.jpg', 125), -- Nón lưỡi trai, Màu Xanh lá, Size S
    (13, 4, 2, 710000, N'hinh2.jpg', 135), -- Nón lưỡi trai, Màu Vàng, Size M
    (14, 5, 3, 720000, N'hinh3.jpg', 145), -- Nón rộng vành, Màu Tím, Size L
    (14, 6, 4, 730000, N'hinh4.jpg', 155), -- Nón rộng vành, Màu Cam, Size XL
    (15, 7, 1, 800000, N'hinh1.jpg', 165), -- Kính mát thời trang, Màu Hồng, Size S
    (15, 8, 2, 810000, N'hinh2.jpg', 175), -- Kính mát thời trang, Màu Nâu, Size M
    (16, 9, 3, 820000, N'hinh3.jpg', 185), -- Kính mát thể thao, Màu Xám, Size L
    (16, 10, 4, 830000, N'hinh4.jpg', 195),-- Kính mát thể thao, Màu Trắng, Size XL
    (17, 11, 1, 900000, N'hinh1.jpg', 205), -- Dây chuyền, Màu Đen, Size S
    (17, 1, 2, 910000, N'hinh2.jpg', 215),  -- Dây chuyền, Màu Đỏ, Size M
    (18, 2, 3, 920000, N'hinh3.jpg', 225),  -- Bông tai, Màu Xanh dương, Size L
    (18, 3, 4, 930000, N'hinh4.jpg', 235),  -- Bông tai, Màu Xanh lá, Size XL
    (19, 4, 1, 1000000, N'hinh1.jpg', 245), -- Đồng hồ nam, Màu Vàng, Size S
    (19, 5, 2, 1010000, N'hinh2.jpg', 255), -- Đồng hồ nam, Màu Tím, Size M
    (20, 6, 3, 1020000, N'hinh3.jpg', 265), -- Đồng hồ nữ, Màu Cam, Size L
    (20, 7, 4, 1030000, N'hinh4.jpg', 275); -- Đồng hồ nữ, Màu Hồng, Size XL

-- Thêm dữ liệu vào bảng ChiTietSanPham
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, HinhAnhUrl, SoLuongTonKho)
VALUES
    (1, 3, 1, 150000, N'hinh1.jpg', 110),  -- Áo thun cổ tròn, Màu Xanh lá, Size S
    (1, 4, 2, 160000, N'hinh2.jpg', 210),  -- Áo thun cổ tròn, Màu Vàng, Size M
    (2, 5, 3, 170000, N'hinh3.jpg', 160),  -- Áo thun cổ tim, Màu Tím, Size L
    (2, 6, 4, 180000, N'hinh4.jpg', 130),  -- Áo thun cổ tim, Màu Cam, Size XL
    (3, 7, 1, 220000, N'hinh1.jpg', 95),   -- Quần jean nam, Màu Hồng, Size S
    (3, 8, 2, 230000, N'hinh2.jpg', 85),   -- Quần jean nam, Màu Nâu, Size M
    (4, 9, 3, 240000, N'hinh3.jpg', 75),   -- Quần jean nữ, Màu Xám, Size L
    (4, 10, 4, 250000, N'hinh4.jpg', 65),  -- Quần jean nữ, Màu Trắng, Size XL
    (5, 11, 1, 300000, N'hinh1.jpg', 55),  -- Áo khoác da, Màu Đen, Size S
    (5, 1, 2, 310000, N'hinh2.jpg', 45),   -- Áo khoác da, Màu Đỏ, Size M
    (6, 2, 3, 320000, N'hinh3.jpg', 35),   -- Áo khoác nỉ, Màu Xanh dương, Size L
    (6, 3, 4, 330000, N'hinh4.jpg', 25),   -- Áo khoác nỉ, Màu Xanh lá, Size XL
    (7, 4, 1, 400000, N'hinh1.jpg', 15),   -- Váy dạ hội, Màu Vàng, Size S
    (7, 5, 2, 410000, N'hinh2.jpg', 20),   -- Váy dạ hội, Màu Tím, Size M
    (8, 6, 3, 420000, N'hinh3.jpg', 30),   -- Váy công sở, Màu Cam, Size L
    (8, 7, 4, 430000, N'hinh4.jpg', 40),   -- Váy công sở, Màu Hồng, Size XL
    (9, 8, 1, 500000, N'hinh1.jpg', 50),   -- Giày thể thao, Màu Nâu, Size S
    (9, 9, 2, 510000, N'hinh2.jpg', 60),   -- Giày thể thao, Màu Xám, Size M
    (10, 10, 3, 520000, N'hinh3.jpg', 70), -- Giày cao gót, Màu Trắng, Size L
    (10, 11, 4, 530000, N'hinh4.jpg', 80), -- Giày cao gót, Màu Đen, Size XL
    (11, 1, 1, 600000, N'hinh1.jpg', 90),  -- Túi xách da, Màu Đỏ, Size S
    (11, 2, 2, 610000, N'hinh2.jpg', 100), -- Túi xách da, Màu Xanh dương, Size M
    (12, 3, 3, 620000, N'hinh3.jpg', 110), -- Túi xách vải, Màu Xanh lá, Size L
    (12, 4, 4, 630000, N'hinh4.jpg', 120), -- Túi xách vải, Màu Vàng, Size XL
    (13, 5, 1, 700000, N'hinh1.jpg', 130), -- Nón lưỡi trai, Màu Tím, Size S
    (13, 6, 2, 710000, N'hinh2.jpg', 140), -- Nón lưỡi trai, Màu Cam, Size M
    (14, 7, 3, 720000, N'hinh3.jpg', 150), -- Nón rộng vành, Màu Hồng, Size L
    (14, 8, 4, 730000, N'hinh4.jpg', 160), -- Nón rộng vành, Màu Nâu, Size XL
    (15, 9, 1, 800000, N'hinh1.jpg', 170), -- Kính mát thời trang, Màu Xám, Size S
    (15, 10, 2, 810000, N'hinh2.jpg', 180),-- Kính mát thời trang, Màu Trắng, Size M
    (16, 11, 3, 820000, N'hinh3.jpg', 190),-- Kính mát thể thao, Màu Đen, Size L
    (16, 1, 4, 830000, N'hinh4.jpg', 200), -- Kính mát thể thao, Màu Đỏ, Size XL
    (17, 2, 1, 900000, N'hinh1.jpg', 210), -- Dây chuyền, Màu Xanh dương, Size S
    (17, 3, 2, 910000, N'hinh2.jpg', 220), -- Dây chuyền, Màu Xanh lá, Size M
    (18, 4, 3, 920000, N'hinh3.jpg', 230), -- Bông tai, Màu Vàng, Size L
    (18, 5, 4, 930000, N'hinh4.jpg', 240), -- Bông tai, Màu Tím, Size XL
    (19, 6, 1, 1000000, N'hinh1.jpg', 250),-- Đồng hồ nam, Màu Cam, Size S
    (19, 7, 2, 1010000, N'hinh2.jpg', 260),-- Đồng hồ nam, Màu Hồng, Size M
    (20, 8, 3, 1020000, N'hinh3.jpg', 270),-- Đồng hồ nữ, Màu Nâu, Size L
    (20, 9, 4, 1030000, N'hinh4.jpg', 280);-- Đồng hồ nữ, Màu Xám, Size XL

-- Thêm dữ liệu vào bảng ChiTietSanPham
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, HinhAnhUrl, SoLuongTonKho)
VALUES
    (1, 5, 1, 150000, N'hinh1.jpg', 115),  -- Áo thun cổ tròn, Màu Tím, Size S
    (1, 6, 2, 160000, N'hinh2.jpg', 215),  -- Áo thun cổ tròn, Màu Cam, Size M
    (2, 7, 3, 170000, N'hinh3.jpg', 165),  -- Áo thun cổ tim, Màu Hồng, Size L
    (2, 8, 4, 180000, N'hinh4.jpg', 135),  -- Áo thun cổ tim, Màu Nâu, Size XL
    (3, 9, 1, 220000, N'hinh1.jpg', 100),  -- Quần jean nam, Màu Xám, Size S
    (3, 10, 2, 230000, N'hinh2.jpg', 90),  -- Quần jean nam, Màu Trắng, Size M
    (4, 11, 3, 240000, N'hinh3.jpg', 80),  -- Quần jean nữ, Màu Đen, Size L
    (4, 1, 4, 250000, N'hinh4.jpg', 70),   -- Quần jean nữ, Màu Đỏ, Size XL
    (5, 2, 1, 300000, N'hinh1.jpg', 60),   -- Áo khoác da, Màu Xanh dương, Size S
    (5, 3, 2, 310000, N'hinh2.jpg', 50),   -- Áo khoác da, Màu Xanh lá, Size M
    (6, 4, 3, 320000, N'hinh3.jpg', 40),   -- Áo khoác nỉ, Màu Vàng, Size L
    (6, 5, 4, 330000, N'hinh4.jpg', 30),   -- Áo khoác nỉ, Màu Tím, Size XL
    (7, 6, 1, 400000, N'hinh1.jpg', 20),   -- Váy dạ hội, Màu Cam, Size S
    (7, 7, 2, 410000, N'hinh2.jpg', 25),   -- Váy dạ hội, Màu Hồng, Size M
    (8, 8, 3, 420000, N'hinh3.jpg', 35),   -- Váy công sở, Màu Nâu, Size L
    (8, 9, 4, 430000, N'hinh4.jpg', 45),   -- Váy công sở, Màu Xám, Size XL
    (9, 10, 1, 500000, N'hinh1.jpg', 55),  -- Giày thể thao, Màu Trắng, Size S
    (9, 11, 2, 510000, N'hinh2.jpg', 65),  -- Giày thể thao, Màu Đen, Size M
    (10, 1, 3, 520000, N'hinh3.jpg', 75),  -- Giày cao gót, Màu Đỏ, Size L
    (10, 2, 4, 530000, N'hinh4.jpg', 85),  -- Giày cao gót, Màu Xanh dương, Size XL
    (11, 3, 1, 600000, N'hinh1.jpg', 95),  -- Túi xách da, Màu Xanh lá, Size S
    (11, 4, 2, 610000, N'hinh2.jpg', 105), -- Túi xách da, Màu Vàng, Size M
    (12, 5, 3, 620000, N'hinh3.jpg', 115), -- Túi xách vải, Màu Tím, Size L
    (12, 6, 4, 630000, N'hinh4.jpg', 125), -- Túi xách vải, Màu Cam, Size XL
    (13, 7, 1, 700000, N'hinh1.jpg', 135), -- Nón lưỡi trai, Màu Hồng, Size S
    (13, 8, 2, 710000, N'hinh2.jpg', 145), -- Nón lưỡi trai, Màu Nâu, Size M
    (14, 9, 3, 720000, N'hinh3.jpg', 155), -- Nón rộng vành, Màu Xám, Size L
    (14, 10, 4, 730000, N'hinh4.jpg', 165),-- Nón rộng vành, Màu Trắng, Size XL
    (15, 11, 1, 800000, N'hinh1.jpg', 175),-- Kính mát thời trang, Màu Đen, Size S
    (15, 1, 2, 810000, N'hinh2.jpg', 185), -- Kính mát thời trang, Màu Đỏ, Size M
    (16, 2, 3, 820000, N'hinh3.jpg', 195), -- Kính mát thể thao, Màu Xanh dương, Size L
    (16, 3, 4, 830000, N'hinh4.jpg', 205), -- Kính mát thể thao, Màu Xanh lá, Size XL
    (17, 4, 1, 900000, N'hinh1.jpg', 215), -- Dây chuyền, Màu Vàng, Size S
    (17, 5, 2, 910000, N'hinh2.jpg', 225), -- Dây chuyền, Màu Tím, Size M
    (18, 6, 3, 920000, N'hinh3.jpg', 235), -- Bông tai, Màu Cam, Size L
    (18, 7, 4, 930000, N'hinh4.jpg', 245), -- Bông tai, Màu Hồng, Size XL
    (19, 8, 1, 1000000, N'hinh1.jpg', 255),-- Đồng hồ nam, Màu Nâu, Size S
    (19, 9, 2, 1010000, N'hinh2.jpg', 265),-- Đồng hồ nam, Màu Xám, Size M
    (20, 10, 3, 1020000, N'hinh3.jpg', 275),-- Đồng hồ nữ, Màu Trắng, Size L
    (20, 11, 4, 1030000, N'hinh4.jpg', 285);-- Đồng hồ nữ, Màu Đen, Size XL

-- Thêm dữ liệu vào bảng ChiTietSanPham
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, HinhAnhUrl, SoLuongTonKho)
VALUES
    (1, 7, 5, 170000, N'hinh1.jpg', 300),  -- Áo thun cổ tròn, Màu Hồng, Size XXL
    (2, 9, 5, 190000, N'hinh2.jpg', 290),  -- Áo thun cổ tim, Màu Xám, Size XXL
    (3, 10, 5, 240000, N'hinh3.jpg', 280), -- Quần jean nam, Màu Trắng, Size XXL
    (4, 11, 5, 260000, N'hinh4.jpg', 270), -- Quần jean nữ, Màu Đen, Size XXL
    (5, 2, 5, 320000, N'hinh1.jpg', 260),  -- Áo khoác da, Màu Xanh dương, Size XXL
    (6, 4, 5, 350000, N'hinh2.jpg', 250),  -- Áo khoác nỉ, Màu Vàng, Size XXL
    (7, 6, 5, 430000, N'hinh3.jpg', 240),  -- Váy dạ hội, Màu Cam, Size XXL
    (8, 8, 5, 450000, N'hinh4.jpg', 230),  -- Váy công sở, Màu Nâu, Size XXL
    (9, 11, 5, 550000, N'hinh1.jpg', 220), -- Giày thể thao, Màu Đen, Size XXL
    (10, 1, 5, 550000, N'hinh2.jpg', 210), -- Giày cao gót, Màu Đỏ, Size XXL
    (11, 5, 5, 650000, N'hinh3.jpg', 200), -- Túi xách da, Màu Tím, Size XXL
    (12, 3, 5, 650000, N'hinh4.jpg', 190), -- Túi xách vải, Màu Xanh lá, Size XXL
    (13, 8, 5, 750000, N'hinh1.jpg', 180), -- Nón lưỡi trai, Màu Nâu, Size XXL
    (14, 10, 5, 750000, N'hinh2.jpg', 170),-- Nón rộng vành, Màu Trắng, Size XXL
    (15, 1, 5, 850000, N'hinh3.jpg', 160), -- Kính mát thời trang, Màu Đỏ, Size XXL
    (16, 7, 5, 850000, N'hinh4.jpg', 150), -- Kính mát thể thao, Màu Hồng, Size XXL
    (17, 6, 5, 950000, N'hinh1.jpg', 140), -- Dây chuyền, Màu Cam, Size XXL
    (18, 9, 5, 950000, N'hinh2.jpg', 130), -- Bông tai, Màu Xám, Size XXL
    (19, 2, 5, 1050000, N'hinh3.jpg', 120),-- Đồng hồ nam, Màu Xanh dương, Size XXL
    (20, 11, 5, 1050000, N'hinh4.jpg', 110),-- Đồng hồ nữ, Màu Đen, Size XXL
    (1, 9, 1, 160000, N'hinh1.jpg', 320),  -- Áo thun cổ tròn, Màu Xám, Size S
    (2, 11, 2, 180000, N'hinh2.jpg', 310), -- Áo thun cổ tim, Màu Đen, Size M
    (3, 3, 3, 230000, N'hinh3.jpg', 300),  -- Quần jean nam, Màu Xanh lá, Size L
    (4, 5, 4, 250000, N'hinh4.jpg', 290),  -- Quần jean nữ, Màu Tím, Size XL
    (5, 7, 1, 310000, N'hinh1.jpg', 280),  -- Áo khoác da, Màu Hồng, Size S
    (6, 10, 2, 330000, N'hinh2.jpg', 270), -- Áo khoác nỉ, Màu Trắng, Size M
    (7, 2, 3, 410000, N'hinh3.jpg', 260),  -- Váy dạ hội, Màu Xanh dương, Size L
    (8, 4, 4, 430000, N'hinh4.jpg', 250),  -- Váy công sở, Màu Vàng, Size XL
    (10, 8, 2, 530000, N'hinh2.jpg', 230), -- Giày cao gót, Màu Nâu, Size M
    (11, 1, 3, 590000, N'hinh3.jpg', 220), -- Túi xách da, Màu Đỏ, Size L
    (12, 5, 4, 630000, N'hinh4.jpg', 210), -- Túi xách vải, Màu Tím, Size XL
    (13, 9, 1, 690000, N'hinh1.jpg', 200), -- Nón lưỡi trai, Màu Xám, Size S
    (14, 11, 2, 730000, N'hinh2.jpg', 190),-- Nón rộng vành, Màu Đen, Size M
    (15, 3, 3, 790000, N'hinh3.jpg', 180), -- Kính mát thời trang, Màu Xanh lá, Size L
    (16, 6, 4, 830000, N'hinh4.jpg', 170), -- Kính mát thể thao, Màu Cam, Size XL
    (18, 4, 2, 930000, N'hinh2.jpg', 150), -- Bông tai, Màu Vàng, Size M
    (19, 7, 3, 990000, N'hinh3.jpg', 140), -- Đồng hồ nam, Màu Hồng, Size L
    (20, 10, 4, 1030000, N'hinh4.jpg', 130),-- Đồng hồ nữ, Màu Trắng, Size XL
    (1, 4, 3, 165000, N'hinh1.jpg', 340),  -- Áo thun cổ tròn, Màu Vàng, Size L
    (2, 7, 1, 175000, N'hinh2.jpg', 330),  -- Áo thun cổ tim, Màu Hồng, Size S
    (4, 2, 5, 255000, N'hinh4.jpg', 310),  -- Quần jean nữ, Màu Xanh dương, Size XXL
    (5, 11, 3, 315000, N'hinh1.jpg', 300), -- Áo khoác da, Màu Đen, Size L
    (6, 5, 1, 335000, N'hinh2.jpg', 290),  -- Áo khoác nỉ, Màu Tím, Size S
    (7, 9, 4, 415000, N'hinh3.jpg', 280),  -- Váy dạ hội, Màu Xám, Size XL
    (8, 1, 2, 435000, N'hinh4.jpg', 270),  -- Váy công sở, Màu Đỏ, Size M
    (9, 3, 5, 495000, N'hinh1.jpg', 260),  -- Giày thể thao, Màu Xanh lá, Size XXL
    (10, 7, 3, 535000, N'hinh2.jpg', 250), -- Giày cao gót, Màu Hồng, Size L
    (12, 8, 2, 635000, N'hinh4.jpg', 230), -- Túi xách vải, Màu Nâu, Size M
    (13, 5, 4, 695000, N'hinh1.jpg', 220), -- Nón lưỡi trai, Màu Tím, Size XL
    (14, 3, 1, 735000, N'hinh2.jpg', 210), -- Nón rộng vành, Màu Xanh lá, Size S
    (15, 11, 2, 795000, N'hinh3.jpg', 200),-- Kính mát thời trang, Màu Đen, Size M
    (16, 2, 5, 835000, N'hinh4.jpg', 190), -- Kính mát thể thao, Màu Xanh dương, Size XXL
    (17, 6, 3, 895000, N'hinh1.jpg', 180), -- Dây chuyền, Màu Cam, Size L
    (18, 1, 1, 935000, N'hinh2.jpg', 170), -- Bông tai, Màu Đỏ, Size S
    (19, 4, 4, 995000, N'hinh3.jpg', 160), -- Đồng hồ nam, Màu Vàng, Size XL
    (20, 9, 2, 1035000, N'hinh4.jpg', 150);-- Đồng hồ nữ, Màu Xám, Size M

SELECT sp.SanPhamID, sp.TenSanPham, sp.MoTa, m.TenMau, s.TenSize, ctp.Gia, ctp.HinhAnhUrl, ctp.SoLuongTonKho
FROM SanPham sp
INNER JOIN ChiTietSanPham ctp ON sp.SanPhamID = ctp.SanPhamID
INNER JOIN Mau m ON ctp.MauID = m.MauID
INNER JOIN Size s ON ctp.SizeID = s.SizeID
WHERE sp.DanhMucID = 3
