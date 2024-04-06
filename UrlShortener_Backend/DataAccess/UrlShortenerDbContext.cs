using System;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener_Backend.DataAccess
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
           : base(options)
        {
        }

        public DbSet<Models.Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Url>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Models.Url>()
                .Property(u => u.Id)
                .UseMySqlIdentityColumn()
                .ValueGeneratedOnAdd();
        }
    }

}