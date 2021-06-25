using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RuilwinkelVaals.WebApp.Data;
using System;
using System.Threading.Tasks;

namespace RuilwinkelVaals.Tests
{
    public static class TestDb
    {
        public static async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            await databaseContext.Database.EnsureCreatedAsync();
            return databaseContext;
        }
    }    
}
