using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class IsExpertRepository
    {
        private MusicCompetitionDbContext dbContext;
        public IsExpertRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(int idGenre,long JMBG)
        {
            try
            {
                dbContext.IsExpertSet.Add(new IsExpert() { GenreID_GENRE = idGenre, JuryMemberJMBG_SIN = JMBG });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                this.dbContext = new MusicCompetitionDbContext();
                return false;
            }
        }


        public bool Remove(int idGenre,long JMBG)
        {
            try
            {
                var exp = dbContext.IsExpertSet.FirstOrDefault((x) => x.GenreID_GENRE == idGenre && x.JuryMemberJMBG_SIN == JMBG);

                var ev = dbContext.Evaluations.FirstOrDefault((x) => x.IsExpertJuryMemberJMBG_SIN == JMBG && x.IsExpertGenreID_GENRE == idGenre);
                while (ev != null)
                {
                    dbContext.Evaluations.Remove(ev);
                    dbContext.SaveChanges();
                    ev = dbContext.Evaluations.FirstOrDefault((x) => x.IsExpertJuryMemberJMBG_SIN == JMBG && x.IsExpertGenreID_GENRE == idGenre);
                }


                dbContext.IsExpertSet.Remove(exp);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Common.Models.IsExpert Read(int idGenre,long JMBG)
        {
            var temp = dbContext.IsExpertSet.FirstOrDefault((x) => x.GenreID_GENRE == idGenre && x.JuryMemberJMBG_SIN == JMBG);
            if(temp == null)
            {
                return null;
            }

            Common.Models.JuryMember jmtemp = new Common.Models.JuryMember(temp.JuryMemberJMBG_SIN, temp.JuryMember.FIRSTNAME_SIN, temp.JuryMember.LASTNAME_SIN, temp.JuryMember.BIRTHDATE_SIN, temp.JuryMember.EMAIL_SIN, temp.JuryMember.PHONE_NO_SIN,
                new Common.Models.ADDRESS(temp.JuryMember.ADDRESS_SIN.HOME_NUMBER, temp.JuryMember.ADDRESS_SIN.CITY, temp.JuryMember.ADDRESS_SIN.STREET));
            Common.Models.Genre gntemp = new Common.Models.Genre(temp.GenreID_GENRE, temp.Genre.GENRE_NAME);
            return new Common.Models.IsExpert(jmtemp.JMBG_SIN, gntemp.ID_GENRE, jmtemp, gntemp);

        }

        public IEnumerable<Common.Models.IsExpert> ReadAll()
        {
            var ret = new List<Common.Models.IsExpert>();
            dbContext.IsExpertSet.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.JuryMember jmtemp = new Common.Models.JuryMember(temp.JuryMemberJMBG_SIN, temp.JuryMember.FIRSTNAME_SIN, temp.JuryMember.LASTNAME_SIN, temp.JuryMember.BIRTHDATE_SIN, temp.JuryMember.EMAIL_SIN, temp.JuryMember.PHONE_NO_SIN,
                new Common.Models.ADDRESS(temp.JuryMember.ADDRESS_SIN.HOME_NUMBER, temp.JuryMember.ADDRESS_SIN.CITY, temp.JuryMember.ADDRESS_SIN.STREET));
                Common.Models.Genre gntemp = new Common.Models.Genre(temp.GenreID_GENRE, temp.Genre.GENRE_NAME);
                ret.Add(new Common.Models.IsExpert(jmtemp.JMBG_SIN, gntemp.ID_GENRE, jmtemp, gntemp));
            });
            return ret;
        }

        ~IsExpertRepository()
        {
            dbContext.Dispose();
        }
    }
}
