using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class City : IEntity
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public Guid Id { get; set; }
        public Guid CountyId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual County County { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
