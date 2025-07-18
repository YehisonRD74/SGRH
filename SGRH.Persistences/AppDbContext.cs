using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;


namespace SGRH.Persistences
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Define los DbSet para cada entidad que quieres mapear a tablas
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Room> Room { get; set; }

        
    }
}