using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.Classes;
using System.Threading.Tasks;
using Xunit;
using RuilwinkelVaals.WebApp.ViewModels.Users;
using Moq;
using RuilwinkelVaals.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.IdentityOverrides;

namespace RuilwinkelVaals.Tests
{
    public class Tests
    {
        [Fact]
        public async Task UserTest()
        {
            // Fetch the in memory context to test on
            var context = await TestDb.GetDatabaseContext();

            // Setup mock for user manager
            var userStore = new Mock<UserStore>(context);
            var userManager = new Mock<UserManagerExtension>(userStore.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(um => 
                um.CreateAsync(It.IsAny<UserData>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<UserData, string>((x,y) =>
                {
                    context.Users.Add(x);
                    context.SaveChangesAsync();
                });

            var role = context.Roles.Add(new("TestRole"));
            await context.SaveChangesAsync();

            // Creating the Controller
            var controller = new UsersController(context, userManager.Object);

            // Generate random user
            UserFormViewModel user = new()
            {
                RoleId = role.Entity.Id,
                Email = "test@test.nl",
                FirstName = "TestUser",
                LastName = "",
                Password = "Werty123!",
                City = "TestCity",
                PostalCode = "1111TE",
                Street = "TestStreet",
                StreetNumber = 11,
                DateOfBirth = System.DateTime.Now,
                PhoneNumber = "123456789",
                Balance = 1,
            };

            // Let controller create user
            var controllerResult = await controller.Create(user);
            Assert.IsType<RedirectToActionResult>(controllerResult);

            // Check if added correctly
            var result = context.Roles.Find(role.Entity.Id);
            Assert.NotNull(result);
            Assert.Equal(role.Entity.Name, result.Name);                        
        }

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
    }
}
