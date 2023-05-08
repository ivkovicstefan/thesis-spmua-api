using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.ServiceType
{
    public class ServiceTypeDTO
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; } = String.Empty;
        public decimal ServiceTypePrice { get; set; }
        public int ServiceTypeDuration { get; set; }
        public bool IsAvailableOnMonday { get; set; } = false;
        public bool IsAvailableOnTuesday { get; set; } = false;
        public bool IsAvailableOnWednesday { get; set; } = false;
        public bool IsAvailableOnThursday { get; set; } = false;
        public bool IsAvailableOnFriday { get; set; } = false;
        public bool IsAvailableOnSaturday { get; set; } = false;
        public bool IsAvailableOnSunday { get; set; } = false;
    }
}
