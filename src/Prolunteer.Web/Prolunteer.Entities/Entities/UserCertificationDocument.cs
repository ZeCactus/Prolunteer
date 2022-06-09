using Prolunteer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.Entities
{
    public partial class UserCertificationDocument : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int CertificationId { get; set; }
        public byte[] Document { get; set; }
        public virtual UserCertification UserCertification { get; set; }
    }
}
