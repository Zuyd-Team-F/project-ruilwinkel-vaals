using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RuilwinkelVaals.WebApp.Classes.Services;
using RuilwinkelVaals.WebApp.Data;
using System;
using System.IO;

namespace RuilwinkelVaals.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitializeDB(host);
            InitializeImgHandler(host);

            host.Run();
        }

        public static void InitializeDB(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                DbConstructor dbInit = scope.ServiceProvider.GetRequiredService<DbConstructor>();
                dbInit.Init().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddScoped<DbConstructor>();
                });

        public static void InitializeImgHandler(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var imgHandler = scope.ServiceProvider.GetRequiredService<IImageHandler>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                var location = Path.Combine(env.WebRootPath, "img/storage");
                var storage = new DirectoryInfo(location).GetDirectories();

                if (env.IsDevelopment())
                {
                    // Disposes images for dev environment
                    imgHandler.DisposeImages(storage);
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred with the image storage handler.");
            }
        }
    }
}
