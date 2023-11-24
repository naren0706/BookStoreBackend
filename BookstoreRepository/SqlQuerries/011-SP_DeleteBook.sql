create procedure SP_DeleteBook(
	@bookId int
)
As Begin
	DELETE FROM BookstoreBooks WHERE BookId=@bookId;
end