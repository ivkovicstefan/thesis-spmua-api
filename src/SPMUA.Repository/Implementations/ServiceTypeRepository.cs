using Microsoft.EntityFrameworkCore;
using SPMUA.Model.DTOs.ServiceType;
using SPMUA.Model.Exceptions;
using SPMUA.Repository.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;

namespace SPMUA.Repository.Implementations
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly SpmuaDbContext _spmuaDbContext;

        public ServiceTypeRepository(SpmuaDbContext spmuaDbContext)
        {
            _spmuaDbContext = spmuaDbContext;
        }

        public async Task<List<ServiceTypeDTO>> GetAllServiceTypesAsync()
        {
            List<ServiceTypeDTO> result = new();

            try
            {
                result = await _spmuaDbContext.ServiceTypes
                    .Where(st => st.IsActive && !st.IsDeleted)                                       
                    .Select(st => new ServiceTypeDTO()
                    {
                        ServiceTypeId = st.ServiceTypeId,
                        ServiceTypeName = st.ServiceTypeName,
                        ServiceTypePrice = st.ServiceTypePriceHistories
                            .OrderByDescending(stph => stph.CreatedDate)
                            .First().ServiceTypePrice,
                        ServiceTypeDuration = st.ServiceTypeDuration,
                        IsAvailableOnMonday = st.IsAvailableOnMonday,
                        IsAvailableOnTuesday = st.IsAvailableOnTuesday,
                        IsAvailableOnWednesday = st.IsAvailableOnWednesday,
                        IsAvailableOnThursday = st.IsAvailableOnThursday,
                        IsAvailableOnFriday = st.IsAvailableOnFriday,
                        IsAvailableOnSaturday = st.IsAvailableOnSaturday,
                        IsAvailableOnSunday = st.IsAvailableOnSunday
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<ServiceTypeDTO> GetServiceTypeByIdAsync(int serviceTypeId)
        {
            ServiceTypeDTO? result = null;

            try
            {
                result = await _spmuaDbContext.ServiceTypes
                    .Where(st => st.ServiceTypeId == serviceTypeId 
                                 && st.IsActive 
                                 && !st.IsDeleted)
                    .Select(st => new ServiceTypeDTO()
                    {
                        ServiceTypeId = st.ServiceTypeId,
                        ServiceTypeName = st.ServiceTypeName,
                        ServiceTypePrice = st.ServiceTypePriceHistories
                            .OrderByDescending(stph => stph.CreatedDate)
                            .First().ServiceTypePrice,
                        ServiceTypeDuration = st.ServiceTypeDuration,
                        IsAvailableOnMonday = st.IsAvailableOnMonday,
                        IsAvailableOnTuesday = st.IsAvailableOnTuesday,
                        IsAvailableOnWednesday = st.IsAvailableOnWednesday,
                        IsAvailableOnThursday = st.IsAvailableOnThursday,
                        IsAvailableOnFriday = st.IsAvailableOnFriday,
                        IsAvailableOnSaturday = st.IsAvailableOnSaturday,
                        IsAvailableOnSunday = st.IsAvailableOnSunday
                    })
                    .FirstOrDefaultAsync();
            
                if (result is null)
                {
                    throw new EntityNotFoundException(serviceTypeId);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<int> CreateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO)
        {
            int result = 0;

            try
            {
                ServiceType newServiceType = new()
                {
                    ServiceTypeName = serviceTypeDTO.ServiceTypeName,
                    ServiceTypeDuration= serviceTypeDTO.ServiceTypeDuration,
                    IsAvailableOnMonday = serviceTypeDTO.IsAvailableOnMonday,
                    IsAvailableOnTuesday = serviceTypeDTO.IsAvailableOnTuesday,
                    IsAvailableOnWednesday = serviceTypeDTO.IsAvailableOnWednesday,
                    IsAvailableOnThursday = serviceTypeDTO.IsAvailableOnThursday,
                    IsAvailableOnFriday = serviceTypeDTO.IsAvailableOnFriday,
                    IsAvailableOnSaturday = serviceTypeDTO.IsAvailableOnSaturday,
                    IsAvailableOnSunday = serviceTypeDTO.IsAvailableOnSunday
                };

                newServiceType.ServiceTypePriceHistories.Add(new()
                {
                    ServiceTypePrice = serviceTypeDTO.ServiceTypePrice
                });

                _spmuaDbContext.Add(newServiceType);
                await _spmuaDbContext.SaveChangesAsync();

                result = newServiceType.ServiceTypeId;
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task UpdateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO)
        {
            try
            {
                ServiceType? serviceType = await _spmuaDbContext.ServiceTypes
                    .Where(st => st.ServiceTypeId == serviceTypeDTO.ServiceTypeId 
                              && st.IsActive 
                              && !st.IsDeleted)
                    .AsTracking()
                    .FirstOrDefaultAsync();

                if (serviceType is not null)
                {
                    serviceType.ServiceTypeName = serviceTypeDTO.ServiceTypeName;
                    serviceType.ServiceTypeDuration = serviceTypeDTO.ServiceTypeDuration;
                    serviceType.IsAvailableOnMonday = serviceTypeDTO.IsAvailableOnMonday;
                    serviceType.IsAvailableOnTuesday = serviceTypeDTO.IsAvailableOnTuesday;
                    serviceType.IsAvailableOnWednesday = serviceTypeDTO.IsAvailableOnWednesday;
                    serviceType.IsAvailableOnThursday = serviceTypeDTO.IsAvailableOnThursday;
                    serviceType.IsAvailableOnFriday = serviceTypeDTO.IsAvailableOnFriday;
                    serviceType.IsAvailableOnSaturday = serviceTypeDTO.IsAvailableOnSaturday;
                    serviceType.IsAvailableOnSunday = serviceTypeDTO.IsAvailableOnSunday;
                    serviceType.LastModifiedDate = DateTime.Now;

                    ServiceTypePriceHistory serviceTypePriceHistory = await _spmuaDbContext.ServiceTypePriceHistory
                        .Where(stph => stph.ServiceTypeId == serviceType.ServiceTypeId)
                        .OrderByDescending(stph => stph.CreatedDate)
                        .FirstAsync();

                    if (serviceTypeDTO.ServiceTypePrice != serviceTypePriceHistory.ServiceTypePrice)
                    {
                        ServiceTypePriceHistory newServiceTypePriceHistory = new()
                        {
                            ServiceTypeId = serviceType.ServiceTypeId,
                            ServiceTypePrice = serviceTypeDTO.ServiceTypePrice
                        };

                        _spmuaDbContext.Add(newServiceTypePriceHistory);
                    }

                    await _spmuaDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException(serviceTypeDTO.ServiceTypeId);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteServiceTypeAsync(int serviceTypeId)
        {
            try
            {
                ServiceType? serviceType = await _spmuaDbContext.ServiceTypes.Where(st => st.ServiceTypeId == serviceTypeId
                                                                                       && st.IsActive
                                                                                       && !st.IsDeleted)
                                                                             .AsTracking()
                                                                             .FirstOrDefaultAsync();

                if (serviceType is not null)
                {
                    serviceType.IsActive = false;
                    serviceType.IsDeleted = true;
                    serviceType.LastModifiedDate = DateTime.Now;

                    await _spmuaDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException(serviceTypeId);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
