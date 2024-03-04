using AutoMapper;
using BlazorMicroservices.Services.CouponApi.Data;
using BlazorMicroservices.Services.CouponApi.Models;
using BlazorMicroservices.Services.CouponApi.Models.Dtos;
using BlazorMicroservices.Services.CouponApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMicroservices.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public CouponApiController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var result = _db.Coupons.ToList();
                _response.Result = _mapper.Map<List<CouponDto>>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
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
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("getbycode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                var result = _db.Coupons.First(x => x.Code.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto dto)
        {
            try
            {
                var model = _mapper.Map<Coupon>(dto);
                var result = _db.Coupons.Add(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(result.Entity);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto dto)
        {
            try
            {
                var model = _mapper.Map<Coupon>(dto);
                var result = _db.Coupons.Update(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(result.Entity);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var model = _db.Coupons.First(x => x.Id == id);
                var result = _db.Coupons.Remove(model);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(result.Entity);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
