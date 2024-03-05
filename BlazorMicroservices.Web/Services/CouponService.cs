using BlazorMicroservices.Web.Dtos;
using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities;

namespace BlazorMicroservices.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.Get,
                Url = SD.CouponApiBase
            });
        }
        public async Task<ResponseDto?> GetAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.Get,
                Url = SD.CouponApiBase  + id.ToString()
            });
        }
        public async Task<ResponseDto?> GetByCodeAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.Get,
                Url = SD.CouponApiBase + "getbycode/" + code
            });
        }
        public async Task<ResponseDto?> CreateAsync(CouponDto dto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.CouponApiBase,
                Data = dto
            });
        }
        public async Task<ResponseDto?> UpdateAsync(CouponDto dto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.CouponApiBase,
                Data = dto
            });
        }
        public async Task<ResponseDto?> DeleteAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBase + id.ToString()
            });
        }
    }
}
