using Microsoft.EntityFrameworkCore;

namespace SaleSampleAPI.Data
{
    public class SalesContext : DbContext
    {

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }

        public DbSet<SaleSampleAPI.Models.Sale> Sales { get; set; }
        public DbSet<SaleSampleAPI.Models.Product> Product { get; set; } 
        public DbSet<SaleSampleAPI.Models.SaleProducts> SaleProducts { get; set; } 
        public DbSet<SaleSampleAPI.Models.TaxesRegion> TaxesRegigons { get; set; }
    }
}
