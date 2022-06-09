using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.City;
using Prolunteer.BusinessLogic.Implementation.City.Models;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Prolunteer.WebApp.Controllers
{
    public class CityController : BaseController
    {

        private readonly CityService Service;
        public CityController(ControllerDependencies dependencies, CityService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Cities()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCities(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetCities(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveCity(Guid id)
        {
            if (Service.RemoveCity(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCity(CityCreateModel model)
        {
            Service.AddCity(model);

            return RedirectToAction("Cities");
        }

        [HttpGet]
        public IActionResult GetCitiesForSelect(Guid id)
        {
            var model = (id == Guid.Empty) ? Service.GetCitiesAsListItemModelList() : Service.GetCitiesAsListItemModelList(id);

            return Ok(model);
        }
    }
}
