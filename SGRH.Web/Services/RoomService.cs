using Microsoft.Extensions.Logging;
using SGRH.Web.Controllers;
using SGRH.Web.Models.Room;
using SGRH.Web.Response;
using SGRH.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRH.Web.Services
{
    public class RoomService : IRoomService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RoomService> _logger;

        public RoomService(HttpClient httpClient, ILogger<RoomService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private async Task<T?> ExecuteApiCallAsync<T>(Func<Task<HttpResponseMessage>> apiCall) where T : class
        {
            if (_httpClient.BaseAddress == null)
            {
                _logger.LogError("HttpClient BaseAddress no está configurada. Por favor, revisa appsettings.json.");
                throw new InvalidOperationException("HttpClient BaseAddress no está configurada.");
            }

            try
            {
                var response = await apiCall();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        return await response.Content.ReadFromJsonAsync<T>();
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error de formato JSON: {Message}", ex.Message);
                        return null;
                    }
                }
                else
                {
                    _logger.LogWarning("La llamada a la API falló con el código: {StatusCode}. Motivo: {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión a la API: {Message}", ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado: {Message}", ex.Message);
                return null;
            }
        }

        public async Task<List<RoomModels>> GetAllRoomsAsync()
        {
            var response = await ExecuteApiCallAsync<GetAllRoomResponse>(() => _httpClient.GetAsync("api/Room/GetAllRoom"));
            _logger.LogInformation("Datos de habitaciones obtenidos correctamente.");
            return response?.Data ?? new List<RoomModels>();
        }

        public async Task<RoomModels?> GetRoomByIdAsync(int id)
        {
            var response = await ExecuteApiCallAsync<GetRoomCreateResponse>(() => _httpClient.GetAsync($"api/Room/GetRoomById/{id}"));

            if (response?.Data != null)
            {
                _logger.LogInformation("Habitación con ID {Id} obtenida correctamente.", id);
            }
            return response?.Data;
        }

        public async Task<bool> CreateRoomAsync(RoomModels model)
        {
            if (model == null)
            {
                _logger.LogWarning("El modelo para crear una habitación es nulo.");
                return false;
            }

            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
            model.CreatedBy = "admin";
            model.UpdatedBy = "admin";
            model.IsDeleted = false;

            var response = await ExecuteApiCallAsync<object?>(() => _httpClient.PostAsJsonAsync("api/Room/CreateRoom", model));

            if (response != null)
            {
                _logger.LogInformation("Habitación creada correctamente.");
                return true;
            }
            _logger.LogWarning("Fallo al crear la habitación.");
            return false;
        }

        public async Task<bool> UpdateRoomAsync(int id, RoomEditModels model)
        {
            if (model == null)
            {
                _logger.LogWarning("El modelo para actualizar una habitación es nulo.");
                return false;
            }

            model.UpdatedAt = DateTime.UtcNow;

            var response = await ExecuteApiCallAsync<object?>(() => _httpClient.PostAsJsonAsync("api/Room/UpdateRoom", model));

            if (response != null)
            {
                _logger.LogInformation("Habitación con ID {Id} actualizada correctamente.", id);
                return true;
            }
            _logger.LogWarning("Fallo al actualizar la habitación con ID {Id}.", id);
            return false;
        }

        public async Task<bool> DisableRoomAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("El ID para deshabilitar una habitación no es válido.");
                return false;
            }

            var response = await ExecuteApiCallAsync<object?>(() => _httpClient.DeleteAsync($"api/Room/DisableRoom/{id}"));

            if (response != null)
            {
                _logger.LogInformation("Habitación con ID {Id} deshabilitada correctamente.", id);
                return true;
            }
            _logger.LogWarning("Fallo al deshabilitar la habitación con ID {Id}.", id);
            return false;
        }
    }
}
