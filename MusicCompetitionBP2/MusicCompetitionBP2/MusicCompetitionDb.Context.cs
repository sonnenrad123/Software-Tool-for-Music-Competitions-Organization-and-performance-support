﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MusicCompetitionDbContext : DbContext
    {
        public MusicCompetitionDbContext()
            : base("name=MusicCompetitionDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Singer> Singers { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MusicPerformance> MusicPerformances { get; set; }
        public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }
        public virtual DbSet<PerformanceHall> PerformanceHalls { get; set; }
        public virtual DbSet<Organize> Organizations { get; set; }
        public virtual DbSet<HiredFor> HiredForSet { get; set; }
        public virtual DbSet<PossessesA> PossessesASet { get; set; }
        public virtual DbSet<IsExpert> IsExpertSet { get; set; }
        public virtual DbSet<Evaluate> Evaluations { get; set; }
        public virtual DbSet<Reserve> Reservations { get; set; }
        public virtual DbSet<Competiting> Competitings { get; set; }
    }
}
