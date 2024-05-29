using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPMUA.Model.Dictionaries.Email;
using SPMUA.Model.DTOs.Admin;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Model.DTOs.Email;
using SPMUA.Model.DTOs.EmailTemplate;
using SPMUA.Model.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using SPMUA.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SPMUA.Repository.Implementations
{
    public class EmailQueueRepository : IEmailQueueRepository
    {
        private readonly SpmuaDbContext _spmuaDbContext;

        public EmailQueueRepository(SpmuaDbContext spmuaDbContext)
        {
            _spmuaDbContext = spmuaDbContext;
        }

        public async Task<EmailTemplateDTO> GetEmailTemplate(int emailTemplateId)
        {
            EmailTemplateDTO result = new();

            try
            {
                result = await _spmuaDbContext.EmailTemplates.Where(et => et.EmailTemplateId == emailTemplateId)
                                                             .Select(et => new EmailTemplateDTO()
                                                             {
                                                                 EmailTemplateTitle = et.EmailTemplateTitle,
                                                                 EmailTemplateHtml = et.EmailTemplateHtml
                                                             })
                                                             .FirstAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<AppointmentRequestConfirmationPendingEmailParamDTO> GetAppointmentRequestConfirmationEmailData(int appointmentId)
        {
            AppointmentRequestConfirmationPendingEmailParamDTO result = new();

            try
            {
                result = await _spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId)
                                                           .Select(a => new AppointmentRequestConfirmationPendingEmailParamDTO()
                                                          {
                                                               AppointmentId = a.AppointmentId.ToString(),
                                                               CustomerFullName = a.CustomerFirstName + " " + a.CustomerLastName, 
                                                               ServiceTypeName = a.ServiceType.ServiceTypeName,
                                                               AppointmentDate = a.AppointmentDate.ToString("dd/MM/yyyy"),
                                                               AppointmentTimeInterval = Helper.CreateAppointmentTimeInterval(TimeOnly.FromDateTime(a.AppointmentDate),
                                                                                                                             a.ServiceType.ServiceTypeDuration,
                                                                                                                             false).ToString(),
                                                               ServiceTypePrice = a.ServiceType.ServiceTypePriceHistories
                                                                 .Where(stph => stph.CreatedDate < a.CreatedDate)
                                                                 .OrderByDescending(stph => stph.CreatedDate)
                                                                 .First().ServiceTypePrice.ToString()
                                                           })
                                                           .FirstAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<AppointmentRequestArrivedEmailParamDTO> GetAppointmentRequestArrivedEmailData(int appointmentId)
        {
            AppointmentRequestArrivedEmailParamDTO result = new();

            try
            {
                result = await _spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId)
                                                           .Select(a => new AppointmentRequestArrivedEmailParamDTO()
                                                           {
                                                               AppointmentId = a.AppointmentId.ToString(),
                                                               CustomerFullName = a.CustomerFirstName + " " + a.CustomerLastName,
                                                               CustomerEmail = a.CustomerEmail ?? String.Empty,
                                                               CustomerPhone = a.CustomerPhone,
                                                               ServiceTypeName = a.ServiceType.ServiceTypeName,
                                                               AppointmentDate = a.AppointmentDate.ToString("dd/MM/yyyy"),
                                                               AppointmentTimeInterval = Helper.CreateAppointmentTimeInterval(TimeOnly.FromDateTime(a.AppointmentDate),
                                                                                                                              a.ServiceType.ServiceTypeDuration,
                                                                                                                              false).ToString(),
                                                               ServiceTypePrice = a.ServiceType.ServiceTypePriceHistories
                                                                 .Where(stph => stph.CreatedDate < a.CreatedDate)
                                                                 .OrderByDescending(stph => stph.CreatedDate)
                                                                 .First().ServiceTypePrice.ToString()
                                                           })
                                                           .FirstAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<AppointmentResponseEmailParamDTO> GetAppointmentResponseEmailData(int appointmentId)
        {
            AppointmentResponseEmailParamDTO result = new();

            try
            {                
                result = await _spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId)
                                                           .Select(a => new AppointmentResponseEmailParamDTO {
                                                               AppointmentId = a.AppointmentId.ToString(),
                                                               CustomerFullName = a.CustomerFirstName + " " + a.CustomerLastName,
                                                               ServiceTypeName = a.ServiceType.ServiceTypeName,
                                                               AppointmentDate = a.AppointmentDate.ToString("dd/MM/yyyy"),
                                                               AppointmentTimeInterval = Helper.CreateAppointmentTimeInterval(TimeOnly.FromDateTime(a.AppointmentDate),
                                                                                                                              a.ServiceType.ServiceTypeDuration,
                                                                                                                              false).ToString(),
                                                               ServiceTypePrice = a.ServiceType.ServiceTypePriceHistories
                                                                 .Where(stph => stph.CreatedDate < a.CreatedDate)
                                                                 .OrderByDescending(stph => stph.CreatedDate)
                                                                 .First().ServiceTypePrice.ToString(),
                                                               ResponseComment = a.ResponseComment ?? String.Empty
                                                           })
                                                           .FirstAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<int> CreateEmailQueueItemAsync(EmailQueueItemDTO emailQueueItem)
        {
            int result = 0;

            try
            {
                EmailQueue newEmailQueueItem = new()
                {
                    ToEmail = emailQueueItem.ToEmail,
                    EmailSubject = emailQueueItem.EmailSubject,
                    EmailBody = emailQueueItem.EmailBody,
                    EmailQueueStatusId = (int)EmailQueueStatusEnum.Ready,
                    NoOfAttempts = 0
                };

                _spmuaDbContext.Add(newEmailQueueItem);
                await _spmuaDbContext.SaveChangesAsync();

                result = newEmailQueueItem.EmailQueueId;
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
