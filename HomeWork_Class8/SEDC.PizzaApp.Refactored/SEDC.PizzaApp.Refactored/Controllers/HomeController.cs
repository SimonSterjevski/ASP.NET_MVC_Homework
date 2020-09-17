using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Refactored.Models;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Home;

namespace SEDC.PizzaApp.Refactored.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService;
        private IPizzaService _pizzaService;
        public HomeController(IOrderService orderService, IPizzaService pizzaService)
        {
            _orderService = orderService;
            _pizzaService = pizzaService;
        }
        
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            try
            {
                homeViewModel.OrderCount = _orderService.GetOrderCount();
                homeViewModel.MostPopularPizza = _pizzaService.GetMostPopularPizza();
                homeViewModel.PizzaOnPromotion = _pizzaService.GetPizzaOnPromotion();
                return View(homeViewModel);
            }
            catch
            {
                return View("ExceptionView");
            }
           
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
