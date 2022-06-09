using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.Event;
using Prolunteer.BusinessLogic.Implementation.Event.Models;
using Prolunteer.WebApp.Code.Base;
using System;

namespace Prolunteer.WebApp.Controllers
{
    public class EventController : BaseController
    {
        private readonly EventService Service;
        public EventController(ControllerDependencies controllerDependencies, EventService service)
            : base(controllerDependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        public IActionResult EventDetails(Guid id)
        {
            var model = Service.GetEventDetails(id);
            return View(model); 
        }

        [Authorize(Roles = "EventManager")]
        [HttpGet]
        public IActionResult MyEvents()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "EventManager")]
        public IActionResult GetMyEvents(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetEvents(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [Authorize(Roles = "EventManager")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            var model = new EventCreateModel();

            return View(model);
        }

        [Authorize(Roles = "EventManager")]
        [HttpPost]
        public IActionResult AddEvent(EventCreateModel model)
        {
            Service.CreateEvent(model);
            return RedirectToAction("MyEvents");
        }

        [HttpGet]
        [Authorize(Roles = "Volunteer")]
        public IActionResult AvailableEvents()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Volunteer")]
        public IActionResult GetAvailableEvents(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetAvailableEvents(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Volunteer")]
        public IActionResult AvailableEventDetails(Guid Id)
        {
            var model = Service.GetAvailableEventDetails(Id);

            return View(model);
        }

        [HttpDelete]
        [Authorize(Roles = "EventManager")]
        public IActionResult RemoveEvent(Guid id)
        {
            if (Service.RemoveEvent(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
