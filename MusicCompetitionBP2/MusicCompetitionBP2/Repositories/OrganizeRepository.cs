using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class OrganizeRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public OrganizeRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(int idComp,int phID)
        {
            try
            {
                dbContext.Organizations.Add(new Organize() { CompetitionID_COMP = idComp, PublishingHouseID_PH = phID });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(int idComp,int phID)
        {
            try
            {
                var org = dbContext.Organizations.FirstOrDefault((x) => x.CompetitionID_COMP == idComp && x.PublishingHouseID_PH == phID);
                var reserv = dbContext.Reservations.FirstOrDefault((x) => x.OrganizeCompetitionID_COMP == idComp && x.OrganizePublishingHouseID_PH == phID);

                //obrisem sve rezervacije hala
                while (reserv != null)
                {
                    dbContext.Reservations.Remove(reserv);
                    dbContext.SaveChanges();
                    reserv = dbContext.Reservations.FirstOrDefault((x) => x.OrganizeCompetitionID_COMP == idComp && x.OrganizePublishingHouseID_PH == phID);
                }

                //sve competitings na takmicenju idComp koje organizuje kuca sa PHID i dalje kaskadno sta treba
                CompetitingRepository cr = new CompetitingRepository(dbContext);

                Competiting competiting = dbContext.Competitings.FirstOrDefault(x => x.OrganizeCompetitionID_COMP == idComp && x.OrganizePublishingHouseID_PH == phID);
                while(competiting != null)
                {
                    cr.Remove(competiting.CompetitorJMBG_SIN, competiting.OrganizeCompetitionID_COMP, competiting.OrganizePublishingHouseID_PH);
                    competiting = dbContext.Competitings.FirstOrDefault(x => x.OrganizeCompetitionID_COMP == idComp && x.OrganizePublishingHouseID_PH == phID);
                }


                dbContext.Organizations.Remove(org);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Common.Models.Organize Read(int idComp,int phID)
        {
            var temp = dbContext.Organizations.FirstOrDefault((x) => x.CompetitionID_COMP == idComp && x.PublishingHouseID_PH == phID);

            if (temp == null)
            {
                return null;
            }

            Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
            Common.Models.PublishingHouse phtemp = new Common.Models.PublishingHouse(temp.PublishingHouse.ID_PH, temp.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.PublishingHouse.ADR_PH.HOME_NUMBER, temp.PublishingHouse.ADR_PH.CITY, temp.PublishingHouse.ADR_PH.STREET));
            return new Common.Models.Organize(phtemp.ID_PH, cmptemp.ID_COMP, phtemp, cmptemp);
        }

        public IEnumerable<Common.Models.Organize> ReadAll()
        {
            var ret = new List<Common.Models.Organize>();
            dbContext.Organizations.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
                Common.Models.PublishingHouse phtemp = new Common.Models.PublishingHouse(temp.PublishingHouse.ID_PH, temp.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.PublishingHouse.ADR_PH.HOME_NUMBER, temp.PublishingHouse.ADR_PH.CITY, temp.PublishingHouse.ADR_PH.STREET));
                ret.Add(new Common.Models.Organize(phtemp.ID_PH, cmptemp.ID_COMP, phtemp, cmptemp));
            });
            return ret;
        }

        ~OrganizeRepository()
        {
            dbContext.Dispose();
        }
    }
}
