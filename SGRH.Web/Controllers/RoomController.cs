using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRH.Web.Models;

namespace SGRH.Web.Controllers
{
    public class RoomController : Controller
    {
        // GET: RoomController
        public async Task<IActionResult> Index()
        {
            GetAllRoomResponse getAllRoomResponse = null;


            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7114/");
                    var response = await client.GetAsync("api/Room/GetAllRoom");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllRoomResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllRoomResponse>(
                            responseString,
                            new System.Text.Json.JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                    }
                    else
                    {
                        getAllRoomResponse = new GetAllRoomResponse
                        {
                            IsSuccess = false,
                            Message = "Error al obtener los datos de las habitaciones.",
                            Data = new List<RoomModels>()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllRoomResponse = new GetAllRoomResponse
                {
                    IsSuccess = false,
                    Message = $"Error durante la transacción: {ex.Message}.",
                    Data = new List<RoomModels>()
                };
            }

            return View(getAllRoomResponse.Data); 
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
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

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomController/Edit/5
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

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
