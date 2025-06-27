using System;
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
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "Error: El objeto CreateReservationDTO no puede ser nulo.";
                    return resultOperation;
                }

                if (string.IsNullOrWhiteSpace(createReservationDTO.CreatedBy))
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CreatedBy' no puede estar vacío.";
                    return resultOperation;
                }

                if (createReservationDTO.CreatedBy.Length > 100)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CreatedBy' no puede tener más de 100 caracteres.";
                    return resultOperation;
                }

                if (createReservationDTO.UserId <= 0)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'UserId' debe ser mayor a 0.";
                    return resultOperation;
                }

                if (createReservationDTO.CheckInDate == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CheckInDate' no puede estar vacío.";
                    return resultOperation;
                }

                if (createReservationDTO.CheckOutDate == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CheckOutDate' no puede estar vacío.";
                    return resultOperation;
                }

                if (createReservationDTO.CreatedAt == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CreatedAt' no puede estar vacío.";
                    return resultOperation;
                }

                if (string.IsNullOrWhiteSpace(createReservationDTO.Status))
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'Status' no puede estar vacío.";
                    return resultOperation;
                }

                if (createReservationDTO.Status.Length > 20)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'Status' no puede tener más de 20 caracteres.";
                    return resultOperation;
                }

                if (createReservationDTO.TotalAmount <= 0)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'TotalAmount' debe ser mayor a 0.";
                    return resultOperation;
                }

                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("dbo.CreateReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CheckInDate", createReservationDTO.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", createReservationDTO.CheckOutDate);
                    command.Parameters.AddWithValue("@Status", createReservationDTO.Status);
                    command.Parameters.AddWithValue("@TotalAmount", createReservationDTO.TotalAmount);
                    command.Parameters.AddWithValue("@UserId", createReservationDTO.UserId);
                    command.Parameters.AddWithValue("@CreatedBy", createReservationDTO.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedAt", createReservationDTO.CreatedAt);

                    var p_result = new SqlParameter("@presult", SqlDbType.VarChar)
                    {
                        Direction = ParameterDirection.Output,
                        Size = 1000
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
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "Error: El objeto UpDateReservationDTO no puede ser nulo.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.ReservationId <= 0)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'ReservationId' debe ser mayor a 0.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.UserId <= 0)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'UserId' debe ser mayor a 0.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.CheckInDate == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CheckInDate' no puede estar vacío.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.CheckOutDate == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'CheckOutDate' no puede estar vacío.";
                    return resultOperation;
                }

                if (string.IsNullOrWhiteSpace(UpDateReservationDTO.Status))
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'Status' no puede estar vacío.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.Status.Length > 20)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'Status' no puede tener más de 20 caracteres.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.TotalAmount <= 0)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'TotalAmount' debe ser mayor a 0.";
                    return resultOperation;
                }

                if (UpDateReservationDTO.UpdateAT == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'UpdateAT' no puede estar vacío.";
                    return resultOperation;
                }

                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("dbo.UpdateReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ReservationId", UpDateReservationDTO.ReservationId);
                    command.Parameters.AddWithValue("@CheckInDate", UpDateReservationDTO.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", UpDateReservationDTO.CheckOutDate);
                    command.Parameters.AddWithValue("@Status", UpDateReservationDTO.Status);
                    command.Parameters.AddWithValue("@TotalAmount", UpDateReservationDTO.TotalAmount);
                    command.Parameters.AddWithValue("@UserId", UpDateReservationDTO.UserId);
                    command.Parameters.AddWithValue("@UpdateAT", UpDateReservationDTO.UpdateAT);

                    var p_result = new SqlParameter("@presult", SqlDbType.VarChar)
                    {
                        Direction = ParameterDirection.Output,
                        Size = 1000
                    };
                    command.Parameters.Add(p_result);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    var mensajeSP = p_result.Value?.ToString();
                    resultOperation.Message = mensajeSP;
                    if (mensajeSP == "Reserva actualizada correctamente.")
                    {
                        resultOperation.IsSuccess = true;
                        LogInformation("Reserva ID {ReservationId} actualizada correctamente.",
                            UpDateReservationDTO.ReservationId);
                    }
                    else
                    {
                        resultOperation.IsSuccess = false;
                        LogError(new Exception("SP Execution Error"), mensajeSP);
                    }
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
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "Error: El objeto DisableReservationDT no puede ser nulo.";
                    return resultOperation;
                }

                if (DisableReservationDT.ReservationId <= 0)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'ReservationId' debe ser mayor a 0.";
                    return resultOperation;
                }

                if (DisableReservationDT.UpdateAT == DateTime.MinValue)
                {
                    resultOperation.IsSuccess = false;
                    resultOperation.Message = "El campo 'UpdateAT' no puede estar vacío.";
                    return resultOperation;
                }

                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("dbo.DisableReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ReservationId", DisableReservationDT.ReservationId);
                    command.Parameters.AddWithValue("@UpdateAT", DisableReservationDT.UpdateAT);

                    var p_result = new SqlParameter("@presult", SqlDbType.VarChar)
                    {
                        Direction = ParameterDirection.Output,
                        Size = 1000
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
            OperationResult presult = new OperationResult();

            try
            {
                using (SqlConnection connection = new SqlConnection(this._connectionString))
                using (SqlCommand command = new SqlCommand("dbo.GetActiveReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<GetActiveReservation> reservations = new List<GetActiveReservation>();

                        if (!reader.HasRows)
                        {
                            presult.IsSuccess = true;
                            presult.Data = reservations;
                            presult.Message = "No hay reservas activas registradas.";
                            return presult;
                        }

                        while (await reader.ReadAsync())
                        {
                            GetActiveReservation reservation = new GetActiveReservation
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                CheckInDate = reader.GetDateTime(reader.GetOrdinal("CheckInDate")),
                                CheckOutDate = reader.GetDateTime(reader.GetOrdinal("CheckOutDate")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                            };

                            reservations.Add(reservation);
                        }

                        presult.IsSuccess = true;
                        presult.Data = reservations;
                        presult.Message = "Reservas activas obtenidas correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                presult.IsSuccess = false;
                presult.Message = $"Error al obtener las reservas activas: {ex.Message}";
                LogError(ex, "Error al obtener las reservas activas.");
            }

            return presult;
        }

    }
}