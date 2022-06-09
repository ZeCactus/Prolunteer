using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.EventType;
using Prolunteer.BusinessLogic.Implementation.EventType.Models;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Controllers
{
    public class EventTypeController : BaseController
    {
        private readonly EventTypeService Service;

        public EventTypeController(ControllerDependencies dependencies, EventTypeService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EventTypes()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetEventTypes(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetEventTypes(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddEventType()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddEventType(EventTypeCreateModel model)
        {
            Service.AddEventType(model);

            return RedirectToAction("EventTypes");
        }


        [HttpGet]
        public IActionResult GetEventTypesForSelect()
        {
            var model = Service.GetEventTypesAsListItemModelList();

            return Ok(model);
        }
    }
}
