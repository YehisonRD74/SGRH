using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SGRH.Persistences;
using SGRH.Persistences.Context;

namespace SGRH.Persistences
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<SGRHContext>
    {
        public SGRHContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SGRHContext>();

            optionsBuilder.UseSqlServer("Server=localhost;Database=SGRHDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new SGRHContext(optionsBuilder.Options);
        }
    }
}