create Table BookstoreBooks(
BookId int primary key identity(1,1),
BookName varchar(max) not null,
BookDescription varchar(max),
BookAuthor varchar(max) not null,
BookImage varchar(max) not null,
BookCount int not null,
BookPrize int not null,
Rating Decimal(10,2),
IsAvailable bit not null,
);

drop table BookstoreBooks




ALTER TABLE BookstoreBooks
DROP COLUMN Rating;

ALTER TABLE BookstoreBooks
add Rating  Decimal(10,2);

select * from BookstoreBooks