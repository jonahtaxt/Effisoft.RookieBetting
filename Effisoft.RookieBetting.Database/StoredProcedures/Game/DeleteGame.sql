CREATE PROCEDURE [dbo].[DeleteGame]
	@GameId INT
AS
BEGIN

	BEGIN TRY

		BEGIN TRAN DeleteGameTran;

			DELETE FROM dbo.Game
			WHERE  GameId = @GameId;

		COMMIT TRAN DeleteGameTran;

	END TRY
	BEGIN CATCH

		DECLARE @msg NVARCHAR(1000);

		SET @msg = 'An error ocurred: ' + ERROR_MESSAGE() + ' line ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.';
		PRINT @msg;

		ROLLBACK TRAN DeleteGameTran;

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	
	END CATCH

END

