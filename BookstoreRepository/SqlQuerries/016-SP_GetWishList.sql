create procedure SP_GetWishList
(
	@userID int
)
as begin
	select * from 
		BookstoreBooks as book INNER JOIN
		BooksWishList as wishlist ON Book.BookId = Wishlist.BookId INNER JOIN
		BookstoreUser as users ON wishlist.UserId =users.userId 
end