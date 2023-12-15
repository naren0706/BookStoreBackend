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
	
	update BookstoreBooks set Rating = (select Avg(Rating) from tbl_feedBack where BookId=@BookId and UserId=@UserId) where BookId=@BookId
end 

drop procedure SP_AddFeedBack

UPDATE BookstoreBooks
SET Rating = (
    SELECT CAST(sum(Rating)/COUNT(*) AS DECIMAL(4,2))
    FROM tbl_feedback
    WHERE BookId = 1 AND UserId = 1
)
WHERE BookId = 1;




	SELECT ROUND(AVG(Rating), 2) FROM BookstoreBooks;
	SELECT CAST(AVG(Rating) AS DECIMAL(2,2)) AS average_value FROM BookstoreBooks;
