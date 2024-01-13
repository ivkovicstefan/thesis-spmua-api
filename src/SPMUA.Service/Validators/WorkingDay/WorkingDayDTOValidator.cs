using FluentValidation;
using SPMUA.Model.DTOs.WorkingDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Validators.WorkingDay
{
    internal class WorkingDayDTOValidator : AbstractValidator<WorkingDayDTO>
    {
        public WorkingDayDTOValidator()
        {
            RuleFor(wd => wd.WorkingDayName).NotEmpty()
                                            .MaximumLength(10);

            RuleFor(wd => wd.StartTime).NotNull()
                                       .When(wd => wd.IsActive);

            RuleFor(wd => wd.EndTime).GreaterThan(wd => wd.StartTime)
                                     .NotNull()
                                     .When(wd => wd.IsActive);
        }
    }
}
