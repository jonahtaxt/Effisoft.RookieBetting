CREATE PROCEDURE [dbo].[GetDivisionsByConferenceName]
	@ConferenceName VARCHAR(8)
AS
	SELECT a.DivisionId,
		   a.DivisionName,
		   a.ConferenceId
	FROM   dbo.Division a
	INNER JOIN
	       dbo.Conference b
	ON     b.ConferenceId = a.ConferenceId
	WHERE  b.ConferenceName = @ConferenceName
