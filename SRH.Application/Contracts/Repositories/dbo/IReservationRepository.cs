using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SGRH._Domain.Entities; 
using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;

namespace SGM.Application.Contracts.Repositories
{
    public interface IReservationRepository 
    {
             
       
               Task<OperationResult> AddAsync(CreateReservationDTO CreateReservationDTO);
        

               Task<OperationResult> UpdateAsync(UpDateReservationDTO UpDateReservationDTO);
       
               Task<OperationResult> DisableAsync(DisableReservationDTO DisableReservationDTO);
       
               Task<OperationResult> GetAllAsync();
               Task<OperationResult> GetByIdAsync(int id);
               
    }

   
}