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
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "More about our pizza app.";

            return View();
        }
        [Route("ContactUs")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Our contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Pizzas")]
        public IActionResult Pizzas()
        {
            return RedirectToAction("Index", "Pizza");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
