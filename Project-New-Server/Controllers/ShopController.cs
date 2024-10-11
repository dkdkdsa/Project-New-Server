using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

namespace Project_New_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {

        private const string URL_FORMAT = "https://docs.google.com/spreadsheets/d/{0}/export?format=tsv&gid={1}";
        private HttpClient _client;

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            _client = new HttpClient();
            var msg = await _client.GetAsync(string.Format(URL_FORMAT, "1WGoF0bZKSI7BTcpdeDqrX1B7qRVbazdopzW-kaZCiP8", 0));
            var str = await msg.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            
            return Ok();
        }

    }
}
