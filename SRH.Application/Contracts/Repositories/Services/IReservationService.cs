using System.Linq.Expressions;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.DTO.reservations;
using SRH.Application.DTO.dbo;

namespace SGRH.Application.Contracts.Repositories.Services
{
    public interface IReservationService
    {
        Task<OperationResult<IEnumerable<ReservationDto>>> GetAllReservationDto(Expression<Func<Reservation, bool>>? predicate = null);

        Task<OperationResult<Reservation>> GetReservationById(int id);

        Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto createReservationDto);

        Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto updateReservationDto);

        Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto);
    }
}