using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IAllOperations:ICreateOperations,IReadOperations,IUpdateOperations,IDeleteOperations,ILoginOperations
    {
        [OperationContract]
        bool AddCompetitorToCompetition(long competitorJMBG, int competitionID,int pubhouseID);
        [OperationContract]
        bool AddEvaluationForMusicPerformance(long juryMemberJMBG, int genreID, int performanceID, short mark, string comment);
        [OperationContract]
        bool AddGenreExpertise(int genreID, long juryMemberJMBG);
        [OperationContract]
        bool AddGenreToCompetition(int genreID, int compID);
        [OperationContract]
        bool HireSingerForCompetition(long juryMemberJMBG, int competitionID);
        [OperationContract]
        bool AddPublishingHouseOrganization(int competitionID, int phID);
        [OperationContract]
        bool AddHallReservation(int competitionID, int phID, int hallID, DateTime dATE_RES, TimeSpan sTART_TIME, TimeSpan eND_TIME);
        [OperationContract]
        IEnumerable<Competiting> ReadCompetitings();
        [OperationContract]
        IEnumerable<PossessesA> ReadPossessATable();
        [OperationContract]
        IEnumerable<Evaluate> ReadEvaluations();
        [OperationContract]
        IEnumerable<IsExpert> ReadExpertises();
        [OperationContract]
        IEnumerable<HiredFor> ReadEngagemenets();
        [OperationContract]
        IEnumerable<Organize> ReadOrganizations();
        [OperationContract]
        IEnumerable<Reserve> ReadHallReservations();
        [OperationContract]
        bool DeleteCompetiting(long jmbg,int compid,int pubhouseid);
        [OperationContract]
        bool DeletePossessA(int gid,int compid);
        [OperationContract]
        bool DeleteEvaluation(long jmbg_jury,int id_perf);
        [OperationContract]
        bool DeleteExpertise(long jmbg_jury,int gid);
        [OperationContract]
        bool DeleteHiredFor(long jmbg_jury, int compid);
        [OperationContract]
        bool DeleteOrganization(int publhouseid,int compid);
        [OperationContract]
        bool DeleteHallReservation(int publhouseid,int compid,int hallid);
        [OperationContract]
        void EditReservation(Common.Models.Reserve r);
        [OperationContract]
        void EditEvaluation(short ocena, string komentar, long juryJMBG, int idPerf);
    }
}
