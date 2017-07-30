using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Entities
{
    public class AhvazpmuDbContext:DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImage { get; set; }
        public AhvazpmuDbContext(DbContextOptions<AhvazpmuDbContext> options): base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<News>(e =>
            {
                e.HasKey(c => c.tbNewsID);

                e.Property(c => c.fldNewsTitle)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(c => c.fldNewsDescription)
                .IsRequired()
                .HasMaxLength(4000);

                e.Property(c => c.fldNewsDate)
                .IsRequired()
                .HasMaxLength(10);

                e.Property(c => c.fldRegisterDate)
                    .HasMaxLength(10);

                e.Property(c => c.fldSummaryNews)
                .HasMaxLength(1000);

                e.Property(c => c.fldRegisterDate)
                .HasMaxLength(5);

                e.Property(c => c.fldNewsExternalLink)
                    .HasMaxLength(4000);

                e.HasOne(t => t.NewsImage).WithOne(t => t.News);
            });

            modelBuilder.Entity<NewsImage>(e =>
            {
                e.HasKey(c => c.tbNewsID);
            });
        }
    }
}
