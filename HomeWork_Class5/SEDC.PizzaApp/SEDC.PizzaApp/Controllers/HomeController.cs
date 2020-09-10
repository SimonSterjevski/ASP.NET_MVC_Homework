using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models;

namespace SEDC.PizzaApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // returns ViewResult
        }

        [Route("FindMoreAboutUs")]  // localhost:port/FindMoreAboutUs
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View(); // returns ViewResult
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View(); // returns ViewResult
        }

        public IActionResult Privacy()
        {
            return View(); // returns ViewResult
        }

        public IActionResult UseSecondLayout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
