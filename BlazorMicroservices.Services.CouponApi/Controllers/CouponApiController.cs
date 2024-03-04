using BlazorMicroservices.Services.CouponApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMicroservices.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CouponApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                return _db.Coupons.ToList();
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        [HttpGet("{id:int}")]
        public object Get(int id)
        {
            try
            {
                return _db.Coupons.First(x=> x.Id == id);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
