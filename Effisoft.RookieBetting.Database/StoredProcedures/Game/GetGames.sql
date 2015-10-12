CREATE PROCEDURE [dbo].[GetGames]
AS
	SELECT a.GameId,
		   a.GameDate,
		   a.[Week],
		   a.HomeTeamId,
		   a.AwayTeamId,
		   a.Season,
		   a.MondayNight
	FROM   dbo.Game a
