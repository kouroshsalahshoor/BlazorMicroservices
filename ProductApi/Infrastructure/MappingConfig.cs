using AutoMapper;
using ProductApi.Data;

namespace ProductApi.Infrastructure
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMapps()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
            });
            return mapperConfiguration;
        }
    }
}
