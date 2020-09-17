using SEDC.PizzaApp.DataAccess;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.Mappers.Feedback;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Feedback;
using SEDC.PizzaApp.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDC.PizzaApp.DataAccess.Interfaces;

namespace SEDC.PizzaApp.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public string CreteFeedback(FeedbackViewModel feedback)
        {
            List<Feedback> allFeedback = _feedbackRepository.GetFeedbackFromEmail(feedback.Email);
            string email = FeedbackNumberPerEmail.CalcFeedbackNumber(allFeedback, feedback.Email);
            if (email == null)
            {
                return null;
            }
            int feedbackId = _feedbackRepository.Insert(feedback.ToFeedbackDomainModel());
            if (feedbackId <= 0)
            {
                throw new Exception("Feedback was not saved! Something went wrong!");
            }
            return email;
        }


        public void DeleteFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedback feedback = _feedbackRepository.GetById(feedbackViewModel.Id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id: {feedbackViewModel.Id} does not exist!");
            }
            _feedbackRepository.DeleteById(feedbackViewModel.Id);
        }

        public List<FeedbackViewModel> GetAllFeedback()
        {
            List<FeedbackViewModel> allFeedbackViewModel = _feedbackRepository.GetAll().Select(x => x.ToFeedbackViewModel()).ToList();
            return allFeedbackViewModel;
        }

        public FeedbackViewModel GetFeedbackById(int id)
        {
            Feedback feedback = _feedbackRepository.GetById(id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id: {id} does not exist!");
            }
            return feedback.ToFeedbackViewModel();
        }

        public string UpdateFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedback feedback = _feedbackRepository.GetById(feedbackViewModel.Id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id: {feedbackViewModel.Id} does not exist!");
            }
            List<Feedback> allFeedback = _feedbackRepository.GetFeedbackFromEmail(feedbackViewModel.Email);
            string email = FeedbackNumberPerEmail.CalcFeedbackNumber(allFeedback, feedbackViewModel.Email);
            if (email == null)
            {
                return null;
            }
            _feedbackRepository.Update(feedbackViewModel.ToFeedbackDomainModel());
            return feedbackViewModel.Email;
        }
    }
}
