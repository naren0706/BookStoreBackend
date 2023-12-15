create Table BooksWishList(
WishListId int primary key identity(1,1),
UserId int not null,
BookId int not null,
isAvailable bit,
FOREIGN KEY (UserId) REFERENCES BookstoreUser(UserID),
FOREIGN KEY (BookId) REFERENCES BookstoreBooks(BookId)
);

drop table BooksWishList