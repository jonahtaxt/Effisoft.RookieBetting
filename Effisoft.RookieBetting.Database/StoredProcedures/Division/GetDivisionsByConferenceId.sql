CREATE PROCEDURE [dbo].[GetDivisionsByConferenceId]
	@ConferenceId INT
AS
	SELECT a.DivisionId,
		   a.DivisionName,
		   a.ConferenceId
	FROM   dbo.Division a
	WHERE  a.ConferenceId = @ConferenceId