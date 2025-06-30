namespace SGRH.Application.DTO.dbo
{
    public record GetActiveRoomDTO
    (
        int RoomId,
        int Number,
        string Type,
        int FloorId,
        decimal Price,
        string Description
    );
}


