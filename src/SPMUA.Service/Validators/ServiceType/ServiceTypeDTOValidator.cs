using FluentValidation;
using SPMUA.Model.DTOs.ServiceType;

namespace SPMUA.Service.Validators.ServiceType
{
    public class ServiceTypeDTOValidator : AbstractValidator<ServiceTypeDTO>
    {
        public ServiceTypeDTOValidator()
        {
            RuleFor(st => st.ServiceTypeName).NotEmpty()
                                             .MaximumLength(50);

            RuleFor(st => st.ServiceTypePrice).NotEmpty();

            RuleFor(st => st.ServiceTypeDuration).NotEmpty();
        }
    }
}
