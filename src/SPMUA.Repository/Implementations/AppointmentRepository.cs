using Microsoft.EntityFrameworkCore;
using SPMUA.Model.Commons;
using SPMUA.Model.Dictionaries.Appointment;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Model.Exceptions;
using SPMUA.Model.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using SPMUA.Utility.Helpers;

namespace SPMUA.Repository.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SpmuaDbContext _spmuaDbContext;

        public AppointmentRepository(SpmuaDbContext spmuaDbContext)
        {
            _spmuaDbContext = spmuaDbContext;
        }

        public async Task<List<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            List<AppointmentDTO> result = new();

            try
            {
                result = await _spmuaDbContext.Appointments.Where(a => a.AppointmentDate >= DateTime.Now
                                                                    && a.IsActive
                                                                    && !a.IsDeleted)
                                                           .Select(a => new AppointmentDTO() 
                                                           { 
                                                               AppointmentId = a.AppointmentId,
                                                               CustomerFirstName = a.CustomerFirstName,
                                                               CustomerLastName = a.CustomerLastName,
                                                               CustomerEmail = a.CustomerEmail,
                                                               CustomerPhone = a.CustomerPhone,
                                                               AppointmentDate = a.AppointmentDate,
                                                               ServiceTypeId = a.ServiceTypeId,
                                                               ServiceTypeName = a.ServiceType.ServiceTypeName,
                                                               AppointmentStatusId = a.AppointmentStatusId,
                                                               AppointmentStatusName = a.AppointmentStatus.AppointmentStatusName,
                                                               CreatedDate = a.CreatedDate
                                                           })
                                                           .ToListAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId)
        {
            AppointmentDTO? result = null;

            try
            {
                result = await _spmuaDbContext.Appointments.Where(a => a.AppointmentId == appointmentId
                                                                    && a.IsActive
                                                                    && !a.IsDeleted)
                                                           .Select(a => new AppointmentDTO()
                                                           {
                                                               AppointmentId = a.AppointmentId,
                                                               CustomerFirstName = a.CustomerFirstName,
                                                               CustomerLastName = a.CustomerLastName,
                                                               CustomerEmail = a.CustomerEmail,
                                                               CustomerPhone = a.CustomerPhone,
                                                               AppointmentDate = a.AppointmentDate,
                                                               ServiceTypeId = a.ServiceTypeId,
                                                               ServiceTypeName = a.ServiceType.ServiceTypeName,
                                                               AppointmentStatusId = a.AppointmentStatusId,
                                                               AppointmentStatusName = a.AppointmentStatus.AppointmentStatusName,
                                                               CreatedDate = a.CreatedDate
                                                           })
                                                           .FirstOrDefaultAsync();

                if (result is null)
                {
                    throw new EntityNotFoundException();
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            int result = 0;

            try
            {
                Appointment newAppointment = new Appointment()
                {
                    CustomerFirstName = appointmentDTO.CustomerFirstName,
                    CustomerLastName = appointmentDTO.CustomerLastName,
                    CustomerEmail = appointmentDTO.CustomerEmail,
                    CustomerPhone = appointmentDTO.CustomerPhone,
                    AppointmentDate = appointmentDTO.AppointmentDate,
                    ServiceTypeId = appointmentDTO.ServiceTypeId,
                    AppointmentStatusId = (int)AppointmentStatusEnum.ConfirmationPending
                };

                _spmuaDbContext.Add(newAppointment);
                await _spmuaDbContext.SaveChangesAsync();

                result = newAppointment.AppointmentId;
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<List<TimeInterval>> GetBookedAppointmentIntervalsForAsync(DateTime date)
        {
            List<TimeInterval> result;

            try
            {
                result = await _spmuaDbContext.Appointments.Where(a => a.AppointmentDate.Date == date.Date 
                                                                    && a.AppointmentStatusId != (int)AppointmentStatusEnum.Rejected)
                                                           .OrderBy(a => a.AppointmentDate)
                                                           .Select(a => Helper.CreateAppointmentTimeInterval(TimeOnly.FromDateTime(a.AppointmentDate),
                                                                                                             a.ServiceType.ServiceTypeDuration))
                                                           .ToListAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<List<DateOnly>> GetDatesWithAppointmentsAsync(DateTime fromDate, DateTime toDate)
        {
            List<DateOnly> result = new();

            try
            {
                result = await _spmuaDbContext.Appointments.GroupBy(a => a.AppointmentDate.Date)
                                                           .Where(a => a.Key > DateTime.Now.Date
                                                                    && a.Key > fromDate
                                                                    && a.Key <= toDate)
                                                           .Select(s => DateOnly.FromDateTime(s.Key))
                                                           .ToListAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
