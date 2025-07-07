using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories;
using SGRH._Domain.Base;
using SGRH.Persistences.Base;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.DTO.dbo;

namespace SGRH.Persistences.Repositories
{
    public class ReservationRepository : BaseRepository<ReservationRepository>, IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(string connectionString, ILogger<ReservationRepository>? logger)
            : base(logger)
        {
            _connectionString = connectionString;
        }

        [Obsolete("Obsolete")]
        public async Task<OperationResult> AddAsync(CreateReservationDto createReservationDto)
        {
            var resultOperation = new OperationResult();

            try
            {
                if (createReservationDto?.UserId != null)
                {
                    if (createReservationDto?.UserId != null)
                    {
                        LogInformation("Creando reservación: {UserId}", createReservationDto?.UserId);

                        if (string.IsNullOrWhiteSpace(createReservationDto.CreatedBy))
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'CreatedBy' no puede estar vacío." };

                        if (createReservationDto.CreatedBy.Length > 100)
                            return new OperationResult
                            {
                                IsSuccess = false,
                                Message = "El campo 'CreatedBy' no puede tener más de 100 caracteres."
                            };

                        if (createReservationDto.UserId <= 0)
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'UserId' debe ser mayor a 0." };

                        if (createReservationDto.CheckInDate == DateTime.MinValue)
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'CheckInDate' no puede estar vacío." };

                        if (createReservationDto.CheckOutDate == DateTime.MinValue)
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'CheckOutDate' no puede estar vacío." };

                        if (createReservationDto.CreatedAt == DateTime.MinValue)
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'CreatedAt' no puede estar vacío." };

                        if (string.IsNullOrWhiteSpace(createReservationDto.Status))
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'Status' no puede estar vacío." };

                        if (createReservationDto.Status.Length > 20)
                            return new OperationResult
                            {
                                IsSuccess = false, Message = "El campo 'Status' no puede tener más de 20 caracteres."
                            };

                        if (createReservationDto.TotalAmount <= 0)
                            return new OperationResult
                                { IsSuccess = false, Message = "El campo 'TotalAmount' debe ser mayor a 0." };

                        using var connection = new SqlConnection(_connectionString);
                        using var command = new SqlCommand("dbo.CreateReservation", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CheckInDate", createReservationDto.CheckInDate);
                        command.Parameters.AddWithValue("@CheckOutDate", createReservationDto.CheckOutDate);
                        command.Parameters.AddWithValue("@Status", createReservationDto.Status);
                        command.Parameters.AddWithValue("@TotalAmount", createReservationDto.TotalAmount);
                        command.Parameters.AddWithValue("@UserId", createReservationDto.UserId);
                        command.Parameters.AddWithValue("@CreatedBy", createReservationDto.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedAt", createReservationDto.CreatedAt);

                        var pResult = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(pResult);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        var mensajeSp = pResult.Value?.ToString();
                        resultOperation.Message = mensajeSp;

                        if (mensajeSp == "Reserva creada exitosamente.")
                        {
                            resultOperation.IsSuccess = true;
                            LogInformation(mensajeSp);
                        }
                        else
                        {
                            resultOperation.IsSuccess = false;
                            if (mensajeSp != null) LogError(new Exception("SP Execution Error"), mensajeSp);
                        }
                    }
                }
            }
            catch (Exception? ex)
            {
                LogError(ex, "Error al crear la reserva.");
                resultOperation.IsSuccess = false;
                resultOperation.Message = "Ocurrió un error: " + ex.Message;
            }

            return resultOperation;
        }

        [Obsolete("Obsolete")]
        public async Task<OperationResult> UpdateAsync(UpDateReservationDto upDateReservationDto)
        {
            var resultOperation = new OperationResult();

            try
            {
                if (upDateReservationDto?.ReservationId != null)
                {
                    LogInformation("Actualizando reservación ID: {ReservationId}", upDateReservationDto?.ReservationId);

                    if (upDateReservationDto.ReservationId <= 0)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'ReservationId' debe ser mayor a 0." };

                    if (upDateReservationDto.UserId <= 0)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'UserId' debe ser mayor a 0." };

                    if (upDateReservationDto.CheckInDate == DateTime.MinValue)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'CheckInDate' no puede estar vacío." };

                    if (upDateReservationDto.CheckOutDate == DateTime.MinValue)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'CheckOutDate' no puede estar vacío." };

                    if (string.IsNullOrWhiteSpace(upDateReservationDto.Status))
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'Status' no puede estar vacío." };

                    if (upDateReservationDto.Status.Length > 20)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'Status' no puede tener más de 20 caracteres." };

                    if (upDateReservationDto.TotalAmount <= 0)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'TotalAmount' debe ser mayor a 0." };

                    if (upDateReservationDto.UpdateAT == DateTime.MinValue)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'UpdateAT' no puede estar vacío." };

                    using var connection = new SqlConnection(_connectionString);
                    using var command = new SqlCommand("dbo.UpdateReservation", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@ReservationId", upDateReservationDto.ReservationId);
                    command.Parameters.AddWithValue("@CheckInDate", upDateReservationDto.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", upDateReservationDto.CheckOutDate);
                    command.Parameters.AddWithValue("@Status", upDateReservationDto.Status);
                    command.Parameters.AddWithValue("@TotalAmount", upDateReservationDto.TotalAmount);
                    command.Parameters.AddWithValue("@UserId", upDateReservationDto.UserId);
                    command.Parameters.AddWithValue("@UpdateAT", upDateReservationDto.UpdateAT);

                    var p_result = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(p_result);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    var mensajeSp = p_result.Value?.ToString();
                    resultOperation.Message = mensajeSp;

                    if (mensajeSp == "Reserva actualizada correctamente.")
                    {
                        resultOperation.IsSuccess = true;
                        LogInformation("Reserva ID {ReservationId} actualizada correctamente.",
                            upDateReservationDto.ReservationId);
                    }
                    else
                    {
                        resultOperation.IsSuccess = false;
                        LogError(new Exception("SP Execution Error"), mensajeSp);
                    }
                }
            }
            catch (Exception? ex)
            {
                LogError(ex, "Error al actualizar la reserva.");
                resultOperation.IsSuccess = false;
                resultOperation.Message = "Ocurrió un error: " + ex.Message;
            }

            return resultOperation;
        }

        [Obsolete("Obsolete")]
        public async Task<OperationResult> DisableAsync(DisableReservationDto disableReservationDt)
        {
            if (disableReservationDt == null) throw new ArgumentNullException(nameof(disableReservationDt));
            var resultOperation = new OperationResult();

            try
            {
                if (disableReservationDt?.ReservationId != null)
                {
                    LogInformation("Desactivando reserva ID: {ReservationId}", disableReservationDt?.ReservationId);

                    if (disableReservationDt.ReservationId <= 0)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'ReservationId' debe ser mayor a 0." };

                    if (disableReservationDt.UpdateAT == DateTime.MinValue)
                        return new OperationResult
                            { IsSuccess = false, Message = "El campo 'UpdateAT' no puede estar vacío." };

                    using var connection = new SqlConnection(_connectionString);
                    using var command = new SqlCommand($"dbo.DisableReservation", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ReservationId", disableReservationDt.ReservationId);
                    command.Parameters.AddWithValue("@UpdateAT", disableReservationDt.UpdateAT);

                    var pResult = new SqlParameter("@presult", SqlDbType.VarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(pResult);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    var mensajeSp = pResult.Value?.ToString();
                    resultOperation.Message = mensajeSp;

                    if (mensajeSp == "Reserva desactivada correctamente.")
                    {
                        resultOperation.IsSuccess = true;
                        LogInformation(mensajeSp);
                    }
                    else
                    {
                        resultOperation.IsSuccess = false;
                        if (mensajeSp != null) LogError(new Exception("SP Execution Error"), mensajeSp);
                    }
                }
            }
            catch (Exception? ex)
            {
                LogError(ex, "Error al desactivar la reserva.");
                resultOperation.IsSuccess = false;
                resultOperation.Message = "Ocurrió un error: " + ex.Message;
            }

            return resultOperation;
        }

        [Obsolete("Obsolete")]
        public async Task<OperationResult> GetAllAsync()
        {
            var presult = new OperationResult();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand($"dbo.{nameof(GetActiveReservationDto)}", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                var reservations = new List<GetActiveReservationDto>();

                if (!reader.HasRows)
                {
                    presult.Message = "No se encontraron datos.";
                    presult.IsSuccess = false;
                    LogInformation("No se encontraron reservas activas.");
                    return presult;
                }

                while (await reader.ReadAsync())
                {
                    var reservation = new GetActiveReservationDto()
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
            catch (Exception? ex)
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
