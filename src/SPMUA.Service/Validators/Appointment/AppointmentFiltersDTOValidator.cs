using FluentValidation;
using SPMUA.Model.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Validators.Appointment
{
    internal class AppointmentFiltersDTOValidator : AbstractValidator<AppointmentFiltersDTO>
    {
        public AppointmentFiltersDTOValidator()
        {
            RuleFor(a => a.CustomerFullName).MaximumLength(71);

            RuleFor(a => a.CustomerEmail).MaximumLength(100);

            RuleFor(a => a.CustomerPhone).MaximumLength(10);
        }
    }
}
