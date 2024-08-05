using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Repository.Models
{
    public class EmailQueue
    {
        public int EmailQueueId { get; set; }
        public string ToEmail { get; set; }
        public string EmailSubject { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string EmailBody { get; set; }
        public int EmailQueueStatusId { get; set; }
        public int NoOfAttempts { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public virtual EmailQueueStatus EmailQueueStatuses { get; set; } = null!;
    }
}
