using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace RuilwinkelVaals.Tests.ProductManagementTests.Viewtests
{
    class CreateLoanTest
    {
        public static async Task CreateLoanedProductViewTest()
        {
            var context = await TestDb.GetDatabaseContext();
            var user = DbSeeder.GenerateUser("Naam");
            user.Balance = 10;
            context.Users.Add(user);
            var product = DbSeeder.GenerateProduct("Chromebook");
            product.CreditValue = 10;
            context.Users.Add(user);
            var controller = new LoanedProductsController(context);
            var result = await controller.Create();
            await context.SaveChangesAsync();
            Assert.IsType<RedirectToActionResult>(result);
            var productResult = await context.Product.FindAsync(product.Id);
            Assert.NotNull(productResult);
            Assert.Equal(product.Name, productResult.Name);

        }
    }
}
