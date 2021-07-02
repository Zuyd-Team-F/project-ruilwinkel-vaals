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
using RuilwinkelVaals.WebApp;
using NToastNotify;

namespace RuilwinkelVaals.Tests.ProductManagementTests.Viewtests
{
    public class CreateLoanTest
    {
        private readonly IToastNotification _toast;
        #region integration test
        [Fact]
        public async Task CreateLoanedProductViewTest()
        {
            var context = await TestDb.GetDatabaseContext();
            await DbSeeder.SeedStatuses(context);
            var user = DbSeeder.GenerateUser("Naam");
            user.Balance = 10;
            context.Users.Add(user);
            var product = DbSeeder.GenerateProduct("Chromebook");
            product.CreditValue = 10;
            context.Product.Add(product);
            var loanedProduct = DbSeeder.GenerateLoanedProduct(product, user);
            context.LoanedProducts.Add(loanedProduct);
            var controller = new LoanedProductsController(context, _toast);
            var result = await controller.Create(loanedProduct);
            await context.SaveChangesAsync();
            Assert.IsType<RedirectToActionResult>(result);
            var productResult = await context.Product.FindAsync(product.Id);
            Assert.NotNull(productResult);
            Assert.Equal(product.Name, productResult.Name);

            var userResult = await context.UserData.FindAsync(user.Id);
            Assert.NotNull(userResult);
            Assert.Equal(user.FirstName, userResult.FirstName);

            var loanResult = await context.LoanedProducts.FindAsync(loanedProduct.Id);
            Assert.NotNull(loanResult);
            Assert.Equal(loanedProduct.User, loanResult.User);
            Assert.Equal(0, userResult.Balance);//hier moet nog ff naar gekeken worden
            Assert.Equal((int)(Constants.Statuses.Uitgeleend) + 1, productResult.StatusId);
        }
        #endregion
    }
}
