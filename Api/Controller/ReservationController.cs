using Microsoft.AspNetCore.Mvc;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace Api.Controllers
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
        [HttpGet("GetAllReservations")]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservation();

            if (result == null || !result.IsSuccess || result.Data == null)
                return NotFound(new
                {
                    result?.IsSuccess,
                    result?.Message,
                    Data = result?.Data
                });

            return Ok(new
            {
                result.IsSuccess,
                result.Message,
                Data = result.Data
            });
        }



        [HttpGet("GetReservationBy/{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var result = await _reservationService.GetReservationById(id);

            if (result == null || !result.IsSuccess || result.Data == null)
                return NotFound(new
                {
                    result?.IsSuccess,
                    result?.Message,
                    Data = result
                });

            return Ok(new
            {
                result.IsSuccess,
                result.Message,
                Data = result.Data
            });
        }

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            if (!ModelState.IsValid || dto == null)
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = "Solicitud inválida.",
                    Errors = ModelState
                });

            var result = await _reservationService.CreateReservation(dto);

            if (!result.IsSuccess || result.Data == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation([FromBody] UpDateReservationDto dto)
        {
            if (!ModelState.IsValid || dto == null)
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = "Datos de actualización inválidos.",
                    Errors = ModelState
                });

            var result = await _reservationService.UpdateReservation(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("DisableReservation")]
        public async Task<IActionResult> DisableReservation([FromBody] DisableReservationDto dto)
        {
            if (!ModelState.IsValid || dto == null)
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = "Datos inválidos.",
                    Errors = ModelState
                });

            var result = await _reservationService.DisableReservation(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
