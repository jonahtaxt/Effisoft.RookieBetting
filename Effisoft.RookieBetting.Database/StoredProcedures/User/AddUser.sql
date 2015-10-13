CREATE PROCEDURE [dbo].[AddUser]
@Name VARCHAR(150),
@LastName VARCHAR(150),
@SurName VARCHAR(150) = NULL,
@Email VARCHAR(100),
@Password VARCHAR(100) = NULL,
@CreationDate DATETIME,
@UpdateDate DATETIME,
@RoleId SMALLINT,
@Active BIT

AS
BEGIN
	
	DECLARE @msg VARCHAR

    SET NOCOUNT ON;

	BEGIN TRAN AddUserTran

	BEGIN TRY

		INSERT INTO [dbo].[User]
				([Name]
				,[LastName]       
				,[SurName]        
				,[Email]     
				,[Password] 
				,[CreationDate]   
				,[UpdateDate]     
				,[Active]   
				,[RoleId])   
		 VALUES
			   (@Name
				,@LastName       
				,@SurName        
				,@Email
				,@Password
				,@CreationDate
				,@UpdateDate    
				,@Active
				,@RoleId);

        COMMIT TRAN AddUserTran;

    END TRY
	Begin Catch
        SET @msg = 'Ocurrio un Error: ' + ERROR_MESSAGE() + ' en la línea ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.'
		PRINT @msg
        Rollback TRAN AddUserTran

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage,@ErrorSeverity, @ErrorState);
    End Catch

END