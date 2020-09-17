using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.ViewModels.Feedback
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
