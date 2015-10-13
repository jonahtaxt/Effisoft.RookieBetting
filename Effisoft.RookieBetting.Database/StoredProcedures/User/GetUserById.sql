CREATE PROCEDURE [dbo].[GetUserById]
	@UserId INT
AS
BEGIN
	SELECT 
		U.[UserId]         
		,U.[Name] AS NameUser          
		,U.[LastName]       
		,U.[SurName]        
		,U.[Email]
		,U.[Password]  
		,U.[CreationDate]   
		,U.[UpdateDate]     
		,U.[Active]
		,U.[RoleId]
	FROM 
		[User] U
	WHERE U.UserId = @UserId
END