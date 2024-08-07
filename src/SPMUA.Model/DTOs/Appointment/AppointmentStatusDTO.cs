using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.Appointment
{
    public class AppointmentStatusDTO
    {
        public int AppointmentId { get; set; }
        public string AppointmentStatusName { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
    }
}
