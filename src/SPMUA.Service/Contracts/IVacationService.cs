using SPMUA.Model.DTOs.Vacation;

namespace SPMUA.Service.Contracts
{
    public interface IVacationService
    {
        Task<List<VacationDTO>> GetAllVacationsAsync();

        Task<VacationDTO> GetVacationByIdAsync(int vacationId);

        Task<int> CreateVacationAsync(VacationDTO vacationDTO);

        Task UpdateVacationAsync(VacationDTO vacationDTO);

        Task DeleteVacationAsync(int vacationId);

    }
}
