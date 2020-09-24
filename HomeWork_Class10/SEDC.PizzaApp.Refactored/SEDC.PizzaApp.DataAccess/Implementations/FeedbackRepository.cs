using SEDC.PizzaApp.DataAccess.Interfaces;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.Implementations
{
    public class FeedbackRepository: IFeedbackRepository
    {

        public List<Feedback> GetAll()
        {
            return StaticDb.Feedback;
        }

        public Feedback GetById(int id)
        {
            return StaticDb.Feedback.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Feedback entity)
        {
            StaticDb.FeedbackId++;
            entity.Id = StaticDb.FeedbackId;
            StaticDb.Feedback.Add(entity);
            return entity.Id;
        }

        public void UpdateEntity(Feedback entity)
        {
            Feedback feedback = StaticDb.Feedback.FirstOrDefault(x => x.Id == entity.Id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id {entity.Id} does not exist!");
            }
            int index = StaticDb.Feedback.IndexOf(feedback);
            StaticDb.Feedback[index] = entity;
        }

        public void DeleteById(int id)
        {
            Feedback feedback = StaticDb.Feedback.FirstOrDefault(x => x.Id == id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with id {id} does not exist!");
            }
            StaticDb.Feedback.Remove(feedback);
        }

        public List<Feedback> GetFeedbackFromEmail(string email)
        {
            return StaticDb.Feedback.Where(x => x.Email == email).ToList();
        }
    }
}
