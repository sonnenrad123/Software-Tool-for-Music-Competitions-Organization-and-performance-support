Create or alter trigger CascadeDeleteJuryMemberTrigger
ON dbo.Singers_JuryMember
INSTEAD OF DELETE
AS

	DECLARE @JuryMemberJMBG bigint;
	SELECT @JuryMemberJMBG = cmp.JMBG_SIN from deleted cmp;


	DELETE FROM HiredForSet
	where JuryMemberJMBG_SIN in (select JMBG_SIN from deleted);

	DELETE FROM Evaluations
	where IsExpertJuryMemberJMBG_SIN in (select JMBG_SIN from deleted);

	DELETE FROM IsExpertSet
	where JuryMemberJMBG_SIN in (select JMBG_SIN from deleted);

	DELETE FROM Singers_JuryMember
	where JMBG_SIN in (select JMBG_SIN from deleted)