create Table BookstoreBooks(
BookId int primary key identity(1,1),
BookName varchar(max) not null,
BookDescription varchar(max),
BookAuthor varchar(max) not null,
BookImage varchar(max) not null,
BookCount int not null,
BookPrize int not null,
Rating int not null,
IsAvailable bit not null,
);

drop table BookstoreBooks