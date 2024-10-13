
namespace Project_New_Server.Middleware
{
    public class AuthMiddleware : IMiddleware
    {

        private readonly IConfiguration _config;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            

        }

    }
}
