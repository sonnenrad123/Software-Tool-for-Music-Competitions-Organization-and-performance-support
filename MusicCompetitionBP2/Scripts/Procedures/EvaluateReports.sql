create or alter procedure print_competiting_reports
as
begin
	declare @nameSing varchar(30)
	declare @lastNameSing varchar(30)
	declare @nameJM varchar(30)
	declare @lastNameJM varchar(30)
	declare @genreName varchar(30)
	declare @mark smallint
	declare @comment varchar(30)

	DECLARE competitors_cursor CURSOR FOR 
	select s.FIRSTNAME_SIN, s.LASTNAME_SIN, jm.FIRSTNAME_SIN, jm.LASTNAME_SIN,g.GENRE_NAME, ev.MARK,ev.COMMENT
	from Evaluations ev inner join MusicPerformances mp on ev.MusicPerformanceID_PERF=mp.ID_PERF
	left join Competitings cmp on mp.CompetitingCompetitorJMBG_SIN = cmp.CompetitorJMBG_SIN and mp.CompetitingCompetitionID_COMP = cmp.CompetitionID_COMP
	left join Singers_Competitor ss on ss.JMBG_SIN = cmp.CompetitorJMBG_SIN
	left join Singers_JuryMember jmm on jmm.JMBG_SIN = ev.IsExpertJuryMemberJMBG_SIN
	left join Singers s on s.JMBG_SIN = ss.JMBG_SIN
	left join Singers jm on jm.JMBG_SIN = jmm.JMBG_SIN
	left join Competitions comp on comp.ID_COMP = cmp.CompetitionID_COMP
	left join Genres g on g.ID_GENRE = ev.IsExpertGenreID_GENRE

	open competitors_cursor
	fetch next from competitors_cursor INTO @nameSing,@lastNameSing,@nameJM,@lastNameJM,@genreName,@mark,@comment
	while(@@FETCH_STATUS = 0)
		begin

			print  'Singer: '+ @nameSing + ' ' + @lastNameSing + ', was evaluated by: ' + @nameJM + ' ' + @lastNameJM + ', who is expert for: ' + @genreName + '.' + char(13) +
							   'Comment for performance: ' + @comment + ', Mark: ' + CAST(@mark AS VARCHAR) + '.' + char(13)

			fetch next from competitors_cursor INTO @nameSing,@lastNameSing,@nameJM,@lastNameJM,@genreName,@mark,@comment
		end
	close competitors_cursor;
	deallocate competitors_cursor;
end