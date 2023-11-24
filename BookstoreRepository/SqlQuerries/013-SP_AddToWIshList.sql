create procedure SP_AddToWIshList(
	@userId int,
	@bookId int
)
as begin
	If Not Exists(select * from BooksWishList where UserId=@userId and BookId=@bookId)
	begin
		insert into BooksWishList(UserId,BookId) values(@userId,@BookId) 
	end
	select * from 
		BookstoreBooks as book INNER JOIN
		BooksWishList as wishlist ON Book.BookId = Wishlist.BookId INNER JOIN
		BookstoreUser as users ON wishlist.UserId =users.userId 
	where wishlist.UserId=1 and wishlist.BookId =1 

end

drop procedure SP_AddToWIshList


