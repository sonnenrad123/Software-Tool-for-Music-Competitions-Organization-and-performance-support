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
    public interface ICreateOperations
    {
       
        [OperationContract]
        bool AddCompetitor(Competitor c);
        [OperationContract]
        bool AddJuryMember(JuryMember jM);
        [OperationContract]
        bool AddEventOrganizer(EventOrganizer eo);
        [OperationContract]
        bool AddMusicPerformance(MusicPerformance mP);
        [OperationContract]
        bool AddGenre(Genre g);
        [OperationContract]
        bool AddCompetition(Competition c,out int idComp);
        [OperationContract]
        bool AddPublishingHouse(PublishingHouse ph);
        [OperationContract]
        bool AddPerformanceHall(PerformanceHall pH);


    }
}
