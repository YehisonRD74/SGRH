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
    public class ReservationDetailRepository : BaseRepository<ReservationDetailRepository>, IReservationDetailRepository
    {
        private readonly string _connectionString;

        public ReservationDetailRepository(string connectionString, ILogger<ReservationDetailRepository> logger)
            : base(logger)
        {
            _connectionString = connectionString;
        }

        public async Task<OperationResult> AddAsync(CreateReservationDetailDTO dto)
        {
            var result = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.CreateReservationDetail", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@NightPrice", dto.NightPrice);
                command.Parameters.AddWithValue("@Subtotal", dto.Subtotal);
                command.Parameters.AddWithValue("@ReservationId", dto.ReservationId);
                command.Parameters.AddWithValue("@RoomId", dto.RoomId);
                command.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.Message = p_result.Value?.ToString();
                result.IsSuccess = result.Message == "Detalle de reserva creado exitosamente.";
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al crear el detalle de reserva.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(UpdateReservationDetailDTO dto)
        {
            var result = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.UpdateReservationDetail", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", dto.Id);
                command.Parameters.AddWithValue("@NightPrice", dto.NightPrice);
                command.Parameters.AddWithValue("@Subtotal", dto.Subtotal);
                command.Parameters.AddWithValue("@ReservationId", dto.ReservationId);
                command.Parameters.AddWithValue("@RoomId", dto.RoomId);
                command.Parameters.AddWithValue("@UpdatedAt", dto.UpdatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.Message = p_result.Value?.ToString();
                result.IsSuccess = result.Message == "Detalle de reserva actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al actualizar el detalle de reserva.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> DisableAsync(DisableReservationDetailDTO dto)
        {
            var result = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.DisableReservationDetail", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", dto.Id);
                command.Parameters.AddWithValue("@UpdatedAt", dto.UpdatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.Message = p_result.Value?.ToString();
                result.IsSuccess = result.Message == "Detalle de reserva desactivado correctamente.";
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al desactivar el detalle de reserva.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            var result = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.GetReservationDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                var list = new List<GetReservationDetailDTO>();

                while (await reader.ReadAsync())
                {
                    var detail = new GetReservationDetailDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        NightPrice = reader.GetDecimal(reader.GetOrdinal("NightPrice")),
                        Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                        ReservationId = reader.GetInt32(reader.GetOrdinal("ReservationId")),
                        RoomId = reader.GetInt32(reader.GetOrdinal("RoomId"))
                    };

                    list.Add(detail);
                }

                result.IsSuccess = true;
                result.Data = list;
                result.Message = "Detalles de reserva obtenidos correctamente.";
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al obtener detalles de reserva.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
