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




        public bool Create(long CompetitorJMBG, int organizeCompetitionID_COMP, int organizePublishingHouseID_PH)
        {


            try
            {
                dbContext.Competitings.Add(new Competiting() { CompetitorJMBG_SIN = CompetitorJMBG, OrganizeCompetitionID_COMP = organizeCompetitionID_COMP, OrganizePublishingHouseID_PH = organizePublishingHouseID_PH }) ;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;   
            }


        }

        public bool Remove(long CompetitorJMBG, int organizeCompetitionID_COMP, int organizePublishingHouseID_PH)
        {
            try
            {
                var competiting = dbContext.Competitings.FirstOrDefault((x) => x.CompetitorJMBG_SIN == CompetitorJMBG && x.OrganizeCompetitionID_COMP == organizeCompetitionID_COMP && x.OrganizePublishingHouseID_PH == organizePublishingHouseID_PH);
                var mf = dbContext.MusicPerformances.FirstOrDefault((x) => x.CompetitingOrganizeCompetitionID_COMP == organizeCompetitionID_COMP && x.CompetitingOrganizePublishingHouseID_PH == organizePublishingHouseID_PH  && x.CompetitingCompetitorJMBG_SIN == CompetitorJMBG);

                while (mf != null)
                {
                    dbContext.MusicPerformances.Remove(mf);
                   
                    dbContext.SaveChanges();
                    mf = dbContext.MusicPerformances.FirstOrDefault((x) => x.CompetitingOrganizeCompetitionID_COMP == organizeCompetitionID_COMP && x.CompetitingOrganizePublishingHouseID_PH == organizePublishingHouseID_PH && x.CompetitingCompetitorJMBG_SIN == CompetitorJMBG);
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


        public Common.Models.Competiting Read(long CompetitorJMBG, int organizeCompetitionID_COMP, int organizePublishingHouseID_PH)
        {
            var temp = dbContext.Competitings.FirstOrDefault((x) => x.CompetitorJMBG_SIN == CompetitorJMBG && x.OrganizeCompetitionID_COMP == organizeCompetitionID_COMP && x.OrganizePublishingHouseID_PH == organizePublishingHouseID_PH);
            if (temp == null)
            {
                return null;
            }

            Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Organize.CompetitionID_COMP, temp.Organize.Competition.DATE_START, temp.Organize.Competition.DATE_END, temp.Organize.Competition.NAME_COMP, temp.Organize.Competition.MAX_COMPETITORS);
            Common.Models.PublishingHouse pubhoustmp = new Common.Models.PublishingHouse(temp.OrganizePublishingHouseID_PH, temp.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.Organize.PublishingHouse.ADR_PH.CITY, temp.Organize.PublishingHouse.ADR_PH.STREET));
            Common.Models.Organize cmptemp1 = new Common.Models.Organize(temp.OrganizePublishingHouseID_PH, temp.OrganizeCompetitionID_COMP, pubhoustmp, cmptemp);
            Common.Models.Competitor cmptemp2 = new Common.Models.Competitor(temp.Competitor.JMBG_SIN, temp.Competitor.FIRSTNAME_SIN, temp.Competitor.LASTNAME_SIN, temp.Competitor.BIRTHDATE_SIN, temp.Competitor.EMAIL_SIN, temp.Competitor.PHONE_NO_SIN,
                new Common.Models.ADDRESS(temp.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.Competitor.ADDRESS_SIN.CITY, temp.Competitor.ADDRESS_SIN.STREET));

            return new Common.Models.Competiting(temp.CompetitorJMBG_SIN, temp.OrganizeCompetitionID_COMP, temp.OrganizePublishingHouseID_PH, cmptemp2, cmptemp1);
        }

        public IEnumerable<Common.Models.Competiting> ReadAll()
        {
            var ret = new List<Common.Models.Competiting>();
            dbContext.Competitings.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Organize.CompetitionID_COMP, temp.Organize.Competition.DATE_START, temp.Organize.Competition.DATE_END, temp.Organize.Competition.NAME_COMP, temp.Organize.Competition.MAX_COMPETITORS);
                Common.Models.PublishingHouse pubhoustmp = new Common.Models.PublishingHouse(temp.OrganizePublishingHouseID_PH, temp.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.Organize.PublishingHouse.ADR_PH.CITY, temp.Organize.PublishingHouse.ADR_PH.STREET));
                Common.Models.Organize cmptemp1 = new Common.Models.Organize(temp.OrganizePublishingHouseID_PH, temp.OrganizeCompetitionID_COMP, pubhoustmp, cmptemp);
                Common.Models.Competitor cmptemp2 = new Common.Models.Competitor(temp.Competitor.JMBG_SIN, temp.Competitor.FIRSTNAME_SIN, temp.Competitor.LASTNAME_SIN, temp.Competitor.BIRTHDATE_SIN, temp.Competitor.EMAIL_SIN, temp.Competitor.PHONE_NO_SIN,
                    new Common.Models.ADDRESS(temp.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.Competitor.ADDRESS_SIN.CITY, temp.Competitor.ADDRESS_SIN.STREET));

                ret.Add(new Common.Models.Competiting(temp.CompetitorJMBG_SIN, temp.OrganizeCompetitionID_COMP, temp.OrganizePublishingHouseID_PH, cmptemp2, cmptemp1));
            });
            return ret;
        }

        ~CompetitingRepository()
        {
            dbContext.Dispose();
        }
    }
}
