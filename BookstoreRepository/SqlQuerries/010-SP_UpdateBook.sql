
use Bookstore
create procedure SP_UpdateBook(
	@bookId int,
	@bookName varchar(50),
	@bookDescription varchar(max),
	@bookAuthor varchar(50),
	@bookImage varchar(max),
	@bookCount int,
	@bookPrize int
)

As Begin
	update BookstoreBooks 
	SET 
	BookName=@bookName,
	BookDescription=@bookDescription,
	BookAuthor=@bookAuthor,
	BookImage=@bookImage,
	BookCount=@bookCount,
	BookPrize=@bookPrize
	WHERE BookId=@bookId;

	select * from BookstoreBooks where BookId=@bookId;
End

drop procedure SP_UpdateBook