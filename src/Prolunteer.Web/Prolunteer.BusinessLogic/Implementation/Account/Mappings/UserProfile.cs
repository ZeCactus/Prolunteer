using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.Account.Models;
using Prolunteer.BusinessLogic.Implementation.Account.Utilities;
using Prolunteer.Entities;
using Prolunteer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Account.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, Entities.User>()
                .ForMember(u => u.Id, u => u.MapFrom(rm => Guid.NewGuid()))
                .ForMember(u => u.PasswordHash, u => u.MapFrom(rm => PasswordUtilities.Hash(rm.Password)))
                .ForMember(u => u.UserRoles, u => u.MapFrom(rm => new HashSet<UserRole> { new UserRole { RoleId = rm.Role} }));

            CreateMap<Entities.User, UserViewModel>()
                .ForMember(uvm => uvm.Name, uvm => uvm.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(uvm => uvm.Role, uvm => uvm.MapFrom(u => ((RoleTypes)u.UserRoles.FirstOrDefault().RoleId).ToString()));
        }
    }
}
