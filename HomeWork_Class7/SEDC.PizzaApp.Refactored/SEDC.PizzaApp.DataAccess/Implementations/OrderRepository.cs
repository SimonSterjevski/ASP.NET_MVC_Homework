using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDC.PizzaApp.Domain.Models;

namespace SEDC.PizzaApp.DataAccess.Implementations
{
    public class OrderRepository : IRepository<Order>
    {
        public OrderRepository()
        {
            
        }
        public List<Order> GetAll()
        {
            //return domain models (all records from the table in DB)
            return StaticDb.Orders;
        }

        public Order GetById(int id)
        {
            //returns one record from a table in DB (by id)
            return StaticDb.Orders.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Order entity)
        {
            StaticDb.OrderId++;
            entity.Id = StaticDb.OrderId;
            //send the record to the DB
            StaticDb.Orders.Add(entity);
            return entity.Id;
        }

        public void Update(Order entity)
        {
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == entity.Id);
            if (order == null)
            {
                //log the exception
                throw new Exception($"Order with id {entity.Id} does not exist!");
            }
            //update the record in DB
            int index = StaticDb.Orders.IndexOf(order);
            StaticDb.Orders[index] = entity;
        }

        public void DeleteById(int id)
        {
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id ==id);
            if (order == null)
            {
                throw new Exception($"Order with id {id} does not exist!");
            }
            //delete record from DB
            StaticDb.Orders.Remove(order);
        }
    }
}
