using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.Validation;
using SEDC.PizzaApp.ViewModels.Pizza;

namespace SEDC.PizzaApp.Refactored.Controllers
{
    public class PizzController : Controller
    {
        private IPizzaService _pizzaService;

        public PizzController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }
        public IActionResult Index()
        {
            List<PizzaViewModel> pizzas = _pizzaService.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Details(int? id)
        {
            PizzaViewModel pizza = _pizzaService.GetPizzaById(id.Value);
            try
            {
                return View(pizza);
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        public IActionResult CreatePizza()
        {
            PizzaViewModel pizzaViewModel = new PizzaViewModel();
            return View(pizzaViewModel);
        }

        [HttpPost]
        public IActionResult AddPizza(PizzaViewModel pizzaViewModel)
        {
            try
            {
                if (!_pizzaService.CreatePizza(pizzaViewModel))
                {
                    ViewData["Limit"] = PizzasOnPromotionCheck.LimitNumber;
                    return View("PromotionLimit");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ExceptionView");
            }
        }
    }
}
