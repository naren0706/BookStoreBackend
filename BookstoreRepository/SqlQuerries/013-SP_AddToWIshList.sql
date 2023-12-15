create procedure SP_AddToWIshList(
	@userId int,
	@bookId int
)
as begin
	if Exists(select * from BooksWishList where UserId=@userId and BookId=@bookId and isAvailable=0)
	begin
		update BooksWishList set isAvailable=1 where  UserId=@userId and BookId=@bookId 
	end
	If Not Exists(select * from BooksWishList where UserId=@userId and BookId=@bookId)
	begin
		insert into BooksWishList(UserId,BookId,isAvailable) values(@userId,@BookId,1) 
	end
	select * from 
		BookstoreBooks as book INNER JOIN
		BooksWishList as wishlist ON Book.BookId = Wishlist.BookId INNER JOIN
		BookstoreUser as users ON wishlist.UserId =users.userId 
	where wishlist.UserId=1 and wishlist.BookId =1 

end

drop procedure SP_AddToWIshList


