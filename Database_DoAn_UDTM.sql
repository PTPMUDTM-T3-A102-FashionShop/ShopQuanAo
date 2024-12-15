CREATE DATABASE [DoAnKetMon_UDTM21114]
GO
use DoAnKetMon_UDTM21114
GO
CREATE TABLE NhomNguoiDung (
	MaNhomNguoiDung INT PRIMARY KEY IDENTITY(1,1),
	TenNhomNguoiDung NVARCHAR(255) NOT NULL,
)
CREATE TABLE NguoiDung (
    NguoiDungID INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(15),
    DiaChi NVARCHAR(255),
	NgaySinh Date,
    MaNhomNguoiDung INT,
    NgayTao DATETIME DEFAULT GETDATE(),           
    KichHoat BIT DEFAULT 1,
	Train BIT DEFAULT 0, 
	MucChiTieu INT NULL,
	DoTuoi INT NULL,
	PhanKhucKH NVARCHAR(100) DEFAULT N'Khách Hàng Mới',
	SoThich NVARCHAR(255),
	GioiTinh NVARCHAR(10) NULL,   
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
	DiaChiMacDinh BIT NOT NULL, -- Địa chỉ mặc định 1:mặc định/ 0:không phải mặc định
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
	GiaDuocGiam DECIMAL(18, 2) DEFAULT 0,
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
CREATE TABLE KhuyenMai (
    MaKhuyenMai INT PRIMARY KEY IDENTITY(1,1), 
    TenChuongTrinhKM NVARCHAR(100),
	MucGiam INT,--%
    MoTa NVARCHAR(255),
    NgayBatDau DATE,
    NgayKetThuc DATE
);
CREATE TABLE ChiTietKhuyenMai (
	ChiTietKhuyenMaiID INT PRIMARY KEY IDENTITY(1,1), 
    SanPhamID INT,
    KhuyenMaiID INT,
	DaHetHan bit Default 0,
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID),
    FOREIGN KEY (KhuyenMaiID) REFERENCES KhuyenMai(MaKhuyenMai)
);



--------------------------------------------------------------------------------------------Dữ liệu train KNN--------------------------------------------------------------------------

-- Thêm dữ liệu train từ 0 - 100 tuổi 
DECLARE @Age INT = 0;
DECLARE @IDPrefix NVARCHAR(50);

WHILE @Age <= 100
BEGIN
    SET @IDPrefix = CONCAT(N'TaiKhoanTrain', @Age);

    INSERT INTO NguoiDung (TenDangNhap, MatKhau, DoTuoi, MucChiTieu, Train)
    SELECT 
        CONCAT(@IDPrefix, RIGHT('00' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS NVARCHAR), 3)) AS TenDangNhap,
        '123456' AS MatKhau,
        @Age AS DoTuoi,
        MucChiTieu,
        1 AS Train
    FROM (
        SELECT TOP (100) (ROW_NUMBER() OVER (ORDER BY (SELECT NULL))) * 1000000 AS MucChiTieu
        FROM master.dbo.spt_values
    ) AS Spending(MucChiTieu);

    SET @Age = @Age + 1;
END;










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
--Insert vào thông tin giao hàng
INSERT INTO ThongTinGiaoHang (NguoiDungID, TenNguoiNhan, SoDienThoai, DiaChiGiaoHang, DiaChiMacDinh) --Thông tin giao hàng của khách hàng 1
VALUES 
(1, N'Nguyễn Văn A', N'0987654321', N'123 Đường ABC, Phường XYZ, TP. Hồ Chí Minh', 1)


-- Thêm dữ liệu vào bảng DanhMuc
INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Quần áo Nam');
INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Quần áo Nữ');
INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Phụ kiện thời trang');

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
-------Thêm dữ liệu vào bảng sản phẩm---------------
-- Thêm 10 sản phẩm cho Danh mục 'Quần áo Nam'
INSERT INTO SanPham (TenSanPham, MoTa, DanhMucID)
VALUES 
    (N'Áo thun nam 01', N'Dành cho người trẻ, phong cách hiện đại.', 1), --thấp trẻ 
    (N'Áo sơ mi nam 02', N'Phù hợp với trung niên, lịch lãm.', 1), -- thấp trung
    (N'Quần jeans nam 03', N'Phong cách người trẻ, năng động.', 1), -- thấp trẻ
    (N'Quần kaki nam 04', N'Thiết kế dành cho cao tuổi, thoải mái.', 1), -- thấp cao
    (N'Áo khoác nam 05', N'Phong cách trẻ trung, hợp thời, phù hợp người trẻ.', 1), --trung trẻ
    (N'Áo len nam 06', N'Phù hợp cho trung niên, giữ ấm tốt.', 1),-- thấp trung
    (N'Áo blazer nam 07', N'Dành cho người trẻ, lịch sự.', 1),-- cao trẻ
    (N'Quần shorts nam 08', N'Thiết kế dành cho cao tuổi, thoáng mát.', 1), -- trung cao
    (N'Áo polo nam 09', N'Phong cách dành cho trung niên, tiện dụng.', 1), -- trung trung
    (N'Áo vest nam 10', N'Dành cho người trẻ, sang trọng.', 1); -- trung trẻ

