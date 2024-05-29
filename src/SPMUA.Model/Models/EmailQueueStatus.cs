using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Models
{
    public class EmailQueueStatus
    {
        public int EmailQueueStatusId { get; set; }
        public string EmailQueueStatusName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<EmailQueue> EmailQueue { get; } = new List<EmailQueue>();
    }
}
