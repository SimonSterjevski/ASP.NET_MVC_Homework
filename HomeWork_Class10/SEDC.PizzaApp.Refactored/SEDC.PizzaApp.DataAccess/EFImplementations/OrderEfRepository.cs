using Microsoft.EntityFrameworkCore;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.EFImplementations
{
    public class OrderEfRepository : IRepository<Order>
    {

        private PizzaAppContextDB _contextDB;

        public OrderEfRepository(PizzaAppContextDB efRepository)
        {
            _contextDB = efRepository;
        }
        public void DeleteById(int id)
        {
            Order order = _contextDB.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new Exception($"Feedback with id {id} does not exist!");
            }
            _contextDB.Orders.Remove(order);
            _contextDB.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _contextDB.Orders
                .Include(x => x.PizzaOrders)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.User)
                .ToList();
        }

        public Order GetById(int id)
        {
            return _contextDB.Orders
                .Include(x => x.PizzaOrders)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Order entity)
        {
            _contextDB.Orders.Add(entity);
            return _contextDB.SaveChanges();
        }

        public void UpdateEntity(Order entity)
        {
            Order order = _contextDB.Orders.FirstOrDefault(x => x.Id == entity.Id);
            if (order == null)
            {
                throw new Exception($"Feedback with id {entity.Id} does not exist!");
            }
            _contextDB.Entry(order).CurrentValues.SetValues(entity);
            //_contextDB.Orders.Update(entity);
            _contextDB.SaveChanges();
        }
    }
}
