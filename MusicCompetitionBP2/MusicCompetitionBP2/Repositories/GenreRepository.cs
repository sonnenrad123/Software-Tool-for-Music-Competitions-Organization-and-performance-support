using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class GenreRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public GenreRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Common.Models.Genre genre)
        {
            Genre tempgenre = new Genre()
            {
                GENRE_NAME = genre.GENRE_NAME,
                ID_GENRE = genre.ID_GENRE
            };
            dbContext.Genres.Add(tempgenre);
            dbContext.SaveChanges();
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public Common.Models.Genre Read(int genreId)
        {
            var temp = dbContext.Genres.AsNoTracking().FirstOrDefault((x)=>x.ID_GENRE == genreId);
            if(temp == null)
            {
                return null;
            }
            return new Common.Models.Genre(temp.ID_GENRE, temp.GENRE_NAME);
        }


        public IEnumerable<Common.Models.Genre> ReadAll()
        {
            var ret = new List<Common.Models.Genre>();
            dbContext.Genres.AsNoTracking().ToList().ForEach((temp) =>
            {
                ret.Add(new Common.Models.Genre(temp.ID_GENRE, temp.GENRE_NAME));
            });
            return ret;
        }

        public bool Remove(int genreId)
        {
            try
            {
                Genre g = dbContext.Genres.FirstOrDefault((x) => x.ID_GENRE == genreId);
                if(g == null)
                {
                    return false;
                }

                //kaskadno brisanje

                //obrisati sve iz tabele possesA
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from PossessesASet where GenreID_GENRE = {0}", genreId));

                //ocene za nastupe tog zanra
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from Evaluations where MusicPerformanceID_PERF in (select ID_PERF from MusicPerformances where GenreID_GENRE = {0})", genreId));

                //ekspertize za dati zanr
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from IsExpertSet where GenreID_GENRE = {0}", genreId));

                //nastupe sa tim zanrom
                dbContext.Database.ExecuteSqlCommand(string.Format("DELETE from MusicPerformances where GenreID_GENRE = {0}", genreId));


                dbContext.Genres.Remove(g);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void Update(Common.Models.Genre genre)
        {
            var temp = dbContext.Genres.FirstOrDefault((x) => x.ID_GENRE == genre.ID_GENRE);
            dbContext.Entry(temp).CurrentValues.SetValues(genre);
            dbContext.SaveChanges();
        }

        ~GenreRepository()
        {
            dbContext.Dispose();
        }
    }
}
