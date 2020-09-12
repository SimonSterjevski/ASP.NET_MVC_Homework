using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.Mappers;
using SEDC.PizzaApp.Models.ViewModels;

namespace SEDC.PizzaApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            List<UserDDViewModel> users = StaticDb.Users.Select(x => UserMapper.ToUserDDViewModel(x)).ToList();
            return View(users);
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                UserDetailsViewModel user = StaticDb.Users.FirstOrDefault(x => x.Id == id).ToUserDetailsViewModel();
                if (user != null)
                {
                    return View(user);
                }
                return View("ResouceNotFound");
            }
            return View("BadRequest");
        }

        public IActionResult CreateUser()
        {
            User user = new User();
            return View(user.ToUserViewModel());
        }

        public IActionResult AddUser(UserViewModel user)
        {
            user.Id = ++StaticDb.UserId;
            StaticDb.Users.Add(user.ToUserDomainModel());
            return RedirectToAction("Index");
        }

        public IActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            User user = StaticDb.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return View("ResourceNotFound");
            }
            return View(user.ToUserViewModel());
        }

        public IActionResult EditChanges(UserViewModel user)
        {
            int index = StaticDb.Users.FindIndex(x => x.Id == user.Id);
            StaticDb.Users[index] = user.ToUserDomainModel();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int? id)
        {
            if (id != null)
            {
                User user = StaticDb.Users.FirstOrDefault(x => x.Id == id);
                List<User> usersWithOrders = StaticDb.Orders.Select(x => x.User).ToList();
                if (user == null)
                {
                    return View("ResourceNotFound");
                }
                if (usersWithOrders.Contains(user))
                {
                    return View("ActionForbidden");
                }
                return View(user.ToUserDetailsViewModel());
            }
            return View("BadRequest");
        }

        public IActionResult ConfirmDelete(UserDetailsViewModel user)
        {
            int index = StaticDb.Users.FindIndex(x => x.Id == user.Id);
            StaticDb.Users.RemoveAt(index);
            return RedirectToAction("Index");
        }
    }
}
