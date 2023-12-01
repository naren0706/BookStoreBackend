create procedure SP_GetAllCustomerDetails
(
 @userId int
 )
 as begin 
	SELECT * FROM CustomerDetails INNER JOIN
                         tbl_Type ON CustomerDetails.typeId = tbl_Type.typeId INNER JOIN
                         BookstoreUser ON CustomerDetails.userId = BookstoreUser.userId where CustomerDetails.userId=@userId

 end