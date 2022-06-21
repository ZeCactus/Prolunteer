using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.Entities.Enums
{
    public enum RoleTypes : int
    {
        Volunteer = 1,
        [DisplayName("Event Manager")]
        EventManager = 2,
        Admin = 3
    }
}
