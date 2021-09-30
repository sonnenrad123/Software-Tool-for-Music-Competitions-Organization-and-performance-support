Create or alter trigger CascadeDeletePerformanceHallTrigger
on dbo.PerformanceHalls
instead of delete
as

	declare @PhID int;
	select @PhID = cmp.ID_HALL from deleted cmp;

	delete from Reservations
	where PerformanceHallID_HALL in (select ID_HALL from deleted);

	delete from PerformanceHalls
	where ID_HALL in (select ID_HALL from deleted);