using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RuilwinkelVaals.WebApp.Classes
{
    public class CheckEvent
    {
        private readonly ApplicationDbContext _context;

        public CheckEvent(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if string is empty.
        public static Boolean isStringEmpty(String input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }

        // Check email for special characters.
        public static Boolean checkEmailOnChars(String email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Check if passwords are identical.
        public static Boolean checkIdenticalPassword(String password1, String password2)
        {
            if (password1 == password2)
            {
                return true;
            }
            return false;
        }

        public static (Boolean, List<string>) checkProductVars(string name, string description, string brand, string category, string status, string condition, int creditValue)
        {
            Boolean checkBool = true;
            List<string> feedbackList = new List<string>();
            if (name.Length > 56 || String.IsNullOrEmpty(name))
            {
                checkBool = false;
                feedbackList.Add("Name is too long or empty.");
            }
            if (brand.Length > 24)
            {
                checkBool = false;
                feedbackList.Add("Brand is too long or empty.");
            }
            int CategoryId = CheckEvent.GetCategoryId(category);
            if (String.IsNullOrEmpty(category) || CategoryId == -1)
			{
                checkBool = false;
                feedbackList.Add("Category is empty.");
            }
            if (String.IsNullOrEmpty(status))
            {
                checkBool = false;
                feedbackList.Add("Status is empty.");
            }
            if (String.IsNullOrEmpty(condition))
            {
                checkBool = false;
                feedbackList.Add("Category is empty.");
            }
            if (String.IsNullOrEmpty(creditValue.ToString()))

            {
                checkBool = false;
                feedbackList.Add("Category is empty.");
            }
            return (checkBool, feedbackList);    

		}
        public static int GetCategoryId(string name)
        {
            return -1;
        //    ApplicationDbContext _context = new ApplicationDbContext();
        //    if (name == null)
        //    {
        //        return -1;
        //    }

        //    var category = _context.Categories
        //        .FirstOrDefault(m => m.Name == name);
        //    if (category == null)
        //    {
        //        return -1;
        //    }

        //    return category.Id;
        }
    }
}
