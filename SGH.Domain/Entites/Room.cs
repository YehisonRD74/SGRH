using SGRH._Domain.Entities;

public class Room
{
    public int Id { get; private set; }
    public string Estado { get; private set; }
    public int NumeroHabitacion { get; private set; }
    
    public int RoomCategoryId { get; private set; }
    
    public RoomCategory RoomCategory { get; private set; }

    public Room(int numeroHabitacion, string estado, int roomCategoryId)
    {
        NumeroHabitacion = numeroHabitacion;
        Estado = estado;
        RoomCategoryId = roomCategoryId;
    }
}