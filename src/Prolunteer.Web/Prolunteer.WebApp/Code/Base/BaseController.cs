using Microsoft.AspNetCore.Mvc;
using Prolunteer.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDTO CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUser = dependencies.CurrentUser;
        }
    }
}
