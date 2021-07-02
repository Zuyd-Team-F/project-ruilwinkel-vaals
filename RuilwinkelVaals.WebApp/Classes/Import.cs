using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.Data;
using Microsoft.AspNetCore.Http;

namespace RuilwinkelVaals.WebApp.Classes
{
	public static class Import
	{

		public static List<string> readCSV(IFormFile file, ApplicationDbContext context)
		{
			List<string> feedbackList = new List<string>();
			using (StreamReader reader = new StreamReader(file.OpenReadStream()))
		    {
		        reader.ReadLine();
		        while (!reader.EndOfStream)
		        {
		            var line = reader.ReadLine();
		            var values = line.Split(',');

		            string name = values[0];
		            string description = values[1];
		            string brand = values[2];
		            string categoryName = values[3];
		            string statusName = values[4];
		            string conditionName = values[5];
					int creditValue;
					try
					{
						creditValue = Int32.Parse(values[6]);
					}
					catch
					{
						creditValue = 0;
					}

					//Convert all names into ID's
					int categoryId = Database.GetCategoryId(categoryName, context);
					int statusId = Database.GetStatusId(statusName, context);
					int conditionId = Database.GetConditionId(conditionName, context);

					//Checks if all data entered in CSV is valid.
					(bool addToDb, string feedback) = CheckEvent.checkProductVars(name, description, brand, categoryId, statusId, conditionId, creditValue);
		            if (addToDb)
		            {
						Product product = new Product() { Name = name, Brand = brand, Description = description, CategoryId = categoryId, ConditionId = conditionId, CreditValue = creditValue, StatusId = statusId };
						Database.AddProduct(product, context);
						feedbackList.Add(name + " has been added!");
					}
		            else
		            {
						feedbackList.Add(feedback);
		            }
		        }
				return feedbackList;
		    }
		}
	}
}
