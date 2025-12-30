-- Họ và tên: Lê Cao Sang 
-- MSSV: 23521342 
-- Đề tài Quản lý Cửa Hàng Kinh Doang Vàng Bạc Đá Quý
CREATE DATABASE QL_VangBacDaQuy
USE QL_VangBacDaQuy

-- TẠO BẢNG 
-- 1. Bảng Loại sản phẩm
CREATE TABLE LOAISANPHAM (
    MaLoaiSP INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiSP NVARCHAR(50) NOT NULL,
    PhanTramLoiNhuan FLOAT DEFAULT 0,
    DonViTinh NVARCHAR(20)
);

-- 2. Bảng Sản phẩm
CREATE TABLE SANPHAM (
    MaSP INT IDENTITY(1,1) PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    MaLoaiSP INT NOT NULL,
    DonGiaMua MONEY DEFAULT 0,
    SoLuongTon INT DEFAULT 0,
    FOREIGN KEY (MaLoaiSP) REFERENCES LOAISANPHAM(MaLoaiSP)
);

-- 3. Bảng Nhà cung cấp 
CREATE TABLE NHACUNGCAP (
    MaNCC INT IDENTITY(1,1) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    SDT VARCHAR(15)
);

-- 4. Bảng Khách hàng 
CREATE TABLE KHACHHANG (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
	SDT VARCHAR(15)
);
-- Thêm cột NgayThamGia để thống kê được khách tham gia mới trong tháng 
ALTER TABLE KHACHHANG
ADD NgayThamGia DATETIME DEFAULT GETDATE();

UPDATE KHACHHANG 
SET NgayThamGia = GETDATE() 
WHERE NgayThamGia IS NULL;

-- 5. Bảng Phiếu mua hàng 
CREATE TABLE PHIEUMUAHANG (
    MaPhieuMua INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    MaNCC INT NOT NULL,
    TongTien MONEY DEFAULT 0,
    FOREIGN KEY (MaNCC) REFERENCES NHACUNGCAP(MaNCC)
);

-- 6. Bảng Chi tiết phiếu mua 
CREATE TABLE CT_PHIEUMUA (
    MaPhieuMua INT,
    MaSP INT,
    SoLuong INT CHECK (SoLuong > 0),
    DonGiaMua MONEY,
    ThanhTien MONEY,
    PRIMARY KEY (MaPhieuMua, MaSP),
    FOREIGN KEY (MaPhieuMua) REFERENCES PHIEUMUAHANG(MaPhieuMua),
    FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
);

-- 7. Bảng Phiếu bán hàng
CREATE TABLE PHIEUBANHANG (
    MaPhieuBan INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    MaKH INT NOT NULL,
    TongTien MONEY DEFAULT 0,
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
);

-- 8. Bảng Chi tiết phiếu bán
CREATE TABLE CT_PHIEUBAN (
    MaPhieuBan INT,
    MaSP INT,
    SoLuong INT CHECK (SoLuong > 0),
    DonGiaBan MONEY,
    ThanhTien MONEY,
    PRIMARY KEY (MaPhieuBan, MaSP),
    FOREIGN KEY (MaPhieuBan) REFERENCES PHIEUBANHANG(MaPhieuBan),
    FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
);

-- 9. Bảng Loại dịch vụ 
CREATE TABLE LOAIDICHVU (
    MaLoaiDV INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiDV NVARCHAR(100) NOT NULL,
    DonGiaDV MONEY DEFAULT 0
);

-- 10. Bảng Phiếu dịch vụ 
CREATE TABLE PHIEUDICHVU (
    MaPhieuDV INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    MaKH INT NOT NULL,
    TongTien MONEY DEFAULT 0,
    TongTraTruoc MONEY DEFAULT 0,
    TongConLai MONEY DEFAULT 0,
    TinhTrang NVARCHAR(50) DEFAULT N'Chưa hoàn thành',
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
);

-- 11. Bảng Chi tiết phiếu dịch vụ 
CREATE TABLE CT_PHIEUDICHVU (
    MaPhieuDV INT,
    MaLoaiDV INT,
    ChiPhiRieng MONEY DEFAULT 0,
    DonGiaDuocTinh MONEY,
    SoLuong INT CHECK (SoLuong > 0),
    ThanhTien MONEY,
    TraTruoc MONEY DEFAULT 0,
    ConLai MONEY DEFAULT 0,
    NgayGiao DATETIME,
    TinhTrang NVARCHAR(50) DEFAULT N'Chưa giao',
    PRIMARY KEY (MaPhieuDV, MaLoaiDV),
    FOREIGN KEY (MaPhieuDV) REFERENCES PHIEUDICHVU(MaPhieuDV),
    FOREIGN KEY (MaLoaiDV) REFERENCES LOAIDICHVU(MaLoaiDV)
);

