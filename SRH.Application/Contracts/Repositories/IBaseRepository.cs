using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SGRH._Domain.Entities;
using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;

namespace SGM.Application.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> GetByIdAsync(int id);

        Task<OperationResult> AddAsync(TEntity entity);

        Task<OperationResult> UpdateAsync(TEntity entity);

        Task<OperationResult> DeleteAsync(TEntity entity);

        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<OperationResult> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}