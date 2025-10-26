create database QL_Sach
use QL_Sach

create table TacGia
(
	MaTG int not null,
	TenTG nvarchar(50),
	DiaChi nvarchar(50),
	TieuSu nvarchar(100),
	DienThoai int,
	constraint pk_tg primary key (MaTG)
)

create table ChuDe 
(
	MaCD int not null,
	TenChuDe nvarchar(50),
	constraint pk_cd primary key (MaCD)
)

create table NhaXuatBan
(
	MaNXB int not null,
	TenNXB nvarchar(50),
	DiaChi nvarchar(50),
	DienThoai int,
	constraint pk_nxb primary key(MaNXB)
)

create table Sach
(
	MaSach int not null,
	TenSach nvarchar(50),
	GiaBan money,
	MoTa nvarchar(100),
	AnhBia nvarchar(50),
	NgayCapNhat date,
	SoLuongTon int,
	MaCD int,
	MaNXB int,
	constraint pk_s primary key (MaSach),
	constraint fk_s_cd foreign key (MaCD) references ChuDe (MaCD),
	constraint fk_s_nxb foreign key (MaNXB) references NhaXuatBan (MaNXB)
)

create table VietSach 
(
	MaTG int not null,
	MaSach int not null,
	VaiTro nvarchar(50),
	ViTri nvarchar(50),
	constraint pk_vs primary key (MaTG, MaSach),
	constraint fk_vs_tg foreign key (MaTG) references TacGia (MaTG),
	constraint fk_vs_s foreign key (MaSach) references Sach (MaSach)
)

create table KhachHang
(
	MaKH int not null,
	HoTen nvarchar(50),
	TaiKhoan nvarchar(50),
	MatKhau nvarchar(50),
	Email nvarchar(50),
	DiaChiKH nvarchar(50),
	DienThoaiKh int,
	Ngay date,
	constraint pk_kh primary key (MaKH)
)

create table DonDatHang
(
	MaDonHang int not null,
	DaThanhToan nvarchar(50),
	TinhTrangKhachHang nvarchar(50),
	NgayDat date,
	NgayGiao date,
	MaKH int,
	constraint pk_ddh primary key (MaDonHang),
	constraint fk_ddh_kh foreign key (MaKH)references KhachHang (MaKH)
)

create table ChiTietDonHang
(
	MaDonHang int not null,
	MaSach int not null,
	SoLuong int,
	DonGia money,
	constraint pk_ctdh primary key (MaDonHang, MaSach),
	constraint fk_ctdh_ddh foreign key (MaDonHang)references DonDathang (MaDonHang),
	constraint fk_ctdh_s foreign key (MaSach)references Sach (MaSach)
)

drop table ChiTietDonHang
drop table DonDatHang
drop table KhachHang
drop table VietSach
drop table Sach
drop table NhaXuatBan
drop table ChuDe
drop table TacGia

select * from ChiTietDonHang
select * from DonDatHang
select * from KhachHang
select * from VietSach
select * from Sach
select * from NhaXuatBan
select * from ChuDe
select * from TacGia

insert into ChuDe values
(1, N'Triết học - Chính trị'),
(2, N'Luật'),
(3, N'Công nghệ thông tin')

insert into NhaXuatBan values
(1, N'Nhà xuất bản Trẻ', N'123 abc', 0987869453),
(2, N'Đại học quốc gia', N'456 bca', 0897869889)

insert into Sach values
(1, N'Giáo trình tin học cơ bản', 26000, N'Nội dung của cuốn: Tin học cơ bản Windows', 'tinhoccb.png', '2014-10-25', 120, 1, 2),
(2, N'Giáo trình tin học văn phòng', 12000, N'Gồm 3 phần', 'tinhocvp.png', '2014-10-23', 25, 2, 2),
(3, N'Lập trình cơ sở dữ liệu', 11500, N'Lập trình CSDL', 'laptrinhcsdl.png', '2014-12-21', 23, 1, 1)

