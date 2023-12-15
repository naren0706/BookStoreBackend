create procedure SP_AddnewPlaceOrder(
	@userId int,
	@customerId int
)
as begin 
	insert into tbl_OrderSummary(CartId) select CartId from TBL_Cart where UserId=@userId and isAvailable=1;

	INSERT INTO tbl_OrderPlaced (CustomerId, CartId,UserId)
		SELECT @customerId, CartId,@userId
		FROM TBL_Cart
		WHERE UserId = @userId and isAvailable=1;
	UPDATE TBL_Cart
	SET isAvailable = 0 
	WHERE UserId = @userId;
end

drop procedure SP_AddnewPlaceOrder
SELECT * FROM tbl_OrderPlaced
select * from TBL_Cart

