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
using Moq;

namespace RuilwinkelVaals.Tests.ProductManagementTests.Viewtests
{
    public class CreateLoanTest
    {

        #region integration test
        [Fact]
        public async Task CreateLoanedProductViewTest()
        {
            var toast = new Mock<IToastNotification>();
            var context = await TestDb.GetDatabaseContext();
            await DbSeeder.SeedStatuses(context);
            var user = DbSeeder.GenerateUser("Naam");

            user.Balance = 10;
            context.Users.Add(user);

            var category = "Electronica";
            var condition = "Zeer Slecht";
            var status = "Voorradig";
            var catModel = context.Categories.Add(new Category(category)).Entity;
            var condModel = context.Conditions.Add(new Condition(condition)).Entity;
            var StatModel = context.Statuses.Add(new Status(status)).Entity;

            var product = DbSeeder.GenerateProduct("Chromebook", catModel.Id, condModel.Id, StatModel.Id);
            product.CreditValue = 10;
            context.Product.Add(product);
            await context.SaveChangesAsync();
            var loanedProduct = DbSeeder.GenerateLoanedProduct(product, user);
            context.LoanedProducts.Add(loanedProduct);            

            var controller = new LoanedProductsController(context, toast.Object);
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
