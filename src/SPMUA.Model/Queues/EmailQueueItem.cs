using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Queues
{
    public record EmailQueueItem
    {
        public int EmailTemplateId { get; set; }
        public int EntityId { get; set; }
        public string ToClientEmail { get; set; } = String.Empty;
        public int NoOfAttempts { get; set; } = 0;
    }
}
