using FluentValidation;
using SPMUA.Model.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SPMUA.Service.Validators.Appointment
{
    public class AppointmentDTOValidator : AbstractValidator<AppointmentDTO>
    {
        public AppointmentDTOValidator()
        {
            RuleFor(a => a.CustomerFirstName).NotEmpty()
                                             .MaximumLength(35);

            RuleFor(a => a.CustomerLastName).NotEmpty()
                                            .MaximumLength(35);

            RuleFor(a => a.CustomerEmail).EmailAddress()
                                         .When(a => !String.IsNullOrEmpty(a.CustomerEmail))
                                         .MaximumLength(100);

            RuleFor(a => a.CustomerPhone).NotEmpty()
                                         .Matches(@"^06\d{7,8}$");

            RuleFor(a => a.AppointmentDate).NotEmpty()
                                           .Must(appointmentDate => appointmentDate > DateTime.Now);

            RuleFor(a => a.ServiceTypeId).NotEmpty();
        }
    }
}
