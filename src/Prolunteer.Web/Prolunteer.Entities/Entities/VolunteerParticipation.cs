using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class VolunteerParticipation : IEntity
    {
        public Guid VolunteerPositionId { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual VolunteerPosition VolunteerPosition { get; set; }
    }
}
