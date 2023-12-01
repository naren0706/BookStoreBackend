create procedure SP_AddToCart
(
	@userId int,
	@bookId int
)
As begin
    DECLARE @cartID int;
	insert into TBL_Cart(UserId,BookId,CartCount) values (@userId,@bookId,1)
	    SET @cartID = SCOPE_IDENTITY();

	insert into tbl_OrderSummary(CartId) values (@cartID)

	SELECT * From dbo.TBL_Cart INNER JOIN
							 dbo.BookstoreUser ON dbo.TBL_Cart.UserId = dbo.BookstoreUser.userId INNER JOIN
							 dbo.BookstoreBooks ON dbo.TBL_Cart.BookId = dbo.BookstoreBooks.BookId where TBL_Cart.CartId = @cartID
end

drop procedure SP_AddToCart

select * from TBL_Cart


