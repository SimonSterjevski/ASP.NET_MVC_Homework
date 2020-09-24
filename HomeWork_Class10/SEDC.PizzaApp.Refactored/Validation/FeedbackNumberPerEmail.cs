using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.Validation
{
    public static class FeedbackNumberPerEmail
    {
        public static int maxLength = 3;
        public static bool CalcFeedbackNumber(List<Feedback> allFeedback)
        {
            if (allFeedback.Count >= maxLength)
            {
                return false;
            }
            return true;
        }
    }
}
