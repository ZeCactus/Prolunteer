using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class VolunteerPosition : IEntity
    {
        public VolunteerPosition()
        {
            RequiredCertifications = new HashSet<RequiredCertification>();
            VolunteerParticipations = new HashSet<VolunteerParticipation>();
        }

        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumNrOfVolunteers { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Event Event { get; set; }
        public virtual ICollection<RequiredCertification> RequiredCertifications { get; set; }
        public virtual ICollection<VolunteerParticipation> VolunteerParticipations { get; set; }
    }
}
