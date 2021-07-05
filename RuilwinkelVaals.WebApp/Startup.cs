using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using NToastNotify;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Classes.Services;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.IO;

namespace RuilwinkelVaals.WebApp
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration, 
            IWebHostEnvironment environment
        )
        {
            _env = environment;
            Configuration = configuration;
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

            services.AddSingleton<IImageHandler, ImageHandler>();

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.BottomLeft
            });

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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpMetrics();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapMetrics();
            });
        }
    }
}
