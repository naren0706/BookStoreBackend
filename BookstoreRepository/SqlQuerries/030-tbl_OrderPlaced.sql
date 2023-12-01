create Table tbl_OrderPlaced(
OrderId int primary key identity(1,1),
CustomerId int not null,
CartId int not null,
UserId int not null,
foreign key (CustomerId) references CustomerDetails(CustomerId),
FOREIGN KEY (CartId) REFERENCES TBL_Cart(CartId),
FOREIGN KEY (UserId) REFERENCES BookStoreUser(UserId),
);

drop table tbl_OrderPlaced