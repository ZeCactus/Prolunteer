using System;
using AutoMapper;
using Prolunteer.Common.DTOs;

namespace Prolunteer.BusinessLogic.Implementation.Account.Mappings.CustomLogic
{
    public class UserIdentityResolver : IValueResolver<object, object, Guid>
    {
        private readonly CurrentUserDTO CurrentUser;

        public UserIdentityResolver(CurrentUserDTO currentUser)
        {
            this.CurrentUser = currentUser;
        }

        public Guid Resolve(object source, object destination, Guid id, ResolutionContext context)
        {
            return CurrentUser.Id;
        }
    }
}
