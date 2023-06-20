using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Commons.ErrorHandling
{
    public class Error
    {
        public string FieldName { get; set; } = String.Empty;
        public string ErrorMessage { get; set; } = String.Empty;

        public Error(string fieldName, string errorMessage)
        {
            FieldName = fieldName;
            ErrorMessage = errorMessage;
        }
    }
}
