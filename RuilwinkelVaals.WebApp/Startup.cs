using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.IO;

namespace RuilwinkelVaals.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _env = environment;
            Configuration = configuration;

            //Clears the images folder in dev environment
            if (_env.IsDevelopment())
            {
                var location = Path.Combine(_env.WebRootPath, "img/products");
                var folder = new DirectoryInfo(location);
                foreach (FileInfo file in folder.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString: Configuration.GetConnectionString("DefaultConnection"),
                    serverVersion: MySqlServerVersion.LatestSupportedServerVersion,
                    mySqlOptionsAction: mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: System.TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null
                        );
                    }
                )
            );    

            services.AddIdentity<UserData, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManagerExtension>()                
                .AddDefaultUI()
                .AddDefaultTokenProviders();
                     
            if (_env.IsDevelopment())
            {
                services.Configure<SecurityStampValidatorOptions>(options =>
                {
                    // enables immediate logout, after updating the user's security stamp.
                    options.ValidationInterval = TimeSpan.Zero;
                });
            }

            services.AddScoped<IImageHandler, ImageHandler>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Traefik will manage Https
            // If Traefik is to be used, add this: app.UseHttpsRedirection(); 

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
