using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using RuilwinkelVaals.WebApp.ViewModels.Products;
using RuilwinkelVaals.WebApp.Classes.Services;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;
using Moq;

namespace RuilwinkelVaals.Tests
{
    public class CreateProductTests
    {        
        #region Integration Tests
        [Fact]
        public async Task CreateProductTest()
        {
            var imgHandler = new Mock<IImageHandler>();
            var env = new Mock<IWebHostEnvironment>();
            var toast = new Mock<IToastNotification>();            

            var context = await TestDb.GetDatabaseContext();
            var controller = new ProductsController(context, imgHandler.Object, env.Object, toast.Object);

            var category = "Electronica";
            var condition = "Zeer Slecht";
            var status = "Voorradig";
            var catModel = context.Categories.Add(new Category(category)).Entity;
            var condModel = context.Conditions.Add(new Condition(condition)).Entity;
            var statModel = context.Statuses.Add(new Status(status)).Entity;
            context.SaveChanges();

            var user = DbSeeder.GenerateUser("Naam");
            user.Balance = 10;
            var userModel = context.Users.Add(user).Entity;
            context.SaveChanges();

            var product = DbSeeder.GenerateProduct("Chromebook", catModel.Id, condModel.Id, statModel.Id);
            var result = await controller.Create(product);
            await context.SaveChangesAsync();

            Assert.IsType<RedirectToActionResult>(result);
            var productResult = await context.Product.FindAsync(product.Id + 1);
            Assert.NotNull(productResult);
            Assert.Equal(product.Name, productResult.Name);

        }
        #endregion
    }
}
