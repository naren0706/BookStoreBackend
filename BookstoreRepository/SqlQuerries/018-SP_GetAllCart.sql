create procedure SP_GetAllCart
(
	@userId int
)
As begin
	select * FROM BookstoreUser INNER JOIN
                         TBL_Cart ON BookstoreUser.userId = TBL_Cart.UserId INNER JOIN
                         BookstoreBooks ON TBL_Cart.BookId = BookstoreBooks.BookId
					where TBL_Cart.UserId = @userId and TBL_Cart.IsAvailable=1
end

drop procedure SP_GetAllCart