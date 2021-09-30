using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class Genre
    {
        public Genre(int iD_GENRE, string gENRE_NAME)
        {
            ID_GENRE = iD_GENRE;
            GENRE_NAME = gENRE_NAME;
        }
        [DataMember]
        public int ID_GENRE { get; set; }
        [DataMember]
        public string GENRE_NAME { get; set; }
    }
}
