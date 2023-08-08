using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleSampleAPI.Models
{
    [Table("TaxesRegion")]
    public class TaxesRegion
    {

        [Key,Column("id")]
        public int RegionId { get; set; }
        [Column("taxesRegion")]
        public string TaxRegion { get; set; }
        [Column("percentage")]
        public int Percentage { get; set; }
       
    }
}
