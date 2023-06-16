using FluentValidation;
using SPMUA.Model.DTOs.Vacation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Validators.Vacation
{
    public class VacationDTOValidator : AbstractValidator<VacationDTO>
    {
        public VacationDTOValidator()
        {
            RuleFor(v => v.VacationName).NotEmpty()
                                        .MaximumLength(20);

            RuleFor(v => v.StartDate).NotEmpty()
                                     .GreaterThanOrEqualTo(DateTime.Now);
                                     
            RuleFor(v => v.EndDate).NotEmpty()
                                   .GreaterThan(v => v.StartDate);
        }
    }
}
