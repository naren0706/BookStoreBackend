create procedure SP_ForgotPassword(
	@email varchar(100)
)
As Begin
	select * from BookstoreUser where email=@email 
end


