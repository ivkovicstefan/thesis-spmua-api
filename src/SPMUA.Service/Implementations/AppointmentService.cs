using Microsoft.EntityFrameworkCore;
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

            if (await IsAppointmentDateTimeAvailableFor(appointmentDTO.ServiceTypeId, appointmentDTO.AppointmentDate))
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

        public async Task<List<DateOnly>> GetUnavailableDatesForAsync(DateTime fromDate, DateTime toDate, int serviceTypeId)
        {
            List<DateOnly> result = new();

            List<DateOnly> distinctAppointmentDates =
                await _appointmentRepository.GetDatesWithAppointmentsAsync(fromDate, toDate);

            foreach (var appointmentDate in distinctAppointmentDates)
            {
                if (!await IsAppointmentDateAvailableFor(serviceTypeId, appointmentDate))
                {
                    result.Add(appointmentDate);
                }
            }

            return result;
        }

        private async Task<bool> IsAppointmentDateTimeAvailableFor(int serviceTypeId, DateTime date)
        {
            ValueTuple<TimeOnly?, TimeOnly?> workingHours 
                = await _workingDayRepository.GetWorkingHours(date);

            // Check if working day is still active

            if (workingHours.Item1 is null || workingHours.Item2 is null)
            {
                return false;
            }

            ServiceTypeDTO requestedServiceType 
                = await _serviceTypeRepository.GetServiceTypeAsync(serviceTypeId);

            List<ValueTuple<TimeOnly, TimeOnly>> bookedIntervals 
                = await _appointmentRepository.GetBookedAppointmentIntervalsFor(date);

            ValueTuple<TimeOnly, TimeOnly> requestedInterval 
                = ValueTuple.Create(TimeOnly.FromDateTime(date),
                                    Helper.RoundToNextHour(TimeOnly.FromDateTime(date)
                                                                   .AddMinutes(requestedServiceType.ServiceTypeDuration)));
            
            // Check if there is overlapping between requested and booked intervals

            if (bookedIntervals.Any(bi => bi.Item2 > requestedInterval.Item1 && bi.Item1 < requestedInterval.Item2))
            {
                return false;
            }

            // Check if requested interval is out of working hours

            if (requestedInterval.Item1 < workingHours.Item1 || requestedInterval.Item2 > workingHours.Item2)
            {
                return false;
            }
            
            return true;
        }

        private async Task<bool> IsAppointmentDateAvailableFor(int serviceTypeId, DateOnly date)
        {
            DateTime potentialBookedDate = date.ToDateTime(TimeOnly.MinValue);

            ValueTuple<TimeOnly?, TimeOnly?> workingHours
                = await _workingDayRepository.GetWorkingHours(potentialBookedDate);

            if (workingHours.Item1 is null || workingHours.Item2 is null)
            {
                return false;
            }

            ServiceTypeDTO requestedServiceType
                = await _serviceTypeRepository.GetServiceTypeAsync(serviceTypeId);

            List<ValueTuple<TimeOnly, TimeOnly>> bookedIntervals
                = await _appointmentRepository.GetBookedAppointmentIntervalsFor(potentialBookedDate);

            ValueTuple<TimeOnly, TimeOnly> potentionalFreeInterval
                = ValueTuple.Create(workingHours.Item1.Value,
                                    Helper.RoundToNextHour(workingHours.Item1.Value.AddMinutes(requestedServiceType.ServiceTypeDuration)));

            foreach (var bookedInterval in bookedIntervals)
            {
                if (potentionalFreeInterval.Item2 <= bookedInterval.Item1)
                {
                    return true;
                }
                else
                {
                    potentionalFreeInterval = ValueTuple.Create(bookedInterval.Item2,
                                                                Helper.RoundToNextHour(bookedInterval.Item2.AddMinutes(requestedServiceType.ServiceTypeDuration)));
                }
            }

            // Check if the last interval of the working day is available

            TimeOnly lastIntervalStart = workingHours.Item2.Value.AddHours(-1);

            if (potentionalFreeInterval.Item1 <= lastIntervalStart)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
