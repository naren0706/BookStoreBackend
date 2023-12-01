create Table CustomerDetails(
customerID int primary key identity(1,1),
fullName varchar(max) not null,
mobileNum varchar(max) not null,
address varchar(max) not null,
cityOrTown varchar(max) not null,
state varchar(max) not null,
typeId int not null,
userId int not null,
foreign key(typeId) references tbl_Type(typeId),
foreign key(userId) references BookstoreUser(userId),
);

select * from CustomerDetails
drop table CustomerDetails