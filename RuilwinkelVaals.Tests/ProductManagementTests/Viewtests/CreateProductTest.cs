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
    public class CreateProductTests
    {
        #region Integration Tests
        [Fact]
        public async Task CreateProductViewTest()
        {
            //creating the database context
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            var context = new ApplicationDbContext(optionsBuilder.Options);

            //renew testing database
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            //creating the Controller
            var controller = new ProductsController(context);

            //making an instance of category
            Category category = new Category() { Name = "Electronica" };

            //making an instance of condition
            Condition condition = new Condition() { Name = "Zeerslecht" };

            //making an instance of status
            Status status = new Status() { Name = "Voorradig" };

            //adding the product to the database
            await controller.Create(new Product() { Category = category, Condition = condition, Status = status, Name = "Chromebook", Description = "test test", CreditValue = 123, Brand = "test", Id = 1});

            //verifying if the product has been successfully added to the database
            //..
            //..
            var result = await context.Product
                .Include(p => p.Category)
                .Include(p => p.Condition)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == 1);
            Assert.Equal("Chromebook", result.Name);
            Assert.Equal("test", result.Brand);
            Assert.Equal(category, result.Category);
            Assert.Equal(condition, result.Condition);
            Assert.Equal(status, result.Status);
            Assert.Equal("test test", result.Description);
            Assert.Equal(123, result.CreditValue);
        }
        #endregion
    }
}