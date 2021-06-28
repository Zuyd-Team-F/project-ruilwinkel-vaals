using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.WebApp.Data
{
    public class DbConstructor
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public DbConstructor(IWebHostEnvironment environment, ApplicationDbContext context, ILogger<DbConstructor> logger, IConfiguration configuration)
        {
            _env = environment;
            _context = context;
            _logger = logger;
            _config = configuration;
        }

        public async Task Init()
        {
            if(_env.IsDevelopment())
            {
                UserData devUser = new()
                {
                    FirstName = "Developer",
                    LastName = "Zuyd",
                    UserName = _config.GetSection("DevCredentials")["User"],
                    Email = _config.GetSection("DevCredentials")["User"],
                    City = "Dev City",
                    EmailConfirmed = true,
                    PostalCode = "6666TE",
                    Street = "Test Avenue",
                    StreetNumber = 1,
                    PhoneNumber = "123456789",
                    Balance = 0
                };

                devUser.PasswordHash = new PasswordHasher<UserData>()
                    .HashPassword(devUser, _config.GetSection("DevCredentials")["Password"]);

                _context.Database.EnsureDeleted();
                _context.Database.Migrate();

                await DbSeeder.Init(_context, _env, devUser);
            }
            else
            {
                _context.Database.Migrate();

                if(!_context.Roles.Any())
                {
                    await DbSeeder.Init(_context);
                }
            }
        }
    }
}
