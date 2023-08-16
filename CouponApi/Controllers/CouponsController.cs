using AutoMapper;
using CouponApi.Data;
using CouponApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponApi.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    [Authorize]
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
                var result = _db.Coupons.First(x => x.Id == id);
                _response.Result = _mapper.Map<CouponDto>(result);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }
        [HttpGet("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                var result = _db.Coupons.First(x => x.Code.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(result);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto dto)
        {
            try
            {
                var model = _mapper.Map<Coupon>(dto);
                _db.Coupons.Add(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(model);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto dto)
        {
            try
            {
                var model = _mapper.Map<Coupon>(dto);
                _db.Coupons.Update(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(model);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var model = _db.Coupons.First(x=> x.Id == id);
                _db.Coupons.Remove(model);
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
