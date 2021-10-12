using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class CityRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public CityRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public IEnumerable<Common.Models.City> ReadAll()
        {
            var ret = new List<Common.Models.City>();
            dbContext.Cities.AsNoTracking().ToList().ForEach((temp) =>
            {
                ret.Add(new Common.Models.City(temp.Postcode, temp.CityName));
            }
            );
            return ret;
        }
    }
}