-- 12. Bảng Tham số 
CREATE TABLE THAMSO (
    TenThamSo NVARCHAR(50) PRIMARY KEY,
    GiaTri FLOAT NOT NULL
);

-- 13. Bảng Nhân viên
CREATE TABLE NHANVIEN (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(100) NOT NULL,
    HoTen NVARCHAR(50),
    QuyenHan NVARCHAR(20) 
);

-- INSERT DATA
-- 14. Thêm Nhân viên 
DELETE FROM NHANVIEN;
INSERT INTO NHANVIEN (TenDangNhap, MatKhau, HoTen, QuyenHan) VALUES 
('admin', '123', N'Le Cao Sang QL', 'Admin'),
('nhanvien', '123', N'Le Cao Sang NV', 'NhanVien');

-- 15. Thêm data Tham số
DELETE FROM THAMSO;
INSERT INTO THAMSO (TenThamSo, GiaTri) 
VALUES (N'TyLeTraTruoc', 0.5);

INSERT INTO THAMSO (TenThamSo, GiaTri) 
VALUES (N'ThoiHanHuyPhieu', 20);

-- 16. Thêm data Loại sản phẩm 
DELETE FROM LOAISANPHAM;
INSERT INTO LOAISANPHAM (TenLoaiSP, PhanTramLoiNhuan, DonViTinh) VALUES 
(N'Vàng 24K', 0.02, N'Chỉ'),
(N'Vàng 18K', 0.05, N'Chỉ'),
(N'Đá Quý', 0.10, N'Viên'),
(N'Bạc Ý', 0.15, N'Lượng');

-- 17. Thêm data Loại dịch vụ 
DELETE FROM LOAIDICHVU;
INSERT INTO LOAIDICHVU (TenLoaiDV, DonGiaDV) VALUES 
(N'Làm sạch trang sức', 50000),
(N'Gia công nhẫn trơn', 200000),
(N'Đính đá lại', 150000),
(N'Khắc tên laser', 100000);

-- 18. Thêm data Nhà cung cấp
DELETE FROM NHACUNGCAP;
INSERT INTO NHACUNGCAP (TenNCC, DiaChi, SDT) VALUES 
(N'Công ty Vàng Bạc PNJ', N'TP.HCM', '0909123456'),
(N'Tập đoàn DOJI', N'Hà Nội', '0909987654'),
(N'SJC Sài Gòn', N'Quận 1, TP.HCM', '02839123456');

-- 19. Thêm data Khách hàng 
DELETE FROM KHACHHANG;
INSERT INTO KHACHHANG (TenKH, SDT, DiaChi) VALUES 
(N'Nguyễn Văn A', '0912345678', N'Quận 5'),
(N'Trần Thị B', '0987654321', N'Quận 3');

-- 20. Thêm data Sản phẩm 
DELETE FROM SANPHAM;
INSERT INTO SANPHAM (TenSP, MaLoaiSP, DonGiaMua, SoLuongTon) VALUES 
(N'Nhẫn trơn Vàng 24K 1 chỉ', 1, 5500000, 100),
(N'Nhẫn trơn Vàng 24K 2 chỉ', 1, 11000000, 50),
(N'Dây chuyền Vàng 18K mặt tim', 2, 3500000, 20),
(N'Lắc tay Vàng 18K', 2, 4200000, 15),
(N'Nhẫn Ruby đỏ', 3, 8000000, 10),
(N'Bông tai Bạc Ý', 4, 500000, 200);

SELECT * FROM NHANVIEN;
SELECT * FROM KHACHHANG;
SELECT * FROM SANPHAM;
SELECT * FROM PHIEUBANHANG;
SELECT * FROM PHIEUMUAHANG;
SELECT * FROM SANPHAM;
SELECT * FROM PHIEUDICHVU;
SELECT * FROM CT_PHIEUBAN;
SELECT * FROM THAMSO;
EXEC sp_help 'CT_PHIEUMUA';