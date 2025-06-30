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
    public class ReservationRepository : BaseRepository<ReservationRepository>, IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(string connectionString, ILogger<ReservationRepository> logger)
            : base(logger)
        {
            _connectionString = connectionString;
        }

        public async Task<OperationResult> AddAsync(CreateReservationDTO createReservationDTO)
        {
            var resultOperation = new OperationResult();

            try
            {
                LogInformation("Creando reservación: {UserId}", createReservationDTO?.UserId);

                if (createReservationDTO == null)
                    return new OperationResult { IsSuccess = false, Message = "Error: El objeto CreateReservationDTO no puede ser nulo." };

                if (string.IsNullOrWhiteSpace(createReservationDTO.CreatedBy))
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CreatedBy' no puede estar vacío." };

                if (createReservationDTO.CreatedBy.Length > 100)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CreatedBy' no puede tener más de 100 caracteres." };

                if (createReservationDTO.UserId <= 0)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'UserId' debe ser mayor a 0." };

                if (createReservationDTO.CheckInDate == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CheckInDate' no puede estar vacío." };

                if (createReservationDTO.CheckOutDate == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CheckOutDate' no puede estar vacío." };

                if (createReservationDTO.CreatedAt == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CreatedAt' no puede estar vacío." };

                if (string.IsNullOrWhiteSpace(createReservationDTO.Status))
                    return new OperationResult { IsSuccess = false, Message = "El campo 'Status' no puede estar vacío." };

                if (createReservationDTO.Status.Length > 20)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'Status' no puede tener más de 20 caracteres." };

                if (createReservationDTO.TotalAmount <= 0)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'TotalAmount' debe ser mayor a 0." };

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.CreateReservation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@CheckInDate", createReservationDTO.CheckInDate);
                command.Parameters.AddWithValue("@CheckOutDate", createReservationDTO.CheckOutDate);
                command.Parameters.AddWithValue("@Status", createReservationDTO.Status);
                command.Parameters.AddWithValue("@TotalAmount", createReservationDTO.TotalAmount);
                command.Parameters.AddWithValue("@UserId", createReservationDTO.UserId);
                command.Parameters.AddWithValue("@CreatedBy", createReservationDTO.CreatedBy);
                command.Parameters.AddWithValue("@CreatedAt", createReservationDTO.CreatedAt);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                var mensajeSP = p_result.Value?.ToString();
                resultOperation.Message = mensajeSP;

                if (mensajeSP == "Reserva creada exitosamente.")
                {
                    resultOperation.IsSuccess = true;
                    LogInformation(mensajeSP);
                }
                else
                {
                    resultOperation.IsSuccess = false;
                    LogError(new Exception("SP Execution Error"), mensajeSP);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al crear la reserva.");
                resultOperation.IsSuccess = false;
                resultOperation.Message = "Ocurrió un error: " + ex.Message;
            }

            return resultOperation;
        }

        public async Task<OperationResult> UpdateAsync(UpDateReservationDTO UpDateReservationDTO)
        {
            var resultOperation = new OperationResult();

            try
            {
                LogInformation("Actualizando reservación ID: {ReservationId}", UpDateReservationDTO?.ReservationId);

                if (UpDateReservationDTO == null)
                    return new OperationResult { IsSuccess = false, Message = "Error: El objeto UpDateReservationDTO no puede ser nulo." };

                if (UpDateReservationDTO.ReservationId <= 0)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'ReservationId' debe ser mayor a 0." };

                if (UpDateReservationDTO.UserId <= 0)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'UserId' debe ser mayor a 0." };

                if (UpDateReservationDTO.CheckInDate == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CheckInDate' no puede estar vacío." };

                if (UpDateReservationDTO.CheckOutDate == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'CheckOutDate' no puede estar vacío." };

                if (string.IsNullOrWhiteSpace(UpDateReservationDTO.Status))
                    return new OperationResult { IsSuccess = false, Message = "El campo 'Status' no puede estar vacío." };

                if (UpDateReservationDTO.Status.Length > 20)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'Status' no puede tener más de 20 caracteres." };

                if (UpDateReservationDTO.TotalAmount <= 0)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'TotalAmount' debe ser mayor a 0." };

                if (UpDateReservationDTO.UpdateAT == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'UpdateAT' no puede estar vacío." };

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.UpdateReservation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ReservationId", UpDateReservationDTO.ReservationId);
                command.Parameters.AddWithValue("@CheckInDate", UpDateReservationDTO.CheckInDate);
                command.Parameters.AddWithValue("@CheckOutDate", UpDateReservationDTO.CheckOutDate);
                command.Parameters.AddWithValue("@Status", UpDateReservationDTO.Status);
                command.Parameters.AddWithValue("@TotalAmount", UpDateReservationDTO.TotalAmount);
                command.Parameters.AddWithValue("@UserId", UpDateReservationDTO.UserId);
                command.Parameters.AddWithValue("@UpdateAT", UpDateReservationDTO.UpdateAT);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                var mensajeSP = p_result.Value?.ToString();
                resultOperation.Message = mensajeSP;

                if (mensajeSP == "Reserva actualizada correctamente.")
                {
                    resultOperation.IsSuccess = true;
                    LogInformation("Reserva ID {ReservationId} actualizada correctamente.", UpDateReservationDTO.ReservationId);
                }
                else
                {
                    resultOperation.IsSuccess = false;
                    LogError(new Exception("SP Execution Error"), mensajeSP);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al actualizar la reserva.");
                resultOperation.IsSuccess = false;
                resultOperation.Message = "Ocurrió un error: " + ex.Message;
            }

            return resultOperation;
        }

        public async Task<OperationResult> DisableAsync(DisableReservationDTO DisableReservationDT)
        {
            var resultOperation = new OperationResult();

            try
            {
                LogInformation("Desactivando reserva ID: {ReservationId}", DisableReservationDT?.ReservationId);

                if (DisableReservationDT == null)
                    return new OperationResult { IsSuccess = false, Message = "Error: El objeto DisableReservationDT no puede ser nulo." };

                if (DisableReservationDT.ReservationId <= 0)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'ReservationId' debe ser mayor a 0." };

                if (DisableReservationDT.UpdateAT == DateTime.MinValue)
                    return new OperationResult { IsSuccess = false, Message = "El campo 'UpdateAT' no puede estar vacío." };

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.DisableReservation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ReservationId", DisableReservationDT.ReservationId);
                command.Parameters.AddWithValue("@UpdateAT", DisableReservationDT.UpdateAT);

                var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(p_result);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                var mensajeSP = p_result.Value?.ToString();
                resultOperation.Message = mensajeSP;

                if (mensajeSP == "Reserva desactivada correctamente.")
                {
                    resultOperation.IsSuccess = true;
                    LogInformation(mensajeSP);
                }
                else
                {
                    resultOperation.IsSuccess = false;
                    LogError(new Exception("SP Execution Error"), mensajeSP);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al desactivar la reserva.");
                resultOperation.IsSuccess = false;
                resultOperation.Message = "Ocurrió un error: " + ex.Message;
            }

            return resultOperation;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            var presult = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.GetActiveReservation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                var reservations = new List<GetActiveReservation>();

                if (!reader.HasRows)
                {
                    presult.Message = "No se encontraron datos.";
                    presult.IsSuccess = false;
                    LogInformation("No se encontraron reservas activas.");
                    return presult;
                }

                while (await reader.ReadAsync())
                {
                    var reservation = new GetActiveReservation
                    {
                        Id = reader.GetInt32("ReservationId"),
                        CheckInDate = reader.GetDateTime("CheckInDate"),
                        CheckOutDate = reader.GetDateTime("CheckOutDate"),
                        Status = reader.GetString("Status"),
                        TotalAmount = reader.GetDecimal("TotalAmount"),
                        UserId = reader.GetInt32("UserId")
                    };

                    reservations.Add(reservation);
                }

                presult.IsSuccess = true;
                presult.Message = "Datos encontrados.";
                presult.Data = reservations;

                LogInformation("Reservas activas recuperadas exitosamente.");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al obtener todas las reservas activas.");
                presult.IsSuccess = false;
                presult.Message = "Ocurrió un error: " + ex.Message;
            }

            return presult;
        }

        public Task<OperationResult> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
