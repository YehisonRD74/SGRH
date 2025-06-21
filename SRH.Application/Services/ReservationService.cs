using SGRH._Domain.Entities;
using SRH.Application.Contracts;
using SRH.Application.DTO;
using SRH.Application.Interfaces;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ReservationDto>> GetAllReservationsAsync()
    {
        var reservations = await _repository.GetAllAsync();

        return reservations.Select(r => new ReservationDto
        {
            Id = r.Id,
            CheckInDate = r.CheckInDate,
            CheckOutDate = r.CheckOutDate,
            Status = r.Status,
            TotalAmount = r.TotalAmount
        }).ToList();
    }

    public Task<List<Reservation>> GetAllReservationbyidAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ReservationDto> GetReservationByIdAsync(int id)
    {
        var reservation = await _repository.GetByIdAsync(id);
        if (reservation == null)
            return null;

        return new ReservationDto
        {
            Id = reservation.Id,
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate,
            Status = reservation.Status,
            TotalAmount = reservation.TotalAmount
        };
    }

    Task<List<Reservation>> IReservationService.GetAllReservationsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateReservationAsync(ReservationDto dto)
    {
        var reservation = new Reservation(
            0,
            dto.CheckInDate,
            dto.CheckOutDate,
            dto.Status,
            dto.TotalAmount,
            customerId: 0
        );

        await _repository.AddAsync(reservation);
    }

    // 4. Actualizar reserva
    public async Task UpdateReservationAsync(ReservationDto dto)
    {
        var existingReservation = await _repository.GetByIdAsync(dto.Id);
        if (existingReservation == null)
            throw new Exception("Reserva no encontrada");

        // Actualizar campos
        existingReservation.UpdateDates(dto.CheckInDate, dto.CheckOutDate); // suponiendo que tienes método para eso
        existingReservation.UpdateStatus(dto.Status);
        existingReservation.UpdateTotalAmount(dto.TotalAmount);

        await _repository.UpdateAsync(existingReservation);
    }


    public async Task DeleteReservationAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}