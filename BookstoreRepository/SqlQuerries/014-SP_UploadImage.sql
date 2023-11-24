create procedure SP_UploadImage
(
	@bookid int,
	@fileLink varchar(max)
)
as begin 
	update BookstoreBooks set BookImage = @fileLink where BookId=@bookid
end