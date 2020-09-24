using System;
using System.Collections.Generic;
using System.Text;
using SEDC.PizzaApp.DataAccess;
using SEDC.PizzaApp.Domain.Models;
using SEDC.PizzaApp.Mappers.User;
using SEDC.PizzaApp.Services.Interfaces;
using SEDC.PizzaApp.ViewModels.User;

namespace SEDC.PizzaApp.Services.Implementations
{
    public class UserService :IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)//used DI -> InjectionHelper
        {
            _userRepository = userRepository;
        }
        public List<UserDDViewModel> GetUsersForDropDown()
        {
            //call the DB
            List<User> users = _userRepository.GetAll();
            //do the mapping
            List<UserDDViewModel> userDdViewModels = new List<UserDDViewModel>();
            foreach (User user in users)
            {
                userDdViewModels.Add(user.ToUserDdViewModel());
            }

            return userDdViewModels;
        }
    }
}
