using FluentValidation;
using SPMUA.Model.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Validators.Admin
{
    public class AdminRegisterDTOValidator : AbstractValidator<AdminRegisterDTO>
    {
        public AdminRegisterDTOValidator()
        {
            RuleFor(a => a.AdminFirstName).NotEmpty()
                                     .MaximumLength(35);

            RuleFor(a => a.AdminLastName).NotEmpty()
                                    .MaximumLength(35);

            RuleFor(a => a.AdminEmail).NotEmpty()
                                 .EmailAddress();

            RuleFor(a => a.Password).NotEmpty()
                                    .MinimumLength(8);
        }
    }
}
