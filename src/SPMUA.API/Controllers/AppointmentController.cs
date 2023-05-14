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

        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            return new OkObjectResult("pong");
        }

        [HttpGet("appointments")]
        [ProducesResponseType(typeof(List<AppointmentDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointmentsAsync()
        {
            return new OkObjectResult(await _appointmentService.GetAllAppointmentsAsync());
        }

        [HttpGet("appointment/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentByIdAsync(int appointmentId)
        {
            return new OkObjectResult(await _appointmentService.GetAppointmentByIdAsync(appointmentId));
        }

        [HttpPost("appointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            int result = await _appointmentService.CreateAppointmentAsync(appointmentDTO);

            return new CreatedAtActionResult(nameof(GetAppointmentByIdAsync),
                                             nameof(AppointmentController).Replace("Controller", ""),
                                             new { appointmentId = result },
                                             null);
        }

        [HttpGet("appointment/unavailable-dates/{serviceTypeId}")]
        [ProducesResponseType(typeof(List<DateOnly>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnavailableDatesAsync([FromQuery] DateTime fromDate,
                                                                  [FromQuery] DateTime toDate, 
                                                                  int serviceTypeId)
        {
            return new OkObjectResult(await _appointmentService.GetUnavailableDatesForAsync(fromDate, 
                                                                                            toDate, 
                                                                                            serviceTypeId));
        }

        [HttpGet("appointment/available-hours/serviceTypeId")]
        [ProducesResponseType(typeof(TimeOnly), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableHoursAsync([FromQuery] DateTime date, int serviceTypeId)
        {
            return new OkObjectResult(await _appointmentService.GetAvailableHoursForAsync(date, serviceTypeId));
        }
    }
}
