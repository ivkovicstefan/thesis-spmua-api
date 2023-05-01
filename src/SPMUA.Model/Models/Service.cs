using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Model.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        [MaxLength(50)]
        public string ServiceName { get; set; } = null!;
        public decimal ServicePrice { get; set; }
        public int ServiceDuration { get; set; }
        public bool IsAvailableOnMonday { get; set; }
        public bool IsAvailableOnTuesday { get; set; }
        public bool IsAvailableOnWednesday { get; set; }
        public bool IsAvailableOnThursday { get; set; }
        public bool IsAvailableOnFriday { get; set; }
        public bool IsAvailableOnSaturday { get; set; }
        public bool IsAvailableOnSunday { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<Appointment> Appointment { get; } = new List<Appointment>();
    }
}
