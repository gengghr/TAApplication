using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PS4.Areas.Identity.Data;
using PS4.Data;
using Microsoft.Extensions.DependencyInjection;

namespace PS4
{
    public class Program
    {
        public static void Main(string[] args)
        {
 
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<TAUsersRolesDB>();
                    var um = services.GetRequiredService<UserManager<TAUser>>();
                    var rm = services.GetRequiredService<RoleManager<IdentityRole>>();
                    SeedUsersRolesDB.InitializeAsync(db, um, rm);

                    var context = services.GetRequiredService<TAAPPContext>();
                    DBInitializer.Initialize(context, um, rm);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<TAUsersRolesDB>();
                    var um = services.GetRequiredService<UserManager<TAUser>>();
                    var rm = services.GetRequiredService<RoleManager<IdentityRole>>();
                    SeedUsersRolesDB.InitializeAsync(db, um, rm);
                    var context = services.GetRequiredService<TAAPPContext>();
                    DBInitializer.Initialize(context, um, rm);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
