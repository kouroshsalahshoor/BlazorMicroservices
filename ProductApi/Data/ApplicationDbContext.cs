using Microsoft.EntityFrameworkCore;

namespace ProductApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            for (int i = 1; i <= 4; i++)
            {
                modelBuilder.Entity<Product>().HasData(new Product
                {
                    Id = i,
                    Name = $"Product Name {i}",
                    Description = $"Description for Product {i}",
                    Price = i * 10,
                    ImageUrl ="",
                    CategoryName = $"Category {(i%2) + 1}"
                });
            }
        }
    }
}
