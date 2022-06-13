using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Models;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Controllers
{
    public class NotificationTemplateController : BaseController
    {
        private readonly NotificationTemplateService Service;

        public NotificationTemplateController(ControllerDependencies dependencies, NotificationTemplateService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditNotificationTemplate(int id)
        {
            if(id == 0)
            {
                return View();
            }

            var model = Service.GetNotificationTemplateForEdit(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditNotificationTemplate(NotificationTemplateEditModel model)
        {
            Service.EditNotificationTemplate(model);

            return RedirectToAction("EditNotificationTemplate");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetNotificationTemplatesForSelectList()
        {
            var model = Service.GetNotificationNamesAsListItemModelList();

            return Ok(model);
        }
    }
}
