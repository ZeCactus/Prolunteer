using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.Account;
using Prolunteer.BusinessLogic.Implementation.Account.Models;
using Prolunteer.Common.DTOs;
using Prolunteer.WebApp.Code.Base;
using Prolunteer.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Prolunteer.Entities.Enums;

namespace Prolunteer.WebApp.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly UserAccountService Service;

        public UserAccountController(ControllerDependencies dependencies, UserAccountService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            model.BirthDay = DateTime.Now.Date;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(model == null)
            {
                return View("Error_NotFound");
            }

            var user = Service.Register(model);
            await LogIn(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = Service.Login(model.EMail, model.Password);

            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Account()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            Service.ChangePassword(model);

            return RedirectToAction("Account");
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var model = Service.GetRolesAsListItemModelList();

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetUsers(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveUser(Guid userId)
        {
            if (Service.RemoveUser(userId))
            {
                return Ok();
            }

            return NotFound();
        }

        private async Task LogIn(CurrentUserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, ((RoleTypes)role).ToString()));
            }

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "ProlunteerCookies",
                    principal: principal
                );
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "ProlunteerCookies");
        }
    }
}
