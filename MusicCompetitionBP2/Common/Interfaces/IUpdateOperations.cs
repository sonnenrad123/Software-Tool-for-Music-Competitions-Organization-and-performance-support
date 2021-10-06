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
    public interface IUpdateOperations
    {
        [OperationContract]
        void EditCompetitor(Competitor c);
        [OperationContract]
        void EditAdministrator(Administrator c);
        [OperationContract]
        void EditJuryMember(JuryMember jM);
        [OperationContract]
        void EditEventOrganizer(EventOrganizer eo);
        [OperationContract]
        void EditMusicPerformance(MusicPerformance mP);
        [OperationContract]
        void EditGenre(Genre g);
        [OperationContract]
        void EditCompetition(Competition c);
        [OperationContract]
        void EditPublishingHouse(PublishingHouse ph);
        [OperationContract]
        void EditPerformanceHall(PerformanceHall pH);
        [OperationContract]
        void EditPassword(User u);
    }
}
