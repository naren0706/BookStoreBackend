create procedure SP_AddnewPlaceOrder(
	@userId int,
	@customerId int
)
as begin 
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


