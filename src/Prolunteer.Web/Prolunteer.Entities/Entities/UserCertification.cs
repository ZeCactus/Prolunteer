using Prolunteer.Common;
using Prolunteer.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class UserCertification : IEntity
    {
        public UserCertification()
        {
            UserCertificationDocuments = new HashSet<UserCertificationDocument>();
        }
        public Guid UserId { get; set; }
        public int CertificationId { get; set; }
        public bool Approved { get; set; }

        public virtual Certification Certification { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserCertificationDocument> UserCertificationDocuments { get; set; }
    }
}
