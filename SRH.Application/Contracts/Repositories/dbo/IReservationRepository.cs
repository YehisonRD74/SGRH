using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.dbo
{
    public interface IReservationRepository 
    {
             
       
               Task<OperationResult> AddAsync(CreateReservationDto createReservationDto);
        

               Task<OperationResult> UpdateAsync(UpDateReservationDto upDateReservationDto);
       
               Task<OperationResult> DisableAsync(DisableReservationDto disableReservationDto);
       
               Task<OperationResult> GetAllAsync();
               Task<OperationResult> GetByIdAsync(int id);
               
    }

   
}