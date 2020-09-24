using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.Mappers.Feedback;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.Validation;
using SEDC.PizzaApp.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public void CreteFeedback(FeedbackViewModel feedback)
        {
            List<Feedback> allFeedback = _feedbackRepository.GetFeedbackFromEmail(feedback.Email);
            if (!FeedbackNumberPerEmail.CalcFeedbackNumber(allFeedback))
            {
                throw new Exception($"Feedback count per mail excedeed! Only {FeedbackNumberPerEmail.maxLength} comments per email are allowed");
            }
            if (!CharacterLength.CheckLength(feedback.Message))
            {
                throw new Exception($"Feedback {CharacterLength.maxLength} characters limit excedeed!");
            }
            if (!EmailValidation.CheckEmail(feedback.Email))
            {
                throw new Exception("Invalid email format!");
            }
            int feedbackId = _feedbackRepository.Insert(feedback.ToFeedbackDomainModel());
            if (feedbackId <= 0)
            {
                throw new Exception("Feedback was not saved! Something went wrong!");
            }
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

        public void UpdateFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedback feedback = _feedbackRepository.GetById(feedbackViewModel.Id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id: {feedbackViewModel.Id} does not exist!");
            }
            List<Feedback> allFeedback = _feedbackRepository.GetFeedbackFromEmail(feedbackViewModel.Email).Where(x => x.Id != feedbackViewModel.Id).ToList();
            if (!FeedbackNumberPerEmail.CalcFeedbackNumber(allFeedback))
            {
                throw new Exception($"Feedback count per mail excedeed! Only {FeedbackNumberPerEmail.maxLength} comments per email are allowed");
            }
            if (!CharacterLength.CheckLength(feedback.Message))
            {
                throw new Exception($"Feedback {CharacterLength.maxLength} characters limit excedeed!");
            }
            if (!EmailValidation.CheckEmail(feedback.Email))
            {
                throw new Exception("Invalid email format!");
            }
            _feedbackRepository.UpdateEntity(feedbackViewModel.ToFeedbackDomainModel());
        }
    }
}
