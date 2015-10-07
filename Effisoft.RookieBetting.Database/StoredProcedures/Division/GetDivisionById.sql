CREATE PROCEDURE [dbo].[GetDivisionById]
	@DivisionId INT
AS
	SELECT a.DivisionId,
	       a.DivisionName,
		   a.ConferenceId
	FROM   dbo.Division a
	WHERE  a.DivisionId = @DivisionId
