using Microsoft.EntityFrameworkCore;
using SPMUA.Model.DTOs.WorkingDay;
using SPMUA.Model.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Implementations
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        private readonly SpmuaDbContext _spmuaDbContext;

        public WorkingDayRepository(SpmuaDbContext spmuaDbContext)
        {
            _spmuaDbContext = spmuaDbContext;
        }
        public async Task<List<WorkingDayDTO>> GetWorkingDaysAsync()
        {
            List<WorkingDayDTO> result = new();

            try
            {
                result = await _spmuaDbContext.WorkingDays.Where(wd => !wd.IsDeleted)
                                                          .AsNoTracking()
                                                          .Select(wd => new WorkingDayDTO()
                                                          {
                                                              WorkingDayId = wd.WorkingDayId,
                                                              WorkingDayName = wd.WorkingDayName,
                                                              IsActive = wd.IsActive,
                                                              StartTime = wd.StartTime,
                                                              EndTime = wd.EndTime
                                                          })
                                                          .ToListAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task UpdateWorkingDaysAsync(List<WorkingDayDTO> workingDayDTOs)
        {
            try
            {
                foreach (WorkingDayDTO workingDayDTO in workingDayDTOs)
                {
                    WorkingDay? workingDay = await _spmuaDbContext.WorkingDays.FindAsync(workingDayDTO.WorkingDayId);

                    if (workingDay is not null)
                    {
                        workingDay.StartTime = workingDayDTO.StartTime;
                        workingDay.EndTime = workingDayDTO.EndTime;
                        workingDay.IsActive = workingDayDTO.IsActive;
                        workingDay.LastModifiedDate = DateTime.Now;
                    }
                }

                await _spmuaDbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
