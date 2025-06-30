using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories;
using SGRH._Domain.Base;
using SGRH.Persistences.Base;
using SRH.Application.Contracts.Repositories.SGM.Application.Contracts.Repositories;
using SRH.Application.DTO.dbo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SGRH.Persistences.Repositories
{
    public class RoomCategoryRepository : BaseRepository<RoomCategoryRepository>, IRoomCategoryRepository
    {
        private readonly string _connectionString;

        public RoomCategoryRepository(string connectionString, ILogger<RoomCategoryRepository> logger)
            : base(logger)
        {
            _connectionString = connectionString;
        }

        public async Task<OperationResult> AddAsync(CreateRoomCategoryDTO dto)
        {
            var result = new OperationResult();

            try
            {
                if (dto == null)
                    return FailResult("El objeto DTO no puede ser nulo.");

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.CreateRoomCategory", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@CategoryName", dto.CategoryName);
                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@BaseRate", dto.BaseRate);
                command.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                string message = p_result.Value?.ToString();
                result.Message = message;
                result.IsSuccess = message == "Categoría creada exitosamente.";

                LogResult(result, message);
            }
            catch (Exception ex)
            {
                result = FailResult("Error al crear categoría: " + ex.Message, ex);
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(UpdateRoomCategoryDTO dto)
        {
            var result = new OperationResult();

            try
            {
                if (dto == null)
                    return FailResult("El objeto DTO no puede ser nulo.");

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.UpdateRoomCategory", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@CategoryId", dto.CategoryId);
                command.Parameters.AddWithValue("@CategoryName", dto.CategoryName);
                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@BaseRate", dto.BaseRate);
                command.Parameters.AddWithValue("@UpdatedAt", dto.UpdatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                string message = p_result.Value?.ToString();
                result.Message = message;
                result.IsSuccess = message == "Categoría actualizada exitosamente.";

                LogResult(result, message);
            }
            catch (Exception ex)
            {
                result = FailResult("Error al actualizar categoría: " + ex.Message, ex);
            }

            return result;
        }

        public async Task<OperationResult> DisableAsync(DisableRoomCategoryDTO dto)
        {
            var result = new OperationResult();

            try
            {
                if (dto == null)
                    return FailResult("El objeto DTO no puede ser nulo.");

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.DisableRoomCategory", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@CategoryId", dto.CategoryId);
                command.Parameters.AddWithValue("@UpdatedAt", dto.UpdatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                string message = p_result.Value?.ToString();
                result.Message = message;
                result.IsSuccess = message == "Categoría desactivada correctamente.";

                LogResult(result, message);
            }
            catch (Exception ex)
            {
                result = FailResult("Error al desactivar categoría: " + ex.Message, ex);
            }

            return result;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            var result = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.GetRoomCategories", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                var categories = new List<GetRoomCategoryDTO>();

                while (await reader.ReadAsync())
                {
                    categories.Add(new GetRoomCategoryDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        BaseRate = reader.GetDecimal(reader.GetOrdinal("BaseRate"))
                    });
                }

                result.IsSuccess = true;
                result.Data = categories;
                result.Message = categories.Count > 0 ? "Categorías obtenidas correctamente." : "No hay categorías activas registradas.";
            }
            catch (Exception ex)
            {
                result = FailResult("Error al obtener categorías: " + ex.Message, ex);
            }

            return result;
        }

        // Métodos auxiliares
        private OperationResult FailResult(string message, Exception ex = null)
        {
            if (ex != null) LogError(ex, message);
            return new OperationResult { IsSuccess = false, Message = message };
        }

        private void LogResult(OperationResult result, string message)
        {
            if (result.IsSuccess)
                LogInformation(message);
            else
                LogError(new Exception("SP Error"), message);
        }
    }
}

