using FluentValidation;
using SPMUA.Model.DTOs.WorkingDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Validators.WorkingDay
{
    public class WorkingDayDTOListValidator : AbstractValidator<List<WorkingDayDTO>>
    {
        public WorkingDayDTOListValidator()
        {
            RuleForEach(wd => wd).SetValidator(new WorkingDayDTOValidator());
        }
    }
}
