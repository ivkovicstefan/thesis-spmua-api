using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Model.DTOs.Email;
using SPMUA.Model.DTOs.EmailTemplate;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using SPMUA.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Implementations
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<EmailTemplateDTO> GetEmailTemplate(int emailTemplateId)
        {
            EmailTemplateDTO result = new();

            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                using SpmuaDbContext spmuaDbContext = scope.ServiceProvider.GetRequiredService<SpmuaDbContext>();

                result = await spmuaDbContext.EmailTemplates.Where(et => et.EmailTemplateId == emailTemplateId)
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
                using IServiceScope scope = _serviceProvider.CreateScope();
                using SpmuaDbContext spmuaDbContext = scope.ServiceProvider.GetRequiredService<SpmuaDbContext>();

                result = await spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId)
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
                using IServiceScope scope = _serviceProvider.CreateScope();
                using SpmuaDbContext spmuaDbContext = scope.ServiceProvider.GetRequiredService<SpmuaDbContext>();

                result = await spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId)
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
                using IServiceScope scope = _serviceProvider.CreateScope();
                using SpmuaDbContext spmuaDbContext = scope.ServiceProvider.GetRequiredService<SpmuaDbContext>();
                
                result = await spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId)
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
    }
}
