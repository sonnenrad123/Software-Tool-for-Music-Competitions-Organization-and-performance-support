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
    
    public partial class PublishingHouse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PublishingHouse()
        {
            this.Organize = new HashSet<Organize>();
            this.EventOrganizer = new HashSet<EventOrganizer>();
            this.ADR_PH = new ADDRESS();
        }
    
        public int ID_PH { get; set; }
        public string NAME_PH { get; set; }
    
        public ADDRESS ADR_PH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organize> Organize { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventOrganizer> EventOrganizer { get; set; }
    }
}
