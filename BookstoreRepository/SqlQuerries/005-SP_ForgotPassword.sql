create procedure SP_ForgotPassword(
	@email varchar(100)
)
As Begin
	select * from BookstoreUser where email=@email 
end


DELETE FROM BookstoreUser WHERE userId=2;

	select * from BookstoreUser where email='narenthrakishhore@gmaill.com' 
