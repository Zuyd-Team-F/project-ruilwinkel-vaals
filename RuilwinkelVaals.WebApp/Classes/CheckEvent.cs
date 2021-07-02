using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RuilwinkelVaals.WebApp;

namespace RuilwinkelVaals.WebApp.Classes
{
    public class CheckEvent
    {
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

        public static (Boolean, string) checkProductVars(string name, string description, string brand, int category, int status, int condition, int creditValue)
        {
            Boolean checkBool = true;
            List<string> feedbackList = new List<string>();
            feedbackList.Add(name);
            if (name.Length > 56 || String.IsNullOrEmpty(name))
            {
                checkBool = false;
                feedbackList.Add("Name is te lang of leeg");
            }
            if (brand.Length > 24)
            {
                checkBool = false;
                feedbackList.Add("Merk is te lang of leeg");
            }
            if (category == -1)
			{
                checkBool = false;
                feedbackList.Add("Category is fout");
            }
            if (status == -1)
            {
                checkBool = false;
                feedbackList.Add("Status is fout");
            }
            if (condition == -1)
            {
                checkBool = false;
                feedbackList.Add("Conditie is fout");
            }
            if (creditValue == 0)

            {
                checkBool = false;
                feedbackList.Add("Punten waarde is leeg of 0");
            }
            string feedbackString = string.Join(", ", feedbackList);
            return (checkBool, feedbackString);    

		}
    }
}
