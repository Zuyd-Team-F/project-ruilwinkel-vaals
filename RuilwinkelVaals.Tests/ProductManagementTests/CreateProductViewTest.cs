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
            context.Categories.Add(new Category(category));
            context.Conditions.Add(new Condition(condition));
            context.Statuses.Add(new Status(status));

            var user = DbSeeder.GenerateUser("Naam");
            user.Balance = 10;
            context.Users.Add(user);
            context.SaveChanges();

            ProductCreateViewModel product = new()
            {
                CategoryId = context.Categories.FirstOrDefault(c => c.Name == ),
                ConditionId = ,
                StatusId = ,
                Name = "Chromebook",
                Brand = "Lenovo",
                CreditValue = 10,
                Description = "test test",
                UserId = user.Id
            };

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
