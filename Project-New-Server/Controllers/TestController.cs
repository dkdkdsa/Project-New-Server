using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Project_New_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IDistributedCache _db;

        public TestController(IDistributedCache db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // 10분간 캐싱
            };
            _db.SetStringAsync("1", "김성호는 나다", options);
            return Ok("김성호는 누구");

        }

        [HttpGet("E")]
        public async Task<ActionResult> Testing()
        {

            var str = await _db.GetStringAsync("1");
            return Ok(str);

        }

    }
}
