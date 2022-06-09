using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Event.Models
{
    public class EventCreateModel
    {
        public int EventTypeId { get; set; }
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
