using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.Seeding;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Controllers
{
    public class SeedingController : BaseController
    {
        private readonly SeedingService Service;

        public SeedingController(ControllerDependencies dependencies, SeedingService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeedUsers()
        {
            Service.SeedUsers();

            return RedirectToAction("Index");
        }

        public IActionResult SeedCounties()
        {
            Service.SeedCounties();

            return RedirectToAction("Index");
        }

        public IActionResult SeedCities()
        {
            Service.SeedCities();

            return RedirectToAction("Index");
        }

        public IActionResult SeedCertifications()
        {
            Service.SeedCertifications();

            return RedirectToAction("Index");
        }

        public IActionResult SeedEventTypes()
        {
            Service.SeedEventTypes();

            return RedirectToAction("Index");
        }

        public IActionResult SeedUserCertifications()
        {
            Service.SeedUserCertifications();

            return RedirectToAction("Index");
        }

        public IActionResult SeedEvents()
        {
            Service.SeedEvents();

            return RedirectToAction("Index");
        }

        public IActionResult SeedVolunteerParticipations()
        {
            Service.SeedVolunteerParticipation();

            return RedirectToAction("Index");
        }

        public IActionResult TestEFCORE()
        {
            Service.TestEFCore();

            return Ok();
        }
    }
}
