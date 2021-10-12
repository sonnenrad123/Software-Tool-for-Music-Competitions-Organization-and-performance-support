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
    public interface IReadMultipleOperations
    {
       
        [OperationContract]
        IEnumerable<Competitor> ReadCompetitors();
        [OperationContract]
        IEnumerable<JuryMember> ReadJuryMembers();
        [OperationContract]
        IEnumerable<EventOrganizer> ReadEventOrganizers();
        [OperationContract]
        IEnumerable<MusicPerformance> ReadMusicPerformances();
        [OperationContract]
        IEnumerable<Genre> ReadGenres();
        [OperationContract]
        IEnumerable<Competition> ReadCompetitions();
        [OperationContract]
        IEnumerable<PublishingHouse> ReadPublishingHouses();
        [OperationContract]
        IEnumerable<PerformanceHall> ReadPerformanceHalls();
        [OperationContract]
        IEnumerable<City> ReadCities();
    }
}
