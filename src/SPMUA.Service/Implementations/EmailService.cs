using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SPMUA.Model.Dictionaries.EmailTemplate;
using SPMUA.Model.DTOs.Email;
using SPMUA.Model.DTOs.EmailTemplate;
using SPMUA.Model.Queues;
using SPMUA.Repository.Contracts;
using SPMUA.Utility.Helpers;
using System.Net;
using System.Net.Mail;


namespace SPMUA.Service.Implementations
{
    public class EmailService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailRepository _emailRepository;
        private readonly EmailQueue _emailQueue;

		private readonly string _fromEmail;
        private readonly string _password;
        private readonly string _toAdminEmail;
		private readonly string _smtpClientHost;
		private readonly int _smtpClientPort;
		private readonly int _delayDuration;

        public EmailService(IConfiguration configuration, 
							IEmailRepository emailRepository,
							EmailQueue emailQueue)
        {
            _configuration = configuration;
            _emailRepository = emailRepository;
            _emailQueue = emailQueue;

            _fromEmail = _configuration["EmailConfig:FromEmail"] ?? String.Empty;
            _password = _configuration["EmailConfig:Password"] ?? String.Empty;
			_toAdminEmail = _configuration["EmailConfig:ToAdminEmail"] ?? String.Empty;
            _smtpClientHost = _configuration["EmailConfig:SmtpClientHost"] ?? String.Empty;
            _smtpClientPort = Convert.ToInt32(_configuration["EmailConfig:SmtpClientPort"]);
			_delayDuration = Convert.ToInt32(_configuration["EmailConfig:DelayDuration"]);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) { 

				if (!_emailQueue.IsEmpty())
				{
					EmailQueueItem emailQueueItem = _emailQueue.Dequeue();
					EmailTemplateDTO emailTemplate = await _emailRepository.GetEmailTemplate(emailQueueItem.EmailTemplateId);
					EmailDTO emailData = new();

                    switch (emailQueueItem.EmailTemplateId)
					{
						case (int)EmailTemplateEnum.AppointmentRequestConfirmationPending:
                            {
                                AppointmentRequestConfirmationPendingEmailParamDTO emailTemplateParamData
									= await _emailRepository.GetAppointmentRequestConfirmationEmailData(emailQueueItem.EntityId);

                                emailData.ToEmail = emailQueueItem.ToClientEmail;
                                emailData.Subject = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateTitle, emailTemplateParamData);
                                emailData.Body = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateHtml, emailTemplateParamData);  
                            }

                            break;
                        case (int)EmailTemplateEnum.AppointmentRequestArrived:
							{
                                AppointmentRequestArrivedEmailParamDTO emailTemplateParamData
									= await _emailRepository.GetAppointmentRequestArrivedEmailData(emailQueueItem.EntityId);

                                emailData.ToEmail = _toAdminEmail;
                                emailData.Subject = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateTitle, emailTemplateParamData);
								emailData.Body = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateHtml, emailTemplateParamData);
                            }
							
							break;
						case (int)EmailTemplateEnum.AppointmentRequestConfirmed:
						case (int)EmailTemplateEnum.AppointmentRequestRejected:
							{
                                AppointmentResponseEmailParamDTO emailTemplateParamData
									= await _emailRepository.GetAppointmentResponseEmailData(emailQueueItem.EntityId);

                                emailData.ToEmail = emailQueueItem.ToClientEmail;
                                emailData.Subject = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateTitle, emailTemplateParamData);
                                emailData.Body = Helper.ReplaceTemplateWithParamData(emailTemplate.EmailTemplateHtml, emailTemplateParamData);
                            }
                            break;
						default:
							break;
					}

					await SendEmailAsync(emailData);
				}

				await Task.Delay(_delayDuration, cancellationToken);
			}
        }

        private async Task SendEmailAsync(EmailDTO emailData)
		{
			try
			{
				using SmtpClient smtpClient = new SmtpClient(_smtpClientHost, _smtpClientPort)
				{
					UseDefaultCredentials = false,
					EnableSsl = true,
					Credentials = new NetworkCredential(_fromEmail, _password)	
				};

				MailMessage email = new MailMessage(from: _fromEmail,
													to: emailData.ToEmail,
													subject: emailData.Subject,
													body: emailData.Body)
				{
					IsBodyHtml = true
				};

				await smtpClient.SendMailAsync(email);
            }
			catch
			{
				throw;
			}
		}
    }
}
