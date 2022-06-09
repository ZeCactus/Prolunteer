using System.Collections.Generic;
using Prolunteer.Common;
using Prolunteer.Entities;

#nullable disable

namespace Prolunteer.Entities
{
    public partial class Certification : IEntity
    {
        public Certification()
        {
            RequiredCertifications = new HashSet<RequiredCertification>();
            UserCertifications = new HashSet<UserCertification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<RequiredCertification> RequiredCertifications { get; set; }
        public virtual ICollection<UserCertification> UserCertifications { get; set; }
    }
}
