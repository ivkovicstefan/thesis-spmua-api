using FluentValidation;
using FluentValidation.Results;
using SPMUA.Model.Exceptions;

namespace SPMUA.Service.Validators
{
    public class RequestValidator<TValidator, TRequest> 
        where TValidator : AbstractValidator<TRequest>, new()
        where TRequest: class
    {

        /// <summary>
        /// Validates request body using mapped validator. Throws a <see cref="RequestValidationException"/> if validation fails that provides a 400 - Bad Request.
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="RequestValidationException"></exception>
        public void Validate(TRequest request)
        {
            TValidator validator = new TValidator();

            ValidationResult result = validator.Validate(request);

            if (!result.IsValid)
            {
                throw new RequestValidationException(result.Errors);
            }
        }
    }
}
