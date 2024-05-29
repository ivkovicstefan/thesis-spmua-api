using SPMUA.Model.DTOs.Email;
using SPMUA.Model.DTOs.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Contracts
{
    public interface IEmailQueueRepository
    {
        Task<EmailTemplateDTO> GetEmailTemplate(int emailTemplateId);

        Task<AppointmentRequestConfirmationPendingEmailParamDTO> GetAppointmentRequestConfirmationEmailData(int appointmentId);

        Task<AppointmentRequestArrivedEmailParamDTO> GetAppointmentRequestArrivedEmailData(int appointmentId);

        Task<AppointmentResponseEmailParamDTO> GetAppointmentResponseEmailData(int appointmentId);

        Task<int> CreateEmailQueueItemAsync(EmailQueueItemDTO emailQueueItem);
    }
}
