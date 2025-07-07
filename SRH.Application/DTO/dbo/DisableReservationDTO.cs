namespace SRH.Application.DTO.dbo;

public record DisableReservationDto
{
    public int ReservationId{ get; init; }
    public DateTime UpdateAT { get; init; }
}
