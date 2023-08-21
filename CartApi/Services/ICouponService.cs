using CartApi.Data;

namespace CartApi.Services
{
    public interface ICouponService
    {
        Task<CouponDto> Get(string code);
    }
}
