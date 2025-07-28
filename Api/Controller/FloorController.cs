
using Microsoft.AspNetCore.Mvc;
using SGRH.Application.DTO.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace Api.Controller  
{
    [ApiController]
    [Route("api/[controller]")]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet("GetAllFloor")]
        public async Task<IActionResult> GetFloor()
        {
            var result = await _floorService.GetFloor();

            if (result.IsSuccess)
         
           {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("GetFloorById/{id}")]
        public async Task<IActionResult> GetFloorById(int id)
        {
            var dto = new GetFloorByIdDto { Id = id };
         
            var result = await _floorService.GetFloorById(id, dto);
        
            if (!result.IsSuccess)
                return NotFound(result);
        
            return Ok(result);
        }
        
        [HttpPost("CreateFloor")]
        public async Task<IActionResult> CreateFloor([FromBody] CreateFloorDto? dto)
        {
            //if (!ModelState.IsValid)
              // return BadRequest(ModelState);
        
            var result = await _floorService.CreateFloor(dto);
            return Ok(result);
        }
        
        [HttpPost("updateFloor")]
        public async Task<IActionResult> UpdateFloor([FromBody] UpdateFloorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var result = await _floorService.UpDateFloor(dto); 
            return Ok(result);
        }
        
        [HttpPost("disableFloor")]
        public async Task<IActionResult> DisableFloor([FromBody] DisableFloorDto? dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var result = await _floorService.DisableFloor(dto);
            return Ok(result);
        }
    }
}
