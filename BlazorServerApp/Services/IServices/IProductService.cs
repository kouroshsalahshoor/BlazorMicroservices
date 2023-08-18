using BlazorServerApp.Data;
using BlazorServerApp.Infrastructure;

namespace BlazorServerApp.Services
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAll();
        Task<ResponseDto?> GetById(int id);
        Task<ResponseDto?> Create(ProductDto dto);
        Task<ResponseDto?> Update(ProductDto dto);
        Task<ResponseDto?> Delete(int id);
    }
}
