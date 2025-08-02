namespace SGRH.Web.Services
{
    using SGRH._Domain.Base;
    using SGRH._Domain.Entites;
    using SGRH.Application.DTO.dbo;
    using SGRH.Web.Models;
    using SGRH.Web.Models.Floor;
    using SGRH.Web.Services.Interface;
    using SRH.Application.DTO.dbo;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Json;
   

    public class FloorService : IFloorService
    {
        private readonly HttpClient _httpClient;

        public FloorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<FloorModels>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Floor/GetAllFloor");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetAllFloorResponse>();
                return result?.Data ?? new List<FloorModels>();
            }
            return new List<FloorModels>();
        }

        public async Task<FloorModels> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Floor/GetFloorById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetFloorResponse>();
                return result?.data;
            }
            return null;
        }

        public async Task<bool> CreateAsync(FloorCreateModels model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
            model.CreatedBy = "admin";
            model.UpdatedBy = "admin";
            model.IsDeleted = false;

            var response = await _httpClient.PostAsJsonAsync("api/Floor/CreateFloor", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(FloorEditModels model)
        {
            model.UpdatedAt = DateTime.UtcNow;
            var response = await _httpClient.PostAsJsonAsync("api/Floor/UpdateFloor", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Floor/DeleteFloor/{id}");
            return response.IsSuccessStatusCode;
        }

        public Task<OperationResult<IEnumerable<Floor>>> GetFloor()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<Floor>> GetFloorById(int id, GetFloorByIdDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<Floor>> UpDateFloor(UpdateFloorDto updateFloorDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<bool>> DisableFloor(DisableFloorDto disableFloorDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<CreateFloorDto>> CreateFloor(CreateFloorDto createFloorDto)
        {
            throw new NotImplementedException();
        }

      public static void ConfigureHttpClient(IServiceProvider client)
        {
            throw new NotImplementedException();
        }
    }

}
