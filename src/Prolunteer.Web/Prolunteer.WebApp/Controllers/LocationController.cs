using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.Location;
using Prolunteer.BusinessLogic.Implementation.Location.Models;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Controllers
{
    public class LocationController : BaseController
    {
        private LocationService Service;
        public LocationController(ControllerDependencies dependencies, LocationService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Locations()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLocations(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetLocations(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddLocation(LocationCreateModel model)
        {
            Service.AddLocation(model);

            return RedirectToAction("Locations");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveLocation(Guid id)
        {
            if (Service.RemoveLocation(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetLocationsForSelect(Guid cityId)
        {
            var model = (cityId == Guid.Empty) ? Service.GetLocationsAsListItemModelList() : Service.GetLocationsAsListItemModelList(cityId);

            return Ok(model);
        }

    }
}
