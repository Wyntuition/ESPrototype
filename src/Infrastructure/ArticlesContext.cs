using Microsoft.EntityFrameworkCore;
using NCARB.EesaService.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NCARB.EesaService.Infrastructure
{
    public class ArticlesContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        //public DbSet<Error> Errors { get; set; }

        public ArticlesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./articles.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            modelBuilder.Entity<Article>().Property(p => p.Title).IsRequired();
        }
    }
  }
