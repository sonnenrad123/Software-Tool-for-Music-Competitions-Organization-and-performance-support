using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class IsExpert
    {
        public IsExpert(long juryMemberJMBG_SIN, int genreID_GENRE, JuryMember juryMember, Genre genre)
        {
            JuryMemberJMBG_SIN = juryMemberJMBG_SIN;
            GenreID_GENRE = genreID_GENRE;
            JuryMember = juryMember;
            Genre = genre;
        }
        [DataMember]
        public long JuryMemberJMBG_SIN { get; set; }
        [DataMember]
        public int GenreID_GENRE { get; set; }
        [DataMember]
        public  JuryMember JuryMember { get; set; }
        [DataMember]
        public Genre Genre { get; set; }
    }
}
