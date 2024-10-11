using Microsoft.EntityFrameworkCore;

namespace Project_New_Server
{
    public static class DbInitializer
    {

        public static void Initialize<T>(T context) where T : DbContext
        {

            context.Database.EnsureCreated();

        }

    }
}
