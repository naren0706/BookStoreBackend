create Table tbl_OrderSummary(
    SummaryId int primary key identity(1,1),
	CartId int not null,
	foreign key (CartId) references Tbl_cart(CartId)
);

drop table tbl_OrderSummary