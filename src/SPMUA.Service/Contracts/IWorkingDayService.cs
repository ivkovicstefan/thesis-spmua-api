using SPMUA.Model.DTOs.WorkingDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Contracts
{
    public interface IWorkingDayService
    {
        Task<List<WorkingDayDTO>> GetAllWorkingDaysAsync();

        Task UpdateWorkingDaysAsync(List<WorkingDayDTO> workingDayDTOs);
    }
}
