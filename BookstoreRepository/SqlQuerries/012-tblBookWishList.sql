create Table BooksWishList(
WishListId int primary key identity(1,1),
UserId int not null,
BookId int not null
FOREIGN KEY (UserId) REFERENCES BookstoreUser(UserID),
FOREIGN KEY (BookId) REFERENCES BookstoreBooks(BookId)
);
