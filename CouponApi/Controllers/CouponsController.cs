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

        public CouponsController(ApplicationDbContext db, ILogger<CouponsController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet]
        public object Get()
        {
            try
            {
                return _db.Coupons.ToList();
            }
            catch (Exception e)
            {
            }
            return null;
        }
        [HttpGet("{id:int}")]
        public object Get(int id)
        {
            try
            {
                return _db.Coupons.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}
