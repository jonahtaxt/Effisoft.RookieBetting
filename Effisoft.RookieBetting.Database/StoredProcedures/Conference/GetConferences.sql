CREATE PROCEDURE [dbo].[GetConferences]
AS
	SELECT a.ConferenceId,
	       a.ConferenceName
	FROM   dbo.Conference a
