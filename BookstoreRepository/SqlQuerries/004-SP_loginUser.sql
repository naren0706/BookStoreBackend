create procedure SP_loginUser(
	@email varchar(100),
	@password varchar(100)
)
As Begin
	select * from BookstoreUser where email=@email and password=@password
end


