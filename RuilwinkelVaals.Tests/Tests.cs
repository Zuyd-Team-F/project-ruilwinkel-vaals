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
using System.Linq;

namespace RuilwinkelVaals.Tests
{
    public class Tests
    {
        [Fact]
        public async Task UserCreationThroughController()
        {
            // Fetch the in memory context to test on
            var context = await TestDb.GetDatabaseContext();

            // Create test role
            var role = context.Roles.Add(new("TestRole")).Entity;
            await context.SaveChangesAsync();

            // Setup user form
            UserFormViewModel user = new()
            {
                RoleId = role.Id,
                Email = "test@test.nl",
                FirstName = "TestUser",
                LastName = "Test",
                Password = "Werty123!",
                City = "TestCity",
                PostalCode = "1111TE",
                Street = "TestStreet",
                StreetNumber = 11,
                DateOfBirth = System.DateTime.Now,
                PhoneNumber = "123456789",
                Balance = 1
            };

            // Setup mock for user manager
            var userStore = new Mock<UserStore>(context);
            var userManager = new Mock<UserManagerExtension>(userStore.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(um => um.CreateAsync(It.IsAny<UserData>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<UserData, string>(async (x,y) =>
                {
                    var id = (await context.Users.AddAsync(x)).Entity.Id;
                    await context.SaveChangesAsync();
                    user.Id = id;
                });

            userManager.Setup(um => um.SetRoleAsync(It.IsAny<UserData>(), It.IsAny<string>()))
                .Callback<UserData,string>(async (x,y) =>
                {
                    var roleId = context.Roles.Where(r => r.Name == y).FirstOrDefault().Id;
                    await context.UserRoles.AddAsync(new() { UserId = x.Id, RoleId = roleId });
                    await context.SaveChangesAsync();
                });

            // Creating the Controller
            var controller = new UsersController(context, userManager.Object);

            // Let controller create user
            var controllerResult = await controller.Create(user);
            Assert.IsType<RedirectToActionResult>(controllerResult);

            // Check if role was added correctly
            var roleResult = await context.Roles.FindAsync(role.Id);
            Assert.NotNull(roleResult);
            Assert.Equal(role.Name, roleResult.Name);

            // Check if user was added correctly
            var userResult = await context.Users.FindAsync(user.Id);
            Assert.NotNull(userResult);
            Assert.Equal(user.FirstName, userResult.FirstName);

            // Check if user has been assigned to role
            var userRole = context.UserRoles.Where(ur => ur.UserId == user.Id && ur.RoleId == role.Id).FirstOrDefault();
            Assert.NotNull(userRole);
        }
    }
}
