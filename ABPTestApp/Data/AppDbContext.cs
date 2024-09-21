using ABPTestApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata;

namespace ABPTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<ConferenceHall> ConferenceHalls { get; set; }

        public DbSet<Models.Service> Services { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasMany(e => e.Services)
            .WithMany(e => e.Bookings);

            modelBuilder.Entity<ConferenceHall>()
                .HasMany(e => e.Bookings)
                .WithOne(e => e.ConferenceHall)
                .HasForeignKey(e => e.HallId)
                .IsRequired();

            modelBuilder.Entity<ConferenceHall>().HasData(
                new ConferenceHall() { Id = 1, Name = "Hall A", RatePerHour = 2000, Capacity = 50 },
                new ConferenceHall() { Id = 2, Name = "Hall B", RatePerHour = 3500, Capacity = 100 },
                new ConferenceHall() { Id = 3, Name = "Hall C", RatePerHour = 1500, Capacity = 30 }
            );

            modelBuilder.Entity<Models.Service>().HasData(
                new Models.Service() { Id = 1, Name = "Projector", Price = 500 },
                new Models.Service() { Id = 2, Name = "WiFi", Price = 300 },
                new Models.Service() { Id = 3, Name = "Sound", Price = 700 }
            );
        }
    }
}
