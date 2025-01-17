
using Microsoft.EntityFrameworkCore;
using Project_New_Server.Context;
using Project_New_Server.Middleware;

namespace Project_New_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ShopContainer.SetUp();

            var builder = WebApplication.CreateBuilder(args);

            SetUpBuilder(builder);

            var app = builder.Build();

            SetUpApp(app);

            app.Run();

        }

        private static void SetUpBuilder(WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();
            builder.Services.AddAuthentication();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserContext>(x =>
            {

                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

        }

        private static void SetUpApp(WebApplication app)
        {

            CreateDbIfNotExists(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
            {
                appBuilder.UseMiddleware<AuthMiddleware>();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {

                    //User
                    {

                        var context = services.GetRequiredService<UserContext>();
                        DbInitializer.Initialize(context);

                    }


                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

    }
}
