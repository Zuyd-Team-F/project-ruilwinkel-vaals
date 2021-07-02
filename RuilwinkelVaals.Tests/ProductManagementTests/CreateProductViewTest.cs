using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using RuilwinkelVaals.WebApp.ViewModels.Products;
using RuilwinkelVaals.WebApp.Classes.Services;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;

namespace RuilwinkelVaals.Tests
{
    public class CreateProductTests
    {
        private readonly IImageHandler _imgHandler;
        private readonly IWebHostEnvironment _env;
        private readonly IToastNotification _toast;

        #region Integration Tests
        [Fact]
        public async Task CreateProductTest()
        {
            var context = await TestDb.GetDatabaseContext();
            var controller = new ProductsController(context, _imgHandler, _env, _toast);
            var user = DbSeeder.GenerateUser("Naam");
            user.Balance = 10;
            context.Users.Add(user);
            ProductCreateViewModel product = DbSeeder.GenerateProductView("Chromebook", user.Id);
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
