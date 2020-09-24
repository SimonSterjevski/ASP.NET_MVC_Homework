using SEDC.PizzaApp.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.PizzaApp.Mappers.Feedback
{
    public static class FeedbackMapper
    {
        public static FeedbackViewModel ToFeedbackViewModel(this Domain.Models.Feedback feedback)
        {
            return new FeedbackViewModel
            {
                Id = feedback.Id,
                Name = feedback.Name,
                Email = feedback.Email,
                Message = feedback.Message
            };
        }

        public static Domain.Models.Feedback ToFeedbackDomainModel(this FeedbackViewModel feedbackViewModel)
        {
            return new Domain.Models.Feedback
            {
                Id = feedbackViewModel.Id,
                Name = feedbackViewModel.Name,
                Email = feedbackViewModel.Email,
                Message = feedbackViewModel.Message
            };
        }
    }
}
