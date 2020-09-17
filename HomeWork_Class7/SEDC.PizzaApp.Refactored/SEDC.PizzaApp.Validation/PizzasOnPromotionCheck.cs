using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.Validation
{
    public static class PizzasOnPromotionCheck
    {
        public static int LimitNumber = 1;
        public static bool CheckPizzasOnPromotion(List<Pizza> pizzas, bool param)
        {
            if (param)
            {
                if (pizzas.Count >= LimitNumber)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
