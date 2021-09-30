using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class Competiting
    {
        public Competiting(long competitorJMBG_SIN, int organizeCompetitionID_COMP, int organizePublishingHouseID_PH, Competitor competitor, Organize organize)
        {
            CompetitorJMBG_SIN = competitorJMBG_SIN;
            OrganizeCompetitionID_COMP = organizeCompetitionID_COMP;
            OrganizePublishingHouseID_PH = organizePublishingHouseID_PH;
            Competitor = competitor;
            Organize = organize;
        }
        [DataMember]
        public long CompetitorJMBG_SIN { get; set; }
        [DataMember]
        public int OrganizeCompetitionID_COMP { get; set; }
        [DataMember]
        public int OrganizePublishingHouseID_PH { get; set; }
        [DataMember]

        public Competitor Competitor { get; set; }
        [DataMember]
        public Organize Organize { get; set; }
    }
}
