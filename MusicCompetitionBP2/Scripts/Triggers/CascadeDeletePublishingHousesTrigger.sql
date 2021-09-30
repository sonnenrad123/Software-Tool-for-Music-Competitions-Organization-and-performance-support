Create or alter trigger CascadeDeletePublishingHouse
on dbo.PublishingHouses
instead of delete
as

	declare @PhID int;
	select @PhID = cmp.ID_PH from deleted cmp;

	delete from Reservations
	where OrganizePublishingHouseID_PH in (select ID_PH from deleted);

	delete from Organizations
	where PublishingHouseID_PH in (select ID_PH from deleted);

	delete from PublishingHouses
	where ID_PH in (select ID_PH from deleted);