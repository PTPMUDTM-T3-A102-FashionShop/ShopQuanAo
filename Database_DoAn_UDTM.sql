Create database DoAnKetMon_UDTM
use DoAnKetMon_UDTM
CREATE TABLE NhomNguoiDung (
	MaNhomNguoiDung INT PRIMARY KEY IDENTITY(1,1),
	TenNhomNguoiDung NVARCHAR(255) NOT NULL,
)
CREATE TABLE NguoiDung (
    NguoiDungID INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(20) NOT NULL,
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
	DiaChiMacDinh BIT DEFAULT 0, -- Địa chỉ mặc định
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
CREATE TABLE SanPham (
    SanPhamID INT PRIMARY KEY IDENTITY(1,1),
    TenSanPham NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX),
    DanhMucID INT,
    NhaCungCapID INT,
	SoLuongDaBan INT DEFAULT 0,
    KichHoat BIT DEFAULT 1,
    FOREIGN KEY (DanhMucID) REFERENCES DanhMuc(DanhMucID),
    FOREIGN KEY (NhaCungCapID) REFERENCES NhaCungCap(NhaCungCapID)
);
CREATE TABLE NhaCungCapSanPham(
	NhaCungCapID INT,
	SanPhamID INT,
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
	KichHoat BIT DEFAULT 1,
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
    TinhTrangDonHang NVARCHAR(50) NOT NULL,
    NgayDatHang DATETIME DEFAULT GETDATE(),
	HinhThucThanhToan NVARCHAR(50) NOT NULL,
    TinhTrangThanhToan NVARCHAR(50) NOT NULL,
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID),
    FOREIGN KEY (NhanVienID) REFERENCES NguoiDung(NguoiDungID),
    FOREIGN KEY (DiaChiID) REFERENCES ThongTinGiaoHang(DiaChiID),
);
CREATE TABLE ChiTietDonHang (
    DonHangID INT NOT NULL,
    ChiTietID INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (DonHangID) REFERENCES DonHang(DonHangID),
    FOREIGN KEY (ChiTietID) REFERENCES ChiTietSanPham(ChiTietID)
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
INSERT INTO SanPham (TenSanPham, MoTa, DanhMucID, NhaCungCapID, KichHoat)
VALUES
    (N'Áo thun trắng', N'Áo thun trắng cổ tròn', 1, 1, 1),
    (N'Áo thun đen', N'Áo thun đen cổ tim', 1, 1, 1),
    (N'Áo thun xanh', N'Áo thun xanh cổ chữ V', 1, 1, 1),
    (N'Áo thun đỏ', N'Áo thun đỏ cổ tròn', 1, 2, 1),
    (N'Áo thun vàng', N'Áo thun vàng cổ tròn', 1, 2, 1),
    (N'Quần jean xanh', N'Quần jean xanh nam', 2, 1, 1),
    (N'Quần jean đen', N'Quần jean đen nữ', 2, 1, 1),
    (N'Quần jean xám', N'Quần jean xám nam', 2, 2, 1),
    (N'Quần jean rách', N'Quần jean rách nữ', 2, 2, 1),
    (N'Quần jean skinny', N'Quần jean skinny nam', 2, 2, 1);

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

