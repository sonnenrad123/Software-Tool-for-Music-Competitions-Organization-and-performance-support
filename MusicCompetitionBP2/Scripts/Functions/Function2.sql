CREATE OR ALTER FUNCTION PublishingHousesOrganizingCompetitonInHallWithSpecifiedID
(
	@hallID as int
)

returns table
as
	return select ph.ID_PH, ph.NAME_PH, hall.NAME_HALL,hall.CAPACITY
	from PublishingHouses ph inner join Organizations org on ph.ID_PH = org.PublishingHouseID_PH
	inner join Reservations r on r.OrganizePublishingHouseID_PH = org.PublishingHouseID_PH
	inner join PerformanceHalls hall on hall.ID_HALL = r.PerformanceHallID_HALL and hall.ID_HALL = @hallID;
