using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Services.Implementations;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Order;

namespace SEDC.PizzaApp.Refactored.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;
        private IPizzaService _pizzaService;
        //called for each request
        //this is what the app does
        //new OrderController(new OrderService())
        public OrderController(IOrderService orderService, IUserService userService, IPizzaService pizzaService) // we are using Dependency Injection - needs to instantiate the service
        {
            //manual
            //_orderService = new OrderService();

            _orderService = orderService;
            _userService = userService;
            _pizzaService = pizzaService;
        }
        public IActionResult Index()
        {
            //call to business layer
            List<OrderDetailsViewModel> orderDetailsViewModels = _orderService.GetAllOrders();
            return View(orderDetailsViewModels);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }

            try
            {
                OrderDetailsViewModel orderDetailsViewModel = _orderService.GetOrderById(id.Value);
                return View(orderDetailsViewModel);
            }
            catch(Exception ex)
            {
                return View("ExceptionView");
            }
        }

        public IActionResult CreateOrder()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            ViewBag.Users = _userService.GetUsersForDropDown();
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult CreateOrderPost(OrderViewModel orderViewModel)
        {
            try
            {
                _orderService.CreateOrder(orderViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionView");
            }
            
        }

        public IActionResult AddPizza(int id)//order id
        {
            ViewBag.Pizzas = _pizzaService.GetPizzasForDropdown();
            PizzaOrderViewModel pizzaOrderViewModel = new PizzaOrderViewModel();
            //the order that gets the pizza
            pizzaOrderViewModel.OrderId = id;
            return View(pizzaOrderViewModel);
        }

        [HttpPost]
        public IActionResult AddPizzaPost(PizzaOrderViewModel pizzaOrderViewModel)
        {
            try
            {
                _orderService.AddPizzaToOrder(pizzaOrderViewModel);
                return RedirectToAction("Details", new {id = pizzaOrderViewModel.OrderId});
            }
            catch (Exception e)
            {
                return View("ExceptionView");
            }
        }

        public IActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }

            try
            {
                OrderViewModel orderViewModel = _orderService.GetOrderForEditing(id.Value);
                ViewBag.Users = _userService.GetUsersForDropDown();
                return View(orderViewModel);
            }
            catch
            {
                return View("ExceptionView");
            }
            
        }

        [HttpPost]
        public IActionResult EditOrderPost(OrderViewModel orderViewModel)
        {
            try
            {
                _orderService.EditOrder(orderViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ExceptionView");
            }
           
        }

        public IActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }

            try
            {
                OrderDetailsViewModel orderDetailsViewModel = _orderService.GetOrderById(id.Value);
                return View(orderDetailsViewModel);
            }
            catch
            {
                return View("ExceptionView");
            }
        }

        [HttpPost]
        public IActionResult DeleteOrderPost(OrderDetailsViewModel orderDetailsViewModel)
        {
            try
            {
                _orderService.DeleteOrder(orderDetailsViewModel.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ExceptionView");
            }
        }
    }
}
