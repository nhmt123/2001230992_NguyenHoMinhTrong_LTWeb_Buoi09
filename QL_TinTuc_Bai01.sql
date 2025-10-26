create database QL_TinTuc
use QL_TinTuc

create table Theloaitin
(	
	IDLoai int not null identity(1,1),
	Tentheloai nvarchar(100),
	constraint pk_tlt primary key (IDLoai)
)

create table Tintuc
(
	IdTin int not null identity(1,1),
	IDLoai int,
	Tieudetin nvarchar(100),
	Noidungtin nText,
	constraint pk_tt primary key (IdTin),
	constraint fk_tt_tlt foreign key (IDLoai) references Theloaitin (IDLoai)
)

insert into Theloaitin(Tentheloai) values
(N'Thể thao'),
(N'Kinh tế'),
(N'Thế giới')

select * from Theloaitin
select * from Tintuc
drop table Tintuc
drop table Theloaitin