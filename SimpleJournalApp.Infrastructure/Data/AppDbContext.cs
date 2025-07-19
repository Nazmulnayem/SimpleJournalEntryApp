using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleJournalApp.Domain.Entities;

namespace SimpleJournalApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<AppUser> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<JournalEntry> JournalEntry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JournalEntry>().HasKey(x => x.Id);
            modelBuilder.Entity<AppUser>().ToTable("Users").HasKey(x => x.Id);
            // --- Seed Admin User ---
            var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("1234");
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = 1,
                Username = "admin",
                PasswordHash = adminPasswordHash,
                Role = "Admin"
            });

        }
    }
}
