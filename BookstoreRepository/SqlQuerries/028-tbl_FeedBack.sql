create Table tbl_feedBack(
FeedbackId int primary key identity(1,1),
UserId int not null,
BookId int not null,
CustomerDescription varchar(max) not null,
Rating int not null,

foreign key (UserId) references BookStoreUser(UserId),
FOREIGN KEY (BookId) REFERENCES BookstoreBooks(BookID),
);

drop table tbl_feedBack
