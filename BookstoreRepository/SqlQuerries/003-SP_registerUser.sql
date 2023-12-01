create procedure SP_RegisterUser(
	@fullName varchar(50),
	@email varchar(100),
	@password varchar(100),
	@mobileNumber varchar(20)
)
As Begin
	IF NOT EXISTS(select * from BookstoreUser where email=@email)
	begin
		insert into BookstoreUser(FullName,email,password,MobileNumber) values(@fullName,@email,@password,@mobileNumber)    
		select * from BookstoreUser where email=@email
	end 
end

drop procedure SP_RegisterUser


