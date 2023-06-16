using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Model.DTOs.Vacation;
using SPMUA.Service.Contracts;
using System.Diagnostics;

namespace SPMUA.API.Controllers
{
    [Route("api/vacation")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;

        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            return new OkObjectResult("pong");
        }

        [HttpGet("vacations")]
        [ProducesResponseType(typeof(List<VacationDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllVacationsAsync()
        {
            return new OkObjectResult(await _vacationService.GetAllVacationsAsync());
        }

        [HttpGet("vacations/{vacationId}")]
        [ProducesResponseType(typeof(VacationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVacationByIdAsync([FromRoute] int vacationId)
        {
            return new OkObjectResult(await _vacationService.GetVacationByIdAsync(vacationId));
        }

        [HttpPost("vacation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVacationAsync([FromBody] VacationDTO vacationDTO)
        {
            int result = await _vacationService.CreateVacationAsync(vacationDTO);

            return new CreatedAtActionResult(nameof(GetVacationByIdAsync),
                                             nameof(VacationController).Replace("Controller", ""),
                                             new { vacationId = result },
                                             null);
        }

        [HttpPut("vacation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVacationAsync([FromBody] VacationDTO vacationDTO)
        {
            await _vacationService.UpdateVacationAsync(vacationDTO);

            return new NoContentResult();
        }

        [HttpDelete("vacation/{vacationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVacationAsync([FromRoute] int vacationId)
        {
            await _vacationService.DeleteVacationAsync(vacationId); 
            
            return new NoContentResult();
        }
    }
}
