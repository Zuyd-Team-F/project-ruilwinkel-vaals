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

namespace RuilwinkelVaals.Tests
{
    public static class CreateProductTests
    {
        #region Integration Tests
        [Fact]
        public static async Task CreateProductTest()
        {
            var context = await TestDb.GetDatabaseContext();
            var controller = new ProductsController(context);
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
