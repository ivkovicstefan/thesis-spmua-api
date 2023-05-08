using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Model.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        [MaxLength(35)]
        public string CustomerFirstName { get; set; } = null!;
        [MaxLength(35)]
        public string CustomerLastName { get; set; } = null!;
        [MaxLength(100)]
        public string CustomerEmail { get; set; } = null!;
        [MaxLength(15)]
        public string CustomerPhone { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public int ServiceId { get; set; }
        public int AppointmentStatusId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; } = null!;
        public virtual ServiceType Service { get; set; } = null!;
    }
}
