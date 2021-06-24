using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.Tests.ProductManagementTests
{
    public static class ModelTests
    {
        #region Unit Tests
        [Fact]
        public static void ProductModelTest()
        {
            //making an instance of category
            Category category = new Category("Electronica");

            //making an instance of condition
            Condition condition = new Condition("Zeerslecht");

            //making an instance of status
            Status status = new Status("Voorraadig");

            //making an instance of product
            Product item = new Product() { Category = category, Condition = condition, Status = status, Name = "Chromebook", Description = "test test", CreditValue = 123, Brand = "test" };

            //testing to see if the input values remain correct when creating the object 
            Assert.Equal("Chromebook", item.Name);
            Assert.Equal("test", item.Brand);
            Assert.Equal(category, item.Category);
            Assert.Equal(condition, item.Condition);
            Assert.Equal(status, item.Status);
            Assert.Equal("test test", item.Description);
            Assert.Equal(123, item.CreditValue);

            //testing to see if the input values remain correct when creating the object 
            Assert.Equal("Zeerslecht", condition.Name);

            //testing to see if the input values remain correct when creating the object 
            Assert.Equal("Electronica", category.Name);

            //testing to see if the input values remain correct when creating the object 
            Assert.Equal("Voorraadig", status.Name);
        }
        #endregion
    }
}
