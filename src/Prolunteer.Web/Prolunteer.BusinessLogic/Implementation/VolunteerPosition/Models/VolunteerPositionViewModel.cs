using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models
{
    public class VolunteerPositionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EnrolledVolunteers { get; set; }
        public int MaximumNrOfVolunteers { get; set; }
    }
}
