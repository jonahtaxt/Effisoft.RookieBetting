CREATE PROCEDURE [dbo].[UpdateGameBet]
	@GameBetId INT,
	@UserId INT,
    @GameId INT,
    @HomeWins BIT,
    @HomeScore SMALLINT,
    @AwayScore SMALLINT
AS
BEGIN

	BEGIN TRY

		BEGIN TRAN UpdateGameBetTran;

			
			UPDATE dbo.GameBet 
			SET    UserId = @UserId,
				   GameId = @GameId,
				   HomeWins = @HomeWins,
				   HomeScore = @HomeScore, 
				   AwayScore = @AwayScore
			WHERE  GameBetId = @GameBetId;

		COMMIT TRAN UpdateGameBetTran;

	END TRY
	BEGIN CATCH

		DECLARE @msg NVARCHAR(1000);

		SET @msg = 'An error ocurred: ' + ERROR_MESSAGE() + ' line ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.';
		PRINT @msg;

		ROLLBACK TRAN UpdateGameBetTran;

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	
	END CATCH

END
