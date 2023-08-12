using BlazorServerApp.Data;

namespace BlazorServerApp.Services
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
