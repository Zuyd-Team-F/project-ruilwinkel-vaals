using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using RuilwinkelVaals.WebApp.Data.Models;
using Microsoft.AspNetCore.Http;

namespace RuilwinkelVaals.WebApp.Classes
{
	public class Import
	{
		public static void readCSV(IFormFile file)
		{
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
		            string status = values[4];
		            string condition = values[5];
		            int creditValue = Int32.Parse(values[6]);


		            (bool addToDb, List<string> feedbackList) = CheckEvent.checkProductVars(name, description, brand, categoryName, status, condition, creditValue);
		            if (addToDb)
		            {
						int categoryId = CheckEvent.GetCategoryId(categoryName);
						int statusId = CheckEvent.GetCategoryId(categoryName);
						int conditionId = CheckEvent.GetCategoryId(categoryName);
						Product product = new Product();
						//database.createproduct(product);
					}
		            else
		            {
		                string stringTempText = feedbackList.ToString();
		            }
		        }
		    }
		}
	}
}
