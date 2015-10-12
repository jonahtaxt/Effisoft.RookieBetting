CREATE PROCEDURE [dbo].[AddGameBet]
	@UserId INT,
    @GameId INT,
    @HomeWins BIT,
    @HomeScore SMALLINT,
    @AwayScore SMALLINT
AS
BEGIN

	DECLARE @GameBetId INT;

	BEGIN TRY

		BEGIN TRAN AddGameBetTran;

			SELECT @GameBetId = (ISNULL(MAX(a.GameBetId), 0) + 1)
			FROM   dbo.GameBet a;

			INSERT INTO dbo.GameBet (GameBetId, UserId, GameId, HomeWins, HomeScore, AwayScore)
			VALUES (@GameBetId, @UserId, @GameId, @HomeWins, @HomeScore, @AwayScore);

		COMMIT TRAN AddGameBetTran;

	END TRY
	BEGIN CATCH

		DECLARE @msg NVARCHAR(1000);

		SET @msg = 'An error ocurred: ' + ERROR_MESSAGE() + ' line ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.';
		PRINT @msg;

		ROLLBACK TRAN AddGameBetTran;

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	
	END CATCH

END