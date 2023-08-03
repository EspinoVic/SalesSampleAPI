using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleSampleAPI.Models
{
    [Table("Sale_Product")] //Names infered
    [Keyless]
    public class SaleProducts
    {
        public int idSale { get; set; }
        public int idProduct { get; set; }
        public DateTime dtAdded { get; set; }
        public int amount { get; set; }

    }
}
