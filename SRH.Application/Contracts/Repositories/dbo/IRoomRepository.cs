using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.dbo
{
    public interface IRoomRepository
    {
        Task<OperationResult<Room>> CreateRoom(CreateRoomDto entity);

        Task<OperationResult<Room>> UpdateRoom(UpdateRoomDto entity);

        Task<OperationResult<Room>> DisableRoom(DisableRoomDto entity);

        Task<IEnumerable<Room>> GetAllRoom(Expression<Func<Room, bool>>? predicate = null);

        Task<OperationResult<Room>> GetRoomById(int id);

        Task<bool> ExistAsync(Expression<Func<Room, bool>>? predicate);
    }
}