using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleSampleAPI.Models
{
    [Table("TaxesRegion")]
    public class TaxesRegion
    {
        [Key]
        public int RegionId { get; set; } 
        public string TaxRegion { get; set; }
        public int Percentage { get; set; }
       
    }
}
