using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Models
{
    public class ServiceTypePriceHistory
    {
        public int ServiceTypePriceHistoryId { get; set; }
        public int ServiceTypeId { get; set; }
        public decimal ServiceTypePrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
