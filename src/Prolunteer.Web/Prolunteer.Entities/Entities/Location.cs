using Prolunteer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.Entities
{
    public partial class Location : IEntity
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
