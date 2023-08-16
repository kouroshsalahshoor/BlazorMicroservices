using BlazorServerApp.Services;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static BlazorServerApp.Infrastructure.ApplicationConstants;

namespace BlazorServerApp.Infrastructure
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProviderService _tokenProviderService;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProviderService tokenProviderService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProviderService = tokenProviderService;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BlazorMicroservicesApp");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = await _tokenProviderService.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", requestDto.Token);
                }

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (requestDto.ApiType)
                {
                    case ApiTypes.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiTypes.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiTypes.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

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
                        var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return responseDto;
                }
            }
            catch (Exception e)
            {
                return new ResponseDto
                {
                    IsSuccessful = false,
                    Message = e.Message,
                };
            }

        }
    }
}
