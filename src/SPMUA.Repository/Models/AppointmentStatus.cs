using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Repository.Models
{
    public class AppointmentStatus
    {
        public int AppointmentStatusId { get; set; }
        [MaxLength(20)]
        public string AppointmentStatusName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Appointment> Appointment { get; } = new List<Appointment>();
    }
}
