using BlazorMicroservices.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMicroservices.Services.CouponApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
