using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class Competition
    {
        public Competition(int iD_COMP, DateTime dATE_START, DateTime dATE_END, string nAME_COMP, int mAX_COMPETITORS)
        {
            ID_COMP = iD_COMP;
            DATE_START = dATE_START;
            DATE_END = dATE_END;
            NAME_COMP = nAME_COMP;
            MAX_COMPETITORS = mAX_COMPETITORS;
        }
        [DataMember]
        public int ID_COMP { get; set; }
        [DataMember]
        public System.DateTime DATE_START { get; set; }
        [DataMember]
        public System.DateTime DATE_END { get; set; }
        [DataMember]
        public string NAME_COMP { get; set; }
        [DataMember]
        public int MAX_COMPETITORS { get; set; }
    }
}
