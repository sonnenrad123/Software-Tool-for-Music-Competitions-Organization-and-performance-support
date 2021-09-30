using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class ADDRESS
    {
        public ADDRESS(string hOME_NUMBER, string cITY, string sTREET)
        {
            HOME_NUMBER = hOME_NUMBER;
            CITY = cITY;
            STREET = sTREET;
        }
        [DataMember]
        public string HOME_NUMBER { get; set; }
        [DataMember]
        public string CITY { get; set; }
        [DataMember]
        public string STREET { get; set; }

        public override string ToString()
        {
            return STREET + " " + HOME_NUMBER + ", " + CITY;
        }
    }
}
