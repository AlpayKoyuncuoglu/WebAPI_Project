using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Products4 { get; set; }
    }
}
