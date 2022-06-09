using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.County;
using Prolunteer.BusinessLogic.Implementation.County.Models;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Controllers
{
    public class CountyController : BaseController
    {
        private readonly CountyService Service;

        public CountyController(ControllerDependencies dependencies, CountyService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Counties()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCounties(string filter, int pageNumber = 1, int pageSize = 10)
        {
            var model = Service.GetCounties(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCounty()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCounty(CountyCreateModel model)
        {
            Service.AddCounty(model);

            return RedirectToAction("Counties");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveCounty(Guid id)
        {
            if (Service.RemoveCounty(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetCountiesForSelect()
        {
            var model = Service.GetCountiesAsListItemModelList();

            return Ok(model);
        }
    }
}
