using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Exceptions
{
    public class RequestValidationException : Exception
    {
        public RequestValidationException(List<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public RequestValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
            Errors = new();
        }

        public List<ValidationFailure> Errors { get; set; }
    }
}
