using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using Prolunteer.WebApp.Code.Base;
using System;

namespace Prolunteer.WebApp.Controllers
{
    public class VolunteerPositionController : BaseController
    {
        private readonly VolunteerPositionService Service;

        public VolunteerPositionController(ControllerDependencies dependencies, VolunteerPositionService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult VolunteerPositionDetails(Guid id)
        {
            var model = Service.GetVolunteerPositionDetails(id);

            return model != null ? View(model) : Redirect("Error_NotFound");
        }

        [HttpGet]
        [Authorize(Roles = "EventManager")]
        public IActionResult AddVolunteerPosition(Guid id)
        {
            var model = new VolunteerPositionCreateModel(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "EventManager")]
        public IActionResult AddVolunteerPosition(VolunteerPositionCreateModel model)
        {
            Service.AddVolunteerPosition(model);

            return RedirectToAction("EventDetails", "Event", new { id = model.EventId });
        }

        [HttpGet]
        [Authorize(Roles = "Volunteer")]
        public IActionResult Enroll(Guid id)
        {
            if (Service.Enroll(id))
            {
                return RedirectToAction("AvailableEvents", "Event");
            }

            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "EventManager")]
        public IActionResult RemoveVolunteerPosition(Guid id)
        {
            if(Service.RemoveVolunteerPosition(id) == false)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
