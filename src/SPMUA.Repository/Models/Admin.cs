using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [MaxLength(35)]
        public string AdminFirstName { get; set; } = null!;
        [MaxLength(35)]
        public string AdminLastName { get; set; } = null!;
        [MaxLength(100)]
        public string AdminEmail { get; set; } = null!;
        [MaxLength(60)]
        public string PasswordHash { get; set; } = null!;
    }
}
