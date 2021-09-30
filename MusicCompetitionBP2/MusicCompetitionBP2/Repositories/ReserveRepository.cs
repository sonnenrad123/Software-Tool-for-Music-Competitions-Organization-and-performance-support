using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class ReserveRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public ReserveRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public bool Create(int idPublishingHouse,int idComp,int idPerformanceHall,DateTime dateRes,TimeSpan startTime, TimeSpan endTime)
        {
           
            try
            {

                dbContext.Reservations.Add(new Reserve() { START_TIME = startTime, END_TIME = endTime, DATE_RES = dateRes, OrganizeCompetitionID_COMP = idComp, OrganizePublishingHouseID_PH = idPublishingHouse, PerformanceHallID_HALL = idPerformanceHall });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(int idPublishingHouse, int idComp, int idPerformanceHall)
        {
            try
            {
                var res = dbContext.Reservations.FirstOrDefault((x) => x.PerformanceHallID_HALL==idPerformanceHall && x.OrganizeCompetitionID_COMP == idComp && x.OrganizePublishingHouseID_PH == idPublishingHouse);
                dbContext.Reservations.Remove(res);
                dbContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public Common.Models.Reserve Read(int idPublishingHouse, int idComp, int idPerformanceHall)
        {
            var temp = dbContext.Reservations.FirstOrDefault((x) => x.PerformanceHallID_HALL == idPerformanceHall && x.OrganizeCompetitionID_COMP == idComp && x.OrganizePublishingHouseID_PH == idPublishingHouse);
            if (temp == null)
            {
                return null;
            }

            Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Organize.Competition.ID_COMP, temp.Organize.Competition.DATE_START, temp.Organize.Competition.DATE_END, temp.Organize.Competition.NAME_COMP, temp.Organize.Competition.MAX_COMPETITORS);
            Common.Models.PublishingHouse pubhousetemp = new Common.Models.PublishingHouse(temp.Organize.PublishingHouseID_PH, temp.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.Organize.PublishingHouse.ADR_PH.CITY, temp.Organize.PublishingHouse.ADR_PH.STREET));

            Common.Models.Organize orgtemp = new Common.Models.Organize(temp.OrganizePublishingHouseID_PH, temp.OrganizeCompetitionID_COMP, pubhousetemp, cmptemp);
            Common.Models.PerformanceHall perfhaltemp = new Common.Models.PerformanceHall(idPerformanceHall, temp.PerformanceHall.NAME_HALL, temp.PerformanceHall.CAPACITY);
            return new Common.Models.Reserve(temp.DATE_RES, temp.START_TIME, temp.END_TIME, temp.Organize.PublishingHouseID_PH, temp.Organize.CompetitionID_COMP, temp.PerformanceHall.ID_HALL, orgtemp, perfhaltemp);

        }


        public IEnumerable<Common.Models.Reserve> ReadAll()
        {
            var ret = new List<Common.Models.Reserve>();
            dbContext.Reservations.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Organize.Competition.ID_COMP, temp.Organize.Competition.DATE_START, temp.Organize.Competition.DATE_END, temp.Organize.Competition.NAME_COMP, temp.Organize.Competition.MAX_COMPETITORS);
                Common.Models.PublishingHouse pubhousetemp = new Common.Models.PublishingHouse(temp.Organize.PublishingHouseID_PH, temp.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.Organize.PublishingHouse.ADR_PH.CITY, temp.Organize.PublishingHouse.ADR_PH.STREET));

                Common.Models.Organize orgtemp = new Common.Models.Organize(temp.OrganizePublishingHouseID_PH, temp.OrganizeCompetitionID_COMP, pubhousetemp, cmptemp);
                Common.Models.PerformanceHall perfhaltemp = new Common.Models.PerformanceHall(temp.PerformanceHallID_HALL, temp.PerformanceHall.NAME_HALL, temp.PerformanceHall.CAPACITY);
                ret.Add(new Common.Models.Reserve(temp.DATE_RES, temp.START_TIME, temp.END_TIME, temp.Organize.PublishingHouseID_PH, temp.Organize.CompetitionID_COMP, temp.PerformanceHall.ID_HALL, orgtemp, perfhaltemp));
            });
            return ret;
        }

        public void Update(Common.Models.Reserve ph)
        {
            var temp = dbContext.Reservations.FirstOrDefault((x) => x.OrganizeCompetitionID_COMP == ph.OrganizeCompetitionID_COMP && x.OrganizePublishingHouseID_PH == ph.OrganizePublishingHouseID_PH && ph.PerformanceHallID_HALL == x.PerformanceHallID_HALL);
            dbContext.Entry(temp).CurrentValues.SetValues(ph);
            dbContext.SaveChanges();
        }

        ~ReserveRepository()
        {
            dbContext.Dispose();
        }
    }
}
