using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Services
{
    public class RoomService : BaseService<GetActiveRoomDto>, IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository, ILogger<GetActiveRoomDto> logger)
            : base(logger)
        {
            _roomRepository = roomRepository;
        }

        public async Task<OperationResult<IEnumerable<GetActiveRoomDto>>> GetAllRoom()
        {
            try
            {
                var rooms = await _roomRepository.GetAllRoom();


                var mapped = rooms.Select(room => new GetActiveRoomDto(
                    room.Id,
                    room.NumeroHabitacion,
                    room.Type,
                    room.FloorId,
                    room.Price,
                    room.Status
                ));

                return new OperationResult<IEnumerable<GetActiveRoomDto>>
                {
                    IsSuccess = true,
                    Message = "Habitaciones obtenidas correctamente",
                    Data = mapped
                };
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al obtener habitaciones");
                return new OperationResult<IEnumerable<GetActiveRoomDto>>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<OperationResult<GetActiveRoomDto>> GetRoomById(int id)
        {
            try
            {
                var result = await _roomRepository.GetRoomById(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    return new OperationResult<GetActiveRoomDto>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    };
                }

                var dto = new GetActiveRoomDto(
                    result.Data.Id,
                    result.Data.NumeroHabitacion,
                    result.Data.Type,
                    result.Data.FloorId,
                    result.Data.Price,
                    result.Data.Status
                );

                return new OperationResult<GetActiveRoomDto>
                {
                    IsSuccess = true,
                    Message = "Habitaciones obtenitos exitosamente",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al obtener habitación por ID");
                return new OperationResult<GetActiveRoomDto>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<OperationResult<GetActiveRoomDto>> CreateRoom(CreateRoomDto createRoomDto)
        {
            try
            {
                var result = await _roomRepository.CreateRoom(createRoomDto);

                if (!result.IsSuccess || result.Data == null)
                {
                    return new OperationResult<GetActiveRoomDto>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    };
                }

                var dto = new GetActiveRoomDto(
                    result.Data.Id,
                    result.Data.NumeroHabitacion,
                    result.Data.Type,
                    result.Data.FloorId,
                    result.Data.Price,
                    result.Data.Status
                );

                return new OperationResult<GetActiveRoomDto>
                {
                    IsSuccess = true,
                    Message = "Habitación creada exitosamente",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en CreateRoom");
                return new OperationResult<GetActiveRoomDto>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<OperationResult<GetActiveRoomDto>> UpdateRoom(UpdateRoomDto updateRoomDto)
        {
            try
            {
                var result = await _roomRepository.UpdateRoom(updateRoomDto);

                if (!result.IsSuccess || result.Data == null)
                {
                    return new OperationResult<GetActiveRoomDto>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    };
                }


                var dto = new GetActiveRoomDto(
                    result.Data.Id,
                    result.Data.NumeroHabitacion,
                    result.Data.Type,
                    result.Data.FloorId,
                    result.Data.Price,
                    result.Data.Status
                );

                return new OperationResult<GetActiveRoomDto>
                {
                    IsSuccess = true,
                    Message = "Habitación actualizada exitosamente",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en UpdateRoom");
                return new OperationResult<GetActiveRoomDto>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<OperationResult<bool>> DisableRoom(DisableRoomDto disableRoomDto)
        {
            try
            {
                var result = await _roomRepository.DisableRoom(disableRoomDto);

                return new OperationResult<bool>
                {
                    IsSuccess = result.IsSuccess,
                    Message = "Habitacion desabilitada exitosamente",
                    Data = result.IsSuccess
                };
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en DisableRoom");
                return new OperationResult<bool>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}