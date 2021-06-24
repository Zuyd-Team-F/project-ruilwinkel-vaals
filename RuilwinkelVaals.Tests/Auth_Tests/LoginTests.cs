using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RuilwinkelVaals.Tests.Auth_Tests
{
    public class LoginTests
    {
        #region LogOn
        [Fact]
        public async Task LogOnTest()
        {
            // Creating the DB context
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            var testDatabase = new ApplicationDbContext(optionsBuilder.Options);

            // Renew Testing DB
            await testDatabase.Database.EnsureDeletedAsync();
            await testDatabase.Database.EnsureCreatedAsync();

            // Ensure a role is available to appoint to
            testDatabase.Roles.Add(new Role { Name = "GameMaster" });

            // Creating the Controller
            var controller = new UsersController(testDatabase);

            // Adding to the DB
            await controller.Create(testDatabase.Users.Add(new UserData()
            {
                RoleId = 1,
                Password = HashEvent.hashPassword("admin"),
                FirstName = "kommaSpatie",
                LastName = "Peters",
                DateOfBirth = new DateTime(2001, 4, 9),
                Street = "Agaatstraat",
                StreetNumber = 420,
                StreetAdd = "B",
                PostalCode = "1234AB",
                City = "Heerlen",
                Email = "vaals4@ruilwinkel.nl",
                Phone = 0451234567,
                Balance = 0,
                Blacklist = false
            }));

            // Check if added correctly
            var result = (await controller.GetAll()).ToArray();
            Assert.Single(result);
            Assert.Equal("kommaSpatie", result[0].FirstName);
        }
        #endregion

    }
}