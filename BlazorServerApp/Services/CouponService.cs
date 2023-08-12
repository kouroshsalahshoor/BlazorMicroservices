using BlazorServerApp.Data;
using BlazorServerApp.Infrastructure;
using static BlazorServerApp.Infrastructure.SystemConstants;

namespace BlazorServerApp.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> Create(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.POST,
                Url = SystemConstants.CouponApiUrl + "api/coupons/",
                Data = couponDto
            });
        }

        public async Task<ResponseDto?> Delete(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.DELETE,
                Url = SystemConstants.CouponApiUrl + "api/coupons/" + id
            });
        }

        public async Task<ResponseDto?> GetAll()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.GET,
                Url = SystemConstants.CouponApiUrl + "api/coupons"
            });
        }

        public async Task<ResponseDto?> GetByCode(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.GET,
                Url = SystemConstants.CouponApiUrl + "api/coupons/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> GetById(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.GET,
                Url = SystemConstants.CouponApiUrl + "api/coupons/" + id
            });
        }

        public async Task<ResponseDto?> Update(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.PUT,
                Url = SystemConstants.CouponApiUrl + "api/coupons/",
                Data = couponDto
            });
        }
    }
}
