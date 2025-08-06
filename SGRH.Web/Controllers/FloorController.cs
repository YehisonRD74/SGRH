using Microsoft.AspNetCore.Mvc;
using SGRH.Web.Services;
using System.Threading.Tasks;
using SGRH.Web.Services.Interface;
using SGRH.Web.Models.Floor;

namespace SGRH.Web.Controllers
{
    public class FloorController : Controller
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        // GET: Floor
        public async Task<IActionResult> Index()
        {
            var floors = await _floorService.GetAllAsync();
            return View(floors);
        }

        // GET: Floor/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var floor = await _floorService.GetByIdAsync(id);
            if (floor == null)
            {
                return NotFound();
            }
            return View(floor);
        }

        // GET: Floor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Floor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFloor model)
        {
            if (ModelState.IsValid)
            {
                var result = await _floorService.CreateAsync(model);
                if (result)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Error al crear el piso.");
            }
            return View(model);
        }

        // GET: Floor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var floor = await _floorService.GetByIdAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            var editModel = new FloorEditModels(
      floor.FloorNumber,
      floor.Id,
      floor.CreatedAt,
      floor.CreatedBy,
      floor.UpdatedAt,
      floor.UpdatedBy,
      floor.IsDeleted,
      floor.DeletedBy,
      floor.DeletedAt
  );

            return View(editModel);
        }

        // POST: Floor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FloorEditModels model)
        {
            if (ModelState.IsValid)
            {
                var result = await _floorService.UpdateAsync(model);
                if (result)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Error al actualizar el piso.");
            }
            return View(model);
        }

        // GET: Floor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _floorService.GetByIdAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            var deleteModel = new FloorEditModels(
      floor.FloorNumber,
      floor.Id,
      floor.CreatedAt,
      floor.CreatedBy,
      floor.UpdatedAt,
      floor.UpdatedBy,
      floor.IsDeleted,
      floor.DeletedBy,
      floor.DeletedAt
  );

            return View(deleteModel);
        }

        // POST: Floor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _floorService.DeleteAsync(id);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al eliminar el piso.");
            return View();
        }
    }
}