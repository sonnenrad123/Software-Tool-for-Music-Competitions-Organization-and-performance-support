using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public sealed class MusicPerformanceMap:ClassMap<MusicPerformance>
    {
        public MusicPerformanceMap()
        {
            Map(m => m.ID_PERF).Index(0).Name("Performance_ID");
            Map(m => m.ORIG_PERFORMER).Index(1).Name("Original_performer");
            Map(m => m.SONG_NAME).Index(2).Name("Song_name");
            Map(m => m.SONG_AUTHOR).Index(3).Name("Song_author");
            Map(m => m.DATE_PERF).Index(4).Name("Performance_date");
            Map(m => m.CompetitingCompetitorJMBG_SIN).Index(5).Name("Competitor_JMBG");
            Map(m => m.CompetitingOrganizeCompetitionID_COMP).Index(6).Name("Competition_ID");
            Map(m => m.CompetitingOrganizePublishingHouseID_PH).Index(7).Name("PH_ID");
            Map(m => m.GenreID_GENRE).Index(8).Name("Genre_ID");
            Map(m => m.Genre.GENRE_NAME).Index(9).Name("Genre_Name");
            
        }
    }
}
