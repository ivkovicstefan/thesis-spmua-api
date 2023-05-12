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
                        workingDay.StartTime = (workingDayDTO.IsActive) ? workingDayDTO.StartTime : null;
                        workingDay.EndTime = (workingDayDTO.IsActive) ? workingDayDTO.EndTime : null;
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

        public async Task<ValueTuple<TimeOnly?, TimeOnly?>> GetWorkingHours(DateTime date)
        {
            ValueTuple<TimeOnly?, TimeOnly?> result;

            try
            {
                string dayName = date.DayOfWeek.ToString();

                result = await _spmuaDbContext.WorkingDays.Where(wd => wd.WorkingDayName.ToLower() == dayName.ToLower())
                                                          .AsNoTracking()
                                                          .Select(wd => ValueTuple.Create(wd.StartTime, wd.EndTime))
                                                          .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
