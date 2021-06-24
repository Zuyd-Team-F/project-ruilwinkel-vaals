using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using RuilwinkelVaals.WebApp.Classes;

namespace RuilwinkelVaals.Tests
{
    public class Tests
    {
        #region Integration Tests
        [Fact]
        public async Task UserTest()
        {
            // Creating the DB context
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            var context = new ApplicationDbContext(optionsBuilder.Options);

            // Renew Testing DB
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Ensure a role is available to appoint to
            context.Roles.Add(new Role("Test"));

            // Creating the Controller
            var controller = new UsersController(context);

            // Adding to the DB
            var user = DbSeeder.GenerateUser("John");
            await controller.Create(user);

            // Check if added correctly
            var result = (await controller.GetAll()).ToArray();
            Assert.Single(result);
            Assert.Equal("John", result[0].FirstName);
        }
        #endregion

        #region Unit Tests
        [Fact]
        public void ValidUserUnitTest()
        {
            // Checks if fields in user are not null
            UserData TestUser = new UserData() { FirstName = "Test", LastName = "User", Email = "testuser@test.com", PhoneNumber = "0123456789" };
            Assert.NotNull(TestUser.FirstName);
            Assert.NotNull(TestUser.LastName);
            Assert.NotNull(TestUser.Email);
            Assert.NotNull(TestUser.PhoneNumber);

        }
        #endregion
    }
}
