create procedure SP_UpdateCart
(
	@userId int,
	@bookId int,
	@Cartcount int
)
As begin
	update TBL_Cart set CartCount = @Cartcount where BookId = @bookId and UserID = @userId
	SELECT * From dbo.TBL_Cart INNER JOIN
                         dbo.BookstoreUser ON dbo.TBL_Cart.UserId = dbo.BookstoreUser.userId INNER JOIN
                         dbo.BookstoreBooks ON dbo.TBL_Cart.BookId = dbo.BookstoreBooks.BookId  where TBL_Cart.BookId = @bookId and TBL_Cart.UserID = @userId
end

drop procedure SP_UpdateCart
select * from TBL_Cart





















