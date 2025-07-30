using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRH.Web.Models;

namespace SGRH.Web.Controllers
{
    public class FloorController : Controller
    {
        // GET: FloorController
        public async Task<IActionResult> Index()

        {
            GetAllFloorResponse getAllFloorResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
					client.BaseAddress = new Uri("https://localhost:7114/");
					var response = await client.GetAsync("api/Floor/GetAllFloor");


					if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllFloorResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllFloorResponse>(responseString);
                    }
                    else
                    {
                        getAllFloorResponse = new GetAllFloorResponse
                        {
                            isSuccess = false,
                            message = "Error al obtener los datos de los pisos."
                        };
                    }
                }

               
             
            }
            catch (Exception ex)
            {
				getAllFloorResponse = new GetAllFloorResponse
				{
					isSuccess = false,
					message = $"Error durante la transacion {ex.Message}."
				};

			}

			return View(getAllFloorResponse.data);
        }

        // GET: FloorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }   
            GetFloorResponse getFloorResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7114/");
                    var response = await client.GetAsync($"api/Floor/GetFloorById/{id}");




                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getFloorResponse = System.Text.Json.JsonSerializer.Deserialize<GetFloorResponse>(responseString);
                    }
                    else
                    {
                        getFloorResponse = new GetFloorResponse
                        {
                            isSuccess = false,
                            message = "Error al obtener los datos del piso."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getFloorResponse = new GetFloorResponse
                {
                    isSuccess = false,
                    message = $"Error durante la transacion {ex.Message}."
                };
            }
            if (getFloorResponse?.data == null || getFloorResponse.data.id == 0)
            {
                TempData["Error"] = "No se encontró el piso.";
                return RedirectToAction("Index");
            }

            return View(getFloorResponse.data);
        }

        // GET: FloorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FloorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FloorCreateModels model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Agregar valores automáticos
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;
                model.CreatedBy = "admin"; // O toma del usuario autenticado
                model.UpdatedBy = "admin";
                model.IsDeleted = false;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7114/");

                    var response = await client.PostAsJsonAsync("api/Floor/CreateFloor", model);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Piso creado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al crear el piso.");
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                return View(model);
            }
        }


        // GET: FloorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            if (id <= 0)
            {
                return NotFound();
            }
            GetFloorResponse getFloorResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7114/");
                    var response = await client.GetAsync($"api/Floor/GetFloor/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getFloorResponse = System.Text.Json.JsonSerializer.Deserialize<GetFloorResponse>(responseString);
                    }
                    else
                    {
                        getFloorResponse = new GetFloorResponse
                        {
                            isSuccess = false,
                            message = "Error al obtener los datos del piso."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getFloorResponse = new GetFloorResponse
                {
                    isSuccess = false,
                    message = $"Error durante la transacion {ex.Message}."
                };
            }
            return View(getFloorResponse.data);
        }

        // POST: FloorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FloorEditModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            { 
                GetFloorEditResponse editResponse;

                model.UpdatedAt = DateTime.UtcNow;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7114/");
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Floor/updateFloor", model);

                    if (!response.IsSuccessStatusCode)
                    {
                        TempData["Error"] = "Error al actualizar el piso.";
                        return View(model);
                    }

                    var responseString = await response.Content.ReadAsStringAsync();
                    editResponse = System.Text.Json.JsonSerializer.Deserialize<GetFloorEditResponse>(responseString);

                    if (editResponse != null && editResponse.IsSuccess)
                    {
                        TempData["Success"] = "Piso actualizado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = editResponse?.Message ?? "Error desconocido al actualizar el piso.";
                        return View(model);
                    }
                }
            }
            catch
            {
                TempData["Error"] = "Ocurrió un error inesperado al actualizar el piso.";
                return View(model);
            }
        }


        // GET: FloorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
