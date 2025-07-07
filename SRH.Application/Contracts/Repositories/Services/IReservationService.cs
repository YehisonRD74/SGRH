using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.Services;

public interface IReservationService
{
    Task<OperationResult> GetReservation();

    Task<OperationResult> GetByIdReservation(int id, GetActiveReservationByIdDto dto);      

    Task<OperationResult> UpDateReservation(UpDateReservationDto upDateReservationDto);

    Task<OperationResult> DisableReservation(DisableReservationDto disableReservationDto);

    Task<OperationResult> CreateReservation(CreateReservationDto createReservationDto);
}