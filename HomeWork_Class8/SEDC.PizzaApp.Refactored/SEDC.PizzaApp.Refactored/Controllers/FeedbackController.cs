using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Feedback;
using SEDC.PizzaApp.Validation;

namespace SEDC.PizzaApp.Refactored.Controllers
{
    public class FeedbackController : Controller
    {
        private IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public IActionResult Index()
        {
            List<FeedbackViewModel> allFeedback = _feedbackService.GetAllFeedback();
            if (allFeedback.Count > 0)
            {
                return View(allFeedback);
            }
            return View("NoContent");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                FeedbackViewModel feedback = _feedbackService.GetFeedbackById(id.Value);
                return View(feedback);
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        public IActionResult LeaveFeedback()
        {
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
            return View(feedbackViewModel);
        }

        [HttpPost]
        public IActionResult AddFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (!CharacterLength.CheckLength(feedbackViewModel.Message))
            {
                ViewData["Limit"] = CharacterLength.LimitNumber;
                return View("CharacterLimit");
            }
            if (!EmailValidation.CheckEmail(feedbackViewModel.Email))
            {
                ViewData["Limit"] = FeedbackNumberPerEmail.LimitNumber;
                return View("InvalidMail");
            }
            try
            {
                string email = _feedbackService.CreteFeedback(feedbackViewModel);
                if (email == null)
                {
                    return View("FeedbackNumber");
                }
                return RedirectToAction("LeaveFeedback");
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        public IActionResult EditFeedback(int? id)
        {
            try
            {
                FeedbackViewModel feedback = _feedbackService.GetFeedbackById(id.Value);
                return View(feedback);
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        [HttpPost]
        public IActionResult EditFeedbackPost(FeedbackViewModel feedbackViewModel)
        {
            if (!CharacterLength.CheckLength(feedbackViewModel.Message))
            {
                ViewData["Limit"] = CharacterLength.LimitNumber;
                return View("CharacterLimit");
            }
            if (!EmailValidation.CheckEmail(feedbackViewModel.Email))
            {
                ViewData["Limit"] = FeedbackNumberPerEmail.LimitNumber;
                return View("InvalidMail");
            }
            try
            {
                string email = _feedbackService.UpdateFeedback(feedbackViewModel);
                if (email == null)
                {
                    return View("FeedbackNumber");
                }
                return RedirectToAction("Details", new { id = feedbackViewModel.Id });
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        public IActionResult DeleteFeedback(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                FeedbackViewModel feedback = _feedbackService.GetFeedbackById(id.Value);
                return View(feedback);
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        [HttpPost]
        public IActionResult ConfirmDelete(FeedbackViewModel feedbackViewModel)
        {
            try
            {
                _feedbackService.DeleteFeedback(feedbackViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ExceptionView");
            }
        }
    }
}
