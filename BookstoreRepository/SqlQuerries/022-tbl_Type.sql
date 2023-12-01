create Table tbl_Type(
    typeId int primary key identity(1,1),
    typeValue varchar(max) not null
);

drop table tbl_Type

insert into tbl_Type values('home'),('work'),('others')

select * from tbl_Type