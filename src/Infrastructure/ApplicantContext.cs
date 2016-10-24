using Microsoft.EntityFrameworkCore;
using NCARB.EesaService.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            modelBuilder.Entity<Applicant>().Property(p => p.LastName).IsRequired();
        }
    }
  }
