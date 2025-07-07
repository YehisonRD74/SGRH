using Microsoft.AspNetCore.Mvc;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("GetAllReservation")]
        public async Task<IActionResult> GetReservation()
        {
            var result = await _reservationService.GetReservation();
            return Ok(result);
        }

        [HttpGet("GetReservationBy{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var dto = new GetActiveReservationByIdDto(); 
            var result = await _reservationService.GetByIdReservation(id, dto);
            if (!result.IsSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reservationService.CreateReservation(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation([FromBody] UpDateReservationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reservationService.UpDateReservation(dto); 
            return Ok(result);
        }

        [HttpPost("disableReservation")]
        public async Task<IActionResult> DisableReservation([FromBody] DisableReservationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reservationService.DisableReservation(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        
    }
}