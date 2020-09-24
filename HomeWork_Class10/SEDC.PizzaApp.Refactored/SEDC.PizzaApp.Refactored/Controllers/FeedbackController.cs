using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Feedback;

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
            try
            {
                _feedbackService.CreteFeedback(feedbackViewModel);
                return RedirectToAction("LeaveFeedback");
            }
            catch(Exception ex)
            {
                ViewData["ErrMessage"] = ex.Message;
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
            try
            {
                _feedbackService.UpdateFeedback(feedbackViewModel);
                return RedirectToAction("Details", new { id = feedbackViewModel.Id });
            }
            catch(Exception ex)
            {
                ViewData["ErrMessage"] = ex.Message;
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
