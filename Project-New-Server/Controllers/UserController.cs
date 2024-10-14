using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_New_Server.Context;
using Project_New_Server.Model;
using SharedData;

namespace Project_New_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private const string SQL_QUERY_FIND_USER_ID = "Select * From Users Where Id = {0}";
        private readonly UserContext _context;

        public UserController(UserContext context) 
        { 
            
            _context = context;

        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser()
        {

            var playerId = HttpContext.Items["PlayerId"].ToString();

            var obj = await _context.Users
                .FromSqlRaw(SQL_QUERY_FIND_USER_ID, playerId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (obj != null)
                return BadRequest();

            try
            {

                await _context.Users.AddAsync(new UserData(playerId));
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetUserData()
        {

            var playerId = HttpContext.Items["PlayerId"].ToString();

            var obj = await _context.Users
                .FromSqlRaw(SQL_QUERY_FIND_USER_ID, playerId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (obj == null)
                return Unauthorized("Please Add User");

            return new JsonResult(CreateUserInfo(obj));

        }

        private UserInfo CreateUserInfo(UserData user)
        {

            return new UserInfo
            {

                Coin = user.Coin,
                Jam = user.Jam,
                Towers = user.Towers,
                Decks = user.Decks,

            };

        }

    }
}
