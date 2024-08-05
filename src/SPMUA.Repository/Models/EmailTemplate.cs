using System.ComponentModel.DataAnnotations;

namespace SPMUA.Repository.Models
{
    public class EmailTemplate
    {
        public int EmailTemplateId { get; set; }
        [MaxLength(50)]
        public string EmailTemplateName { get; set; } = null!;
        [MaxLength(100)]
        public string EmailTemplateTitle { get; set; } = null!;
        public string EmailTemplateHtml { get; set; } = null!;
    }
}
