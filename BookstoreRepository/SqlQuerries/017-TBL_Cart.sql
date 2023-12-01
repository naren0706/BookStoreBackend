CREATE TABLE TBL_Cart (
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    CartCount INT,
    isAvailable BIT DEFAULT 1,
    FOREIGN KEY (UserId) REFERENCES BookstoreUser(UserID),
    FOREIGN KEY (BookId) REFERENCES BookstoreBooks(BookId)
);

drop table TBL_Cart

select * from TBL_Cart