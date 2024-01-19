using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.Admin
{
    public class AdminLoginResponseDTO
    {
        public string AdminFirstName { get; set; } = String.Empty;
        public string AdminLastName { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
    }
}
