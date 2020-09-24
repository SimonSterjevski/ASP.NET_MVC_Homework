using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.ViewModels.Feedback;

namespace SEDC.PizzaApp.Services.Interfaces
{
    public interface IFeedbackService
    {
        List<FeedbackViewModel> GetAllFeedback();
        FeedbackViewModel GetFeedbackById(int id);
        void CreteFeedback(FeedbackViewModel feedback);
        void UpdateFeedback(FeedbackViewModel feedback);
        void DeleteFeedback(FeedbackViewModel feedback);

    }
}
