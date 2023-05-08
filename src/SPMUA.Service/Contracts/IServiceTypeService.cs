
using SPMUA.Model.DTOs.ServiceType;

namespace SPMUA.Service.Contracts
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeDTO>> GetServiceTypesAsync();

        Task<ServiceTypeDTO> GetServiceTypeAsync(int serviceTypeId);

        Task<int> CreateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO);

        Task UpdateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO);

        Task DeleteServiceTypeAsync(int serviceTypeId);
    }
}
