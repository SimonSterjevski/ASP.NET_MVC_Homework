using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.DataAccess;
using SEDC.PizzaApp.DataAccess.Implementations;
using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.Mappers.Order;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.Order;

namespace SEDC.PizzaApp.Services.Implementations
{
    public class OrderService : IOrderService
    {
        //only used by the service for its logic
        private IRepository<Order> _orderRepository;
        private IRepository<User> _userRepository;
        private IPizzaRepository _pizzaRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<User> userRepository, IPizzaRepository pizzaRepository) // in order for the service to be instantiated, the repository is needed 
        {
            //implementation of the IRepository<Order> interface
            //_orderRepository = new OrderRepository();

            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _pizzaRepository = pizzaRepository;
        }
        public List<OrderDetailsViewModel> GetAllOrders()
        {
            //call to data access layer
            List<Order> orders = _orderRepository.GetAll();
            List<OrderDetailsViewModel> viewModels = new List<OrderDetailsViewModel>();
            foreach (Order order in orders)
            {
                viewModels.Add(order.ToOrderDetailsViewModel());
            }

           // return orders.ToOrderDetailsViewModelList();

            return viewModels;
        }

        public OrderDetailsViewModel GetOrderById(int id)
        {
            Order order = _orderRepository.GetById(id);
            if (order == null)
            {
                //log the exception
                throw new Exception($"Order with id {id} does not exist!");
            }

            return order.ToOrderDetailsViewModel();
        }

        public void CreateOrder(OrderViewModel orderViewModel)
        {
            Order order = orderViewModel.ToOrder();
            User user = _userRepository.GetById(order.UserId);
            if (user == null)
            {
                //log exception
                throw new Exception($"User with id {order.UserId} was not found!");
            }

            order.User = user;
            int newOrderId = _orderRepository.Insert(order);
            if (newOrderId <= 0)
            {
                throw new Exception("Something went wrong while saving the new order");
            }
        }

        public void AddPizzaToOrder(PizzaOrderViewModel pizzaOrderViewModel)
        {
            //get the order
            Order order = _orderRepository.GetById(pizzaOrderViewModel.OrderId);
            if (order == null)
            {
                //log
                throw new Exception($"Order with id {pizzaOrderViewModel.OrderId} was not found!");
            }
            //get the pizza 
            Pizza pizza = _pizzaRepository.GetById(pizzaOrderViewModel.PizzaId);
            if (pizza == null)
            {
                //log
                throw new Exception($"Pizza with id {pizzaOrderViewModel.PizzaId} was not found!");
            }
            order.PizzaOrders.Add(new PizzaOrder
            {
                Pizza = pizza,
                PizzaSize = pizzaOrderViewModel.PizzaSize,
                Price = pizzaOrderViewModel.Price
            });
            _orderRepository.Update(order);
        }

        public OrderViewModel GetOrderForEditing(int id)
        {
            //get from DB
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                //log
                throw new Exception($"The order with id {id} was not found!");
            }

            return orderDb.ToOrderViewModel();
        }

        public void EditOrder(OrderViewModel orderViewModel)
        {
            Order orderDb = _orderRepository.GetById(orderViewModel.Id);
            if (orderDb == null)
            {
                //log
                throw new Exception($"The order with id {orderViewModel.Id} was not found!");
            }

            User userDb = _userRepository.GetById(orderViewModel.UserId);
            if (userDb == null)
            {
                //log
                throw new Exception($"The user with id {orderViewModel.UserId} was not found!");
            }
            //does not contain pizza orders
            Order editedOrder = orderViewModel.ToOrder();
            //we do not edit the pizza orders
            editedOrder.PizzaOrders = orderDb.PizzaOrders;
            editedOrder.User = userDb;
            _orderRepository.Update(editedOrder);
        }

        public void DeleteOrder(int id)
        {
            try
            {
                _orderRepository.DeleteById(id);
            }
            catch
            {
                //log
                throw; // rethrow
            }
        }

        public int GetOrderCount()
        {
            List<Order> ordersDb = _orderRepository.GetAll();
            return ordersDb.Count;
        }
    }
}
