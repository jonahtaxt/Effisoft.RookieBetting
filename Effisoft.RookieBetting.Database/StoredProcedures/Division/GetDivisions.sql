CREATE PROCEDURE [dbo].[GetDivisions]
	
AS
	SELECT a.DivisionId,
	       a.DivisionName,
		   a.ConferenceId
	FROM   dbo.Division a
