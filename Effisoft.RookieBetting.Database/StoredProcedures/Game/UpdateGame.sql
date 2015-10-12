CREATE PROCEDURE [dbo].[UpdateGame]
	@GameId INT,
	@GameDate DATETIME,
	@Week INT,
    @HomeTeamId INT,
	@HomeScore INT,
    @AwayTeamId INT, 
	@AwayScore INT,
    @Season INT, 
    @MondayNight BIT
AS
BEGIN

	BEGIN TRY

		BEGIN TRAN UpdateGameTran;

			UPDATE dbo.Game
			SET    GameDate = @GameDate,
			       [Week] = @Week,
			       HomeTeamId = @HomeTeamId,
				   HomeScore = @HomeScore,
				   AwayTeamId = @AwayTeamId,
				   AwayScore = @AwayScore,
				   Season = @Season,
				   MondayNight = @MondayNight
			WHERE  GameId = @GameId;

		COMMIT TRAN UpdateGameTran;

	END TRY
	BEGIN CATCH

		DECLARE @msg NVARCHAR(1000);

		SET @msg = 'An error ocurred: ' + ERROR_MESSAGE() + ' line ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.';
		PRINT @msg;

		ROLLBACK TRAN UpdateGameTran;

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	
	END CATCH

END
