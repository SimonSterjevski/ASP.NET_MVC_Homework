using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SEDC.PizzaApp.Validation
{
    public static class CharacterLength
    {
        public static int maxLength = 100;
        public static bool CheckLength (string message)
        {
            if (message.Length > maxLength)
            {
                return false;
            }
            return true;
        }
    }
}
