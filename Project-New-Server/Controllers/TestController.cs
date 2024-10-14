using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

namespace Project_New_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {


        [HttpPost("A")]
        public IActionResult Testssssss([FromBody] string id)
        {

            var hander = new JwtSecurityTokenHandler();
            var token = hander.ReadJwtToken(id);
            return Ok(token.Claims.FirstOrDefault(x=> x.Type == "sub"));

        }


    }
}
