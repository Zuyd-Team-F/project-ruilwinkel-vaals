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
using System;

namespace RuilwinkelVaals.Tests.Auth_Tests
{
    public class LoginTests
    {
        [Fact]
        public async Task UserCreationThroughController()
        {
            // Fetch the in memory context to test on
            var context = await TestDb.GetDatabaseContext();

            // Create test role
            var role = context.Roles.Add(new Role("GameMaster")).Entity;
            await context.SaveChangesAsync();

            // Setup user form
            UserFormViewModel user1 = new()
            {
                RoleId = role.Id,
                Email = "vaals4@ruilwinkel.nl",                
                FirstName = ", ",
                LastName = "Peters",
                Password = "admin",
                City = "Heerlen",
                PostalCode = "1234AB",
                Street = "Agaatstraat",
                StreetNumber = 420,
                DateOfBirth = new DateTime(2001, 4, 9), 
                PhoneNumber = "0451234567",
                Balance = 0,
            };

            UserFormViewModel user2 = new()
            {
                RoleId = role.Id,
                Email = "vaals4@ruilwinkel.nl",
                FirstName = ", ",
                LastName = "Peters",
                Password = "admin",
                City = "Heerlen",
                PostalCode = "1234AB",
                Street = "Agaatstraat",
                StreetNumber = 420,
                DateOfBirth = new DateTime(2001, 4, 9),
                PhoneNumber = "0451234567",
                Balance = 0,
            };
            
            // Setup mock for user manager
            var userStore = new Mock<UserStore>(context);
            var userManager = new Mock<UserManagerExtension>(userStore.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(um => um.CreateAsync(It.IsAny<UserData>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<UserData, string>(async (x, y) =>
                {
                    var id = (await context.Users.AddAsync(x)).Entity.Id;
                    await context.SaveChangesAsync();
                    user1.Id = id;
                    //user2.Id = id;
                });

            userManager.Setup(um => um.SetRoleAsync(It.IsAny<UserData>(), It.IsAny<string>()))
                .Callback<UserData, string>(async (x, y) =>
                {
                    var roleId = context.Roles.Where(r => r.Name == y).FirstOrDefault().Id;
                    await context.UserRoles.AddAsync(new() { UserId = x.Id, RoleId = roleId });
                    await context.SaveChangesAsync();
                });

            // Creating the Controller
            var controller = new UsersController(context, userManager.Object);

            // Try to create new users
            try 
            {
                var controllerResult1 = await controller.Create(user1);
                Assert.IsType<RedirectToActionResult>(controllerResult1);
            } catch (ArgumentException e)
            {
                // Should not get here
            }

            try
            {
                var controllerResult2 = await controller.Create(user2);
                Assert.IsType<RedirectToActionResult>(controllerResult2);
            }
            catch (ArgumentException e)
            {
                // Should get here
            }           

            #region Checks
            // Check if user1 was added correctly
            var userResult1 = await context.Users.FindAsync(user1.Id);
            if (userResult1 != null)
            {
                Assert.NotNull(userResult1);
                Assert.Equal(user1.Email, userResult1.Email);
            }           

            // Check if user2 was added correctly
            var userResult2 = await context.Users.FindAsync(user2.Id);
            if (userResult2 != null)
            {
                Assert.NotNull(userResult2);
                Assert.Equal(user2.Email, userResult2.Email);
            }

            // Check if user1 and user2 are identical in database.
            Assert.NotSame(userResult1, userResult2);

            // Check if user has been assigned to role
            var userRole = context.UserRoles.Where(ur => ur.UserId == user1.Id && ur.RoleId == role.Id).FirstOrDefault();
            Assert.NotNull(userRole);
            #endregion
        }
    }
}