using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.User.Models;
using System;

namespace Prolunteer.BusinessLogic.Implementation.User.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, UserViewModel>();
                
                
        }
    }
}
