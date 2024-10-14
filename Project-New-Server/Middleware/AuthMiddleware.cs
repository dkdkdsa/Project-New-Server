
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Project_New_Server.Middleware
{
    public class AuthMiddleware
    {

        private readonly IConfiguration _config;
        private readonly RequestDelegate _next;
        private JsonWebKeySet _jwks;

        public AuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
            Task.Run(Init);
        }

        private void Init()
        {
            while(true)
            {

                InitKeys();
                Task.Delay(10000);

            }
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if(_jwks == null)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }

            if (!context.Request.Headers.ContainsKey("AccessToken"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var accessToken = context.Request.Headers["AccessToken"].ToString();
            var kid = GetKid(accessToken);
            var sck = GetSecurityKey(kid);
            if(ValidateToken(accessToken, sck))
            {

                var hander = new JwtSecurityTokenHandler();
                var t = hander.ReadJwtToken(accessToken);
                var claim = t.Claims.FirstOrDefault(x => x.Type == "sub");

                if(claim == null)
                {
                    context.Response.StatusCode= StatusCodes.Status401Unauthorized;
                    return;
                }

                var playerId = claim.Value;
                context.Items.Add("PlayerId", playerId);
                await _next(context);

            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

        }

        private bool ValidateToken(string token, SecurityKey key)
        {

            var handler = new JwtSecurityTokenHandler();
            var param = new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(10)
            };

            try
            {
                handler.ValidateToken(token, param, out var _);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        private SecurityKey GetSecurityKey(string kid)
        {
            var key = _jwks.Keys.FirstOrDefault(x => x.Kid == kid);
            if (key == null)
                return null;

            return key;
        }

        private string GetKid(string token)
        {
            var hander = new JwtSecurityTokenHandler();
            var readToken = hander.ReadJwtToken(token);
            return readToken.Header["kid"].ToString();
        }

        private async void InitKeys()
        {

            HttpClient client = new HttpClient();
            var res = await client.GetAsync("https://player-auth.services.api.unity.com/.well-known/jwks.json");
            string json = await res.Content.ReadAsStringAsync();
            _jwks = new JsonWebKeySet(json);

        }

    }
}
