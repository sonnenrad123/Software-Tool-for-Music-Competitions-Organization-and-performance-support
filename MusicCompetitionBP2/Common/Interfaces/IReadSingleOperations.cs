using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IReadSingleOperations
    {
        
        [OperationContract]
        Competitor ReadCompetitor(long JMBG);
        [OperationContract]
        JuryMember ReadJuryMember(long JMBG);
        [OperationContract]
        MusicPerformance ReadMusicPerformance(int iD);
        [OperationContract]
        Genre ReadGenre(int iD);
        [OperationContract]
        Competition ReadCompetition(int iD);
        [OperationContract]
        PublishingHouse ReadPublishingHouse(int iD);
        [OperationContract]
        PerformanceHall ReadPerformanceHall(int iD);
    }
}
