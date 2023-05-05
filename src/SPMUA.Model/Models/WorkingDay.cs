using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Model.Models
{
    public class WorkingDay
    {
        public int WorkingDayId { get; set; }
        [MaxLength(9)]
        public string WorkingDayName { get; set; } = null!;
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
