using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class PublishinHouseRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        
        public PublishinHouseRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public bool Create(Common.Models.PublishingHouse ph)
        {
            PublishingHouse chk = dbContext.PublishingHouses.AsNoTracking().FirstOrDefault(x => x.NAME_PH == ph.NAME_PH);

            if (chk != null)//zabranjeno isto ime
            {
                return false;
            }


            PublishingHouse phtemp = new PublishingHouse()
            {
                ADR_PH = new ADDRESS() { STREET = ph.ADR_PH.STREET, CITY = ph.ADR_PH.CITY, HOME_NUMBER = ph.ADR_PH.HOME_NUMBER },
                NAME_PH = ph.NAME_PH

            };
            dbContext.PublishingHouses.Add(phtemp);
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public Common.Models.PublishingHouse Read(int idPH)
        {
            var temp = dbContext.PublishingHouses.AsNoTracking().FirstOrDefault((x) => x.ID_PH == idPH);
            if(temp == null)
            {
                return null;
            }

            return new Common.Models.PublishingHouse(temp.ID_PH, temp.NAME_PH, new Common.Models.ADDRESS(temp.ADR_PH.HOME_NUMBER, temp.ADR_PH.CITY, temp.ADR_PH.STREET));
        }

        public IEnumerable<Common.Models.PublishingHouse> ReadAll()
        {
            var ret = new List<Common.Models.PublishingHouse>();
            dbContext.PublishingHouses.AsNoTracking().ToList().ForEach((temp) =>
            {
                ret.Add(new Common.Models.PublishingHouse(temp.ID_PH, temp.NAME_PH, new Common.Models.ADDRESS(temp.ADR_PH.HOME_NUMBER, temp.ADR_PH.CITY, temp.ADR_PH.STREET)));
            });
            return ret;
        }


        public void Update(Common.Models.PublishingHouse ph)
        {
            var temp = dbContext.PublishingHouses.FirstOrDefault((x) => x.ID_PH == ph.ID_PH);
            dbContext.Entry(temp).CurrentValues.SetValues(ph);
            dbContext.SaveChanges();
        }


        public bool Remove(int phID)
        {
            try
            {
                PublishingHouse ph = dbContext.PublishingHouses.FirstOrDefault((x) => x.ID_PH == phID);
                if(ph == null)
                {
                    return false;
                }
                dbContext.PublishingHouses.Remove(ph);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ~PublishinHouseRepository()
        {
            dbContext.Dispose();
        }

    }
}
