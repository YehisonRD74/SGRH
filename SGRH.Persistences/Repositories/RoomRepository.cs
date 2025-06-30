using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories;
using SGRH._Domain.Base;
using SGRH.Application.DTO.dbo;
using SGRH.Application.Interfaces.Repositories;

namespace SGM.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(string connectionString, ILogger<RoomRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateRoomDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure("El objeto CreateRoomDTO no puede ser nulo.");

            try
            {
                _logger.LogInformation("Creando habitación");

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.AddRoom", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@NumeroHabitacion", dto.NumeroHabitacion);
                command.Parameters.AddWithValue("@FloorId", dto.FloorId);
                command.Parameters.AddWithValue("@Precio", dto.Price);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    _logger.LogInformation("Habitación creada exitosamente.");
                    return OperationResult.Success("Habitación creada exitosamente.");
                }
                else
                {
                    _logger.LogWarning("No se pudo crear la habitación.");
                    return OperationResult.Failure("No se pudo crear la habitación.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al crear habitación");
                return OperationResult.Failure("Error al crear habitación: " + e.Message);
            }
        }

        public async Task<OperationResult> UpdateAsync(UpdateRoomDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure("El objeto UpdateRoomDTO no puede ser nulo.");

            try
            {
                _logger.LogInformation("Actualizando habitación con ID: {RoomId}", dto.Id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.UpdateRoom", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@RoomId", dto.Id);
                command.Parameters.AddWithValue("@NumeroHabitacion", dto.NumeroHabitacion);
                command.Parameters.AddWithValue("@Tipo", dto.Type);
                command.Parameters.AddWithValue("@FloorId", dto.FloorId);
                command.Parameters.AddWithValue("@Precio", dto.Price);
                command.Parameters.AddWithValue("@Descripcion", dto.Descripcion);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    _logger.LogInformation("Habitación actualizada exitosamente.");
                    return OperationResult.Success("Habitación actualizada exitosamente.");
                }
                else
                {
                    _logger.LogWarning("No se pudo actualizar la habitación.");
                    return OperationResult.Failure("No se pudo actualizar la habitación.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al actualizar habitación con ID: {RoomId}", dto.Id);
                return OperationResult.Failure("Error al actualizar habitación: " + e.Message);
            }
        }

        public async Task<OperationResult> DisableAsync(DisableRoomDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure("El objeto DisableRoomDTO no puede ser nulo.");

            try
            {
                _logger.LogInformation("Desactivando habitación con ID: {RoomId}", dto.RoomId);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.DisableRoom", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@RoomId", dto.RoomId);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    _logger.LogInformation("Habitación desactivada exitosamente.");
                    return OperationResult.Success("Habitación desactivada exitosamente.");
                }
                else
                {
                    _logger.LogWarning("No se pudo desactivar la habitación.");
                    return OperationResult.Failure("No se pudo desactivar la habitación.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al desactivar habitación con ID: {RoomId}", dto.RoomId);
                return OperationResult.Failure("Error al desactivar habitación: " + e.Message);
            }
        }

        public async Task<OperationResult> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Recuperando todas las habitaciones");

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.GetAllRooms", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                var rooms = new List<GetActiveRoomDTO>();

                if (!reader.HasRows)
                {
                    _logger.LogWarning("No se encontraron habitaciones.");
                    return OperationResult.Failure("No se encontraron habitaciones.");
                }

                while (await reader.ReadAsync())
                {
                    var room = new GetActiveRoomDTO(
                        RoomId: reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                        Number: reader.IsDBNull(reader.GetOrdinal("NumeroHabitacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroHabitacion")),
                        Type: reader.IsDBNull(reader.GetOrdinal("Tipo")) ? string.Empty : reader.GetString(reader.GetOrdinal("Tipo")),
                        FloorId: reader.IsDBNull(reader.GetOrdinal("FloorId")) ? 0 : reader.GetInt32(reader.GetOrdinal("FloorId")),
                        Price: reader.IsDBNull(reader.GetOrdinal("Precio")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Precio")),
                        Description: reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? string.Empty : reader.GetString(reader.GetOrdinal("Descripcion"))
                    );

                    rooms.Add(room);
                }

                _logger.LogInformation("Habitaciones recuperadas exitosamente.");
                return OperationResult.Success(rooms, "Habitaciones recuperadas exitosamente.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al recuperar habitaciones");
                return OperationResult.Failure("Error al recuperar habitaciones: " + e.Message);
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando habitación con ID: {RoomId}", id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("dbo.GetRoomById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@RoomId", id);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    _logger.LogWarning("No se encontró la habitación con ID: {RoomId}", id);
                    return OperationResult.Failure("No se encontró la habitación.");
                }

                await reader.ReadAsync();

                var room = new GetActiveRoomDTO(
                    RoomId: reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                    Number: reader.IsDBNull(reader.GetOrdinal("NumeroHabitacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroHabitacion")),
                    Type: reader.IsDBNull(reader.GetOrdinal("Tipo")) ? string.Empty : reader.GetString(reader.GetOrdinal("Tipo")),
                    FloorId: reader.IsDBNull(reader.GetOrdinal("FloorId")) ? 0 : reader.GetInt32(reader.GetOrdinal("FloorId")),
                    Price: reader.IsDBNull(reader.GetOrdinal("Precio")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Precio")),
                    Description: reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? string.Empty : reader.GetString(reader.GetOrdinal("Descripcion"))
                );

                _logger.LogInformation("Habitación recuperada exitosamente.");
                return OperationResult.Success(room, "Habitación recuperada exitosamente.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al recuperar habitación con ID: {RoomId}", id);
                return OperationResult.Failure("Error al recuperar habitación: " + e.Message);
            }
        }
    }
}
