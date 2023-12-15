create procedure SP_AddWishListBookToCart(

@userId int,
@bookId int
)
as begin
    DECLARE @cartID int;

	--insert into TBL_Cart(UserId,BookId,CartCount,isAvailable) values(@userId,@bookId,1,1);

	if not EXISTS(select * from TBL_Cart where UserId=@userId and BookId=@bookId )
	begin
		insert into TBL_Cart(UserId,BookId,CartCount,isAvailable) values (@userId,@bookId,1,1)
			SET @cartID = SCOPE_IDENTITY();

		insert into tbl_OrderSummary(CartId) values (@cartID)

	end	
	if EXISTS(select * from TBL_Cart where UserId=@userId and BookId=@bookId )
	begin
		update TBL_Cart set isAvailable=1 where UserId=@userId and BookId=@bookId 

		insert into tbl_OrderSummary(CartId) values (@cartID)

	end	
	update BooksWishList set isAvailable=0 where UserId=@userId and BookId=@bookId
end


drop procedure SP_AddWishListBookToCart

