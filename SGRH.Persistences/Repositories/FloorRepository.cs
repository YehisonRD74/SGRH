using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SGRH.Persistences.Context;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.DTO.dbo;

namespace SGRH.Persistences.Repositories
{
    public class FloorRepository : IFloorRepository
    {
        private readonly SGRHContext _context;
        private readonly ILogger<FloorRepository> _logger;

        public FloorRepository(SGRHContext context, ILogger<FloorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateFloorDto? entity)
        {
            if (entity == null)
                return OperationResult.Failure("Error: El objeto CreateFloorDTO no puede ser nulo.");

            try
            {
                _logger.LogInformation("Creando piso");

                var floor = new Floor(entity.FloorId, entity.FloorNumer);

                await _context.Floor.AddAsync(floor);
                await _context.SaveChangesAsync();

                return OperationResult.Success("Piso creado exitosamente.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al crear piso");
                return OperationResult.Failure("Error al crear piso");
            }
        }

        public Task<OperationResult> UpdateAsync(UpdateFloorDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> DisableAsync(DisableFloorDto? entity)
        {
            if (entity == null)
                return OperationResult.Failure("Error: El objeto DisableFloorDTO no puede ser nulo.");

            try
            {
                _logger.LogInformation("Intentando desactivar piso con ID: {FloorId}", entity.FloorId);

                var existingEntity = await _context.Floor.FindAsync(entity.FloorId);
                if (existingEntity == null)
                    return OperationResult.Failure("El piso no existe");

                existingEntity.IsDeleted = true;
                existingEntity.DeletedAt = DateTime.UtcNow;
                existingEntity.DeletedBy = Environment.UserName;

                _context.Floor.Update(existingEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Piso con ID {FloorId} desactivado exitosamente por {User}.", entity.FloorId,
                    Environment.UserName);
                return OperationResult.Success("Piso desactivado exitosamente.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al desactivar piso con ID: {FloorId}", entity?.FloorId);
                return OperationResult.Failure("Error al desactivar piso");
            }
        }

        public async Task<OperationResult> GetAllAsync(Expression<Func<Floor, bool>>? filter)
        {
            try
            {
                _logger.LogInformation("Recuperando pisos");
                var data = await _context.Floor.Where(filter).ToListAsync();
                return OperationResult.Success(data, "Pisos recuperados exitosamente.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al recuperar pisos");
                return OperationResult.Failure("Error al recuperar pisos");
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando piso con ID: {FloorId}", id);
                var floor = await _context.Floor.FindAsync(id);
                if (floor == null)
                    return OperationResult.Failure("El piso no existe");

                return OperationResult.Success(floor, "Piso recuperado exitosamente.");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al recuperar piso con ID: {FloorId}", id);
                return OperationResult.Failure("Error al recuperar piso");
            }
        }

        public Task<bool>? ExistAsync(Expression<Func<Floor, bool>>? filter)
        {
            if (filter != null) return _context.Floor.AnyAsync(filter);
            return null;
        }
    }

}
