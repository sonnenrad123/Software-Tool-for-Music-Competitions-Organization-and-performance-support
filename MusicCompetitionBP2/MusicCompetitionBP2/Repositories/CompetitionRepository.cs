using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class CompetitionRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public CompetitionRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Common.Models.Competition cmp,out int idComp)
        {
            
            Competition temp = new Competition()
            {
                DATE_START = cmp.DATE_START,
                MAX_COMPETITORS = cmp.MAX_COMPETITORS,
                NAME_COMP = cmp.NAME_COMP,
                DATE_END = cmp.DATE_END,
                
            };
            dbContext.Competitions.Add(temp);
            
            bool ok = dbContext.SaveChanges() > 0 ? true : false;
            if (ok)
            {
                idComp = temp.ID_COMP;
            }
            else
            {
                idComp = -1;
            }
            return ok;
        }

        public Common.Models.Competition Read(int idComp)
        {
            var temp = dbContext.Competitions.AsNoTracking().FirstOrDefault((x) => x.ID_COMP == idComp);
            if(temp == null)
            {
                return null;
            }
            return new Common.Models.Competition(temp.ID_COMP, temp.DATE_START, temp.DATE_END, temp.NAME_COMP, temp.MAX_COMPETITORS);

        }

        public IEnumerable<Common.Models.Competition> ReadAll()
        {
            var ret = new List<Common.Models.Competition>();
            dbContext.Competitions.AsNoTracking().ToList().ForEach((temp) =>
                {
                    ret.Add(new Common.Models.Competition(temp.ID_COMP, temp.DATE_START, temp.DATE_END, temp.NAME_COMP, temp.MAX_COMPETITORS));
                }
            );
            return ret;
        }

        public void Update(Common.Models.Competition comp)
        {
            var temp = dbContext.Competitions.FirstOrDefault((x) => x.ID_COMP == comp.ID_COMP);
            dbContext.Entry(temp).CurrentValues.SetValues(comp);
            dbContext.SaveChanges();
        }

        public bool Remove(int idComp)
        {
            try
            {
                Competition c = dbContext.Competitions.FirstOrDefault((x) => x.ID_COMP == idComp);
                if(c == null)
                {
                    return false;
                }

                //prvo sve rezervacije za to takmicenje
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from Reservations where OrganizeCompetitionID_COMP =  {0}", idComp));
                OrganizeRepository or = new OrganizeRepository(dbContext);

                //pa organizacije
                Organize orgtemp = dbContext.Organizations.FirstOrDefault(o => o.CompetitionID_COMP == idComp);
                while(orgtemp != null)
                {
                    or.Remove(orgtemp.CompetitionID_COMP, orgtemp.PublishingHouseID_PH);
                    orgtemp = dbContext.Organizations.FirstOrDefault(o => o.CompetitionID_COMP == idComp);
                }
                //pa zaduzenja
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from HiredForSet where CompetitionID_COMP = {0}", idComp));
                //pa poseduje zanr
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from PossessesASet where CompetitionID_COMP = {0}", idComp));


                dbContext.Competitions.Remove(c);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        ~CompetitionRepository()
        {
            dbContext.Dispose();
        }
    }
}
