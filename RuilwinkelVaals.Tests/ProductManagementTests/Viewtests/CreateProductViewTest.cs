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
            var context = await TestDb.GetDatabaseContext();
            var controller = new ProductsController(context);
            var product = DbSeeder.GenerateProduct("Chromebook");
            var result = (await controller.GetAll()).ToArray();
            Assert.Equal("Chromebook", result[0].Name);
        }
        #endregion
    }
}
