using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class Event : IEntity
    {
        public Event()
        {
            VolunteerPositions = new HashSet<VolunteerPosition>();
        }

        public Guid Id { get; set; }
        public Guid OrganizerId { get; set; }
        public int EventTypeId { get; set; }
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Location Location { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual User Organizer { get; set; }
        public virtual ICollection<VolunteerPosition> VolunteerPositions { get; set; }
    }
}