-- Thêm 10 sản phẩm cho Danh mục 'Quần áo Nữ'
INSERT INTO SanPham (TenSanPham, MoTa, DanhMucID)
VALUES 
    (N'Váy maxi nữ 01', N'Dành cho người trẻ, phong cách thanh lịch.', 2),-- cao trẻ
    (N'Váy body nữ 02', N'Phù hợp với trung niên, quyến rũ.', 2), -- cao trung
    (N'Quần culottes nữ 03', N'Dành cho người trẻ, năng động.', 2), -- thấp trẻ
    (N'Quần tây nữ 04', N'Thiết kế dành cho cao tuổi, thoải mái.', 2), -- cao cao
    (N'Áo sơ mi nữ 05', N'Phong cách trẻ trung, hiện đại, phù hợp người trung niên.', 2), -- cao trung
    (N'Áo len nữ 06', N'Phù hợp cho trung niên, giữ ấm tốt.', 2),-- trung trung
    (N'Váy xòe nữ 07', N'Dành cho người trẻ, ngọt ngào.', 2),-- cao trẻ 
    (N'Quần shorts nữ 08', N'Thiết kế dành cho cao tuổi, tiện dụng.', 2),-- thấp cao
    (N'Áo khoác nữ 09', N'Phong cách dành cho trung niên, tinh tế.', 2), -- thấp trung
    (N'Áo vest nữ 10', N'Dành cho người trẻ, nổi bật.', 2); -- TRUNG TRẺ

-- Thêm 10 sản phẩm cho Danh mục 'Phụ kiện thời trang'
INSERT INTO SanPham (TenSanPham, MoTa, DanhMucID)
VALUES 
    (N'Đồng hồ nam 01', N'Phù hợp với người trẻ, hiện đại.', 3), -- CAO TRẺ
    (N'Đồng hồ nữ 02', N'Thiết kế cho trung niên, sang trọng.', 3), -- trung trung
    (N'Kính mát 03', N'Dành cho người cao tuổi, thời trang.', 3), -- trungcao
    (N'Nón rộng vành 04', N'Thiết kế dành cho cao tuổi, bảo vệ tốt.', 3),-- cao cao
    (N'Dây chuyền nữ 05', N'Phong cách cho người trẻ trung, thanh lịch.', 3),-- thấp trẻ
    (N'Vòng tay nữ 06', N'Phù hợp cho trung niên, quyến rũ.', 3),-- cao trung
    (N'Bông tai nữ 07', N'Dành cho người cao tuổi, tinh tế.', 3),-- thấp cao
    (N'Nón lưỡi trai 08', N'Thiết kế dành cho cao tuổi, thoáng mát.', 3),-- trung cao
    (N'Túi xách nữ 09', N'Phong cách dành cho trung niên, tiện dụng.', 3),--thấp trung
    (N'Túi đeo chéo 10', N'Dành cho người cao tuổi, năng động.', 3);-- cao cao
-- Insert vào đơn hàng.
-- Thêm chi tiết sản phẩm cho các sản phẩm 'Quần áo Nam'
-- Áo thun nam 01
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (1, 1, 1, 200000, 0, N'hinh1.jpg', 100),  -- Đỏ, S
    (1, 2, 2, 210000, 0, N'hinh2.jpg', 150);  -- Xanh dương, M

-- Áo sơ mi nam 02
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (2, 3, 2, 250000, 0, N'hinh3.jpg', 120),  -- Xanh lá, M
    (2, 4, 3, 260000, 0, N'hinh4.jpg', 80);  -- Vàng, L

-- Quần jeans nam 03
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (3, 5, 2, 300000, 0, N'hinh5.jpg', 200),  -- Tím, M
    (3, 6, 3, 310000, 0, N'hinh6.jpg', 180);  -- Cam, L

-- Quần kaki nam 04
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (4, 7, 1, 200000, 0, N'hinh7.jpg', 90),  -- Hồng, S
    (4, 8, 3, 210000, 0, N'hinh8.jpg', 110);  -- Nâu, L

