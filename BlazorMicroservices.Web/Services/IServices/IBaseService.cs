using BlazorMicroservices.Web.Utilities;

namespace BlazorMicroservices.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
