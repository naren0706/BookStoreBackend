create procedure SP_ResetPassword(
	@email varchar(100),
	@password varchar(max)
)
As Begin
	UPDATE BookstoreUser
	SET password = @password
	WHERE email=@email;
end


