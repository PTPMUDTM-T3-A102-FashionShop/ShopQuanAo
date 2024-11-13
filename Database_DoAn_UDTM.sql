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
    NgayTao DATETIME DEFAULT GETDATE(),
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
CREATE TABLE ManHinh (
    ManHinhID INT PRIMARY KEY IDENTITY(1,1),
    MaManHinh NVARCHAR(50) UNIQUE NOT NULL, -- Mã màn hình để dùng trong ứng dụng
    TenManHinh NVARCHAR(100) NOT NULL
);
CREATE TABLE PhanQuyen (
    MaNhomNguoiDung INT NOT NULL,
    ManHinhID INT NOT NULL,
    FOREIGN KEY (ManHinhID) REFERENCES ManHinh(ManHinhID),
	FOREIGN KEY (MaNhomNguoiDung) REFERENCES NhomNguoiDung(MaNhomNguoiDung),
    PRIMARY KEY (MaNhomNguoiDung, ManHinhID)
);
-- Thêm dữ liệu vào bảng DanhMuc
INSERT INTO DanhMuc (TenDanhMuc)
VALUES 
    (N'Áo Thun'),
    (N'Quần Jean');

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
    (N'MH002', N'Màn hình quản lý sản phẩm'),
    (N'MH003', N'Màn hình quản lý đơn hàng'),
    (N'MH004', N'Màn hình quản lý người dùng');

-- Thêm dữ liệu vào bảng PhanQuyen
INSERT INTO PhanQuyen (MaNhomNguoiDung, ManHinhID)
VALUES 
    (3, 1), -- Quản lý có quyền trên màn hình chính
    (3, 2), -- Quản lý có quyền trên màn hình quản lý sản phẩm
    (3, 3), -- Quản lý có quyền trên màn hình quản lý đơn hàng
    (3, 4), -- Quản lý có quyền trên màn hình quản lý người dùng
    (2, 2), -- Nhân viên có quyền trên màn hình quản lý sản phẩm
    (2, 3); -- Nhân viên có quyền trên màn hình quản lý đơn hàng
