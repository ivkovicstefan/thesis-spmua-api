using SPMUA.Model.DTOs.ServiceType;
using SPMUA.Model.Exceptions;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;
using SPMUA.Service.Validators;
using SPMUA.Service.Validators.ServiceType;

namespace SPMUA.Service.Implementations
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<List<ServiceTypeDTO>> GetAllServiceTypesAsync()
        {
            return await _serviceTypeRepository.GetAllServiceTypesAsync();
        }

        public async Task<ServiceTypeDTO> GetServiceTypeByIdAsync(int serviceTypeId)
        {
            return await _serviceTypeRepository.GetServiceTypeByIdAsync(serviceTypeId);
        }

        public async Task<int> CreateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO)
        {
            RequestValidator<ServiceTypeDTOValidator, ServiceTypeDTO> validator = new();

            validator.Validate(serviceTypeDTO);

            return await _serviceTypeRepository.CreateServiceTypeAsync(serviceTypeDTO);
        }

        public async Task UpdateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO)
        {
            RequestValidator<ServiceTypeDTOValidator, ServiceTypeDTO> validator = new();

            validator.Validate(serviceTypeDTO);

            await _serviceTypeRepository.UpdateServiceTypeAsync(serviceTypeDTO);
        }

        public async Task DeleteServiceTypeAsync(int serviceTypeId)
        {
            await _serviceTypeRepository.DeleteServiceTypeAsync(serviceTypeId);
        }
    }
}
