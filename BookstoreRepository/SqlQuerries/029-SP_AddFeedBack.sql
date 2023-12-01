create procedure SP_AddFeedBack(
@UserId int,
@BookId int,
@CustomerDescription varchar(max),
@Rating int
)

as begin 
	declare @feedbackId int;
	insert into tbl_feedBack(UserId,BookId,CustomerDescription,Rating) values (@UserId,@BookId,@CustomerDescription,@Rating)
	set @feedbackId = SCOPE_IDENTITY();
	SELECT * FROM tbl_feedBack INNER JOIN
                         BookstoreBooks ON tbl_feedBack.BookId = BookstoreBooks.BookId INNER JOIN
                         BookstoreUser ON tbl_feedBack.UserId = BookstoreUser.userId where tbl_feedBack.FeedbackId=@feedbackId
end 