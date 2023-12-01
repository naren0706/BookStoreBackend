CREATE PROCEDURE SP_UpdateCustomerDetails
(
	@customerID	int,
	@fullName varchar(max),
    @mobileNum varchar(max),
    @address varchar(max),
    @cityOrTown varchar(max),
    @state varchar(max),
    @typeId int,
    @userId int
)
AS
BEGIN

     UPDATE CustomerDetails
    SET
        fullName = @fullName,
        mobileNum = @mobileNum,
        address = @address,
        cityOrTown = @cityOrTown,
        state = @state,
        typeId = @typeId,
        userId = @userId
    WHERE customerID = @customerID;

    SELECT * FROM CustomerDetails INNER JOIN
                         tbl_Type ON CustomerDetails.typeId = tbl_Type.typeId INNER JOIN
                         BookstoreUser ON CustomerDetails.userId = BookstoreUser.userId where customerID=@customerID 
END;