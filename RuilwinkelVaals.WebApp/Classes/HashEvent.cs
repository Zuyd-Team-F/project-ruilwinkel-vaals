using System;

namespace RuilwinkelVaals.WebApp.Classes
{
    public static class HashEvent
    {
        // Hashes the password of the user for security.
        public static String hashPassword(String password)
        {
            if (CheckEvent.isStringEmpty(password) == true)
            {
                // Error because password was empty.
            }
            else
            {
                using (var sha = new System.Security.Cryptography.SHA256Managed())
                {
                    byte[] passwordData = System.Text.Encoding.UTF8.GetBytes(password);
                    byte[] hash = sha.ComputeHash(passwordData);
                    return BitConverter.ToString(hash).Replace("-", String.Empty);
                }
            }
            return null;
        }
    }
}


