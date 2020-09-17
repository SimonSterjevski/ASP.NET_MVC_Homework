using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.Validation
{
    public static class EmailValidation
    {
        public static bool CheckEmail (string email)
        {
            if (email.Contains("@") && !email.EndsWith("@"))
            {
                return true;
            }
            return false;
        }
    }
}
