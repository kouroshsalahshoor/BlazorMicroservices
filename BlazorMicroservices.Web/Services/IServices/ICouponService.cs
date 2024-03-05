using BlazorMicroservices.Web.Dtos;
using BlazorMicroservices.Web.Utilities;

namespace BlazorMicroservices.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllAsync();
        Task<ResponseDto?> GetAsync(int id);
        Task<ResponseDto?> GetByCodeAsync(string code);
        Task<ResponseDto?> CreateAsync(CouponDto dto);
        Task<ResponseDto?> UpdateAsync(CouponDto dto);
        Task<ResponseDto?> DeleteAsync(int id);
    }
}
