using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class MusicPerformance
    {
        public MusicPerformance(int iD_PERF, string oRIG_PERFORMER, string sONG_NAME, string sONG_AUTHOR, DateTime dATE_PERF, long? competitingCompetitorJMBG_SIN, int? competitingOrganizeCompetitionID_COMP,int? competitingOrganizePublishingHouseID_PH, int genreID_GENRE, Competiting competiting, Genre genre)
        {
            ID_PERF = iD_PERF;
            ORIG_PERFORMER = oRIG_PERFORMER;
            SONG_NAME = sONG_NAME;
            SONG_AUTHOR = sONG_AUTHOR;
            DATE_PERF = dATE_PERF;
            CompetitingCompetitorJMBG_SIN = competitingCompetitorJMBG_SIN;
            CompetitingOrganizeCompetitionID_COMP = competitingOrganizeCompetitionID_COMP;
            CompetitingOrganizePublishingHouseID_PH = competitingOrganizePublishingHouseID_PH;
            GenreID_GENRE = genreID_GENRE;
            Competiting = competiting;
            Genre = genre;
        }
        [DataMember]
        public int ID_PERF { get; set; }
        [DataMember]
        public string ORIG_PERFORMER { get; set; }
        [DataMember]
        public string SONG_NAME { get; set; }
        [DataMember]
        public string SONG_AUTHOR { get; set; }
        [DataMember]
        public System.DateTime DATE_PERF { get; set; }
        [DataMember]
        public Nullable<long> CompetitingCompetitorJMBG_SIN { get; set; }
        [DataMember]
        public Nullable<int> CompetitingOrganizePublishingHouseID_PH { get; set; }
        [DataMember]
        public Nullable<int> CompetitingOrganizeCompetitionID_COMP { get; set; }
        [DataMember]
        public int GenreID_GENRE { get; set; }
        [DataMember]
        public virtual Competiting Competiting { get; set; }
        [DataMember]
        public virtual Genre Genre { get; set; }
    }
}
