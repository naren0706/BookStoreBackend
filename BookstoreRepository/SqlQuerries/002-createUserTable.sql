Use Bookstore;

create Table BookstoreUser(
userId int primary key identity(1,1),
FullName varchar(max) not null,
email varchar(max) not null,
password varchar(max) not null,
MobileNumber varchar(max) not null,
isAdmin bit,
);

drop table BookstoreUser

select * from BookstoreBooks

drop database [Bookstore]