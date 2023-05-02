using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.DTOs.WorkingDay;
using SPMUA.Service.Contracts;
using System.Net;

namespace SPMUA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDayController : ControllerBase
    {
        private readonly IWorkingDayService _workingDayService;

        public WorkingDayController(IWorkingDayService workingDayService)
        {
            _workingDayService = workingDayService;
        }

        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            return new OkObjectResult("Pong");
        }

        [HttpGet("working-days")]
        [ProducesResponseType(typeof(List<WorkingDayDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkingDays() 
        {
            return new OkObjectResult(await _workingDayService.GetWorkingDaysAsync());
        }

        [HttpPut("working-days")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWorkingDays(List<WorkingDayDTO> workingDayDTOs)
        {
            await _workingDayService.UpdateWorkingDaysAsync(workingDayDTOs);

            return new OkObjectResult("Working days updated successfully");
        }
    }
}
