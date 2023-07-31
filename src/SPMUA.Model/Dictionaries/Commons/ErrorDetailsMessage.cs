using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Dictionaries.Commons
{
    public static class ErrorDetailsMessage
    {
        public static string InternalServerErrorTitle { get; } = "Internal Server Error";
        public static string InternalServerErrorDescription { get; } = "Some error occurred while processing the request.";
        public static string ValidationErrorTitle { get; } = "Validation Error";
        public static string ValidationErrorDescription { get; } = "One or more values entered are not valid.";
        public static string InvalidCredentialsErrorTitle { get; } = "Invalid Credentials Error";
        public static string InvalidCredentialsErrorDesciption { get; } = "Email or password is incorrect.";
        public static string EntityNotFoundErrorTitle { get; } = "Entity Not Found Error";
        public static string EntityNotFoundErrorDescription { get; } = "Entity with Id {0} does not exist.";
    }
}
