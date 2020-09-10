using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models;
using SEDC.PizzaApp.Models.Domain;

namespace SEDC.PizzaApp.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            List<Pizza> pizzas = StaticDb.Pizzas;
            return View(pizzas); // returns ViewResult
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
            if (id != null)
            {
                Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
                if(pizza != null)
                {
                    return View(pizza);
                }
                return View("ResouceNotFound");
            }
            //  return new EmptyResult();
            return View("BadRequest");
        }

        public IActionResult CreatePizza ()
        {
            Pizza pizza = new Pizza();
            return View(pizza);
        }

        public IActionResult AddPizza(Pizza pizza)
        {
            //if (pizza.Name != "" && pizza.Price.ToString() != "")
            //{
            pizza.Id = ++StaticDb.PizzaId;
            StaticDb.Pizzas.Add(pizza);
            return RedirectToAction("Index");
            //}
            //return View("BadRequest");
        }

        public IActionResult EditPizza(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return View("ResourceNotFound");
            }
            return View(pizza);
        }

        public IActionResult EditChanges(Pizza pizza)
        {
            int index = StaticDb.Pizzas.FindIndex(x => x.Id == pizza.Id);
            StaticDb.Pizzas[index] = pizza;
            return RedirectToAction("Index");
        }

        public IActionResult DeletePizza(int? id)
        {
            if (id != null)
            {
                Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
                List<Pizza> orderedPizzas = StaticDb.Orders.Select(x => x.Pizza).ToList();
                if (pizza == null)
                {
                    return View("ResourceNotFound");
                }
                if (orderedPizzas.Contains(pizza))
                {
                    return View("ActionForbidden");
                }
                    return View(pizza);
            }
            return View("BadRequest");
        }

        public IActionResult ConfirmDelete(Pizza pizza)
        {
            int index = StaticDb.Pizzas.FindIndex(x => x.Id == pizza.Id);
            StaticDb.Pizzas.RemoveAt(index);
            return RedirectToAction("Index");
        }
    }
}