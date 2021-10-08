using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class PerformanceHallRepository
    {
        private readonly MusicCompetitionDbContext dbContext;

        public PerformanceHallRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Common.Models.PerformanceHall ph)
        {
            PerformanceHall phtemp = new PerformanceHall()
            {
                CAPACITY = ph.CAPACITY,
                NAME_HALL = ph.NAME_HALL,

            };
            dbContext.PerformanceHalls.Add(phtemp);
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public Common.Models.PerformanceHall Read(int idPH)
        {
            var temp = dbContext.PerformanceHalls.AsNoTracking().FirstOrDefault((x) => x.ID_HALL == idPH);
            if(temp == null)
            {
                return null;
            }
            return new Common.Models.PerformanceHall(temp.ID_HALL, temp.NAME_HALL, temp.CAPACITY);
        }

        public IEnumerable<Common.Models.PerformanceHall> ReadAll()
        {
            var ret = new List<Common.Models.PerformanceHall>();
            dbContext.PerformanceHalls.AsNoTracking().ToList().ForEach((temp) =>
            {
                ret.Add(new Common.Models.PerformanceHall(temp.ID_HALL, temp.NAME_HALL, temp.CAPACITY));
            });
            return ret;
        }

        public void Update(Common.Models.PerformanceHall ph)
        {
            var temp = dbContext.PerformanceHalls.FirstOrDefault((x) => x.ID_HALL == ph.ID_HALL);
            dbContext.Entry(temp).CurrentValues.SetValues(ph);
            dbContext.SaveChanges();
        }

        public bool Remove(int phID)
        {
            try
            {
                PerformanceHall ph = dbContext.PerformanceHalls.FirstOrDefault((x) => x.ID_HALL == phID);
                if(ph == null)
                {
                    return false;
                }
                
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from Reservations where PerformanceHallID_HALL = {0}", phID));

                dbContext.PerformanceHalls.Remove(ph);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        ~PerformanceHallRepository()
        {
            dbContext.Dispose();
        }
    }
}
