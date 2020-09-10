using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDC.PizzaApp.Models.Domain;
using SEDC.PizzaApp.Models.ViewModels;

namespace SEDC.PizzaApp.Models.Mappers
{
    public static class UserMapper
    {
        public static UserDDViewModel ToUserDDViewModel(User user)
        {
            return new UserDDViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

        public static UserDetailsViewModel ToUserDetailsViewModel(this User user)
        {
            return new UserDetailsViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Address = user.Address
            };
        }
    }
}
