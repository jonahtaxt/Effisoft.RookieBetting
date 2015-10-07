CREATE PROCEDURE [dbo].[GetDivisionByName]
	@DivisionName VARCHAR(9)
AS
	SELECT a.DivisionId,
	       a.DivisionName,
		   a.ConferenceId
	FROM   dbo.Division a
	WHERE  a.DivisionName = @DivisionName
