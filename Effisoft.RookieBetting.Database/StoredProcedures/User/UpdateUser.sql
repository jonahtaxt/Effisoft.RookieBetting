CREATE PROCEDURE [dbo].[UpdateUser]
@UserId int,
@AddressId int,
@Name VARCHAR(150),
@LastName VARCHAR(150),
@SurName VARCHAR(150) = NULL,
@Email VARCHAR(100),
@UpdateDate DATETIME,
@RoleId SMALLINT,
@Active BIT

AS
BEGIN	
	DECLARE @msg VARCHAR

    SET NOCOUNT ON;

	BEGIN TRAN UpdateUserTran
	BEGIN TRY

		UPDATE [dbo].[User]
		SET		[Name] = @Name
				,[LastName] = @LastName    
				,[SurName] = @SurName
				,[Email] = @Email
				,[UpdateDate] = @UpdateDate
				,[Active] = @Active
				,[RoleId] = @RoleId
		WHERE [dbo].[User].[UserId] = @UserId

        COMMIT TRAN UpdateUserTran

    END TRY
	BEGIN CATCH
        SET @msg = 'Ocurrio un Error: ' + ERROR_MESSAGE() + ' en la línea ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.'
		PRINT @msg
        Rollback TRAN AddUser

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage, -- Message text.
				   @ErrorSeverity, -- Severity.
				   @ErrorState -- State.
				   );
    END CATCH

	SELECT 'False'
	RETURN

END