using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleSampleAPI.Models
{
    [Table("Product")] //Names infered
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column("productName")] //Column name inferred
        public string ProductName { get; set; }

        public double Cost { get; set; }

        //public float SalesPrice { get; set; }
        public double SalesPrice { get; set; }

        /*      The float type in C# maps to the real column type in SQL:
      SQL Server Data Type Mappings - ADO.NET | Microsoft Docs[^]

      Confusingly, the float data type in SQL maps to the double type in C#.

      You'll need to modify your C# property to use the correct type that maps to the SQL column type.
      */
        public int InventoryAmount { get; set; }

    }
}
