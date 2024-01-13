using Microsoft.EntityFrameworkCore;
using SPMUA.Model.DTOs.WorkingDay;
using SPMUA.Model.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using SPMUA.Utility.Helpers;
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
        public async Task<List<WorkingDayDTO>> GetAllWorkingDaysAsync()
        {
            List<WorkingDayDTO> result = new();

            try
            {
                result = await _spmuaDbContext.WorkingDays.Where(wd => !wd.IsDeleted)
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
                    WorkingDay? workingDay = await _spmuaDbContext.WorkingDays.Where(wd => wd.WorkingDayId == workingDayDTO.WorkingDayId)
                                                                              .AsTracking()
                                                                              .FirstOrDefaultAsync();

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

        public async Task<ValueTuple<TimeOnly?, TimeOnly?>> GetWorkingHoursForAsync(DateTime date)
        {
            ValueTuple<TimeOnly?, TimeOnly?> result;

            try
            {
                int dayWeekIndex = Helper.ToNormalizedWeekDayIndex((int)date.DayOfWeek);

                result = await _spmuaDbContext.WorkingDays.Where(wd => wd.WorkingDayId == dayWeekIndex)
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
