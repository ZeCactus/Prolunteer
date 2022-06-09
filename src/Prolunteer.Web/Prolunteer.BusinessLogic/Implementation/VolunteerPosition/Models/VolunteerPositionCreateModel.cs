using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models
{
    public class VolunteerPositionCreateModel
    {
        public VolunteerPositionCreateModel(Guid id)
        {
            this.EventId = id;
            this.RequiredCertifications = new List<int>();
        }

        public VolunteerPositionCreateModel()
        {
            this.RequiredCertifications = new List<int>();
        }

        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumNrOfVolunteers { get; set; }
        public List<int> RequiredCertifications { get; set; }
    }
}
