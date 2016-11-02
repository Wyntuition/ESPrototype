using Microsoft.EntityFrameworkCore;
using NCARB.EesaService.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace NCARB.EesaService.Infrastructure
{
    public class ApplicantContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        //public DbSet<Error> Errors { get; set; }

        public ApplicantContext(DbContextOptions options) : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Filename=./EesaService.db");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: to work with Guids in PG, but throwing error; added PG Design dependency but didn't resolve (per https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/58) 
            //modelBuilder.HasPostgresExtension("uuid-ossp");

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            modelBuilder.Entity<Applicant>().Property(p => p.LastName).IsRequired();
        }
    }
  }
