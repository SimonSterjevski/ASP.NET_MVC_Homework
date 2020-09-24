using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.EFImplementations
{
    public class FeedbackEfRepository : IFeedbackRepository
    {
        private PizzaAppContextDB _contextDB;

        public FeedbackEfRepository(PizzaAppContextDB efRepository)
        {
            _contextDB = efRepository;
        }
        public void DeleteById(int id)
        {
            Feedback feedback = _contextDB.Feedback.FirstOrDefault(x => x.Id == id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id {id} does not exist!");
            }
            _contextDB.Feedback.Remove(feedback);
            _contextDB.SaveChanges();
        }

        public List<Feedback> GetAll()
        {
            return _contextDB.Feedback.ToList();
        }

        public Feedback GetById(int id)
        {
            return _contextDB.Feedback.FirstOrDefault(x => x.Id == id);
        }

        public List<Feedback> GetFeedbackFromEmail(string email)
        {
            return _contextDB.Feedback.Where(x => x.Email == email).ToList();
        }

        public int Insert(Feedback entity)
        {
            _contextDB.Feedback.Add(entity);
            return _contextDB.SaveChanges();
        }

        public void UpdateEntity(Feedback entity)
        {
            Feedback feedback = _contextDB.Feedback.FirstOrDefault(x => x.Id == entity.Id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id {entity.Id} does not exist!");
            }
            _contextDB.Entry(feedback).CurrentValues.SetValues(entity);
            //_contextDB.Feedback.Update(entity);
            _contextDB.SaveChanges();
        }
    }
}
