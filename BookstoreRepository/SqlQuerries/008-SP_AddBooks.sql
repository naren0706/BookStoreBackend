create procedure SP_AddBooks(
@bookName varchar(50),
@bookDescription varchar(max),
@bookAuthor varchar(50),
@bookImage varchar(max),
@bookCount int,
@bookPrize int
)

As Begin
	insert into BookstoreBooks(bookname,BookDescription,BookAuthor,BookImage,BookCount,BookPrize,Rating,IsAvailable)
	values (@bookname,@bookDescription,@bookAuthor,@bookImage,@bookCount,@bookPrize,0,1)
	select * from BookstoreBooks where BookName=@bookname and BookAuthor = @bookAuthor;
End

drop procedure SP_AddBooks