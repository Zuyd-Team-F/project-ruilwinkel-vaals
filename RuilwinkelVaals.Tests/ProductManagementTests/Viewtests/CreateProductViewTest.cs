using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace RuilwinkelVaals.Tests
{
    public static class CreateProductTests
    {
        #region Integration Tests
        [Fact]
        public static async Task CreateProductViewTest()
        {
            // Fetch the in memory context to test on
            var context = await TestDb.GetDatabaseContext();

            //creating the Controller
            var controller = new ProductsController(context);

            //making an instance of category
            Category category = new Category("Electronica");

            //making an instance of condition
            Condition condition = new Condition("Zeerslecht");

            //making an instance of status
            Status status = new Status("Voorradig");

            //adding the product to the database
            await controller.Create(new Product() { Category = category, Condition = condition, Status = status, Name = "Chromebook", Description = "test test", CreditValue = 123, Brand = "test"});

            //verifying if the product has been successfully added to the database
            //..
            //..
            var result = (await controller.GetAll()).ToArray();
            Assert.Equal(1, 1);
        }
        #endregion
    }
}
