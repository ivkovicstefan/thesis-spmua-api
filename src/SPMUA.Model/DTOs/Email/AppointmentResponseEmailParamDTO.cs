using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.Email
{
    public class AppointmentResponseEmailParamDTO
    {
        public string AppointmentId { get; set; } = String.Empty;
        public string AppointmentDate { get; set; } = String.Empty;
        public string CustomerFullName { get; set; } = String.Empty;
        public string ServiceTypeName { get; set; } = String.Empty;
        public string AppointmentTimeInterval { get; set; } = String.Empty;
        public string ServiceTypePrice { get; set; } = String.Empty;
        public string ResponseComment { get; set; } = String.Empty;

    }
}
