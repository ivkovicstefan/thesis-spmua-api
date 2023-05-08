﻿using Microsoft.EntityFrameworkCore;
using SPMUA.Model.DTOs.ServiceType;
using SPMUA.Model.Exceptions;
using SPMUA.Model.Models;
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

        public async Task<List<ServiceTypeDTO>> GetServiceTypesAsync()
        {
            List<ServiceTypeDTO> result = new();

            try
            {
                result = await _spmuaDbContext.ServiceTypes.Where(st => st.IsActive && !st.IsDeleted)
                                                           .AsNoTracking()
                                                           .Select(st => new ServiceTypeDTO()
                                                           {
                                                               ServiceTypeId = st.ServiceTypeId,
                                                               ServiceTypeName = st.ServiceTypeName,
                                                               ServiceTypePrice = st.ServiceTypePrice,
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

        public async Task<ServiceTypeDTO> GetServiceTypeAsync(int serviceTypeId)
        {
            ServiceTypeDTO? result = null;

            try
            {
                result = await _spmuaDbContext.ServiceTypes.Where(st => st.ServiceTypeId == serviceTypeId 
                                                                     && st.IsActive 
                                                                     && !st.IsDeleted)
                                                           .AsNoTracking()
                                                           .Select(st => new ServiceTypeDTO()
                                                           {
                                                               ServiceTypeId = st.ServiceTypeId,
                                                               ServiceTypeName = st.ServiceTypeName,
                                                               ServiceTypePrice = st.ServiceTypePrice,
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
                    throw new EntityNotFoundException();
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
                    ServiceTypePrice = serviceTypeDTO.ServiceTypePrice,
                    ServiceTypeDuration= serviceTypeDTO.ServiceTypeDuration,
                    IsAvailableOnMonday = serviceTypeDTO.IsAvailableOnMonday,
                    IsAvailableOnTuesday = serviceTypeDTO.IsAvailableOnTuesday,
                    IsAvailableOnWednesday = serviceTypeDTO.IsAvailableOnWednesday,
                    IsAvailableOnThursday = serviceTypeDTO.IsAvailableOnThursday,
                    IsAvailableOnFriday = serviceTypeDTO.IsAvailableOnFriday,
                    IsAvailableOnSaturday = serviceTypeDTO.IsAvailableOnSaturday,
                    IsAvailableOnSunday = serviceTypeDTO.IsAvailableOnSunday
                };

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
                ServiceType? serviceType = await _spmuaDbContext.ServiceTypes.Where(st => st.ServiceTypeId == serviceTypeDTO.ServiceTypeId 
                                                                                       && st.IsActive 
                                                                                       && !st.IsDeleted)
                                                                             .FirstOrDefaultAsync();

                if (serviceType is not null)
                {
                    serviceType.ServiceTypeName = serviceTypeDTO.ServiceTypeName;
                    serviceType.ServiceTypePrice = serviceTypeDTO.ServiceTypePrice;
                    serviceType.ServiceTypeDuration = serviceTypeDTO.ServiceTypeDuration;
                    serviceType.IsAvailableOnMonday = serviceTypeDTO.IsAvailableOnMonday;
                    serviceType.IsAvailableOnTuesday = serviceTypeDTO.IsAvailableOnTuesday;
                    serviceType.IsAvailableOnWednesday = serviceTypeDTO.IsAvailableOnWednesday;
                    serviceType.IsAvailableOnThursday = serviceTypeDTO.IsAvailableOnThursday;
                    serviceType.IsAvailableOnFriday = serviceTypeDTO.IsAvailableOnFriday;
                    serviceType.IsAvailableOnSaturday = serviceTypeDTO.IsAvailableOnSaturday;
                    serviceType.IsAvailableOnSunday = serviceTypeDTO.IsAvailableOnSunday;
                    serviceType.LastModifiedDate = DateTime.Now;

                    await _spmuaDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException();
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
                    throw new EntityNotFoundException();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
