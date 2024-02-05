using SPMUA.Model.Commons.DataTypes;
using SPMUA.Model.Dictionaries.Appointment;
using SPMUA.Model.Dictionaries.EmailTemplate;
using SPMUA.Model.DTOs.Appointment;
using SPMUA.Model.DTOs.ServiceType;
using SPMUA.Model.DTOs.Vacation;
using SPMUA.Model.Exceptions;
using SPMUA.Model.Queues;
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
        private readonly IVacationRepository _vacationRepository;
        private readonly EmailQueue _emailQueue;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  IWorkingDayRepository workingDayRepository,
                                  IServiceTypeRepository serviceTypeRepository,
                                  IVacationRepository vacationRepository,
                                  EmailQueue emailQueue)
        {
            _appointmentRepository = appointmentRepository;
            _workingDayRepository = workingDayRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _vacationRepository = vacationRepository;
            _emailQueue = emailQueue;
        }

        public async Task<List<AppointmentDTO>> GetAllAppointmentsAsync(AppointmentFiltersDTO appointmentFiltersDTO)
        {
            RequestValidator<AppointmentFiltersDTOValidator, AppointmentFiltersDTO> validator = new();

            validator.Validate(appointmentFiltersDTO);

            return await _appointmentRepository.GetAllAppointmentsAsync(appointmentFiltersDTO);
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        }

        public async Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            int result;

            RequestValidator<AppointmentDTOValidator, AppointmentDTO> validator = new();

            validator.Validate(appointmentDTO);

            if (await IsAppointmentDateTimeAvailableForAsync(appointmentDTO.ServiceTypeId, appointmentDTO.AppointmentDate))
            {
                result = await _appointmentRepository.CreateAppointmentAsync(appointmentDTO);
            }
            else
            {
                throw new RequestValidationException("Appointment date is not available anymore.", null);
            }

            if (!String.IsNullOrEmpty(appointmentDTO.CustomerEmail))
            {
                EmailQueueItem clientEmailQueueItem = new()
                {
                    EmailTemplateId = (int)EmailTemplateEnum.AppointmentRequestConfirmationPending,
                    EntityId = result,
                    ToClientEmail = appointmentDTO.CustomerEmail
                };

                _emailQueue.Enqueue(clientEmailQueueItem);
            }

            EmailQueueItem adminEmailQueueItem = new()
            {
                EmailTemplateId = (int)EmailTemplateEnum.AppointmentRequestArrived,
                EntityId = result
            };

            _emailQueue.Enqueue(adminEmailQueueItem);

            return result;
        }

        public async Task<List<DateOnly>> GetUnavailableAppointmentDatesForAsync(DateTime fromDate, DateTime toDate, int serviceTypeId)
        {
            List<DateOnly> result = new();

            List<DateOnly> distinctAppointmentDates =
                await _appointmentRepository.GetDatesWithAppointmentsAsync(fromDate, toDate);

            List<VacationDTO> vacations = await _vacationRepository.GetAllVacationsAsync();

            // Get days populated with appointments

            foreach (var appointmentDate in distinctAppointmentDates)
            {
                if (!await IsAppointmentDateAvailableForAsync(serviceTypeId, appointmentDate))
                {
                    result.Add(appointmentDate);
                }
            }

            // Get vacation days

            foreach (var vacation in vacations)
            {
                for (var day = vacation.StartDate; day <= vacation.EndDate; day = day.AddDays(1))
                {
                    result.Add(DateOnly.FromDateTime(day));
                }
            }

            return result;
        }

        private async Task<bool> IsAppointmentDateTimeAvailableForAsync(int serviceTypeId, DateTime date)
        {
            // Check if the date is overlapping with vacations

            if (await _vacationRepository.IsDateOverlappingWithVacationAsync(date))
            {
                return false;
            }

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

            if (requestedInterval.StartingTime < openingTime || requestedInterval.StartingTime >= closingTime)
            {
                return false;
            }
            
            return true;
        }

        private async Task<bool> IsAppointmentDateAvailableForAsync(int serviceTypeId, DateOnly date)
        {
            // Check if the date is overlapping with vacations

            if (await _vacationRepository.IsDateOverlappingWithVacationAsync(date.ToDateTime(new TimeOnly(0, 0))))
            {
                return false;
            }

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
                        = Helper.CreateAppointmentTimeInterval(Helper.RoundToNextHour(bookedInterval.EndingTime), requestedServiceType.ServiceTypeDuration, true);
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

            if (await _vacationRepository.IsDateOverlappingWithVacationAsync(date))
            {
                return result;
            }

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

        public async Task UpdateAppointmentStatusAsync(UpdateAppointmentStatusDTO updateAppointmentStatusDTO)
        {
            if (updateAppointmentStatusDTO.IsAppointmentConfirmed)
            {
                await _appointmentRepository.UpdateAppointmentStatusAsync(updateAppointmentStatusDTO.AppointmentId, 
                                                                          (int)AppointmentStatusEnum.Confirmed,
                                                                          updateAppointmentStatusDTO.ResponseComment);
            }
            else
            {
                await _appointmentRepository.UpdateAppointmentStatusAsync(updateAppointmentStatusDTO.AppointmentId, 
                                                                          (int)AppointmentStatusEnum.Rejected,
                                                                          updateAppointmentStatusDTO.ResponseComment);
            }

            string clientEmail = await _appointmentRepository.GetAppointmentCustomerEmail(updateAppointmentStatusDTO.AppointmentId);

            if (!String.IsNullOrEmpty(clientEmail))
            {
                EmailQueueItem clientEmailQueueItem = new()
                {
                    EntityId = updateAppointmentStatusDTO.AppointmentId,
                    EmailTemplateId = updateAppointmentStatusDTO.IsAppointmentConfirmed ? (int)EmailTemplateEnum.AppointmentRequestConfirmed
                                                                                        : (int)EmailTemplateEnum.AppointmentRequestRejected,
                    ToClientEmail = clientEmail
                };

                _emailQueue.Enqueue(clientEmailQueueItem);
            }
        }

        public async Task<string> GetAppointmentmentStatusByIdAsync(int appointmentId)
        {
            AppointmentDTO requestedAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);

            return requestedAppointment.AppointmentStatusName;
        }
    }
}
