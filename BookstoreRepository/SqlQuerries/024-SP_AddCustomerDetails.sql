CREATE PROCEDURE SP_AddCustomerDetails
(
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
    DECLARE @customerID int;

    INSERT INTO CustomerDetails (fullName, mobileNum, address, cityOrTown, state, typeId, userId)
    VALUES (@fullName, @mobileNum, @address, @cityOrTown, @state, @typeId, @userId);
    SET @customerID = SCOPE_IDENTITY();
    SELECT * FROM CustomerDetails INNER JOIN
                         tbl_Type ON CustomerDetails.typeId = tbl_Type.typeId INNER JOIN
                         BookstoreUser ON CustomerDetails.userId = BookstoreUser.userId where customerID=@customerID 
END;

drop procedure SP_AddCustomerDetails
-- Example usage of the stored procedure
	
DECLARE @customerID int;

EXEC SP_AddCustomerDetails
    @fullName = 'John Doe',
    @mobileNum = '123-456-7890',
    @address = '123 Main St',
    @cityOrTown = 'Anytown',
    @state = 'CA',
    @typeId = 1,
    @userId = 1,
    @customerID = @customerID OUTPUT;

-- Now @customerID contains the customerID of the inserted record
PRINT 'Inserted Customer ID: ' + CAST(@customerID AS varchar);
