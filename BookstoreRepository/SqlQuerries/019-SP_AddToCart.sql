create procedure SP_AddToCart
(
	@userId int,
	@bookId int
)
As begin
    DECLARE @cartID int;
	if EXISTS(select * from TBL_Cart where UserId=@userId and BookId=@bookId and isAvailable=0)
	begin
		UPDATE TBL_Cart SET isAvailable = 1,CartCount=1 WHERE UserId=@userId and BookId=@bookId;
		insert into tbl_OrderSummary(CartId) (select cartid from TBL_Cart WHERE UserId=@userId and BookId=@bookId and isAvailable=1);
		SELECT * From dbo.TBL_Cart INNER JOIN
								 dbo.BookstoreUser ON dbo.TBL_Cart.UserId = dbo.BookstoreUser.userId INNER JOIN
								 dbo.BookstoreBooks ON dbo.TBL_Cart.BookId = dbo.BookstoreBooks.BookId where TBL_Cart.UserId=@userId and TBL_Cart.BookId=@bookId;
	end
	if not EXISTS(select * from TBL_Cart where UserId=@userId and BookId=@bookId )
	begin
		insert into TBL_Cart(UserId,BookId,CartCount,isAvailable) values (@userId,@bookId,1,1)
			SET @cartID = SCOPE_IDENTITY();

		insert into tbl_OrderSummary(CartId) values (@cartID)

		SELECT * From dbo.TBL_Cart INNER JOIN
								 dbo.BookstoreUser ON dbo.TBL_Cart.UserId = dbo.BookstoreUser.userId INNER JOIN
								 dbo.BookstoreBooks ON dbo.TBL_Cart.BookId = dbo.BookstoreBooks.BookId where TBL_Cart.CartId = @cartID
	end	
end

drop procedure SP_AddToCart

 (select * from TBL_Cart where UserId=1 and BookId=1)

 SELECT * From dbo.TBL_Cart INNER JOIN
								 dbo.BookstoreUser ON dbo.TBL_Cart.UserId = dbo.BookstoreUser.userId INNER JOIN
								 dbo.BookstoreBooks ON dbo.TBL_Cart.BookId = dbo.BookstoreBooks.BookId where TBL_Cart.CartId = 17

DELETE FROM [tbl_OrderPlaced] WHERE  UserId=1
DELETE FROM TBL_Cart WHERE  UserId=1