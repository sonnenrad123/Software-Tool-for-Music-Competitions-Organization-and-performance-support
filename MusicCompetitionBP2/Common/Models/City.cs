using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class City
    {
        public City(int postcode,string cityName)
        {
            Postcode = postcode;
            CityName = cityName;
        }
        [DataMember]
        public int Postcode { get; set; }
        [DataMember]
        public string CityName { get; set; }
    }
}
