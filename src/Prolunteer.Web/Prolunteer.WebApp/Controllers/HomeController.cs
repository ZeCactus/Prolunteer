using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prolunteer.DataAccess.EntityFramework;
using Prolunteer.WebApp.Code.Base;
using Prolunteer.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProlunteerContext _context;

        public HomeController(ILogger<HomeController> logger, ProlunteerContext Context, ControllerDependencies dependencies)
            : base(dependencies)
        {
            _logger = logger;
            _context = Context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}