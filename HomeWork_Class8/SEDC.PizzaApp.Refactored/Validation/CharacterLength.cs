using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.Validation
{
    public static class CharacterLength
    {
        public static int LimitNumber = 100;
        public static bool CheckLength (string message)
        {
            if (message.Length > LimitNumber)
            {
                return false;
            }
            return true;
        }
    }
}
