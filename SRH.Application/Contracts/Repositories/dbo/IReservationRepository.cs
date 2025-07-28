using System.Linq.Expressions;
using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;
using System.Threading.Tasks;
using SGRH._Domain.Entites;

namespace SRH.Application.Contracts.Repositories.dbo
{
    public interface IReservationRepository
    {
        Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto? createReservationDto);

        Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto updateReservationDto);

        Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto);

        Task<IEnumerable<Reservation>> GetAllResevation(Expression<Func<Reservation, bool>>? predicate = null);

        Task<OperationResult<Reservation>> GetReservationById(int id);
    }
}