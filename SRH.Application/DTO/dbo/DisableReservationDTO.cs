namespace SRH.Application.DTO.dbo;

public record DisableReservationDTO
{
    public int ReservationId{ get; init; }
    public DateTime UpdateAT { get; init; }
}
