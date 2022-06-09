using Prolunteer.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class User : IEntity
    {
        public User()
        {
            Events = new HashSet<Event>();
            UserCertifications = new HashSet<UserCertification>();
            UserRoles = new HashSet<UserRole>();
            VolunteerParticipations = new HashSet<VolunteerParticipation>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public DateTime BirthDay { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<UserCertification> UserCertifications { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<VolunteerParticipation> VolunteerParticipations { get; set; }
    }
}
