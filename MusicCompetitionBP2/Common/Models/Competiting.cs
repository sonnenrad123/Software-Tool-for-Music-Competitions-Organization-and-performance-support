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
        public Competiting(long competitorJMBG_SIN, int competitionID_COMP, Competitor competitor, Competition competition)
        {
            CompetitorJMBG_SIN = competitorJMBG_SIN;
            CompetitionID_COMP = competitionID_COMP;
            Competitor = competitor;
            Competition = competition;
        }
        [DataMember]
        public long CompetitorJMBG_SIN { get; set; }
        [DataMember]
        public int CompetitionID_COMP { get; set; }
        [DataMember]

        public Competitor Competitor { get; set; }
        [DataMember]
        public Competition Competition { get; set; }
    }
}
