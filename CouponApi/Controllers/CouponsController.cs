using AutoMapper;
using CouponApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private ApplicationDbContext _db { get; set; }
        private readonly ILogger<CouponsController> _logger;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponsController(ApplicationDbContext db, ILogger<CouponsController> logger, IMapper mapper)
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
                var result =_db.Coupons.ToList();
                _response.Result = _mapper.Map<List<CouponDto>>(result);
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
                var result = _db.Coupons.FirstOrDefault(x => x.Id == id);
                _response.Result = _mapper.Map<CouponDto>(result);
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
