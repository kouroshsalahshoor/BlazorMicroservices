using System.ComponentModel.DataAnnotations;

namespace BlazorMicroservices.Services.CouponApi.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required] 
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
