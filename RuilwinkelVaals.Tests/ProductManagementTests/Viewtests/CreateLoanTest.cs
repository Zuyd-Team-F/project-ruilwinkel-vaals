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
            //hier de CreateLoanedProduct view aanhalen en dan kijken of het antwoord klopt
            //dit een paar keer doen
            await context.SaveChangesAsync();
        }
    }
}
