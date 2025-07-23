using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Base;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SRH.Application.Contracts.Repositories.dbo;
using System.Linq.Expressions;
using SGRH._Domain.Entites;
using SGRH.Persistences.Context;
using SRH.Application.DTO.dbo;

namespace SGRH.Persistences.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly SGRHContext _context;

    public RoomRepository(SGRHContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<Room>> CreateRoom(CreateRoomDto dto)
    {
        try
        {
            var room = new Room
            {
                NumeroHabitacion = dto.NumeroHabitacion,
                Type = dto.Type,
                FloorId = dto.FloorId, 
                Price= dto.Price,
                Status = dto.Status
            };

            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return new OperationResult<Room>
            {
                IsSuccess = true,
                Message = "Habitación creada correctamente",
                Data = room
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<Room>
            {
                IsSuccess = false,
                Message = $"Error: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<OperationResult<Room>> UpdateRoom(UpdateRoomDto dto)
    {
        try
        {
            var room = await _context.Room.FindAsync(dto.Id);
            if (room == null)
                return new OperationResult<Room> { IsSuccess = false, Message = "Habitación no encontrada" };

            room.NumeroHabitacion = dto.NumeroHabitacion;
            room.Type = dto.Type;
            room.FloorId = dto.FloorId;
            room.Price = dto.Price;
            room.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new OperationResult<Room>
            {
                IsSuccess = true,
                Message = "Habitación actualizada",
                Data = room
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<Room>
            {
                IsSuccess = false,
                Message = $"Error: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<OperationResult<Room>> DisableRoom(DisableRoomDto dto)
    {
        try
        {
            var room = await _context.Room.FindAsync(dto.RoomId);
            if (room == null)
                return new OperationResult<Room> { IsSuccess = false, Message = "Habitación no encontrada" };

            room.Status = "Inactiva";
            await _context.SaveChangesAsync();

            return new OperationResult<Room>
            {
                IsSuccess = true,
                Message = "Habitación deshabilitada",
                Data = room
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<Room>
            {
                IsSuccess = false,
                Message = $"Error: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<OperationResult<Room>> GetRoomById(int id)
    {
        try
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
                return new OperationResult<Room> { IsSuccess = false, Message = "Habitación no encontrada" };

            return new OperationResult<Room>
            {
                IsSuccess = true,
                Message = "Habitación encontrada",
                Data = room
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<Room>
            {
                IsSuccess = false,
                Message = $"Error: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<IEnumerable<Room>> GetAllRoom(Expression<Func<Room, bool>>? predicate = null)
    {
        if (predicate != null)
            return await _context.Room.Where(predicate).ToListAsync();

        return await _context.Room.ToListAsync();
    }

    public async Task<bool> ExistAsync(Expression<Func<Room, bool>>? predicate)
    {
        if (predicate == null)
            return false;

        return await _context.Room.AnyAsync(predicate);
    }
}
