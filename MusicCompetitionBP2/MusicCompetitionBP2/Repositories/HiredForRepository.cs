using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class HiredForRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public HiredForRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(long JuryJMBG,int competitionID)
        {
            try
            {
                dbContext.HiredForSet.Add(new HiredFor(){ CompetitionID_COMP = competitionID,JuryMemberJMBG_SIN = JuryJMBG});
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(long JuryJMBG, int competitionID)
        {
            try
            {
                var hiredFor = dbContext.HiredForSet.FirstOrDefault((x) => x.CompetitionID_COMP == competitionID && x.JuryMemberJMBG_SIN == JuryJMBG);
                dbContext.HiredForSet.Remove(hiredFor);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Common.Models.HiredFor Read(long JuryJMBG, int competitionID)
        {
            var temp = dbContext.HiredForSet.FirstOrDefault((x) => x.CompetitionID_COMP == competitionID && x.JuryMemberJMBG_SIN == JuryJMBG);
            if(temp == null)
            {
                return null;
            }
            Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
            Common.Models.JuryMember jurytemp = new Common.Models.JuryMember(temp.JuryMember.JMBG_SIN, temp.JuryMember.FIRSTNAME_SIN, temp.JuryMember.LASTNAME_SIN, temp.JuryMember.BIRTHDATE_SIN, temp.JuryMember.EMAIL_SIN,
                temp.JuryMember.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.JuryMember.ADDRESS_SIN.HOME_NUMBER, temp.JuryMember.ADDRESS_SIN.CITY, temp.JuryMember.ADDRESS_SIN.STREET));
            return new Common.Models.HiredFor(temp.JuryMemberJMBG_SIN, temp.CompetitionID_COMP, jurytemp, cmptemp);
        }

        public IEnumerable<Common.Models.HiredFor> ReadAll()
        {
            var ret = new List<Common.Models.HiredFor>();
            dbContext.HiredForSet.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
                Common.Models.JuryMember jurytemp = new Common.Models.JuryMember(temp.JuryMember.JMBG_SIN, temp.JuryMember.FIRSTNAME_SIN, temp.JuryMember.LASTNAME_SIN, temp.JuryMember.BIRTHDATE_SIN, temp.JuryMember.EMAIL_SIN,
                temp.JuryMember.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.JuryMember.ADDRESS_SIN.HOME_NUMBER, temp.JuryMember.ADDRESS_SIN.CITY, temp.JuryMember.ADDRESS_SIN.STREET));
                ret.Add(new Common.Models.HiredFor(temp.JuryMemberJMBG_SIN, temp.CompetitionID_COMP, jurytemp, cmptemp));
            });
            return ret;
        }

        ~HiredForRepository()
        {
            dbContext.Dispose();
        }
    }
}
