using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI.Resources.ImportClasses
{
    public class MusicPerformanceCSVImportTemplate
    {
        public MusicPerformanceCSVImportTemplate()
        {
        }

       
        public int Performance_ID { get; set; }
       
        public string Original_performer { get; set; }
    
        public string Song_name { get; set; }
  
        public string Song_author{ get; set; }

        public System.DateTime Performance_date { get; set; }

        public Nullable<long> Competitor_JMBG { get; set; }

        public Nullable<int> Competition_ID { get; set; }

        public Nullable<int> PH_ID { get; set; }

        public int Genre_ID { get; set; }

        public string Genre_Name { get; set; }
    }
}
