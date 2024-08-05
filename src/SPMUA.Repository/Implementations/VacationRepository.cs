using Microsoft.EntityFrameworkCore;
using SPMUA.Model.DTOs.Vacation;
using SPMUA.Model.Exceptions;
using SPMUA.Repository.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;

namespace SPMUA.Repository.Implementations
{
    public class VacationRepository : IVacationRepository
    {
        private readonly SpmuaDbContext _spmuaDbContext;

        public VacationRepository(SpmuaDbContext spmuaDbContext)
        {
            _spmuaDbContext = spmuaDbContext;
        }

        public async Task<List<VacationDTO>> GetAllVacationsAsync()
        {
            List<VacationDTO> result = new List<VacationDTO>();

            try
            {
                result = await _spmuaDbContext.Vacations.Where(v => v.EndDate > DateTime.Now)
                                                        .Select(v => new VacationDTO
                                                        {
                                                            VacationId = v.VacationId,
                                                            VacationName = v.VacationName,
                                                            StartDate = v.StartDate,
                                                            EndDate = v.EndDate
                                                        })
                                                        .ToListAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<VacationDTO> GetVacationByIdAsync(int vacationId)
        {
            VacationDTO? result = null;

            try
            {
                result = await _spmuaDbContext.Vacations.Where(v => v.VacationId == vacationId)
                                                        .Select(v => new VacationDTO()
                                                        {
                                                            VacationId = v.VacationId,
                                                            VacationName = v.VacationName,
                                                            StartDate = v.StartDate,
                                                            EndDate = v.EndDate
                                                        })
                                                        .FirstOrDefaultAsync();

                if (result is null)
                {
                    throw new EntityNotFoundException(vacationId);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<int> CreateVacationAsync(VacationDTO vacationDTO)
        {
            int result = 0;

            try
            {
                Vacation newVacation = new()
                {
                    VacationName = vacationDTO.VacationName,
                    StartDate = vacationDTO.StartDate,
                    EndDate = vacationDTO.EndDate
                };

                _spmuaDbContext.Add(newVacation);
                await _spmuaDbContext.SaveChangesAsync();

                result = newVacation.VacationId;
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task UpdateVacationAsync(VacationDTO vacationDTO)
        {
            try
            {
                Vacation? vacation = await _spmuaDbContext.Vacations.Where(v => v.VacationId == vacationDTO.VacationId)
                                                                    .AsTracking()
                                                                    .FirstOrDefaultAsync();

                if (vacation is not null)
                {
                    vacation.VacationName = vacationDTO.VacationName;
                    vacation.StartDate = vacationDTO.StartDate;
                    vacation.EndDate = vacationDTO.EndDate;

                    await _spmuaDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException(vacationDTO.VacationId);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteVacationAsync(int vacationId)
        {
            try
            {
                Vacation? vacation = await _spmuaDbContext.Vacations.Where(v => v.VacationId == vacationId)
                                                                    .AsTracking()
                                                                    .FirstOrDefaultAsync();

                if (vacation is not null)
                {
                    _spmuaDbContext.Remove(vacation);
                    await _spmuaDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException(vacationId);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> IsDateOverlappingWithVacationAsync(DateTime date)
        {
            bool result = false;

            try
            {
                Vacation? conflictingVacation = await _spmuaDbContext.Vacations.Where(v => v.StartDate <= date && v.EndDate >= date)
                                                                               .FirstOrDefaultAsync();

                result = conflictingVacation is not null;
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
