using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class PerformanceHall
    {
        public PerformanceHall(int iD_HALL, string nAME_HALL, int cAPACITY)
        {
            ID_HALL = iD_HALL;
            NAME_HALL = nAME_HALL;
            CAPACITY = cAPACITY;
        }
        [DataMember]
        public int ID_HALL { get; set; }
        [DataMember]
        public string NAME_HALL { get; set; }
        [DataMember]
        public int CAPACITY { get; set; }
    }
}
