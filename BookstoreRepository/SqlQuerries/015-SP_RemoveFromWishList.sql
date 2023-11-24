create Procedure SP_RemoveFromWishList
(
	@userId int,
	@bookId int
)
as begin
	DELETE FROM BooksWishList WHERE BookId=@bookId and UserID=@userId;
end