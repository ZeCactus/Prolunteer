using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.Common.DTOs
{
    public class CurrentUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<int> Roles { get; set; }
        public List<int> Certifications { get; set; }

        public CurrentUserDTO()
        {
            Roles = new List<int>();
            Certifications = new List<int>();
        }

    }
}
