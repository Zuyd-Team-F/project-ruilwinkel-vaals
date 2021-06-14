using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
