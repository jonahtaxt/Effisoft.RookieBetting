CREATE PROCEDURE [dbo].[GetConferenceById]
	@ConferenceId INT
AS
	SELECT a.ConferenceId,
	       a.ConferenceName
	FROM   dbo.Conference a
	WHERE  a.ConferenceId = @ConferenceId
