using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SGRH.Persistences.Context;
using SRH.Application.Contracts.Repositories.Services;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly SGRHContext _context;
        private readonly IRoomService _roomService;


        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpGet("GetAllRoom")]
        public async Task<IActionResult> GetAllRoom()
        {
            var result = await _roomService.GetAllRoom();

            if (!result.IsSuccess)
                return StatusCode(500, result.Message);

            return Ok(result); 
        }



        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (room.Id != 0)
                return BadRequest("No se debe enviar el Id manualmente. Es generado por la base de datos.");


            bool categoryExists = await _context.RoomCategory.AnyAsync(c => c.Id == room.RoomCategoryId);
            if (!categoryExists)
                return BadRequest($"El CategoryId '{room.RoomCategoryId}' no existe.");


            bool floorExists = await _context.Floor.AnyAsync(f => f.Id == room.FloorId);
            if (!floorExists)
                return BadRequest($"El FloorId '{room.FloorId}' no existe.");


            var statusValid = new[] { "Available", "Occupied", "Maintenance" };
            if (!statusValid.Contains(room.Status))
                return BadRequest($"El Status '{room.Status}' no es válido. Debe ser: {string.Join(", ", statusValid)}.");


            room.IsDeleted = false;


            _context.Room.Add(room);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetActiveRoomByIdDto), new { id = room.Id }, room);
        }

        [HttpPost("UpdateRoom/{id}")]
        public async Task<IActionResult> UpdateRoom([FromBody] Room room, int id)
        {
            var entity = await _context.Room.FindAsync(id);

            if (entity != null)
            {
                entity.FloorId = room.FloorId;
                entity.NumeroHabitacion = room.NumeroHabitacion;
                entity.Price = room.Price;
                entity.RoomCategoryId = room.RoomCategoryId;
            }

            await _context.SaveChangesAsync();
            return Ok("Update entity");
        }

        [HttpPost("DisableRoom/{id}")]
        public async Task<IActionResult> DisableRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room != null)
            {
                room.IsDeleted = true;
                room.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return Ok(new
            {
                isSuccess = true,
                message = "Habitación deshabilitada exitosamente",
                data = room
            });
        }

    }
}
