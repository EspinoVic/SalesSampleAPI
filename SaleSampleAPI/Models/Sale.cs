using System.ComponentModel.DataAnnotations.Schema;

namespace SaleSampleAPI.Models
{
    [Table("Sale")] //Names infered
    public class Sale
    {
        public int Id { get; set; }
        public DateTime dtSale { get; set; }
        public int taxesRegion { get; set; }
    }
}
