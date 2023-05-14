using FluentValidation;
using SPMUA.Model.DTOs.WorkingDay;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;
using SPMUA.Service.Validators;
using SPMUA.Service.Validators.WorkingDay;

namespace SPMUA.Service.Implementations
{
    public class WorkingDayService : IWorkingDayService
    {
        private readonly IWorkingDayRepository _workingDayRepository;

        public WorkingDayService(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }
        public async Task<List<WorkingDayDTO>> GetAllWorkingDaysAsync()
        {
            return await _workingDayRepository.GetAllWorkingDaysAsync();
        }

        public async Task UpdateWorkingDaysAsync(List<WorkingDayDTO> workingDayDTOs)
        {
            RequestValidator<WorkingDayDTOListValidator, List<WorkingDayDTO>> validator = new();

            validator.Validate(workingDayDTOs);

            await _workingDayRepository.UpdateWorkingDaysAsync(workingDayDTOs);
        }
    }
}
