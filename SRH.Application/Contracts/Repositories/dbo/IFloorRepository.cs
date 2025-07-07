using System.Linq.Expressions;
using SGRH._Domain.Base;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.dbo;

public interface IFloorRepository
{
    Task<OperationResult> AddAsync(CreateFloorDto? entity);
    Task<OperationResult> UpdateAsync(UpdateFloorDto entity);
    Task<OperationResult> DisableAsync(DisableFloorDto? entity);
    Task<OperationResult> GetAllAsync(Expression<Func<Floor, bool>>? predicate = null);
    Task<OperationResult> GetByIdAsync(int id);
    Task<bool>? ExistAsync(Expression<Func<Floor, bool>>? predicate);
}