using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Entites;
using SGRH.Web.Respositories.Interface;
using SGRH.Persistences;
using SGRH.Persistences.Context;

namespace SGRH.Web.Respositories
{
    public class FloorRepository : IFloorRepository
    {
        private readonly SGRHContext _context;

        public FloorRepository(SGRHContext context)
        {
            _context = context;
        }

        public async Task<List<Floor>> GetAllAsync()
        {
            return await _context.Floor
                .AsNoTracking()
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<Floor> GetByIdAsync(int id)
        {
            return await _context.Floor.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Floor floor)
        {
            _context.Floor.Add(floor);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Floor floor)
        {
            _context.Floor.Update(floor);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var floor = await _context.Floor.FindAsync(id);
            if (floor == null) return false;

            floor.IsDeleted = true;  
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
