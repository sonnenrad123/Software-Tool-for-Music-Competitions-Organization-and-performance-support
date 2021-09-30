using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class EvaluateRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public EvaluateRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(long JMBG_jury,int id_perf,short ocena,string komentar)
        {
            
            try
            {
                var musifperf = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == id_perf);
                int music_perf_id_genre = musifperf.GenreID_GENRE;



                dbContext.Evaluations.Add(new Evaluate() { COMMENT = komentar, IsExpertGenreID_GENRE = music_perf_id_genre, IsExpertJuryMemberJMBG_SIN = JMBG_jury, MARK = ocena, MusicPerformanceID_PERF = id_perf });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(long JMBG_jury, int id_perf)
        {
            try
            {
                var musifperf = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == id_perf);
                int music_perf_id_genre = musifperf.GenreID_GENRE;

                var res = dbContext.Evaluations.FirstOrDefault((x) => x.IsExpertJuryMemberJMBG_SIN == JMBG_jury && x.IsExpertGenreID_GENRE == music_perf_id_genre && x.MusicPerformanceID_PERF == id_perf);
                dbContext.Evaluations.Remove(res);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public Common.Models.Evaluate Read(long JMBG_jury, int id_perf)
        {
            int music_perf_id_genre = 0;
            try
            {
                var musifperf = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == id_perf);
                music_perf_id_genre = musifperf.GenreID_GENRE;
            }
            catch(Exception)
            {
                return null;
            }


            var temp = dbContext.Evaluations.FirstOrDefault((x) => x.IsExpertJuryMemberJMBG_SIN == JMBG_jury && x.IsExpertGenreID_GENRE == music_perf_id_genre && x.MusicPerformanceID_PERF == id_perf);
            if (temp == null)
            {
                return null;
            }

            Common.Models.JuryMember jurytemp = new Common.Models.JuryMember(temp.IsExpertJuryMemberJMBG_SIN, temp.IsExpert.JuryMember.FIRSTNAME_SIN, temp.IsExpert.JuryMember.LASTNAME_SIN, temp.IsExpert.JuryMember.BIRTHDATE_SIN, temp.IsExpert.JuryMember.EMAIL_SIN
                , temp.IsExpert.JuryMember.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.IsExpert.JuryMember.ADDRESS_SIN.HOME_NUMBER, temp.IsExpert.JuryMember.ADDRESS_SIN.CITY, temp.IsExpert.JuryMember.ADDRESS_SIN.STREET));

            Common.Models.Genre genretemp = new Common.Models.Genre(temp.IsExpertGenreID_GENRE, temp.IsExpert.Genre.GENRE_NAME);

            //is exp i musicperf

            Common.Models.IsExpert isexptemp = new Common.Models.IsExpert(temp.IsExpertJuryMemberJMBG_SIN, music_perf_id_genre, jurytemp, genretemp);


            //competiting competitor competition

            Common.Models.Competitor competitorTemp = new Common.Models.Competitor(temp.MusicPerformance.Competiting.Competitor.JMBG_SIN, temp.MusicPerformance.Competiting.Competitor.FIRSTNAME_SIN, temp.MusicPerformance.Competiting.Competitor.LASTNAME_SIN,
                temp.MusicPerformance.Competiting.Competitor.BIRTHDATE_SIN, temp.MusicPerformance.Competiting.Competitor.EMAIL_SIN, temp.MusicPerformance.Competiting.Competitor.PHONE_NO_SIN, 
                new Common.Models.ADDRESS(temp.MusicPerformance.Competiting.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.MusicPerformance.Competiting.Competitor.ADDRESS_SIN.CITY, temp.MusicPerformance.Competiting.Competitor.ADDRESS_SIN.STREET));

            Common.Models.Competition competitionTemp = new Common.Models.Competition(temp.MusicPerformance.Competiting.Organize.Competition.ID_COMP, temp.MusicPerformance.Competiting.Organize.Competition.DATE_START, temp.MusicPerformance.Competiting.Organize.Competition.DATE_END, temp.MusicPerformance.Competiting.Organize.Competition.NAME_COMP, temp.MusicPerformance.Competiting.Organize.Competition.MAX_COMPETITORS);

            Common.Models.PublishingHouse pubhouseTemp = new Common.Models.PublishingHouse(temp.MusicPerformance.Competiting.Organize.PublishingHouse.ID_PH, temp.MusicPerformance.Competiting.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.MusicPerformance.Competiting.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.MusicPerformance.Competiting.Organize.PublishingHouse.ADR_PH.CITY, temp.MusicPerformance.Competiting.Organize.PublishingHouse.ADR_PH.STREET));

            Common.Models.Organize organizeTemp = new Common.Models.Organize(temp.MusicPerformance.Competiting.Organize.PublishingHouseID_PH, temp.MusicPerformance.Competiting.Organize.CompetitionID_COMP, pubhouseTemp, competitionTemp);
            
            Common.Models.Competiting competitingTemp = new Common.Models.Competiting(temp.MusicPerformance.Competiting.CompetitorJMBG_SIN, temp.MusicPerformance.Competiting.OrganizeCompetitionID_COMP, temp.MusicPerformance.Competiting.OrganizePublishingHouseID_PH, competitorTemp,organizeTemp);

            
            Common.Models.MusicPerformance mptemp = new Common.Models.MusicPerformance(temp.MusicPerformanceID_PERF, temp.MusicPerformance.ORIG_PERFORMER, temp.MusicPerformance.SONG_NAME, temp.MusicPerformance.SONG_AUTHOR, temp.MusicPerformance.DATE_PERF, temp.MusicPerformance.CompetitingCompetitorJMBG_SIN, temp.MusicPerformance.Competiting.Organize.CompetitionID_COMP, temp.MusicPerformance.Competiting.Organize.PublishingHouseID_PH, temp.MusicPerformance.Genre.ID_GENRE, competitingTemp, genretemp);


            return new Common.Models.Evaluate(temp.MARK, temp.COMMENT, jurytemp.JMBG_SIN, genretemp.ID_GENRE, mptemp.ID_PERF, isexptemp, mptemp);
        }

        public IEnumerable<Common.Models.Evaluate> ReadAll()
        {
           

            var ret = new List<Common.Models.Evaluate>();
            dbContext.Evaluations.AsNoTracking().ToList().ForEach((temp) =>
            {
                Common.Models.JuryMember jurytemp = new Common.Models.JuryMember(temp.IsExpertJuryMemberJMBG_SIN, temp.IsExpert.JuryMember.FIRSTNAME_SIN, temp.IsExpert.JuryMember.LASTNAME_SIN, temp.IsExpert.JuryMember.BIRTHDATE_SIN, temp.IsExpert.JuryMember.EMAIL_SIN
                 , temp.IsExpert.JuryMember.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.IsExpert.JuryMember.ADDRESS_SIN.HOME_NUMBER, temp.IsExpert.JuryMember.ADDRESS_SIN.CITY, temp.IsExpert.JuryMember.ADDRESS_SIN.STREET));

                Common.Models.Genre genretemp = new Common.Models.Genre(temp.IsExpertGenreID_GENRE, temp.IsExpert.Genre.GENRE_NAME);

                //is exp i musicperf

                Common.Models.IsExpert isexptemp = new Common.Models.IsExpert(temp.IsExpertJuryMemberJMBG_SIN, temp.IsExpertGenreID_GENRE, jurytemp, genretemp);


                //competiting competitor competition

                Common.Models.Competitor competitorTemp = new Common.Models.Competitor(temp.MusicPerformance.Competiting.Competitor.JMBG_SIN, temp.MusicPerformance.Competiting.Competitor.FIRSTNAME_SIN, temp.MusicPerformance.Competiting.Competitor.LASTNAME_SIN,
                temp.MusicPerformance.Competiting.Competitor.BIRTHDATE_SIN, temp.MusicPerformance.Competiting.Competitor.EMAIL_SIN, temp.MusicPerformance.Competiting.Competitor.PHONE_NO_SIN,
                new Common.Models.ADDRESS(temp.MusicPerformance.Competiting.Competitor.ADDRESS_SIN.HOME_NUMBER, temp.MusicPerformance.Competiting.Competitor.ADDRESS_SIN.CITY, temp.MusicPerformance.Competiting.Competitor.ADDRESS_SIN.STREET));

                Common.Models.Competition competitionTemp = new Common.Models.Competition(temp.MusicPerformance.Competiting.Organize.Competition.ID_COMP, temp.MusicPerformance.Competiting.Organize.Competition.DATE_START, temp.MusicPerformance.Competiting.Organize.Competition.DATE_END, temp.MusicPerformance.Competiting.Organize.Competition.NAME_COMP, temp.MusicPerformance.Competiting.Organize.Competition.MAX_COMPETITORS);

                Common.Models.PublishingHouse pubhouseTemp = new Common.Models.PublishingHouse(temp.MusicPerformance.Competiting.Organize.PublishingHouse.ID_PH, temp.MusicPerformance.Competiting.Organize.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.MusicPerformance.Competiting.Organize.PublishingHouse.ADR_PH.HOME_NUMBER, temp.MusicPerformance.Competiting.Organize.PublishingHouse.ADR_PH.CITY, temp.MusicPerformance.Competiting.Organize.PublishingHouse.ADR_PH.STREET));

                Common.Models.Organize organizeTemp = new Common.Models.Organize(temp.MusicPerformance.Competiting.Organize.PublishingHouseID_PH, temp.MusicPerformance.Competiting.Organize.CompetitionID_COMP, pubhouseTemp, competitionTemp);

                Common.Models.Competiting competitingTemp = new Common.Models.Competiting(temp.MusicPerformance.Competiting.CompetitorJMBG_SIN, temp.MusicPerformance.Competiting.OrganizeCompetitionID_COMP, temp.MusicPerformance.Competiting.OrganizePublishingHouseID_PH, competitorTemp, organizeTemp);


                Common.Models.MusicPerformance mptemp = new Common.Models.MusicPerformance(temp.MusicPerformanceID_PERF, temp.MusicPerformance.ORIG_PERFORMER, temp.MusicPerformance.SONG_NAME, temp.MusicPerformance.SONG_AUTHOR, temp.MusicPerformance.DATE_PERF, temp.MusicPerformance.CompetitingCompetitorJMBG_SIN, temp.MusicPerformance.Competiting.Organize.CompetitionID_COMP, temp.MusicPerformance.Competiting.Organize.PublishingHouseID_PH, temp.MusicPerformance.Genre.ID_GENRE, competitingTemp, genretemp);
                ret.Add(new Common.Models.Evaluate(temp.MARK, temp.COMMENT, jurytemp.JMBG_SIN, genretemp.ID_GENRE, mptemp.ID_PERF, isexptemp, mptemp));
            });
            return ret;
        }

        public void Update(short ocena,string komentar,long juryJMBG,int idPerf)
        {

            int music_perf_id_genre = 0;
            try
            {
                var musifperf = dbContext.MusicPerformances.FirstOrDefault((x) => x.ID_PERF == idPerf);
                music_perf_id_genre = musifperf.GenreID_GENRE;

                Evaluate ev = dbContext.Evaluations.FirstOrDefault((x) => x.IsExpertGenreID_GENRE == music_perf_id_genre && x.IsExpertJuryMemberJMBG_SIN == juryJMBG && x.MusicPerformanceID_PERF == idPerf);
                if(ev == null)
                {
                    return;
                }


                ev.MARK = ocena;
                ev.COMMENT = komentar;
                dbContext.SaveChanges();
                


            }
            catch (Exception)
            {
                return;
            }

            

        }


        ~EvaluateRepository()
        {
            dbContext.Dispose();
        }
    }
}
