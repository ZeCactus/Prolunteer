using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolunteer.BusinessLogic.Implementation.Certification;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using Prolunteer.WebApp.Code.Base;
using System;

namespace Prolunteer.WebApp.Controllers
{
    public class CertificationController : BaseController
    {
        private readonly CertificationService Service;

        public CertificationController(ControllerDependencies dependencies, CertificationService service)
            : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Volunteer")]
        public IActionResult RequestCertification()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Volunteer")]
        public IActionResult RequestCertification(CertificationRequestModel model)
        {
            Service.RequestCertification(model);

            return RedirectToAction("Account", "UserAccount");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Certifications()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCertifications(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetCertifications(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCertification()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCertification(CertificationCreateModel model)
        {
            Service.AddCertification(model);

            return RedirectToAction("Certifications");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult PendingCertifications()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPendingCertifications(int pageNumber, int pageSize, string filter)
        {
            var model = Service.GetPendingCertifications(pageNumber, pageSize, filter);

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult PendingCertificationDetails(Guid userId, int certificationId)
        {
            var model = Service.GetPendingCertification(userId, certificationId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult PendingCertificationDocument(Guid userId, int certificationId)
        {
            var document = Service.GetPendingCertificationDocument(userId, certificationId);

            return Ok(document);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAvailableCertificationsForSelect()
        {
            var model = Service.GetCertificationsAvailableForUserAsListItemModelList();

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveCertificationRequest(Guid userId, int certificationId)
        {
            Service.ApproveCertificationRequest(userId, certificationId);

            return RedirectToAction("PendingCertifications");
        }

        [HttpGet]
        public IActionResult GetCertificationsForSelect()
        {
            var model = Service.GetCertificationsAsListItemModelList();

            return Ok(model);
        }
    }
}