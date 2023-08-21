using BlazorServerApp.Data;
using BlazorServerApp.Infrastructure;

namespace BlazorServerApp.Services
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAll();
        Task<ResponseDto?> GetById(int id);
        Task<ResponseDto?> GetByCode(string couponCode);
        Task<ResponseDto?> Create(CouponDto dto);
        Task<ResponseDto?> Update(CouponDto dto);
        Task<ResponseDto?> Delete(int id);
    }
}
