CREATE PROCEDURE [dbo].[AddGame]
	@GameDate DATETIME,
	@Week INT,
    @HomeTeamId INT, 
    @AwayTeamId INT, 
    @Season INT, 
    @MondayNight BIT
AS
BEGIN

	DECLARE @GameId INT;

	BEGIN TRY

		BEGIN TRAN AddGameTran;

			SELECT @GameId = (ISNULL(MAX(a.GameId), 0) + 1)
			FROM   dbo.Game a;

			INSERT INTO dbo.Game (GameId, GameDate, [Week], HomeTeamId, AwayTeamId, Season, MondayNight)
			VALUES (@GameId, @GameDate, @Week, @HomeTeamId, @AwayTeamId, @Season, @MondayNight);

		COMMIT TRAN AddGameTran;

	END TRY
	BEGIN CATCH

		DECLARE @msg NVARCHAR(1000);

		SET @msg = 'An error ocurred: ' + ERROR_MESSAGE() + ' line ' + CONVERT(NVARCHAR(255), ERROR_LINE() ) + '.';
		PRINT @msg;

		ROLLBACK TRAN AddGameTran;

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE();
		
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	
	END CATCH

END