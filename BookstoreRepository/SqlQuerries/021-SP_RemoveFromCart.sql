CREATE PROCEDURE SP_RemoveFromCart
(
    @userId INT,
    @bookId INT
)
AS
BEGIN
    DELETE FROM tbl_OrderSummary WHERE CartId IN (SELECT CartId FROM TBL_Cart WHERE BookId = @bookId AND UserId = @userId);
    DELETE FROM TBL_Cart WHERE BookId = @bookId AND UserId = @userId;
END;

drop procedure SP_RemoveFromCart

select * from TBL_Cart
select * from tbl_OrderSummary