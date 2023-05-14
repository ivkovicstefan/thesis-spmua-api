
using SPMUA.Model.DTOs.Appointment;

namespace SPMUA.Service.Contracts
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDTO>> GetAllAppointmentsAsync();

        Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId);

        Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO);

        Task<List<DateOnly>> GetUnavailableDatesForAsync(DateTime fromDate, DateTime toDate, int serviceTypeId);

        Task<List<TimeOnly>> GetAvailableHoursForAsync(DateTime date,  int serviceTypeId);
    }
}
