using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models;

namespace SEDC.PizzaApp.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            var pizzas = StaticDb.Pizzas;
            return View(pizzas);
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                return View(id);
            }
            return new EmptyResult();
        }
        [HttpGet("SeePizzas")]
        public IActionResult Redirect()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var pizza = StaticDb.Pizzas.First();
            return View(pizza);
        }

        [HttpPost]
        public IActionResult Delete(Pizza pizza)
        {
            return RedirectToAction("Index");
        }
    }
}