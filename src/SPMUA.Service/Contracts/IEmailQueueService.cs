using SPMUA.Model.Dictionaries.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Contracts
{
    public interface IEmailQueueService
    {
        Task<int> EnqueueEmailAsync(EmailTemplateEnum emailTemplateId, int entityId, string clientEmail = "");
    }
}
