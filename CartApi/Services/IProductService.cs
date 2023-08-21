using CartApi.Data;

namespace CartApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
    }
}
