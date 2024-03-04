using AutoMapper;
using BlazorMicroservices.Services.CouponApi.Models;
using BlazorMicroservices.Services.CouponApi.Models.Dtos;

namespace BlazorMicroservices.Services.CouponApi.Utilities
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
