using Prolunteer.BusinessLogic.Implementation.User.Models;
using System;
using System.Collections.Generic;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models
{
    public class VolunteerPositionDetailsModel
    {
        public VolunteerPositionDetailsModel()
        {
            this.Volunteers = new List<UserViewModel>();
        }
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumNrOfVolunteers { get; set; }
        public List<UserViewModel> Volunteers { get; set; }
    }
}
