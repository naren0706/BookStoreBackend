select * from tbl_OrderSummary

create procedure SP_GetOrderSummary(
	@userId int
)
as begin

SELECT * FROM 
		BookstoreBooks INNER JOIN
        TBL_Cart ON BookstoreBooks.BookId = TBL_Cart.BookId INNER JOIN
        BookstoreUser ON TBL_Cart.UserId = BookstoreUser.userId INNER JOIN
        tbl_OrderSummary ON TBL_Cart.CartId = tbl_OrderSummary.CartId where TBL_Cart.UserId = @userId 

end

drop procedure SP_GetOrderSummary

SELECT * FROM 
		BookstoreBooks INNER JOIN
        TBL_Cart ON BookstoreBooks.BookId = TBL_Cart.BookId INNER JOIN
        BookstoreUser ON TBL_Cart.UserId = BookstoreUser.userId INNER JOIN
        tbl_OrderSummary ON TBL_Cart.CartId = tbl_OrderSummary.CartId where BookstoreUser.userId = 1 