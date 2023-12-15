create procedure SP_GetFeedback(
@bookId int)
as begin
	select * from dbo.tbl_feedBack INNER JOIN
                         dbo.BookstoreBooks ON dbo.tbl_feedBack.BookId = dbo.BookstoreBooks.BookId INNER JOIN
                         dbo.BookstoreUser ON dbo.tbl_feedBack.UserId = dbo.BookstoreUser.userId where tbl_feedBack.BookId=@bookId
end