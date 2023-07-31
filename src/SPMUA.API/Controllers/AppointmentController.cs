using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Service.Contracts;

namespace SPMUA.API.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [AllowAnonymous]
        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            return new OkObjectResult("pong");
        }

        [Authorize]
        [HttpGet("appointments")]
        [ProducesResponseType(typeof(List<AppointmentDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointmentsAsync()
        {
            return new OkObjectResult(await _appointmentService.GetAllAppointmentsAsync());
        }

        [AllowAnonymous]
        [HttpGet("appointment/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentByIdAsync([FromRoute] int appointmentId)
        {
            return new OkObjectResult(await _appointmentService.GetAppointmentByIdAsync(appointmentId));
        }

        [AllowAnonymous]
        [HttpPost("appointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentDTO appointmentDTO)
        {
            int result = await _appointmentService.CreateAppointmentAsync(appointmentDTO);

            return new CreatedAtActionResult(nameof(GetAppointmentByIdAsync),
                                             nameof(AppointmentController).Replace("Controller", ""),
                                             new { appointmentId = result },
                                             null);
        }

        [AllowAnonymous]
        [HttpGet("unavailable-dates")]
        [ProducesResponseType(typeof(List<DateOnly>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnavailableAppointmentDatesAsync([FromQuery] DateTime fromDate,
                                                                             [FromQuery] DateTime toDate, 
                                                                             [FromQuery] int serviceTypeId)
        {
            return new OkObjectResult(await _appointmentService.GetUnavailableAppointmentDatesForAsync(fromDate, 
                                                                                                       toDate, 
                                                                                                       serviceTypeId));
        }

        [AllowAnonymous]
        [HttpGet("available-hours")]
        [ProducesResponseType(typeof(TimeOnly), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableAppointmentHoursAsync([FromQuery] DateTime date, 
                                                                           [FromQuery] int serviceTypeId)
        {
            return new OkObjectResult(await _appointmentService.GetAvailableAppointmentHoursForAsync(date, 
                                                                                                     serviceTypeId));
        }

        [AllowAnonymous]
        [HttpGet("appointment/{appointmentId}/status")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentStatusByIdAsync([FromRoute] int appointmentId)
        {
            return new OkObjectResult(await _appointmentService.GetAppointmentmentStatusByIdAsync(appointmentId));
        }

        [Authorize]
        [HttpPatch("appointment/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAppointmentStatusAsync(UpdateAppointmentStatusDTO updateAppointmentStatusDTO)
        {
            await _appointmentService.UpdateAppointmentStatusAsync(updateAppointmentStatusDTO);

            return new NoContentResult();
        }
    }
}
