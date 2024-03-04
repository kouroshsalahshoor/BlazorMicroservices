using BlazorMicroservices.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMicroservices.Services.CouponApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            for (int i = 1; i <= 2; i++)
            {
                modelBuilder.Entity<Coupon>().HasData(new Coupon
                {
                    Id = i,
                    Code = (i * 10).ToString() + "off",
                    DiscountAmount = i * 10,
                    MinAmount = i * 10,
                });
            }
        }
    }
}
