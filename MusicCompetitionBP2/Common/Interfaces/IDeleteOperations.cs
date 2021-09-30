using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IDeleteOperations
    {
        [OperationContract]
        bool DeleteCompetitor(long JMBG);
        [OperationContract]
        bool DeleteJuryMember(long JMBG);
        [OperationContract]
        bool DeleteMusicPerformance(int iD);
        [OperationContract]
        bool DeleteGenre(int iD);
        [OperationContract]
        bool DeleteCompetition(int iD);
        [OperationContract]
        bool DeletePublishingHouse(int iD);
        [OperationContract]
        bool DeletePerformanceHall(int iD);
    }
}
