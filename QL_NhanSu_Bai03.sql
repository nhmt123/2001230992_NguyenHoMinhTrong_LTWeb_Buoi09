create database QL_NhanSu
use QL_NhanSu

create table Department
(
	DeptId int not null,
	Name nvarchar(50),
	constraint pk_d primary key (DeptId)
)

create table Employee
(
	Id int not null,
	Name nvarchar(50),
	Gender nvarchar(5),
	City nvarchar(50),
	Avatar nvarchar(50),
	DeptId int,
	constraint pk_e primary key (Id),
	constraint fk_e_d foreign key (DeptId) references Department (DeptId)
)

insert into Department values
(1, N'Khoa CNTT'),
(2, N'Khoa Ngoại Ngữ'),
(3, N'Khoa Tài Chính'),
(4, N'Khoa Thực Phẩm'),
(5, N'Phòng Đào Tạo')

insert into Employee values
(1, N'Nguyễn Hải Yến', N'Nữ', N'Đà Lạt', N'chandungnu.png', 1),
(2, N'Trương Mạnh Hùng', N'Nam', N'TP.HCM', N'chandungnam.png', 1),
(3, N'Đinh Duy Minh', N'Nam', N'Thái Bình', N'chandungnam.png', 2),
(4, N'Ngô Thị Nguyệt', N'Nữ', N'Long An', N'chandungnu.png', 2),
(5, N'Đào Minh Châu', N'Nữ', N'Bạc Liêu', N'chandungnu.png', 3),
(6, N'Phan Thị Ngọc Mai', N'Nữ', N'Bến Tre', N'chandungnu.png', 3),
(7, N'Trương Nguyễn Quỳnh Anh', N'Nữ', N'TP.HCM', N'chandungnu.png', 4),
(8, N'Lê Thanh Liêm', N'Nam', N'TP.HCM', N'chandungnam.png', 4)

select * from Department
select * from Employee

drop table Employee
drop table Department