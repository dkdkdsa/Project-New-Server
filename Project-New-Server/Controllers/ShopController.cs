using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_New_Server.Context;
using SharedData;
using System.Net;
using System.Reflection.Metadata;

namespace Project_New_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {

        private const string SQL_QUERY_FIND_USER_ID = "Select * From Users Where Id = {0}";
        private readonly UserContext _context;

        public ShopController(UserContext context)
        {

            _context = context;

        }

        [HttpPost("BuyTower")]
        public async Task<IActionResult> BuyTower([FromBody] ShopData data)
        {

            var playerId = HttpContext.Items["PlayerId"].ToString();

            Console.WriteLine(data == null ? "NULL" : data);

            var obj = await _context.Users
                .FromSqlRaw(SQL_QUERY_FIND_USER_ID, playerId)
                .FirstOrDefaultAsync();

            if (obj == null || !ShopContainer.Container.ContainsKey(data.Name))
                return BadRequest();

            //Check
            if(obj.Coin < ShopContainer.Container[data.Name] || obj.Towers.Contains(data.Name))
                return Unauthorized();

            obj.Coin -= ShopContainer.Container[data.Name];
            obj.Towers.Add(data.Name);

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpGet("GetTowerShopInfo")]
        public IActionResult GetTowerShopInfo()
        {

            List<ShopData> ls = new();

            foreach(var item in ShopContainer.Container)
            {
                ls.Add(new ShopData { Name = item.Key, Price = item.Value });
            }

            return new JsonResult(new ShopInfo { Elements = ls });

        }

    }
}
