using BlazorServerApp.Data;
using BlazorServerApp.Infrastructure;

namespace BlazorServerApp.Services
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAll();
        Task<ResponseDto?> GetById(int id);
        Task<ResponseDto?> GetByCode(string couponCode);
        Task<ResponseDto?> Create(CouponDto couponDto);
        Task<ResponseDto?> Update(CouponDto couponDto);
        Task<ResponseDto?> Delete(int id);
    }
}
