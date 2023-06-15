using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.DTOs.EmailTemplate
{
    public class EmailTemplateDTO
    {
        public string EmailTemplateTitle { get; set; } = String.Empty;
        public string EmailTemplateHtml { get; set; } = String.Empty;
    }
}
