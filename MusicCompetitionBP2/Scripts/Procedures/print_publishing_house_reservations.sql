create or alter procedure print_publishing_house_reservations
as
begin
	declare @pubhName varchar(30)
	declare @date_res datetime
	declare @hallname varchar(30)

	select pubh.NAME_PH,res.DATE_RES,ph.NAME_HALL
	from PublishingHouses pubh inner join Organizations org on pubh.ID_PH =  org.PublishingHouseID_PH
	inner join Reservations res on org.PublishingHouseID_PH = res.OrganizePublishingHouseID_PH
	inner join PerformanceHalls ph on ph.ID_HALL = res.PerformanceHallID_HALL
end