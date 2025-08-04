using Microsoft.AspNetCore.Mvc;
using SGRH.Web.Models.Room;
using SGRH.Web.Services.Interface;
using System.Threading.Tasks;

namespace SGRH.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: RoomController
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return View(rooms);
        }

        // GET: RoomController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room != null)
            {
                return View(room);
            }

            return NotFound();
        }

        // GET: RoomController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomModels room)
        {
            if (ModelState.IsValid)
            {
                var success = await _roomService.CreateRoomAsync(room);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Error al crear la habitación.");
            }

            return View(room);
        }

        // GET: RoomController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room != null)
            {
                return View(room); // Puedes mapearlo a RoomEditModels si es necesario
            }

            return NotFound();
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomEditModels room)
        {
            if (id != room.roomId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var success = await _roomService.UpdateRoomAsync(id, room);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Error al actualizar la habitación.");
            }

            return View(room);
        }

        // GET: RoomController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room != null)
            {
                return View(room);
            }

            return NotFound();
        }

        // POST: RoomController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _roomService.DisableRoomAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al deshabilitar la habitación.");
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
