using System;

namespace RuilwinkelVaals.WebApp.Classes
{
    public static class CheckEvent
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


    }
}
