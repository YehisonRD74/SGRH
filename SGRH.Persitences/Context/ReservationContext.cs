using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SGRH.Persitences.Tests.Context
{
   public class ReservationContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Reservation");
        }
        public DbSet<Reservation> Reservation { get; set; }
        // public DbSet<User> User { set; get; }
        // public DbSet<Customer> Customer { get; set; } 
        public DbSet<ReservationDetail> ReservationDetails { get; set; }  

    }
}
