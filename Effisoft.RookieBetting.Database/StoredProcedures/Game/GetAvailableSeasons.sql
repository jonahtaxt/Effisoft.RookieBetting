CREATE PROCEDURE [dbo].[GetAvailableSeasons]
	
AS
	SELECT DISTINCT(Season)
	FROM   dbo.Game
