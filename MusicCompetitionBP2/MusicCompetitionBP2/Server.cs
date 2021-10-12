using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common;
using Common.Interfaces;

namespace MusicCompetitionBP2
{
    public class Server : IAllOperations
    {

        private MainRepository repo = new MainRepository(new MusicCompetitionDbContext());

        public bool AddCompetition(Common.Models.Competition c,out int idComp)
        {
            return repo.CompetitionRepository.Create(c,out idComp);
        }

        public bool AddCompetitor(Common.Models.Competitor c)
        {
            return repo.CompetitorRepository.Create(c);
        }

        public bool AddCompetitorToCompetition(long competitorJMBG, int competitionID,int pubhouseID)
        {
            return repo.CompetitingRepository.Create(competitorJMBG, competitionID, pubhouseID);
        }

        public bool AddEvaluationForMusicPerformance(long juryMemberJMBG, int genreID, int performanceID, short mark, string comment)
        {
            return repo.EvaluateRepository.Create(juryMemberJMBG, performanceID, mark, comment);
        }

        public bool AddEventOrganizer(Common.Models.EventOrganizer eo)
        {
            return repo.EventOrganizerRepository.Create(eo);
        }

        public bool AddGenre(Common.Models.Genre g)
        {
            return repo.GenreRepository.Create(g);
        }

        public bool AddGenreExpertise(int genreID, long juryMemberJMBG)
        {
            return repo.IsExpertRepository.Create(genreID, juryMemberJMBG);
        }

        public bool AddGenreToCompetition(int genreID, int compID)
        {
            return repo.PossessesARepository.Create(compID, genreID);
        }

        public bool AddHallReservation(int competitionID, int phID, int hallID, DateTime dATE_RES, TimeSpan sTART_TIME, TimeSpan eND_TIME)
        {
            return repo.ReserveRepository.Create(phID, competitionID, hallID, dATE_RES, sTART_TIME, eND_TIME);
        }

        public bool AddJuryMember(Common.Models.JuryMember jM)
        {
            return repo.JuryMemberRepository.Create(jM);
        }

        public bool AddMusicPerformance(Common.Models.MusicPerformance mP)
        {
            return repo.MusicPerformanceRepository.Create(mP.CompetitingOrganizeCompetitionID_COMP, mP.CompetitingCompetitorJMBG_SIN, mP.CompetitingOrganizePublishingHouseID_PH, mP.GenreID_GENRE, mP.SONG_AUTHOR, mP.ORIG_PERFORMER, mP.SONG_NAME, mP.DATE_PERF);
            
        }

        public bool AddPerformanceHall(Common.Models.PerformanceHall pH)
        {
            return repo.PerformanceHallRepository.Create(pH);
        }

        public bool AddPublishingHouse(Common.Models.PublishingHouse ph)
        {
            return repo.PublishingHouseRepository.Create(ph);
        }

        public bool AddPublishingHouseOrganization(int competitionID, int phID)
        {
            return repo.OrganizeRepository.Create(competitionID, phID);
        }

        public bool DeleteCompetiting(long jmbg, int compid, int pubhouseid)
        {
            return repo.CompetitingRepository.Remove(jmbg, compid,pubhouseid);
        }

        public bool DeleteCompetition(int iD)
        {
            return repo.CompetitionRepository.Remove(iD);
        }

        public bool DeleteCompetitor(long JMBG)
        {
            return repo.CompetitorRepository.Remove(JMBG);
        }

        public bool DeleteEvaluation(long jmbg_jury, int id_perf)
        {
            return repo.EvaluateRepository.Remove(jmbg_jury,id_perf);
        }

        public bool DeleteEventOrganizer(long JMBG)
        {
            return repo.EventOrganizerRepository.Remove(JMBG);
        }

        public bool DeleteExpertise(long jmbg_jury, int gid)
        {
            return repo.IsExpertRepository.Remove(gid,jmbg_jury);
        }

        public bool DeleteGenre(int iD)
        {
            return repo.GenreRepository.Remove(iD);
        }

        public bool DeleteHallReservation(int publhouseid, int compid, int hallid)
        {
            return repo.ReserveRepository.Remove(publhouseid, compid, hallid);
        }

        public bool DeleteHiredFor(long jmbg_jury, int compid)
        {
            return repo.HiredForRepository.Remove(jmbg_jury, compid);
        }

        public bool DeleteJuryMember(long JMBG)
        {
            return repo.JuryMemberRepository.Remove(JMBG);
        }

        public bool DeleteMusicPerformance(int iD)
        {
            return repo.MusicPerformanceRepository.Remove(iD);
        }

        public bool DeleteOrganization(int publhouseid, int compid)
        {
            return repo.OrganizeRepository.Remove(compid, publhouseid);
        }

        public bool DeletePerformanceHall(int iD)
        {
            return repo.PerformanceHallRepository.Remove(iD);
        }

        public bool DeletePossessA(int gid, int compid)
        {
            return repo.PossessesARepository.Remove(compid, gid);
        }

        public bool DeletePublishingHouse(int iD)
        {
            return repo.PublishingHouseRepository.Remove(iD);
        }

        public void EditAdministrator(Common.Models.Administrator c)
        {
            repo.AdminRepository.Update(c);
        }

        public void EditCompetition(Common.Models.Competition c)
        {
            repo.CompetitionRepository.Update(c);
        }

        public void EditCompetitor(Common.Models.Competitor c)
        {
            repo.CompetitorRepository.Update(c);
        }

