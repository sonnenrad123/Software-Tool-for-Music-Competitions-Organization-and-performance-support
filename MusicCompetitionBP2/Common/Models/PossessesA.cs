using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class PossessesA
    {
        public PossessesA(int competitionID_COMP, int genreID_GENRE, Competition competition, Genre genre)
        {
            CompetitionID_COMP = competitionID_COMP;
            GenreID_GENRE = genreID_GENRE;
            Competition = competition;
            Genre = genre;
        }
        [DataMember]
        public int CompetitionID_COMP { get; set; }
        [DataMember]
        public int GenreID_GENRE { get; set; }
        [DataMember]
        public  Competition Competition { get; set; }
        [DataMember]
        public Genre Genre { get; set; }
    }
}
