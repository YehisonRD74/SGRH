using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Entites;
using SGRH.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRH.Persitences.Tests.Context
{
    public class FloorContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Floor");
        }
        public DbSet<Floor> Floor { get; set; }

    } 
}
