using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Event.Models
{
    public class VolunteerEventVM
    {
        public Guid Id { get; set; }
        public string Organizer { get; set; }
        public string EventType { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PositionName { get; set; }
        public string Image { get; set; }
    }
}
