using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            List<PizzaViewModel> pizzas = StaticDb.Pizzas.Select(x => x.ToPizzaViewModel()).ToList();
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
                    return View(pizza.ToPizzaViewModel());
                }
                return View("ResouceNotFound");
            }
            //  return new EmptyResult();
            return View("BadRequest");
        }

        public IActionResult CreatePizza ()
        {
            Pizza pizza = new Pizza();
            return View(pizza.ToPizzaViewModel());
        }

        public IActionResult AddPizza(PizzaViewModel pizza)
        {
            //if (pizza.Name != "" && pizza.Price.ToString() != "")
            //{
            pizza.Id = ++StaticDb.PizzaId;
            StaticDb.Pizzas.Add(pizza.ToPizzaDomain());
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
            return View(pizza.ToPizzaViewModel());
        }

        public IActionResult EditChanges(PizzaViewModel pizza)
        {
            int index = StaticDb.Pizzas.FindIndex(x => x.Id == pizza.Id);
            StaticDb.Pizzas[index] = pizza.ToPizzaDomain();
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
                    return View(pizza.ToPizzaViewModel());
            }
            return View("BadRequest");
        }

        public IActionResult ConfirmDelete(PizzaViewModel pizza)
        {
            int index = StaticDb.Pizzas.FindIndex(x => x.Id == pizza.Id);
            StaticDb.Pizzas.RemoveAt(index);
            return RedirectToAction("Index");
        }
    }
}