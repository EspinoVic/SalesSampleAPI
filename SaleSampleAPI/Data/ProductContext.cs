using Microsoft.EntityFrameworkCore;
namespace SaleSampleAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<SaleSampleAPI.Models.Product> Product { get; set; } = default!;

    }
}
