using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.Validation
{
    public static class FeedbackNumberPerEmail
    {
        public static int LimitNumber = 3;
        public static string CalcFeedbackNumber(List<Feedback> allFeedback, string email)
        {
            if (allFeedback.Count >= LimitNumber)
            {
                return null;
            }
            return email;
        }
    }
}
