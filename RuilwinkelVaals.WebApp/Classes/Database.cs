using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.WebApp.Classes
{
    public class Database
    {
        public static void AddProduct(Product product, ApplicationDbContext context)
		{
            context.Product.Add(product);
            context.SaveChanges();
		}

        public static int GetCategoryId(string name, ApplicationDbContext context)
        {
            if (name == null)
            {
                return -1;
            }

            var category = context.Categories
                .FirstOrDefault(m => m.Name == name);
            if (category == null)
            {
                return -1;
            }

            return category.Id;
        }
        public static int GetStatusId(string name, ApplicationDbContext context)
        {
            if (name == null)
            {
                return -1;
            }

            var status = context.Statuses
                .FirstOrDefault(m => m.Name == name);
            if (status == null)
            {
                return -1;
            }

            return status.Id;
        }
        public static int GetConditionId(string name, ApplicationDbContext context)
        {
            if (name == null)
            {
                return -1;
            }

            var condition = context.Conditions
                .FirstOrDefault(m => m.Name == name);
            if (condition == null)
            {
                return -1;
            }

            return condition.Id;
        }
    }
}