        public void EditEvaluation(short ocena, string komentar, long juryJMBG, int idPerf)
        {
            repo.EvaluateRepository.Update(ocena, komentar, juryJMBG, idPerf);
        }

        public void EditEventOrganizer(Common.Models.EventOrganizer eo)
        {
            repo.EventOrganizerRepository.Update(eo);
        }

        public void EditGenre(Common.Models.Genre g)
        {
            repo.GenreRepository.Update(g);
        }

        public void EditJuryMember(Common.Models.JuryMember jM)
        {
            repo.JuryMemberRepository.Update(jM);
        }

        public void EditMusicPerformance(Common.Models.MusicPerformance mP)
        {
            repo.MusicPerformanceRepository.Update(mP.ID_PERF, mP.SONG_AUTHOR, mP.ORIG_PERFORMER, mP.SONG_NAME, mP.DATE_PERF);
        }

        public void EditPassword(Common.Models.User u)
        {
            repo.AuthRepository.ChangePassword(u);
        }

        public void EditPerformanceHall(Common.Models.PerformanceHall pH)
        {
            repo.PerformanceHallRepository.Update(pH);
        }

        public void EditPublishingHouse(Common.Models.PublishingHouse ph)
        {
            repo.PublishingHouseRepository.Update(ph);
        }

        public void EditReservation(Common.Models.Reserve r)
        {
            repo.ReserveRepository.Update(r);
        }

        public bool HireSingerForCompetition(long juryMemberJMBG, int competitionID)
        {
            return repo.HiredForRepository.Create(juryMemberJMBG, competitionID);
        }

        public Common.Models.Administrator ReadAdministrator(long JMBG)
        {
            return repo.AdminRepository.Read(JMBG);
        }

        public IEnumerable<Common.Models.City> ReadCities()
        {
            return repo.CityRepository.ReadAll();
        }

        public IEnumerable<Common.Models.Competiting> ReadCompetitings()
        {
            return repo.CompetitingRepository.ReadAll();
        }

        public Common.Models.Competition ReadCompetition(int iD)
        {
            return repo.CompetitionRepository.Read(iD);
        }

        public IEnumerable<Common.Models.Competition> ReadCompetitions()
        {
            return repo.CompetitionRepository.ReadAll();
        }

        public Common.Models.Competitor ReadCompetitor(long JMBG)
        {
            return repo.CompetitorRepository.Read(JMBG);
        }

        public IEnumerable<Common.Models.Competitor> ReadCompetitors()
        {
            return repo.CompetitorRepository.ReadAll();
        }

        public IEnumerable<Common.Models.HiredFor> ReadEngagemenets()
        {
            return repo.HiredForRepository.ReadAll();
        }

        public IEnumerable<Common.Models.Evaluate> ReadEvaluations()
        {
            return repo.EvaluateRepository.ReadAll();
        }

        public Common.Models.EventOrganizer ReadEventOrganizer(long JMBG)
        {
            return repo.EventOrganizerRepository.Read(JMBG);
        }

        public IEnumerable<Common.Models.EventOrganizer> ReadEventOrganizers()
        {
            return repo.EventOrganizerRepository.ReadAll();
        }

        public IEnumerable<Common.Models.IsExpert> ReadExpertises()
        {
            return repo.IsExpertRepository.ReadAll();
        }

        public Common.Models.Genre ReadGenre(int iD)
        {
            return repo.GenreRepository.Read(iD);
        }

        public IEnumerable<Common.Models.Genre> ReadGenres()
        {
            return repo.GenreRepository.ReadAll();
        }

        public IEnumerable<Common.Models.Reserve> ReadHallReservations()
        {
            return repo.ReserveRepository.ReadAll();
        }

        public Common.Models.JuryMember ReadJuryMember(long JMBG)
        {
            return repo.JuryMemberRepository.Read(JMBG);
        }

        public IEnumerable<Common.Models.JuryMember> ReadJuryMembers()
        {
            return repo.JuryMemberRepository.ReadAll();
        }

        public Common.Models.User ReadLoggedInUser(string email, string password)
        {
            return repo.AuthRepository.ReadUser(email, password);
        }

        public Common.Models.MusicPerformance ReadMusicPerformance(int iD)
        {
            return repo.MusicPerformanceRepository.Read(iD);
        }

        public IEnumerable<Common.Models.MusicPerformance> ReadMusicPerformances()
        {
            return repo.MusicPerformanceRepository.ReadAll();
        }

        public IEnumerable<Common.Models.Organize> ReadOrganizations()
        {
            return repo.OrganizeRepository.ReadAll();
        }

        public Common.Models.PerformanceHall ReadPerformanceHall(int iD)
        {
            return repo.PerformanceHallRepository.Read(iD);
        }

        public IEnumerable<Common.Models.PerformanceHall> ReadPerformanceHalls()
        {
            return repo.PerformanceHallRepository.ReadAll();
        }

        public IEnumerable<Common.Models.PossessesA> ReadPossessATable()
        {
            return repo.PossessesARepository.ReadAll();
        }

        public Common.Models.PublishingHouse ReadPublishingHouse(int iD)
        {
            return repo.PublishingHouseRepository.Read(iD);
        }

        public IEnumerable<Common.Models.PublishingHouse> ReadPublishingHouses()
        {
            return repo.PublishingHouseRepository.ReadAll();
        }

      

    }
}
