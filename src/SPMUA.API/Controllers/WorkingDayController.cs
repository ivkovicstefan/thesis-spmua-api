using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.DTOs.WorkingDay;
using SPMUA.Service.Contracts;
using System.Net;

namespace SPMUA.API.Controllers
{
    [Route("api/working-day")]
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
            return new OkObjectResult("pong");
        }

        [HttpGet("working-days")]
        [ProducesResponseType(typeof(List<WorkingDayDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWorkingDaysAsync() 
        {
            return new OkObjectResult(await _workingDayService.GetAllWorkingDaysAsync());
        }

        [HttpPut("working-days")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWorkingDaysAsync([FromBody] List<WorkingDayDTO> workingDayDTOs)
        {
            await _workingDayService.UpdateWorkingDaysAsync(workingDayDTOs);

            return new NoContentResult();
        }
    }
}
