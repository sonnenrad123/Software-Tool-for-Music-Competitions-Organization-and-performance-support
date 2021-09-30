Create or alter trigger CascadeDeleteCompetition
on dbo.Competitions
instead of delete
as

	declare @CompID int;
	select @CompID = cmp.ID_COMP from deleted cmp;

	delete from MusicPerformances
	where CompetitingCompetitionID_COMP in (select ID_COMP from deleted);

	delete from Competitings
	where CompetitionID_COMP in (select ID_COMP from deleted);

	Delete from PossessesASet
	where CompetitionID_COMP in (select ID_COMP from deleted);

	delete from Reservations
	where OrganizeCompetitionID_COMP in (select ID_COMP from deleted);

	Delete from Organizations
	where CompetitionID_COMP in (select ID_COMP from deleted);

	Delete from Competitions
	where ID_COMP in (select ID_COMP from deleted);