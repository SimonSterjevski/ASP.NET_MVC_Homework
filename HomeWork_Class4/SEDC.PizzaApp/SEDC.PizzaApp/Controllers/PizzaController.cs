using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models;
using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.Mappers;
using SEDC.PizzaApp.Models.ViewModels;

namespace SEDC.PizzaApp.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "List of pizzas:";
            List<Pizza> pizzas = StaticDb.Pizzas;
            List<PizzaViewModel> pizzasViewModel = pizzas.PizzasToViewModel();
            return View(pizzasViewModel); // returns ViewResult
        }

        public IActionResult JsonData()
        {
            Pizza pizza = new Pizza
            {
                Id = 1,
                Name = "Capri"
            };
            return new JsonResult(pizza); // returns JsonResult
        }

        public IActionResult BackToHome()
        {
            return RedirectToAction("Index", "Home"); //redirects to Action Index in Home Controller
        }

        public IActionResult Details(int? id) // localhost:port/Pizza/Details/1 or  localhost:port/Pizza/Details
        {
            ViewData["Title"] = "Pizza details:";
            if (id != null)
            {
                ViewBag.Pizza = StaticDb.Pizzas.PizzasToViewModel().FirstOrDefault(p => p.Id == id);
                if (ViewBag.Pizza != null)
                {
                    return View(ViewBag.Pizza);
                }
                return View("NoData");
            }
            return View("BadRequest");
        }
    }
}