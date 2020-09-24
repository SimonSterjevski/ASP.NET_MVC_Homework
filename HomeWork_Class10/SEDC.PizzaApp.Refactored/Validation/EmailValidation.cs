using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
