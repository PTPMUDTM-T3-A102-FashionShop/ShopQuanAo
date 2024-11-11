Create database DoAnKetMon_UDTM
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
    KichHoat BIT DEFAULT 1
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
    Gia DECIMAL(18, 2) NOT NULL,
    SoLuongTonKho INT NOT NULL,
    DanhMucID INT,
    NhaCungCapID INT,
    HinhAnhUrl NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE(),
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
    SoLuongTonKho INT NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
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
CREATE TABLE KhachHang (
    KhachHangID INT PRIMARY KEY IDENTITY(1,1),
    NguoiDungID INT NOT NULL UNIQUE,
	Email NVARCHAR(MAX),

    FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID)
);
CREATE TABLE ManHinh (
    ManHinhID INT PRIMARY KEY IDENTITY(1,1),
    MaManHinh NVARCHAR(50) UNIQUE NOT NULL, -- Mã màn hình để dùng trong ứng dụng
    TenManHinh NVARCHAR(100) NOT NULL
);
CREATE TABLE PhanQuyen (
    VaiTro NVARCHAR(50) NOT NULL, -- 'Manager' hoặc 'Staff'
    ManHinhID INT NOT NULL,
    FOREIGN KEY (ManHinhID) REFERENCES ManHinh(ManHinhID),
    PRIMARY KEY (VaiTro, ManHinhID)
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
INSERT INTO SanPham (TenSanPham, MoTa, Gia, SoLuongTonKho, DanhMucID, NhaCungCapID, HinhAnhUrl, KichHoat)
VALUES
    (N'Áo thun trắng', N'Áo thun trắng cổ tròn', 150000, 100, 1, 1, N'hinh1.jpg', 1),
    (N'Áo thun đen', N'Áo thun đen cổ tim', 160000, 80, 1, 1, N'hinh2.jpg', 1),
    (N'Áo thun xanh', N'Áo thun xanh cổ chữ V', 170000, 120, 1, 1, N'hinh3.jpg', 1),
    (N'Áo thun đỏ', N'Áo thun đỏ cổ tròn', 155000, 90, 1, 2, N'hinh4.jpg', 1),
    (N'Áo thun vàng', N'Áo thun vàng cổ tròn', 165000, 110, 1, 2, N'hinh5.jpg', 1),
    (N'Quần jean xanh', N'Quần jean xanh nam', 300000, 50, 2, 1, N'hinh6.jpg', 1),
    (N'Quần jean đen', N'Quần jean đen nữ', 320000, 60, 2, 1, N'hinh7.jpg', 1),
    (N'Quần jean xám', N'Quần jean xám nam', 310000, 40, 2, 2, N'hinh8.jpg', 1),
    (N'Quần jean rách', N'Quần jean rách nữ', 350000, 30, 2, 2, N'hinh9.jpg', 1),
    (N'Quần jean skinny', N'Quần jean skinny nam', 360000, 25, 2, 2, N'hinh10.jpg', 1),
    (N'Áo thun cam', N'Áo thun cam cổ tròn', 150000, 85, 1, 1, N'hinh11.jpg', 1),
    (N'Áo thun tím', N'Áo thun tím cổ tim', 160000, 95, 1, 1, N'hinh12.jpg', 1),
    (N'Áo thun nâu', N'Áo thun nâu cổ chữ V', 170000, 75, 1, 2, N'hinh13.jpg', 1),
    (N'Áo thun hồng', N'Áo thun hồng cổ tròn', 155000, 65, 1, 2, N'hinh14.jpg', 1),
    (N'Áo thun xám', N'Áo thun xám cổ tròn', 165000, 70, 1, 2, N'hinh15.jpg', 1),
    (N'Quần jean trắng', N'Quần jean trắng nam', 300000, 45, 2, 1, N'hinh16.jpg', 1),
    (N'Quần jean xanh đậm', N'Quần jean xanh đậm nữ', 320000, 35, 2, 1, N'hinh17.jpg', 1),
    (N'Quần jean bạc', N'Quần jean bạc nam', 310000, 55, 2, 2, N'hinh18.jpg', 1),
    (N'Quần jean lửng', N'Quần jean lửng nữ', 350000, 40, 2, 2, N'hinh19.jpg', 1),
    (N'Quần jean baggy', N'Quần jean baggy nam', 360000, 50, 2, 2, N'hinh20.jpg', 1);