-- Áo khoác nam 05
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (5, 9, 2, 550000, 0, N'hinh9.jpg', 70),  -- Xám, M
    (5, 10, 4, 540000, 0, N'hinh10.jpg', 60);  -- Trắng, XL

-- Áo len nam 06
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (6, 1, 3, 160000, 0, N'hinh11.jpg', 85),  -- Đỏ, L
    (6, 2, 4, 165000, 0, N'hinh12.jpg', 95);  -- Xanh dương, XL

-- Áo blazer nam 07
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (7, 3, 3, 900000, 0, N'hinh13.jpg', 60),  -- Xanh lá, L
    (7, 4, 4, 910000, 0, N'hinh14.jpg', 50);  -- Vàng, XL

-- Quần shorts nam 08
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (8, 5, 1, 1500000, 0, N'hinh15.jpg', 130),  -- Tím, S
    (8, 6, 2, 1560000, 0, N'hinh16.jpg', 110);  -- Cam, M

-- Áo polo nam 09
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (9, 7, 2, 680000, 0, N'hinh17.jpg', 140),  -- Hồng, M
    (9, 8, 3, 690000, 0, N'hinh18.jpg', 120);  -- Nâu, L

-- Áo vest nam 10
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (10, 9, 3, 550000, 0, N'hinh19.jpg', 40),  -- Xám, L
    (10, 10, 4, 560000, 0, N'hinh20.jpg', 30);  -- Trắng, XL
-- Thêm chi tiết sản phẩm cho các sản phẩm 'Quần áo Nữ'
-- Váy maxi nữ 01
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (11, 1, 2, 3500000, 0, N'hinh21.jpg', 90),  -- Đỏ, M
    (11, 2, 3, 3600000, 0, N'hinh22.jpg', 110);  -- Xanh dương, L

-- Váy body nữ 02
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (12, 3, 2, 900000, 0, N'hinh23.jpg', 80),  -- Xanh lá, M
    (12, 4, 3, 950000, 0, N'hinh24.jpg', 60);  -- Vàng, L

-- Quần culottes nữ 03
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (13, 5, 2, 250000, 0, N'hinh25.jpg', 150),  -- Tím, M
    (13, 6, 3, 250000, 0, N'hinh26.jpg', 130);  -- Cam, L

-- Quần tây nữ 04
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (14, 7, 1, 1300000, 0, N'hinh27.jpg', 100),  -- Hồng, S
    (14, 8, 4, 1400000, 0, N'hinh28.jpg', 90);  -- Nâu, XL

-- Áo sơ mi nữ 05
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (15, 9, 2, 1200000, 0, N'hinh29.jpg', 180),  -- Xám, M
    (15, 10, 3, 1210000, 0, N'hinh30.jpg', 150);  -- Trắng, L

-- Áo len nữ 06
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (16, 1, 3, 450000, 0, N'hinh31.jpg', 120),  -- Đỏ, L
    (16, 2, 4, 460000, 0, N'hinh32.jpg', 100);  -- Xanh dương, XL

-- Váy xòe nữ 07
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (17, 3, 2, 1450000, 0, N'hinh33.jpg', 70),  -- Xanh lá, M
    (17, 4, 3, 1350000, 0, N'hinh34.jpg', 50);  -- Vàng, L

-- Quần shorts nữ 08
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (18, 5, 1, 180000,0, N'hinh35.jpg', 160),  -- Tím, S
    (18, 6, 2, 180000, 0, N'hinh36.jpg', 140);  -- Cam, M

-- Áo khoác nữ 09
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (19, 7, 3, 200000, 0, N'hinh37.jpg', 80),  -- Hồng, L
    (19, 8, 4, 210000, 0, N'hinh38.jpg', 70);  -- Nâu, XL

-- Áo vest nữ 10
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (20, 9, 3, 600000, 0, N'hinh39.jpg', 40),  -- Xám, L
    (20, 10, 4, 600000, 0, N'hinh40.jpg', 30);  -- Trắng, XL
-- Thêm chi tiết sản phẩm cho các sản phẩm 'Phụ kiện thời trang'
-- Đồng hồ nam 01
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (21, 1, 1, 1500000, 0, N'hinh41.jpg', 50),  -- Đỏ, S
    (21, 2, 1, 1500000, 0, N'hinh42.jpg', 60);  -- Xanh dương, S

-- Đồng hồ nữ 02
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (22, 3, 1, 700000, 0, N'hinh43.jpg', 40),  -- Xanh lá, S
    (22, 4, 1, 750000, 0, N'hinh44.jpg', 30);  -- Vàng, S

