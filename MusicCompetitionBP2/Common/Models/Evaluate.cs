using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class Evaluate
    {
        public Evaluate(short mARK, string cOMMENT, long isExpertJuryMemberJMBG_SIN, int isExpertGenreID_GENRE, int musicPerformanceID_PERF, IsExpert isExpert, MusicPerformance musicPerformance)
        {
            MARK = mARK;
            COMMENT = cOMMENT;
            IsExpertJuryMemberJMBG_SIN = isExpertJuryMemberJMBG_SIN;
            IsExpertGenreID_GENRE = isExpertGenreID_GENRE;
            MusicPerformanceID_PERF = musicPerformanceID_PERF;
            IsExpert = isExpert;
            MusicPerformance = musicPerformance;
        }
        [DataMember]
        public short MARK { get; set; }
        [DataMember]
        public string COMMENT { get; set; }
        [DataMember]
        public long IsExpertJuryMemberJMBG_SIN { get; set; }
        [DataMember]
        public int IsExpertGenreID_GENRE { get; set; }
        [DataMember]
        public int MusicPerformanceID_PERF { get; set; }
        [DataMember]
        public IsExpert IsExpert { get; set; }
        [DataMember]
        public MusicPerformance MusicPerformance { get; set; }
    }
}
