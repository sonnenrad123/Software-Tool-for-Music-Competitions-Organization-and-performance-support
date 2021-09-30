using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class CompetitingRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public CompetitingRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }




        public bool Create(long CompetitorJMBG, int competitionID)
        {


            try
            {
                dbContext.Competitings.Add(new Competiting() { CompetitorJMBG_SIN = CompetitorJMBG, CompetitionID_COMP = competitionID });
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;   
            }


        }

        public bool Remove(long CompetitorJMBG, int competitionID)
        {
            try
            {
                var competiting = dbContext.Competitings.FirstOrDefault((x) => x.CompetitorJMBG_SIN == CompetitorJMBG && x.CompetitionID_COMP == competitionID);
                var mf = dbContext.MusicPerformances.FirstOrDefault((x) => x.CompetitingCompetitionID_COMP == competitionID && x.CompetitingCompetitorJMBG_SIN == CompetitorJMBG);

                while (mf != null)
                {
                    dbContext.MusicPerformances.Remove(mf);
                   
                    dbContext.SaveChanges();
                    mf = dbContext.MusicPerformances.FirstOrDefault((x) => x.CompetitingCompetitionID_COMP == competitionID && x.CompetitingCompetitorJMBG_SIN == CompetitorJMBG);
                }
                
                
                dbContext.Competitings.Remove(competiting);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Common.Models.Competiting Read(long CompetitorJMBG, int competitionID)
        {
            var temp = dbContext.Competitings.FirstOrDefault((x) => x.CompetitorJMBG_SIN == CompetitorJMBG && x.CompetitionID_COMP == competitionID);
            if(temp == null)
            {
                return null;
            }

            Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
            Common.Models.Competitor cmptemp2 = new Common.Models.Competitor(temp.Competitor.JMBG_SIN, temp.Competitor.FIRSTNAME_SIN, temp.Competitor.LASTNAME_SIN, temp.Competitor.BIRTHDATE_SIN, temp.Competitor.EMAIL_SIN, temp.Competitor.PHONE_NO_SIN,
                new Common.Models.ADDRESS(temp.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.Competitor.ADDRESS_SIN.CITY, temp.Competitor.ADDRESS_SIN.STREET));

            return new Common.Models.Competiting(temp.CompetitorJMBG_SIN, temp.CompetitionID_COMP,cmptemp2,cmptemp);
        }

        public IEnumerable<Common.Models.Competiting> ReadAll()
        {
            var ret = new List<Common.Models.Competiting>();
            dbContext.Competitings.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
                Common.Models.Competitor cmptemp2 = new Common.Models.Competitor(temp.Competitor.JMBG_SIN, temp.Competitor.FIRSTNAME_SIN, temp.Competitor.LASTNAME_SIN, temp.Competitor.BIRTHDATE_SIN, temp.Competitor.EMAIL_SIN, temp.Competitor.PHONE_NO_SIN,
                    new Common.Models.ADDRESS(temp.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.Competitor.ADDRESS_SIN.CITY, temp.Competitor.ADDRESS_SIN.STREET));
                ret.Add(new Common.Models.Competiting(temp.CompetitorJMBG_SIN, temp.CompetitionID_COMP, cmptemp2, cmptemp));
            });
            return ret;
        }

        ~CompetitingRepository()
        {
            dbContext.Dispose();
        }
    }
}
