using SPMUA.Model.DTOs.ServiceType;

namespace SPMUA.Repository.Contracts
{
    public interface IServiceTypeRepository
    {
        Task<List<ServiceTypeDTO>> GetServiceTypesAsync();

        Task<ServiceTypeDTO> GetServiceTypeAsync(int serviceTypeId);

        Task<int> CreateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO);

        Task UpdateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO);

        Task DeleteServiceTypeAsync(int serviceTypeId);
    }
}
