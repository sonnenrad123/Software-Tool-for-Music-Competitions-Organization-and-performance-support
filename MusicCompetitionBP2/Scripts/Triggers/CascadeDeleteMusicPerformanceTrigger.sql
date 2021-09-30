Create or alter trigger CascadeDeleteMusicPerformanceTrigger
ON dbo.MusicPerformances
INSTEAD OF DELETE 
AS
	DECLARE @MFID int;
	SELECT @MFID = cmp.ID_PERF from deleted cmp;


	DELETE FROM Evaluations
	where MusicPerformanceID_PERF in (select ID_PERF from deleted);
	
	DELETE FROM MusicPerformances
	where ID_PERF in (select ID_PERF from deleted);
	