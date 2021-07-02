using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.ViewModels.Users;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RuilwinkelVaals.Tests.Auth_Tests
{
    public class LoginTests
    {
        #region CreateUser
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
                Password = "admin",
                FirstName = ", ",
                LastName = "Peters",
                DateOfBirth = new DateTime(2001, 4, 9),
                Street = "Agaatstraat",
                StreetNumber = 420,
                StreetAdd = "B",
                PostalCode = "1234AB",
                City = "Heerlen",
                Email = "vaals4@ruilwinkel.nl",
                PhoneNumber = "0451234567",
                Balance = 0,
            };

            UserFormViewModel user2 = new()
            {
                RoleId = role.Id,
                Password = "admin",
                FirstName = ", ",
                LastName = "Peters",
                DateOfBirth = new DateTime(2001, 4, 9),
                Street = "Agaatstraat",
                StreetNumber = 420,
                StreetAdd = "B",
                PostalCode = "1234AB",
                City = "Heerlen",
                Email = "vaals4@ruilwinkel.nl",
                PhoneNumber = "0451234567",
                Balance = 0,
            };

            #region MockSetup
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
                });

            userManager.Setup(um => um.SetRoleAsync(It.IsAny<UserData>(), It.IsAny<string>()))
                .Callback<UserData, string>(async (x, y) =>
                {
                    var roleId = context.Roles.Where(r => r.Name == y).FirstOrDefault().Id;
                    await context.UserRoles.AddAsync(new() { UserId = x.Id, RoleId = roleId });
                    await context.SaveChangesAsync();
                });
            #endregion

            // Creating the Controller
            var controller = new UsersController(context, userManager.Object);

            // Let controller create user
            var controllerResult1 = await controller.Create(user1);
            bool isSucces = false;
            try
            {
                var controllerResult2 = await controller.Create(user2);
            }
            catch (ArgumentException e)
            {
                isSucces = true;
            }

            #region Checks
            // Check if role was added correctly
            var roleResult = await context.Roles.FindAsync(role.Id);
            Assert.NotNull(roleResult);
            Assert.Equal(role.Name, roleResult.Name);

            // Check if user was added correctly
            var userResult1 = await context.Users.FindAsync(user1.Id);
            Assert.NotNull(userResult1);
            Assert.Equal(user1.FirstName, userResult1.FirstName);

            // Check if second user is not added, because duplicates. 
            Assert.True(isSucces);

            // Check if user has been assigned to role
            var userRole = context.UserRoles.Where(ur => ur.UserId == user1.Id && ur.RoleId == role.Id).FirstOrDefault();
            Assert.NotNull(userRole);
            #endregion
        }
        #endregion
    }
}