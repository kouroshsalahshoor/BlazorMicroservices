using AutoMapper;
using ProductApi.Data;
using ProductApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private ApplicationDbContext _db { get; set; }
        private readonly ILogger<ProductsController> _logger;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductsController(ApplicationDbContext db, ILogger<ProductsController> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _response = new();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var result = _db.Products.ToList();
                _response.Result = _mapper.Map<List<ProductDto>>(result);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }
        [HttpGet("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var result = _db.Products.First(x => x.Id == id);
                _response.Result = _mapper.Map<ProductDto>(result);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ResponseDto Post([FromBody] ProductDto dto)
        {
            try
            {
                var model = _mapper.Map<Product>(dto);
                _db.Products.Add(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(model);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "Admins")]
        public ResponseDto Put([FromBody] ProductDto dto)
        {
            try
            {
                var model = _mapper.Map<Product>(dto);
                _db.Products.Update(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(model);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admins")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var model = _db.Products.First(x => x.Id == id);
                _db.Products.Remove(model);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }
    }
}
