using SPMUA.Model.DTOs.Vacation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Contracts
{
    public interface IVacationRepository
    {
        Task<List<VacationDTO>> GetAllVacationsAsync();

        Task<VacationDTO> GetVacationByIdAsync(int vacationId);

        Task<int> CreateVacationAsync(VacationDTO vacationDTO);

        Task UpdateVacationAsync(VacationDTO vacationDTO);

        Task DeleteVacationAsync(int vacationId);

    }
}
