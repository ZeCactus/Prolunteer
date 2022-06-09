using System;

namespace Prolunteer.BusinessLogic.Implementation.Location.Models
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
    }
}
