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
        public async Task<IActionResult> GetAllAppointments()
        {
            return new OkObjectResult(await _appointmentService.GetAllAppointmentsAsync());
        }

        [HttpGet("appointment/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            return new OkObjectResult(await _appointmentService.GetAppointmentByIdAsync(appointmentId));
        }

        [HttpPost("appointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAppointment(AppointmentDTO appointmentDTO)
        {
            int result = await _appointmentService.CreateAppointmentAsync(appointmentDTO);

            return new CreatedAtActionResult(nameof(GetAppointmentById),
                                             nameof(AppointmentController).Replace("Controller", ""),
                                             new { appointmentId = result },
                                             null);
        }

        [HttpGet("appointment/unavailable-dates/{serviceTypeId}")]
        [ProducesResponseType(typeof(List<DateOnly>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnavailableDates(DateTime fromDate, DateTime toDate, int serviceTypeId)
        {
            return new OkObjectResult(await _appointmentService.GetUnavailableDatesForAsync(fromDate, 
                                                                                            toDate, 
                                                                                            serviceTypeId));
        }
    }
}
