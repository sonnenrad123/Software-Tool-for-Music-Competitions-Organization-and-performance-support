//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicCompetitionBP2
{
    using System;
    using System.Collections.Generic;
    
    public partial class MusicPerformance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MusicPerformance()
        {
            this.Evaluate = new HashSet<Evaluate>();
        }
    
        public int ID_PERF { get; set; }
        public string ORIG_PERFORMER { get; set; }
        public string SONG_NAME { get; set; }
        public string SONG_AUTHOR { get; set; }
        public System.DateTime DATE_PERF { get; set; }
        public int GenreID_GENRE { get; set; }
        public Nullable<long> CompetitingCompetitorJMBG_SIN { get; set; }
        public Nullable<int> CompetitingOrganizePublishingHouseID_PH { get; set; }
        public Nullable<int> CompetitingOrganizeCompetitionID_COMP { get; set; }
    
        public virtual Genre Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluate> Evaluate { get; set; }
        public virtual Competiting Competiting { get; set; }
    }
}
