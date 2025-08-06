using Microsoft.Extensions.Logging;
using SGRH.Web.Models;
using SGRH.Web.Models.Floor;
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
    public class FloorService : IFloorService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FloorService> _logger;

        public FloorService(HttpClient httpClient, ILogger<FloorService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Método privado para manejar la lógica común de las llamadas a la API
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

        // Método GetAllAsync refactorizado
        public async Task<List<FloorModels>> GetAllAsync()
        {
            var response = await ExecuteApiCallAsync<GetAllFloorCreateResponse>(() => _httpClient.GetAsync("api/Floor/GetAllFloor"));

            _logger.LogInformation("Datos de pisos obtenidos correctamente.");
            return response?.Data ?? new List<FloorModels>();
        }

        // Método GetByIdAsync refactorizado
        public async Task<FloorModels?> GetByIdAsync(int id)
        {
            var response = await ExecuteApiCallAsync<GetFloorCreateResponse>(() => _httpClient.GetAsync($"api/Floor/GetFloorById/{id}"));

            if (response?.Data != null)
            {
                _logger.LogInformation("Piso con ID {Id} obtenido correctamente.", id);
            }
            return response?.Data;
        }

        // Método CreateAsync refactorizado
        public async Task<bool> CreateAsync(CreateFloor model)
        {
            if (model == null)
            {
                _logger.LogWarning("El modelo para crear un piso es nulo.");
                return false;
            }

            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
            model.CreatedBy = "admin";
            model.UpdatedBy = "admin";
            model.IsDeleted = false;

            var response = await ExecuteApiCallAsync<object?>(() => _httpClient.PostAsJsonAsync("api/Floor/CreateFloor", model));

            if (response != null)
            {
                _logger.LogInformation("Piso creado correctamente.");
                return true;
            }
            _logger.LogWarning("Fallo al crear el piso.");
            return false;
        }

        // Método UpdateAsync refactorizado
        public async Task<bool> UpdateAsync(FloorEditModels model)
        {
            if (model == null)
            {
                _logger.LogWarning("El modelo para actualizar un piso es nulo.");
                return false;
            }

            model.UpdatedAt = DateTime.UtcNow;

            var response = await ExecuteApiCallAsync<object?>(() => _httpClient.PostAsJsonAsync("api/Floor/UpdateFloor", model));

            if (response != null)
            {
                _logger.LogInformation("Piso con ID {Id} actualizado correctamente.", model.Id);
                return true;
            }
            _logger.LogWarning("Fallo al actualizar el piso.");
            return false;
        }

        // Método DeleteAsync refactorizado
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("El ID para eliminar un piso no es válido.");
                return false;
            }

            var response = await ExecuteApiCallAsync<object?>(() => _httpClient.DeleteAsync($"api/Floor/DeleteFloor/{id}"));

            if (response != null)
            {
                _logger.LogInformation("Piso con ID {Id} eliminado correctamente.", id);
                return true;
            }
            _logger.LogWarning("Fallo al eliminar el piso con ID {Id}.", id);
            return false;
        }
    }
}