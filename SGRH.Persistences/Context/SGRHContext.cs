using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Base;
using SGRH._Domain.Entities;

namespace SGRH.Persistences.Context
{
    public class SGRHContext : DbContext
    {
        public SGRHContext(DbContextOptions<SGRHContext> options) : base(options)
        {
        }
        
        public DbSet<Room> Room { get; set; }
        public DbSet<Floor> Floor { get; set; }
       

       
    }
}