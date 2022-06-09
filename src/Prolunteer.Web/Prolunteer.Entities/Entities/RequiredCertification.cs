using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class RequiredCertification : IEntity
    {
        public Guid VolunteerPositionId { get; set; }
        public int CertificationId { get; set; }

        public virtual Certification Certification { get; set; }
        public virtual VolunteerPosition VolunteerPosition { get; set; }
    }
}
