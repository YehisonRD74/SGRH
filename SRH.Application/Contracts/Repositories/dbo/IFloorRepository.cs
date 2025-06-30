using System.Threading.Tasks;
using SGRH._Domain.Base;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;

public interface IFloorRepository
{
    Task<OperationResult> AddAsync(CreateFloorDTO TEntity);
    Task<OperationResult> UpdateAsync(UpdateFloorDTO TEntity);
    Task<OperationResult> DisableAsync(DisableFloorDTO TEntity);
    Task<OperationResult> GetAllAsync(System.Linq.Expressions.Expression<Func<Floor, bool>> predicate = null);
    Task<OperationResult> GetByIdAsync(int id);
    Task<bool> ExistAsync(System.Linq.Expressions.Expression<Func<Floor, bool>> predicate);
}