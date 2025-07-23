using System.Linq.Expressions;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SGRH.Application.Contracts.Repositories.dbo
{
    public interface IFloorRepository
    {
        Task<OperationResult<Floor>> CreateFloor(CreateFloorDto entity);

        Task<OperationResult<Floor>> UpdateFloor(OperationResult<Floor> entity);

        Task<OperationResult<Floor>> DisableFloor(DisableFloorDto entity);

        Task<IEnumerable<Floor>> GetAllFloor(Expression<Func<Floor, bool>>? predicate = null);

        Task<OperationResult<Floor>> GetFloorById(int id);

        Task<bool> ExistAsync(Expression<Func<Floor, bool>>? predicate);
    }
}