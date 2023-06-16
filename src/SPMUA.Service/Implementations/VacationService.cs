using SPMUA.Model.DTOs.Vacation;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;
using SPMUA.Service.Validators;
using SPMUA.Service.Validators.Vacation;

namespace SPMUA.Service.Implementations
{
    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;

        public VacationService(IVacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public async Task<List<VacationDTO>> GetAllVacationsAsync()
        {
            return await _vacationRepository.GetAllVacationsAsync();
        }

        public async Task<VacationDTO> GetVacationByIdAsync(int vacationId)
        {
            return await _vacationRepository.GetVacationByIdAsync(vacationId);
        }

        public async Task<int> CreateVacationAsync(VacationDTO vacationDTO)
        {
            RequestValidator<VacationDTOValidator, VacationDTO> validator = new();

            validator.Validate(vacationDTO);

            return await _vacationRepository.CreateVacationAsync(vacationDTO);
        }

        public async Task UpdateVacationAsync(VacationDTO vacationDTO)
        {
            RequestValidator<VacationDTOValidator, VacationDTO> validator = new();

            validator.Validate(vacationDTO);

            await _vacationRepository.UpdateVacationAsync(vacationDTO);
        }

        public async Task DeleteVacationAsync(int vacationId)
        {
            await _vacationRepository.DeleteVacationAsync(vacationId);
        }
    }
}
