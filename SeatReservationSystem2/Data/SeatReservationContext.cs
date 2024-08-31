using Microsoft.EntityFrameworkCore;
using SeatReservationSystem2.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SeatReservationSystem2.Models
{
    public class SeatReservationContext : DbContext
    {
        public SeatReservationContext(DbContextOptions<SeatReservationContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed 50 seats
            for (int i = 1; i <= 50; i++)
            {
                modelBuilder.Entity<Seat>().HasData(new Seat
                {
                    SeatId = i,
                    IsReserved = false
                });
            }
        }
    }
}

