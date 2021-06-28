using RuilwinkelVaals.WebApp.Controllers;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace RuilwinkelVaals.Tests.ProductManagementTests.Viewtests
{
    class CreateLoanTest
    {
        public static async Task CreateLoanTest()
        {
            // Fetch the in memory context to test on
            var context = await TestDb.GetDatabaseContext();

            //creating the Controller
            var controller = new LoanedProductsController(context);

            //adding the product to the database
            await controller.Create(new Product() { Category = category, Condition = condition, Status = status, Name = "Chromebook", Description = "test test", CreditValue = 123, Brand = "test" });

            //making an instance of user
            Condition condition = new Condition("Zeerslecht");
        }
    }
}
