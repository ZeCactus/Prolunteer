using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class EventType : IEntity
    {
        public EventType()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
