
using SPMUA.Model.DTOs.Appointment;

namespace SPMUA.Service.Contracts
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDTO>> GetAllAppointmentsAsync(AppointmentFiltersDTO appointmentFiltersDTO);

        Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId);

        Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO);

        Task<List<DateOnly>> GetUnavailableAppointmentDatesForAsync(DateTime fromDate, DateTime toDate, int serviceTypeId);

        Task<List<TimeOnly>> GetAvailableAppointmentHoursForAsync(DateTime date,  int serviceTypeId);

        Task<AppointmentStatusDTO> GetAppointmentmentStatusAsync(int appointmentId, string customerPhone);

        Task UpdateAppointmentStatusAsync(UpdateAppointmentStatusDTO updateAppointmentStatusDTO);
    }
}
