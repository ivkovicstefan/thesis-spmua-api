using SPMUA.Model.DTOs.WorkingDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Contracts
{
    public interface IWorkingDayRepository
    {
        Task<List<WorkingDayDTO>> GetAllWorkingDaysAsync();

        Task UpdateWorkingDaysAsync(List<WorkingDayDTO> workingDayDTOs);

        Task<ValueTuple<TimeOnly?, TimeOnly?>> GetWorkingHoursForAsync(DateTime date);
    }
}
