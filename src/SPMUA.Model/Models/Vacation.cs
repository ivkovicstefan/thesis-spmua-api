using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMUA.Model.Models
{
    public class Vacation
    {
        public int VacationId { get; set; }
        [MaxLength(20)]
        public string VacationName { get; set; } = null!;
        public DateTime VacationStartDate { get; set; }
        public DateTime VacationEndDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
