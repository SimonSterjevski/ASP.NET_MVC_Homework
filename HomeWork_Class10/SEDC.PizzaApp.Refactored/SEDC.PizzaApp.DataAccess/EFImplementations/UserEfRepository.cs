using Microsoft.EntityFrameworkCore;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.EFImplementations
{
    public class UserEfRepository : IRepository<User>
    {

        private PizzaAppContextDB _contextDB;

        public UserEfRepository(PizzaAppContextDB efRepository)
        {
            _contextDB = efRepository;
        }
        public void DeleteById(int id)
        {
            User user = _contextDB.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new Exception($"Feedback with id {id} does not exist!");
            }
            _contextDB.Users.Remove(user);
            _contextDB.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _contextDB.Users
                .Include(x => x.Orders)
                .ThenInclude(x => x.PizzaOrders)
                .ThenInclude(x => x.Pizza)
                .ToList();
        }

        public User GetById(int id)
        {
            return _contextDB.Users
               .Include(x => x.Orders)
               .ThenInclude(x => x.PizzaOrders)
               .ThenInclude(x => x.Pizza)
               .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(User entity)
        {
            _contextDB.Users.Add(entity);
            return _contextDB.SaveChanges();
        }

        public void UpdateEntity(User entity)
        {
            User user = _contextDB.Users.FirstOrDefault(x => x.Id == entity.Id);
            if (user == null)
            {
                throw new Exception($"Feedback with id {entity.Id} does not exist!");
            }
            _contextDB.Entry(user).CurrentValues.SetValues(entity);
            //_contextDB.Users.Update(entity);
            _contextDB.SaveChanges();
        }
    }
}
