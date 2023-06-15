using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.Appointment
{
    public class UpdateAppointmentStatusDTO
    {
        public int AppointmentId { get; set; }
        public bool IsAppointmentConfirmed { get; set; }
        public string? ResponseComment { get; set; }
    }
}
