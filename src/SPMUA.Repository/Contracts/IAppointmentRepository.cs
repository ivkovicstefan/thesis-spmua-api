using SPMUA.Model.Commons.DataTypes;
using SPMUA.Model.DTOs.Appointment;

namespace SPMUA.Repository.Contracts
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentDTO>> GetAllAppointmentsAsync(AppointmentFiltersDTO appointmentFiltersDTO);

        Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId);

        Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO);

        Task<List<TimeInterval>> GetBookedAppointmentIntervalsForAsync(DateTime date);

        Task<List<DateOnly>> GetDatesWithAppointmentsAsync(DateTime fromDate, DateTime toDate);

        Task UpdateAppointmentStatusAsync(int appointmentId, int appointmentStatusId, string? responseComment);

        Task<string> GetAppointmentCustomerEmail(int appointmentId);
    }
}
