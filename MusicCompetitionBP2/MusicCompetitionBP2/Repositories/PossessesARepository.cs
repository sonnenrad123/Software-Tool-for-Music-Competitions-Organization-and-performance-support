using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class PossessesARepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public PossessesARepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(int idCompetition,int idGenre)
        {
            try
            {
                dbContext.PossessesASet.Add(new PossessesA() { CompetitionID_COMP = idCompetition, GenreID_GENRE = idGenre });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Remove(int idCompetition, int idGenre)
        {
            try
            {
                var psa = dbContext.PossessesASet.FirstOrDefault((x) => x.CompetitionID_COMP == idCompetition && x.GenreID_GENRE == idGenre);
                dbContext.PossessesASet.Remove(psa);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Common.Models.PossessesA Read(int idCompetition, int idGenre)
        {
            var temp = dbContext.PossessesASet.FirstOrDefault((x) => x.CompetitionID_COMP == idCompetition && x.GenreID_GENRE == idGenre);
            if (temp == null) {
                return null;
            }
             
            Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
            Common.Models.Genre gntemp = new Common.Models.Genre(temp.Genre.ID_GENRE, temp.Genre.GENRE_NAME);
            return new Common.Models.PossessesA(cmptemp.ID_COMP, gntemp.ID_GENRE, cmptemp, gntemp);

        }

        public IEnumerable<Common.Models.PossessesA> ReadAll()
        {
            var ret = new List<Common.Models.PossessesA>();
            dbContext.PossessesASet.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competition cmptemp = new Common.Models.Competition(temp.Competition.ID_COMP, temp.Competition.DATE_START, temp.Competition.DATE_END, temp.Competition.NAME_COMP, temp.Competition.MAX_COMPETITORS);
                Common.Models.Genre gntemp = new Common.Models.Genre(temp.Genre.ID_GENRE, temp.Genre.GENRE_NAME);
                ret.Add(new Common.Models.PossessesA(cmptemp.ID_COMP, gntemp.ID_GENRE, cmptemp, gntemp));

            });
            return ret;
        }

        ~PossessesARepository()
        {
            dbContext.Dispose();
        }

    }
}
