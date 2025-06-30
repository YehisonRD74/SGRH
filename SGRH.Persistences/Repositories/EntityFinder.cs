using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base; 

namespace SGM.Persistence.Repositories;

public class EntityFinder
{
    private readonly DbContext _context;

    public EntityFinder(DbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> FindByIdAsync<T>(object id) where T : class
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return OperationResult.Failure($"{typeof(T).Name} no encontrado.");
        }

        return OperationResult.Success(entity, $"{typeof(T).Name} recuperado exitosamente.");
    }
}