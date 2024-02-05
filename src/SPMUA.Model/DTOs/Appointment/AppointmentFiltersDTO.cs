using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.Appointment
{
    public class AppointmentFiltersDTO
    {
        public DateTime? AppointmentDate { get; set; }
        public int? ServiceTypeId { get; set; }
        public string CustomerFullName { get; set; } = String.Empty;
        public string CustomerPhone { get; set; } = String.Empty;
        public string CustomerEmail { get; set; } = String.Empty;

    }
}
