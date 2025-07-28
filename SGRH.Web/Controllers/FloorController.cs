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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FloorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FloorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FloorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FloorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FloorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
