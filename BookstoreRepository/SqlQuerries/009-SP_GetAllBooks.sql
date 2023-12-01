use Bookstore
create procedure SP_GetAllBooks
As Begin
	select * from BookstoreBooks ;
End

drop procedure SP_GetAllBooks