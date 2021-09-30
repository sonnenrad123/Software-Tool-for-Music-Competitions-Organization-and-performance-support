Create or alter trigger CascadeDeleteCompetitorTrigger
ON dbo.Singers_Competitor
INSTEAD OF DELETE 
AS
	
	DECLARE @CompetitorJMBG bigint;
	SELECT @CompetitorJMBG = cmp.JMBG_SIN from deleted cmp;
	

	DELETE FROM MusicPerformances
	where CompetitingCompetitorJMBG_SIN in (select JMBG_SIN from deleted);

	DELETE FROM Competitings 
	where CompetitorJMBG_SIN in (select JMBG_SIN from deleted);

	DELETE FROM Singers_Competitor
	where JMBG_SIN in (select JMBG_SIN from deleted);


	