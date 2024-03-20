using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static BlazorMicroservices.Web.Utilities.SD;

namespace BlazorMicroservices.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public BaseService(IHttpClientFactory httpClientFactory, ProtectedLocalStorage protectedLocalStorage)
        {
            _httpClientFactory = httpClientFactory;
            _protectedLocalStorage = protectedLocalStorage;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("blazor-microservices-client");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //token

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                //response
                var apiResponse = await client.SendAsync(message);
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto { IsSuccessful = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto { IsSuccessful = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto { IsSuccessful = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSuccessful = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccessful = false,
                    Message = ex.Message.ToString()
                };
            }
        }
    }
}
