using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories;
using SGRH._Domain.Base;
using SGRH.Persistences.Base;
using SRH.Application.DTO.dbo;

namespace SGM.Persistence.Repositories
{
    public class RateRepository : BaseRepository<RateRepository>, IRateRepository
    {
        private readonly string _connectionString;

        public RateRepository(string connectionString, ILogger<RateRepository> logger)
            : base(logger)
        {
            _connectionString = connectionString;
        }

        public async Task<OperationResult> AddAsync(CreateRateDTO dto)
        {
            var result = new OperationResult();

            try
            {
                LogInformation("Creando tarifa para la temporada: {Season}", dto?.Season);

                if (dto == null || string.IsNullOrWhiteSpace(dto.Season) || dto.RatePrice <= 0 || dto.CategoryId <= 0)
                {
                    result.IsSuccess = false;
                    result.Message = "Datos inválidos para crear la tarifa.";
                    return result;
                }

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.CreateRate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Season", dto.Season);
                command.Parameters.AddWithValue("@RatePrice", dto.RatePrice);
                command.Parameters.AddWithValue("@CategoryId", dto.CategoryId);

                var output = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(output);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.Message = output.Value?.ToString();
                result.IsSuccess = result.Message == "Tarifa creada exitosamente.";

                if (result.IsSuccess)
                    LogInformation(result.Message);
                else
                    LogError(new Exception("SP Execution Error"), result.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al crear tarifa.");
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(UpdateRateDTO dto)
        {
            var result = new OperationResult();

            try
            {
                LogInformation("Actualizando tarifa ID: {Id}", dto?.Id);

                if (dto == null || dto.Id <= 0 || string.IsNullOrWhiteSpace(dto.Season) || dto.RatePrice <= 0 || dto.CategoryId <= 0)
                {
                    result.IsSuccess = false;
                    result.Message = "Datos inválidos para actualizar la tarifa.";
                    return result;
                }

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.UpdateRate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", dto.Id);
                command.Parameters.AddWithValue("@Season", dto.Season);
                command.Parameters.AddWithValue("@RatePrice", dto.RatePrice);
                command.Parameters.AddWithValue("@CategoryId", dto.CategoryId);

                var output = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(output);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.Message = output.Value?.ToString();
                result.IsSuccess = result.Message == "Tarifa actualizada correctamente.";

                if (result.IsSuccess)
                    LogInformation(result.Message);
                else
                    LogError(new Exception("SP Execution Error"), result.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al actualizar tarifa.");
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> DisableAsync(int id)
        {
            var result = new OperationResult();

            try
            {
                LogInformation("Desactivando tarifa ID: {Id}", id);

                if (id <= 0)
                {
                    result.IsSuccess = false;
                    result.Message = "ID inválido.";
                    return result;
                }

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.DisableRate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);

                var output = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(output);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.Message = output.Value?.ToString();
                result.IsSuccess = result.Message == "Tarifa desactivada correctamente.";

                if (result.IsSuccess)
                    LogInformation(result.Message);
                else
                    LogError(new Exception("SP Execution Error"), result.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al desactivar tarifa.");
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            var result = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.GetActiveRates", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                var rates = new List<GetRateDTO>();

                while (await reader.ReadAsync())
                {
                    var rate = new GetRateDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Season = reader.GetString(reader.GetOrdinal("Season")),
                        RatePrice = reader.GetDecimal(reader.GetOrdinal("RatePrice")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"))
                    };

                    rates.Add(rate);
                }

                result.IsSuccess = true;
                result.Data = rates;
                result.Message = "Tarifas activas obtenidas correctamente.";
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al obtener tarifas.");
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }
    }
}

