using AutoMapper;
using CartApi.Data;

namespace CartApi.Infrastructure
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMapps()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Cart, CartDto>().ReverseMap();
                config.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
            });
            return mapperConfiguration;
        }
    }
}
