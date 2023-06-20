using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Commons.ErrorHandling
{
    public class ErrorDetails
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
