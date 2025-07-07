namespace SGRH.Application.DTO.dbo
{
    public record GetActiveRoomDto
    (
        int RoomId,
        int Number,
        string Type,
        int FloorId,
        decimal Price,
        string Description
    );
}


