using SPMUA.Model.Dictionaries.Appointment;

namespace SPMUA.Model.DTOs.Appointment
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string CustomerFirstName { get; set; } = String.Empty;
        public string CustomerLastName { get; set; } = String.Empty;
        public string? CustomerEmail { get; set; } = String.Empty;
        public string CustomerPhone { get; set; } = String.Empty;
        public DateTime AppointmentDate { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; } = String.Empty;
        public int AppointmentStatusId { get; set; } = (int)AppointmentStatusEnum.ConfirmationPending;
        public string AppointmentStatusName { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
