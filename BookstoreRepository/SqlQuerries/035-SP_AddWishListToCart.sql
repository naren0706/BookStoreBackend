create procedure SP_AddWishListToCart(
    @userId INT
)
AS
BEGIN
    -- Insert values into TBL_Cart from BooksWishList

    INSERT INTO TBL_Cart (UserId, BookId, CartCount, isAvailable)
    SELECT UserId, BookId, 1, 1
    FROM BooksWishList
    WHERE UserId = @userId and isAvailable=1;
END;

drop procedure SP_AddWishListToCart
