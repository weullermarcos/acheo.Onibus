﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace acheoOnibusAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbAcheoOnibusEntities : DbContext
    {
        public dbAcheoOnibusEntities()
            : base("name=dbAcheoOnibusEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblItinerario> tblItinerario { get; set; }
        public virtual DbSet<tblOnibus> tblOnibus { get; set; }
        public virtual DbSet<tblOnibusItinerario> tblOnibusItinerario { get; set; }
        public virtual DbSet<tblViagem> tblViagem { get; set; }
        public virtual DbSet<getItinerarios> getItinerarios { get; set; }
        public virtual DbSet<getOnibus> getOnibus { get; set; }
        public virtual DbSet<getViagens> getViagens { get; set; }
    }
}
