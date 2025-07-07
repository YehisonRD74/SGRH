using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGRH._Domain.Entities;
using SGRH.Persistences.Context;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly SGRHContext _context;

        public RoomController(SGRHContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllRoom")]
        public async Task<IActionResult> GetAllRoom()
        {
            var rooms = await _context.Room.ToListAsync();
            return Ok(rooms);
        }

        [HttpGet("GetRoomBy{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
        }

        [HttpPost("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
        {
            if (id != room.Id)
                return BadRequest();

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost("DisableRoom")]
        public async Task<IActionResult> DisableRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
                return NotFound();

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(r => r.Id == id);
        }
    }
}
