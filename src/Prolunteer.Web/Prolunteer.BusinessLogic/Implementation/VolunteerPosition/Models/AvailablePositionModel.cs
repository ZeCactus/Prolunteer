using System;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models
{
    public class AvailablePositionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int MaximumNrOfVolunteers { get; set; }
        public int EnrolledVolunteers { get; set; }
    }
}
