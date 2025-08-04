namespace SRH.Application.DTO.dbo
{
    public record GetActiveRoomDto(
        int RoomId,
        string NumeroHabitacion,
        string Type,
        int FloorId,
        decimal Price,
        string Status
    );
}