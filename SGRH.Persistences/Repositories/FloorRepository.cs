using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SGRH.Application.Contracts.Repositories.dbo;
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
        
        public async Task<OperationResult<Floor>> CreateFloor(CreateFloorDto? entity)
        {
            try
            {
                if (entity == null)
                    return new OperationResult<Floor> { IsSuccess = false, Message = "Datos inválidos", Data = null! };

                var floor = new Floor(entity.FloorId, entity.FloorNumber);

                await _context.Floor.AddAsync(floor);
                await _context.SaveChangesAsync();

                return new OperationResult<Floor>
                {
                    IsSuccess = true,
                    Message = "Piso creado exitosamente",
                    Data = floor
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en AddAsync");
                return new OperationResult<Floor>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }

        public async Task<OperationResult<Floor>> UpdateFloor(OperationResult<Floor> entity)
        {
            try
            {
                if (entity?.Data == null)
                {
                    return new OperationResult<Floor>
                    {
                        IsSuccess = false,
                        Message = "Datos inválidos: piso no proporcionado",
                        Data = null
                    };
                }

                var existing = await _context.Floor.FindAsync(entity.Data.Id);
                if (existing == null)
                {
                    return new OperationResult<Floor>
                    {
                        IsSuccess = false,
                        Message = $"Piso con ID {entity.Data.Id} no encontrado",
                        Data = null
                    };
                }

                
                existing.FloorNumber = entity.Data.FloorNumber;
                existing.UpdatedAt = entity.Data.UpdatedAt;
                existing.UpdatedBy = entity.Data.UpdatedBy ?? "admin";
                existing.IsDeleted = entity.Data.IsDeleted;
                existing.DeletedAt = entity.Data.DeletedAt;
                existing.DeletedBy = entity.Data.DeletedBy;

                _context.Floor.Update(existing);
                await _context.SaveChangesAsync();

                return new OperationResult<Floor>
                {
                    IsSuccess = true,
                    Message = "Piso actualizado correctamente",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateFloor");
                return new OperationResult<Floor>
                {
                    IsSuccess = false,
                    Message = $"Excepción: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<OperationResult<Floor>> DisableFloor(DisableFloorDto? entity)
        {
            try
            {
                if (entity == null)
                    return new OperationResult<Floor> { IsSuccess = false, Message = "Datos inválidos", Data = null! };

                var floor = await _context.Floor.FindAsync(entity.FloorId);
                if (floor == null)
                    return new OperationResult<Floor> { IsSuccess = false, Message = "Piso no encontrado", Data = null! };

                
                _context.Floor.Update(floor);
                await _context.SaveChangesAsync();

                return new OperationResult<Floor>
                {
                    IsSuccess = true,
                    Message = "Piso deshabilitado",
                    Data = floor
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DisableAsync");
                return new OperationResult<Floor>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }
        
        public async Task<IEnumerable<Floor>> GetAllFloor(Expression<Func<Floor, bool>>? filter)
        {
            return await _context.Floor.ToListAsync();
        }
        
        public async Task<OperationResult<Floor>> GetFloorById(int id)
        {
            try
            {
                var floor = await _context.Floor.FindAsync(id);

                if (floor == null)
                    return new OperationResult<Floor> { IsSuccess = false, Message = "Piso no encontrado", Data = null! };

                return new OperationResult<Floor>
                {
                    IsSuccess = true,
                    Message = "Piso encontrado",
                    Data = floor
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetByIdAsync");
                return new OperationResult<Floor>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }
        
        public Task<bool>? ExistAsync(Expression<Func<Floor, bool>>? filter)
        {
            if (filter != null) return _context.Floor.AnyAsync(filter);
            return null;
        }
    }

}
