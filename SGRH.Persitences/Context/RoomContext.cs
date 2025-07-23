using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRH._Domain.Entites;

namespace SGRH.Persitences.Tests.Context
{
    public class RoomContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Room");
        }
        public DbSet<Room> Room { set; get; }
    }
}
