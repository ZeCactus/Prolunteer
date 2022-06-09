using Microsoft.AspNetCore.Http;
using System;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Models
{
    public class CertificationRequestModel
    {
        public int CertificationId { get; set; }
        public IFormFile Document { get; set; }
    }
}