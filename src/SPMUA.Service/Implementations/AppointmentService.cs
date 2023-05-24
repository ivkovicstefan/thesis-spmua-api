using Microsoft.EntityFrameworkCore;
using SPMUA.Model.Commons;
using SPMUA.Model.Dictionaries.Appointment;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Model.DTOs.ServiceType;
using SPMUA.Model.Exceptions;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;
using SPMUA.Service.Validators;
using SPMUA.Service.Validators.Appointment;
using SPMUA.Utility.Helpers;

namespace SPMUA.Service.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IWorkingDayRepository _workingDayRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  IWorkingDayRepository workingDayRepository,
                                  IServiceTypeRepository serviceTypeRepository)
        {
            _appointmentRepository = appointmentRepository;
            _workingDayRepository = workingDayRepository;
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<List<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        }

        public async Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            RequestValidator<AppointmentDTOValidator, AppointmentDTO> validator = new();

            int result;

            validator.Validate(appointmentDTO);

            if (await IsAppointmentDateTimeAvailableForAsync(appointmentDTO.ServiceTypeId, appointmentDTO.AppointmentDate))
            {
                result = await _appointmentRepository.CreateAppointmentAsync(appointmentDTO);
            }
            else
            {
                throw new RequestValidationException("Appointment date is not available anymore.", null);
            }

            // TODO: Send appointment confirmation pending email to client

            // TODO: Send appointment confirmation pending email to admin

            return result;
        }

        public async Task<List<DateOnly>> GetUnavailableAppointmentDatesForAsync(DateTime fromDate, DateTime toDate, int serviceTypeId)
        {
            List<DateOnly> result = new();

            List<DateOnly> distinctAppointmentDates =
                await _appointmentRepository.GetDatesWithAppointmentsAsync(fromDate, toDate);

            foreach (var appointmentDate in distinctAppointmentDates)
            {
                if (!await IsAppointmentDateAvailableForAsync(serviceTypeId, appointmentDate))
                {
                    result.Add(appointmentDate);
                }
            }

            return result;
        }

        private async Task<bool> IsAppointmentDateTimeAvailableForAsync(int serviceTypeId, DateTime date)
        {
            (TimeOnly? openingTime, TimeOnly? closingTime) 
                = await _workingDayRepository.GetWorkingHoursForAsync(date);

            // Check if working day is still active

            if (openingTime is null || closingTime is null)
            {
                return false;
            }

            ServiceTypeDTO requestedServiceType 
                = await _serviceTypeRepository.GetServiceTypeByIdAsync(serviceTypeId);

            List<TimeInterval> bookedIntervals 
                = await _appointmentRepository.GetBookedAppointmentIntervalsForAsync(date);

            TimeInterval requestedInterval
                = Helper.CreateAppointmentTimeInterval(TimeOnly.FromDateTime(date), requestedServiceType.ServiceTypeDuration);
            
            // Check if there is overlapping between requested and booked intervals

            if (bookedIntervals.Any(bi => bi.EndingTime > requestedInterval.StartingTime 
                                       && bi.StartingTime < requestedInterval.EndingTime))
            {
                return false;
            }

            // Check if requested interval is out of working hours

            if (requestedInterval.StartingTime < openingTime || requestedInterval.EndingTime > closingTime)
            {
                return false;
            }
            
            return true;
        }

        private async Task<bool> IsAppointmentDateAvailableForAsync(int serviceTypeId, DateOnly date)
        {
            DateTime potentialBookedDate = date.ToDateTime(TimeOnly.MinValue);

            (TimeOnly? openingTime, TimeOnly? closingTime)
                = await _workingDayRepository.GetWorkingHoursForAsync(potentialBookedDate);

            if (openingTime is null || closingTime is null)
            {
                return false;
            }

            ServiceTypeDTO requestedServiceType
                = await _serviceTypeRepository.GetServiceTypeByIdAsync(serviceTypeId);

            List<TimeInterval> bookedIntervals
                = await _appointmentRepository.GetBookedAppointmentIntervalsForAsync(potentialBookedDate);

            TimeInterval potentialFreeInterval
                = Helper.CreateAppointmentTimeInterval(openingTime.Value, requestedServiceType.ServiceTypeDuration);

            foreach (var bookedInterval in bookedIntervals)
            {
                if (potentialFreeInterval.EndingTime <= bookedInterval.StartingTime)
                {
                    return true;
                }
                else
                {
                    potentialFreeInterval 
                        = Helper.CreateAppointmentTimeInterval(bookedInterval.EndingTime, requestedServiceType.ServiceTypeDuration);
                }
            }

            // Check if the last interval of the working day is available

            TimeOnly lastIntervalStart = closingTime.Value.AddHours(-1);

            if (potentialFreeInterval.StartingTime <= lastIntervalStart)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TimeOnly>> GetAvailableAppointmentHoursForAsync(DateTime date, int serviceTypeId)
        {
            List<TimeOnly> result = new();

            (TimeOnly? openingTime, TimeOnly? closingTime)
                = await _workingDayRepository.GetWorkingHoursForAsync(date);

            if (openingTime is null || closingTime is null)
            {
                return result;
            }

            ServiceTypeDTO requestedServiceType
                = await _serviceTypeRepository.GetServiceTypeByIdAsync(serviceTypeId);

            List<TimeInterval> bookedIntervals
                = await _appointmentRepository.GetBookedAppointmentIntervalsForAsync(date);

            TimeInterval potentialFreeInterval
                = Helper.CreateAppointmentTimeInterval(openingTime.Value, requestedServiceType.ServiceTypeDuration);

            while (potentialFreeInterval.StartingTime < closingTime)
            {
                if (!bookedIntervals.Any(bi => bi.EndingTime > potentialFreeInterval.StartingTime 
                                            && bi.StartingTime < potentialFreeInterval.EndingTime))
                {
                    result.Add(potentialFreeInterval.StartingTime);
                }

                potentialFreeInterval.StartingTime = potentialFreeInterval.StartingTime.AddHours(1);
                potentialFreeInterval.EndingTime = potentialFreeInterval.EndingTime.AddHours(1);
            }

            return result;
        }

        public async Task UpdateAppointmentStatusAsync(int appointmentId, bool isAppointmentConfirmed)
        {
            if (isAppointmentConfirmed)
            {
                await _appointmentRepository.UpdateAppointmentStatusAsync(appointmentId, 
                                                                          (int)AppointmentStatusEnum.Confirmed);

                // TODO: Send appointment confirmation email to client
            }
            else
            {
                await _appointmentRepository.UpdateAppointmentStatusAsync(appointmentId, 
                                                                          (int)AppointmentStatusEnum.Rejected);

                // TODO: Send appointment rejection email to client
            }
        }

        public async Task<string> GetAppointmentmentStatusByIdAsync(int appointmentId)
        {
            AppointmentDTO requestedAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);

            return requestedAppointment.AppointmentStatusName;
        }
    }
}