-- Kính mát 03
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (23, 5, 1, 450000, 0, N'hinh45.jpg', 120),  -- Tím, S
    (23, 6, 1, 550000, 0, N'hinh46.jpg', 110);  -- Cam, S

-- Nón rộng vành 04
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (24, 7, 2, 2000000, 0, N'hinh47.jpg', 80),  -- Hồng, M
    (24, 8, 2, 2100000, 0, N'hinh48.jpg', 70);  -- Nâu, M

-- Dây chuyền nữ 05
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (25, 9, 1, 200000, 0, N'hinh49.jpg', 60),  -- Xám, S
    (25, 10, 1, 210000, 0, N'hinh50.jpg', 50);  -- Trắng, S

-- Vòng tay nữ 06
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (26, 1, 1, 3000000, 0, N'hinh51.jpg', 100),  -- Đỏ, S
    (26, 2, 1, 3100000, 0, N'hinh52.jpg', 90);  -- Xanh dương, S

-- Bông tai nữ 07
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (27, 3, 1, 150000, 0, N'hinh53.jpg', 140),  -- Xanh lá, S
    (27, 4, 1, 160000, 0, N'hinh54.jpg', 130);  -- Vàng, S

-- Nón lưỡi trai 08
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (28, 5, 2, 800000, 0, N'hinh55.jpg', 110),  -- Tím, M
    (28, 6, 2, 810000, 0, N'hinh56.jpg', 100);  -- Cam, M

-- Túi xách nữ 09
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (29, 7, 3, 160000, 0, N'hinh57.jpg', 50),  -- Hồng, L
    (29, 8, 3, 165000, 0, N'hinh58.jpg', 40);  -- Nâu, L

-- Túi đeo chéo 10
INSERT INTO ChiTietSanPham (SanPhamID, MauID, SizeID, Gia, GiaDuocGiam, HinhAnhUrl, SoLuongTonKho)
VALUES 
    (30, 9, 2, 1400000, 0, N'hinh59.jpg', 60),  -- Xám, M
    (30, 10, 2, 1300000, 0, N'hinh60.jpg', 50);  -- Trắng, M

INSERT [dbo].[DonHang] ([DiaChiID], [NhanVienID], [NguoiDungID], [TongTien], [TinhTrangDonHang], [NgayDatHang], [HinhThucThanhToan], [TinhTrangThanhToan], [NgayThanhToan]) VALUES (1, NULL, 1, CAST(1510000.00 AS Decimal(18, 2)), N'Đã Xác Nhận', CAST(N'2024-11-24 16:04:59.783' AS DateTime), N'Tiền mặt', N'Chưa thanh toán', CAST(N'2024-11-24 16:04:59.783' AS DateTime))
INSERT INTO [dbo].[DonHang] ([DiaChiID], [NhanVienID], [NguoiDungID], [TongTien], [TinhTrangDonHang], [NgayDatHang], [HinhThucThanhToan], [TinhTrangThanhToan], [NgayThanhToan]) VALUES (1, NULL, 1, CAST(690000.00 AS Decimal(18, 2)), N'Đang Vận Chuyển', CAST(N'2024-11-24 16:05:08.440' AS DateTime), N'VNPAY', N'Đã thanh toán', NULL);
INSERT [dbo].[DonHang] ([DiaChiID], [NhanVienID], [NguoiDungID], [TongTien], [TinhTrangDonHang], [NgayDatHang], [HinhThucThanhToan], [TinhTrangThanhToan], [NgayThanhToan]) VALUES (1, NULL, 1, CAST(1830000.00 AS Decimal(18, 2)), N'Hoàn Thành', CAST(N'2024-11-24 16:05:45.167' AS DateTime), N'Tiền mặt', N'Chưa thanh toán', CAST(N'2024-11-24 16:05:45.167' AS DateTime))

-- Vừa insert vào 3 đơn hàng, xem coi 3 đơn hàng đó có id phải là 61, 62, 63 không, nếu không đúng sửa lại
-- Đơn Số 1 có 2 Chi Tiết
-- Đơn Số 2 có 1 Chi Tiết
-- Đơn Số 3 có 3 Chi Tiết

INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (1, 1, 7, CAST(150000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (1, 2, 2, CAST(230000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (2, 3, 3, CAST(230000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (3, 4, 3, CAST(230000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (3, 5, 3, CAST(220000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [DonGia], [TinhTrangDanhGia]) VALUES (3, 6, 3, CAST(160000.00 AS Decimal(18, 2)), 0)











