//using Microsoft.AspNetCore.Mvc;
//using SGRH.Application.Contracts.Repositories.Services;
//using SGRH.Application.DTO.dbo;
//using SGRH.Application.Services;
//using SRH.Application.Contracts.Repositories.Services;
//using SRH.Application.DTO.dbo;

//namespace Api.Controller
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ReservationController : ControllerBase
//    {
//        private readonly IReservationService _reservationService;

//        public ReservationController(IReservationService reservationService)
//        {
//            _reservationService = reservationService;
//        }
//        [HttpGet("GetAllReservations")]
//        public async Task<IActionResult> GetReservation()
//        {
//            var result = await _reservationService.GetReservation();

//            if (result.IsSuccess) 
//            { 
//            return Ok(result);
//        }
//            return BadRequest(result)
//        }




//        [HttpGet("GetReservationBy/{id}")]
//        public async Task<IActionResult> GetReservationById(int id)
//        {
//            var dto = new GetActiveReservationByIdDto { Id = id };

//            var result = await _reservationService.GetReservationById(id, dto);

//            if (!result.IsSuccess)
//                return NotFound(result);

//            return Ok(result);
//        }

//        [HttpPost("CreateReservation")]
//        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(new
//                {
//                    IsSuccess = false,
//                    Message = "Solicitud inválida.",
//                    Errors = ModelState
//                });

//            var result = await _reservationService.CreateReservation(dto);

//            if (!result.IsSuccess || result.Data == null)
//                return BadRequest(result);

//            return Ok(result);
//        }

//        [HttpPost("UpdateReservation")]
//        public async Task<IActionResult> UpdateReservation([FromBody] UpDateReservationDto dto)
//        {
//            if (!ModelState.IsValid || dto == null)
//                return BadRequest(new
//                {
//                    IsSuccess = false,
//                    Message = "Datos de actualización inválidos.",
//                    Errors = ModelState
//                });

//            var result = await _reservationService.UpdateReservation(dto);

//            if (!result.IsSuccess)
//                return BadRequest(result);

//            return Ok(result);
//        }

//        [HttpPost("DisableReservation")]
//        public async Task<IActionResult> DisableReservation([FromBody] DisableReservationDto dto)
//        {
//            if (!ModelState.IsValid || dto == null)
//                return BadRequest(new
//                {
//                    IsSuccess = false,
//                    Message = "Datos inválidos.",
//                    Errors = ModelState
//                });

//            var result = await _reservationService.DisableReservation(dto);

//            if (!result.IsSuccess)
//                return BadRequest(result);

//            return Ok(result);
//        }
//    }
//}
