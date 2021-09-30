CREATE OR ALTER FUNCTION PrintAllCompetitorsCompetitingOnCompetitionsWtihSpecifiedGenre
(
	@genreID as int
)
returns TABLE
as
	return select g.ID_GENRE,sing.FIRSTNAME_SIN,sing.LASTNAME_SIN
		from Singers sing inner join Competitings cmpg on sing.JMBG_SIN = cmpg.CompetitorJMBG_SIN
		inner join Competitions compet on compet.ID_COMP = cmpg.CompetitionID_COMP
		inner join PossessesASet on PossessesASet.CompetitionID_COMP = cmpg.CompetitionID_COMP
		inner join Genres g on g.ID_GENRE = PossessesASet.GenreID_GENRE and g.ID_GENRE = @genreID;


--select * from PrintAllCompetitorsCompetitingOnCompetitionsWtihSpecifiedGenre(3)