using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Queues
{
    public class EmailQueue
    {
        private readonly Queue<EmailQueueItem> _emailQueue;

        public EmailQueue()
        {
            _emailQueue = new Queue<EmailQueueItem>();
        }

        public void Enqueue(EmailQueueItem emailQueueItem)
        {
            _emailQueue.Enqueue(emailQueueItem);
        }

        public EmailQueueItem Dequeue() 
        { 
            return _emailQueue.Dequeue(); 
        }

        public bool IsEmpty()
        {
            return _emailQueue.Count == 0;
        }
    }
}
