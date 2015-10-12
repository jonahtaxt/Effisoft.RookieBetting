CREATE PROCEDURE [dbo].[GetGame]
	@GameDate DATETIME,
	@HomeTeamCode VARCHAR(3),
	@AwayTeamCode VARCHAR(3)
AS
	SELECT a.GameId,
		   a.GameDate,
		   a.[Week],
		   a.HomeTeamId,
		   a.HomeScore,
		   a.AwayTeamId,
		   a.AwayScore,
		   a.Season,
		   a.MondayNight
	FROM   dbo.Game a
	INNER JOIN
		   dbo.Team b
	ON     b.TeamId = a.HomeTeamId
	INNER JOIN
		   dbo.Team c
	ON     c.TeamId = a.AwayTeamId
	WHERE  CONVERT(DATE, a.GameDate) = CONVERT(DATE, @GameDate)
	AND    b.TeamCode = @HomeTeamCode
	AND    c.TeamCode = @AwayTeamCode