using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Exceptions
{
    public class UnauthorizedRequestException : Exception
    {
        public UnauthorizedRequestException(string? message) : base(message)
        {
        }
    }
}
