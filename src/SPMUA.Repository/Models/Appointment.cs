using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Repository.Models 
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        [MaxLength(35)]
        public string CustomerFirstName { get; set; } = null!;
        [MaxLength(35)]
        public string CustomerLastName { get; set; } = null!;
        [MaxLength(100)]
        public string? CustomerEmail { get; set; }
        [MaxLength(10)]
        public string CustomerPhone { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public int ServiceTypeId { get; set; }
        public int AppointmentStatusId { get; set; }
        [MaxLength(200)]
        public string? ResponseComment { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; } = null!;
        public virtual ServiceType ServiceType { get; set; } = null!;
    }
}
