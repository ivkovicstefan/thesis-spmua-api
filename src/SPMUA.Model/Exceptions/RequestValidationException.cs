using FluentValidation.Results;
using SPMUA.Model.Commons.ErrorHandling;

namespace SPMUA.Model.Exceptions
{
    public class RequestValidationException : Exception
    {
        public List<Error> Errors { get; set; }

        public RequestValidationException(List<ValidationFailure> errors)
        {
            Errors = errors.Select(e => new Error(e.PropertyName, e.ErrorMessage)).ToList();
        }

        public RequestValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
            Errors = new();        
        }
    }
}
