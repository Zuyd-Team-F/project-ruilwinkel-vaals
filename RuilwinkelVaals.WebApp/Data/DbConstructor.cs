using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace RuilwinkelVaals.WebApp.Data
{
    public class DbConstructor
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DbConstructor(IWebHostEnvironment environment, ApplicationDbContext context, ILogger<DbConstructor> logger)
        {
            _env = environment;
            _context = context;
            _logger = logger;
        }

        public async Task Init()
        {
            if(_env.IsDevelopment())
            {
                _context.Database.EnsureDeleted();
                _context.Database.Migrate();
                await DbSeeder.Init(_context);
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
