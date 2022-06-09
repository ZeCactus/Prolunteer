using Prolunteer.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserDTO CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDTO currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
