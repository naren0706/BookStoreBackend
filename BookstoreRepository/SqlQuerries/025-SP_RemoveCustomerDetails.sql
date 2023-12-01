create procedure SP_RemoveCustomerDetails
(
	@customerId int,
	@userId int
)
as begin 
	DELETE FROM CustomerDetails WHERE customerId=@customerId and userId = @userId;
end

select * from customerdetails