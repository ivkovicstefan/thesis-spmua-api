using Microsoft.Extensions.Configuration;
using SPMUA.Model.Dictionaries.Email;
using SPMUA.Model.DTOs.Email;
using SPMUA.Model.DTOs.EmailTemplate;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;
using SPMUA.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Implementations
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly IEmailQueueRepository _emailRepository;

        private readonly string _toAdminEmail;

        public EmailQueueService(IEmailQueueRepository emailRepository, IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _toAdminEmail = configuration["EmailConfig:ToAdminEmail"] ?? String.Empty;
        }

        public async Task<int> EnqueueEmailAsync(EmailTemplateEnum emailTemplateId, int entityId, string clientEmail = "")
        {
            int result = 0;
            EmailTemplateDTO emailTemplate = await _emailRepository.GetEmailTemplate((int)emailTemplateId);
            EmailQueueItemDTO emailQueueItem = new();

            switch (emailTemplateId)
            {
                case EmailTemplateEnum.AppointmentRequestConfirmationPending: 
                {
                    AppointmentRequestConfirmationPendingEmailParamDTO emailTemplateParamData
                        = await _emailRepository.GetAppointmentRequestConfirmationEmailData(entityId);

                    emailQueueItem.ToEmail = clientEmail;
                    emailQueueItem.EmailSubject = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateTitle, emailTemplateParamData);
                    emailQueueItem.EmailBody = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateHtml, emailTemplateParamData);

                    break;
                }
                case EmailTemplateEnum.AppointmentRequestArrived:
                {
                    AppointmentRequestArrivedEmailParamDTO emailTemplateParamData
                        = await _emailRepository.GetAppointmentRequestArrivedEmailData(entityId);

                    emailQueueItem.ToEmail = _toAdminEmail;
                    emailQueueItem.EmailSubject = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateTitle, emailTemplateParamData);
                    emailQueueItem.EmailBody = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateHtml, emailTemplateParamData);

                    break;
                }
                case EmailTemplateEnum.AppointmentRequestConfirmed:
                case EmailTemplateEnum.AppointmentRequestRejected:
                {
                    AppointmentResponseEmailParamDTO emailTemplateParamData
                        = await _emailRepository.GetAppointmentResponseEmailData(entityId);

                    emailQueueItem.ToEmail = clientEmail;
                    emailQueueItem.EmailSubject = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateTitle, emailTemplateParamData);
                    emailQueueItem.EmailBody = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateHtml, emailTemplateParamData);

                    break;
                }
            }

            result = await _emailRepository.CreateEmailQueueItemAsync(emailQueueItem);

            return result;
        }
    }
}
