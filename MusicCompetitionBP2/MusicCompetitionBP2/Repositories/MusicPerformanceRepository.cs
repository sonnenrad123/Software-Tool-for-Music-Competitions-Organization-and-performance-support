using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class MusicPerformanceRepository
    {

        private readonly MusicCompetitionDbContext dbContext;
        public MusicPerformanceRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(int? competitionID,long? competitorID,int? pubhouseID,int genreID,string songAuthor,string orig_perf,string song_name,DateTime dateperf)
        {
            
            try
            {
                MusicPerformance temp = new MusicPerformance()
                {
                    CompetitingOrganizeCompetitionID_COMP = competitionID,
                    CompetitingOrganizePublishingHouseID_PH = pubhouseID,
                    CompetitingCompetitorJMBG_SIN = competitorID,
                    GenreID_GENRE = genreID,

                    SONG_AUTHOR = songAuthor,
                    ORIG_PERFORMER = orig_perf,
                    SONG_NAME = song_name,
                    DATE_PERF = dateperf,

                };
                dbContext.MusicPerformances.Add(temp);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool Remove(int perfID)
        {
            try
            {
                var res = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == perfID);
                dbContext.MusicPerformances.Remove(res);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Common.Models.MusicPerformance Read(int perfID)
        {
            var temp = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == perfID);
            if(temp == null)
            {
                return null;
            }

            Common.Models.Competitor competitortemp = new Common.Models.Competitor(temp.Competiting.Competitor.JMBG_SIN, temp.Competiting.Competitor.FIRSTNAME_SIN, temp.Competiting.Competitor.LASTNAME_SIN, temp.Competiting.Competitor.BIRTHDATE_SIN,
                temp.Competiting.Competitor.EMAIL_SIN, temp.Competiting.Competitor.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.Competiting.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.Competiting.Competitor.ADDRESS_SIN.CITY, temp.Competiting.Competitor.ADDRESS_SIN.STREET));
            
            Common.Models.Competition competitiontemp = new Common.Models.Competition(temp.Competiting.Organize.Competition.ID_COMP, temp.Competiting.Organize.Competition.DATE_START, temp.Competiting.Organize.Competition.DATE_END, temp.Competiting.Organize.Competition.NAME_COMP, temp.Competiting.Organize.Competition.MAX_COMPETITORS);

            Common.Models.PublishingHouse pubhousetemp = new Common.Models.PublishingHouse(temp.Competiting.Organize.PublishingHouseID_PH, temp.Competiting.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.Competiting.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.Competiting.Organize.PublishingHouse.ADR_PH.CITY, temp.Competiting.Organize.PublishingHouse.ADR_PH.STREET));

            Common.Models.Organize organizetemp = new Common.Models.Organize(temp.Competiting.OrganizePublishingHouseID_PH,temp.Competiting.OrganizeCompetitionID_COMP,pubhousetemp,competitiontemp);
            

            Common.Models.Competiting competitingTemp = new Common.Models.Competiting(competitortemp.JMBG_SIN, competitiontemp.ID_COMP, pubhousetemp.ID_PH, competitortemp, organizetemp);

            Common.Models.Genre genreTemp = new Common.Models.Genre(temp.GenreID_GENRE, temp.Genre.GENRE_NAME);

            
            return new Common.Models.MusicPerformance(temp.ID_PERF, temp.ORIG_PERFORMER, temp.SONG_NAME, temp.SONG_AUTHOR, temp.DATE_PERF, temp.Competiting.CompetitorJMBG_SIN, temp.Competiting.Organize.CompetitionID_COMP, temp.Competiting.Organize.PublishingHouseID_PH, temp.GenreID_GENRE, competitingTemp, genreTemp);



        }

        public IEnumerable<Common.Models.MusicPerformance> ReadAll()
        {
            var ret = new List<Common.Models.MusicPerformance>();
            dbContext.MusicPerformances.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.Competitor competitortemp = new Common.Models.Competitor(temp.Competiting.Competitor.JMBG_SIN, temp.Competiting.Competitor.FIRSTNAME_SIN, temp.Competiting.Competitor.LASTNAME_SIN, temp.Competiting.Competitor.BIRTHDATE_SIN,
               temp.Competiting.Competitor.EMAIL_SIN, temp.Competiting.Competitor.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.Competiting.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.Competiting.Competitor.ADDRESS_SIN.CITY, temp.Competiting.Competitor.ADDRESS_SIN.STREET));

                Common.Models.Competition competitiontemp = new Common.Models.Competition(temp.Competiting.Organize.Competition.ID_COMP, temp.Competiting.Organize.Competition.DATE_START, temp.Competiting.Organize.Competition.DATE_END, temp.Competiting.Organize.Competition.NAME_COMP, temp.Competiting.Organize.Competition.MAX_COMPETITORS);

                Common.Models.PublishingHouse pubhousetemp = new Common.Models.PublishingHouse(temp.Competiting.Organize.PublishingHouseID_PH, temp.Competiting.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.Competiting.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.Competiting.Organize.PublishingHouse.ADR_PH.CITY, temp.Competiting.Organize.PublishingHouse.ADR_PH.STREET));

                Common.Models.Organize organizetemp = new Common.Models.Organize(temp.Competiting.OrganizePublishingHouseID_PH, temp.Competiting.OrganizeCompetitionID_COMP, pubhousetemp, competitiontemp);


                Common.Models.Competiting competitingTemp = new Common.Models.Competiting(competitortemp.JMBG_SIN, competitiontemp.ID_COMP, pubhousetemp.ID_PH, competitortemp, organizetemp);

                Common.Models.Genre genreTemp = new Common.Models.Genre(temp.GenreID_GENRE, temp.Genre.GENRE_NAME);


                ret.Add( new Common.Models.MusicPerformance(temp.ID_PERF, temp.ORIG_PERFORMER, temp.SONG_NAME, temp.SONG_AUTHOR, temp.DATE_PERF, temp.Competiting.CompetitorJMBG_SIN, temp.Competiting.Organize.CompetitionID_COMP, temp.Competiting.Organize.PublishingHouseID_PH, temp.GenreID_GENRE, competitingTemp, genreTemp));
            });
            return ret;
        }

        public void Update(int id, string songAuthor, string orig_perf, string song_name, DateTime dateperf)
        {
            try
            {
                MusicPerformance musifperf = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == id);
                if(musifperf == null)
                {
                    return;
                }
                musifperf.SONG_AUTHOR = songAuthor;
                musifperf.ORIG_PERFORMER = orig_perf;
                musifperf.SONG_NAME = song_name;
                musifperf.DATE_PERF = dateperf;
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return;
            }
        }

        ~MusicPerformanceRepository()
        {
            dbContext.Dispose();
        }
    }
}
