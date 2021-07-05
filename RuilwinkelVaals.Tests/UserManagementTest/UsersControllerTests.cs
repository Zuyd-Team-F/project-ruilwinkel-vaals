using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.IdentityOverrides;
using RuilwinkelVaals.WebApp.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RuilwinkelVaals.Tests.UserManagementTest
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task UserCreateGetTest()
        {
            // Arrange
            var controller = UsersTestController();

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserFormViewModel>(
                viewResult.ViewData.Model);

            Assert.NotNull(model);
            Assert.NotNull(model.Businesses);
            Assert.NotNull(model.Roles);
        }

        [Fact]
        public async Task UserCreatePostTest()
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
                .Callback<UserData, string>(async (x, y) =>
                {
                    var id = (await context.Users.AddAsync(x)).Entity.Id;
                    await context.SaveChangesAsync();
                    user.Id = id;
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

        [Fact]
        public async Task UserIndexGetTest()
        {
            // Arrange
            var controller = UsersTestController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserIndexViewModel>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);
        }

        [Fact]
        public async Task UserEditGetTest()
        {
            // Arrange
            var context = await TestDb.GetDatabaseContext();            
            var user = new UserData();
            var role = new Role("TestRole");
            var userStore = new Mock<UserStore>(context);
            var userManager = new Mock<UserManagerExtension>(userStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(um => um.GetRoleAsync(It.IsAny<UserData>())).ReturnsAsync(role.Name);
            var controller = UsersTestController(context, userManager, userStore);

            // Act
            context.Roles.Add(role);
            context.UserData.Add(user);
            await context.SaveChangesAsync();
            var nullIdResult = controller.Edit(null).Result;
            var nullUserResult = controller.Edit(2).Result;
            var result = controller.Edit(user.Id).Result;

            // Assert
            Assert.IsType<NotFoundResult>(nullIdResult);
            Assert.IsType<NotFoundResult>(nullUserResult);
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<UserFormViewModel>(
                viewResult.ViewData.Model);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public async Task UserEditPostTest()
        {
            // Arrange
            var context = TestDb.GetDatabaseContext().GetAwaiter().GetResult();
            var controller = UsersTestController(context);
            var user = new UserFormViewModel()
            {
                Id = 1
            };

            // Act
            var nullIdResult = controller.Edit(0, user).Result;
            //var modelStateInvalid = controller.Edit(user.Id, user).Result;

            // Assert
            Assert.IsType<NotFoundResult>(nullIdResult);
            //Assert.IsType<ViewResult>(modelStateInvalid);
        }

        private UsersController UsersTestController(
            ApplicationDbContext context = null, 
            Mock<UserManagerExtension> userManager = null,
            Mock<UserStore> userStore = null)
        {
            var _context = context ?? TestDb.GetDatabaseContext().GetAwaiter().GetResult();

            var _userStore = userStore ?? new Mock<UserStore>(_context);
            var _userManager = userManager ?? new Mock<UserManagerExtension>(_userStore.Object, null, null, null, null, null, null, null, null);
            var controller = new UsersController(_context, _userManager.Object);

            return controller;
        }
    }
}
