using SPMUA.Model.DTOs.WorkingDay;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;

namespace SPMUA.Service.Implementations
{
    public class WorkingDayService : IWorkingDayService
    {
        private readonly IWorkingDayRepository _workingDayRepository;

        public WorkingDayService(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }
        public async Task<List<WorkingDayDTO>> GetWorkingDaysAsync()
        {
            return await _workingDayRepository.GetWorkingDaysAsync();
        }

        public async Task UpdateWorkingDaysAsync(List<WorkingDayDTO> workingDayDTOs)
        {
            await _workingDayRepository.UpdateWorkingDaysAsync(workingDayDTOs);
        }
    }
}
