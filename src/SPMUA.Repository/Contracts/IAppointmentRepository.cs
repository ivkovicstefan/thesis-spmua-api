using SPMUA.Model.DTOs.Appointment;

namespace SPMUA.Repository.Contracts
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentDTO>> GetAllAppointmentsAsync();

        Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId);

        Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO);

        Task<List<ValueTuple<TimeOnly, TimeOnly>>> GetBookedAppointmentIntervalsForAsync(DateTime date);

        Task<List<DateOnly>> GetDatesWithAppointmentsAsync(DateTime fromDate, DateTime toDate);
    }
}
