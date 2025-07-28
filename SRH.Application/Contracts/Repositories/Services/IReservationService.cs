using System.Linq.Expressions;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.DTO.reservations;
using SRH.Application.DTO.dbo;

namespace SGRH.Application.Contracts.Repositories.Services
{
    public interface IReservationService
    {
        Task<OperationResult<IEnumerable<Reservation>>> GetReservation();

        Task<OperationResult<Reservation>> GetReservationById(int id, GetActiveReservationDto dto);

        Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto createReservationDto);

        Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto updateReservationDto);

        Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto);
    }
}