using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class HiredFor
    {
        public HiredFor(long juryMemberJMBG_SIN, int competitionID_COMP, JuryMember juryMember, Competition competition)
        {
            JuryMemberJMBG_SIN = juryMemberJMBG_SIN;
            CompetitionID_COMP = competitionID_COMP;
            JuryMember = juryMember;
            Competition = competition;
        }
        [DataMember]
        public long JuryMemberJMBG_SIN { get; set; }
        [DataMember]
        public int CompetitionID_COMP { get; set; }
        [DataMember]
        public JuryMember JuryMember { get; set; }
        [DataMember]
        public Competition Competition { get; set; }
    }
}
