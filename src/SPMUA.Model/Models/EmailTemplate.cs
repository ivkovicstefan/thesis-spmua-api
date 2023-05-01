﻿using System.ComponentModel.DataAnnotations;

namespace SPMUA.Model.Models
{
    public class EmailTemplate
    {
        public int EmailTemplateId { get; set; }
        [MaxLength(30)]
        public string EmailTemplateName { get; set; } = null!;
        [MaxLength(100)]
        public string EmailTemplateTitle { get; set; } = null!;
        public string EmailTemplateHtml { get; set; } = null!;
    }
}
