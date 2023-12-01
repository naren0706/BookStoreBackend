create procedure SP_DeleteBook(
	@bookId int
)
As Begin
update BookstoreBooks set IsAvailable=0 where BookId=1
end

drop procedure SP_DeleteBook