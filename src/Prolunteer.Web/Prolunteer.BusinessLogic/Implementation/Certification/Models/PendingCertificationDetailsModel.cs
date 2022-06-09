using System;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Models
{
    public class PendingCertificationDetailsModel
    {
        public int CertificationId { get; set; }
        public Guid UserId { get; set; }
        public string CertificationName { get; set; }
        public string UserName { get; set; }
        public string Document { get; set; }

    }
}
