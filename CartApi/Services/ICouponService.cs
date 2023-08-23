using CartApi.Data;

namespace CartApi.Services
{
    public interface ICouponService
    {
        Task<CouponDto> GetByCode(string code);
    }
}
