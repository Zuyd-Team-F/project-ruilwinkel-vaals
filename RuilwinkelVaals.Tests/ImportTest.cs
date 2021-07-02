using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.Classes;
using System.Threading.Tasks;
using Xunit;
using RuilwinkelVaals.WebApp.ViewModels.Users;
using Moq;
using RuilwinkelVaals.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.IdentityOverrides;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using RuilwinkelVaals.WebApp.Data;
using System;
using System.Reflection;
using System.Text;
using System.Threading;

namespace RuilwinkelVaals.Tests
{
    public static class ImportTest
    {
        [Fact]
        public static async Task ImportFileTest()
        {
            // Fetch the in memory context to test on
            var context = await TestDb.GetDatabaseContext();

            // Create Products
            List<string> feedbackList = new List<string>();


            // Init valid product 1
            int categoryId = Database.GetCategoryId("Spellen", context);
            int statusId = Database.GetStatusId("Voorradig", context);
            int conditionId = Database.GetConditionId("Neutraal", context);
            Product validProduct1 = new Product() { Name = "Laptop", Brand = "Intel", Description = "I5 Processor", CategoryId = categoryId, ConditionId = conditionId, CreditValue = 1, StatusId = statusId};
            Import.checkAndWrite(validProduct1, feedbackList, context);

            // Init invalid product 1
            categoryId = Database.GetCategoryId("Tester", context);
            statusId = Database.GetStatusId("Aaaah", context);
            conditionId = Database.GetConditionId("Bah", context);
            Product invalidProduct1 = new Product() { Name = "Boek", Brand = "Intel", Description = "I5 Processor", CategoryId = categoryId, ConditionId = conditionId, CreditValue = 2, StatusId = statusId };
            Import.checkAndWrite(invalidProduct1, feedbackList, context);

            // Init invalid product 2
            categoryId = Database.GetCategoryId("Decoratie", context);
            statusId = Database.GetStatusId("Reparatie", context);
            conditionId = Database.GetConditionId("ZeerGoed", context);
            Product invalidProduct2 = new Product() { Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", Brand = "Intel", Description = "I5 Processor", CategoryId = categoryId, ConditionId = conditionId, CreditValue = 5, StatusId = statusId };
            Import.checkAndWrite(invalidProduct2, feedbackList, context);


            // Check for created products
            string validProductName1 = "Laptop";
            string invalidProductName1 = "Boek";
            string invalidProductName2 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Assert.True(!checkProductExists(validProductName1, context));
            Assert.False(checkProductExists(invalidProductName1, context));
            Assert.False(checkProductExists(invalidProductName2, context));

        }

        public static Boolean checkProductExists(string productName, ApplicationDbContext context)
        {
            var product = context.Product
                    .FirstOrDefault(m => m.Name == productName);
            if (product == null)
            {
                return false;
            }

            return true;
        }
    }

}
