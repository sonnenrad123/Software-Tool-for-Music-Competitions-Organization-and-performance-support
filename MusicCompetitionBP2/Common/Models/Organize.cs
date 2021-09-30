using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class Organize
    {
        public Organize(int publishingHouseID_PH, int competitionID_COMP, PublishingHouse publishingHouse, Competition competition)
        {
            PublishingHouseID_PH = publishingHouseID_PH;
            CompetitionID_COMP = competitionID_COMP;
            PublishingHouse = publishingHouse;
            Competition = competition;
        }
        [DataMember]
        public int PublishingHouseID_PH { get; set; }
        [DataMember]
        public int CompetitionID_COMP { get; set; }
        [DataMember]

        public PublishingHouse PublishingHouse { get; set; }
        [DataMember]
        public Competition Competition { get; set; }
    }
}
