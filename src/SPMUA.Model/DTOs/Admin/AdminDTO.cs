using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.Admin
{
    public class AdminDTO
    {
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; } = String.Empty;
        public string AdminLastName { get; set; } = String.Empty;
        public string AdminEmail { get; set; } = String.Empty;
    }
}
