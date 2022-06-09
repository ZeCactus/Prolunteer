using System;

namespace Prolunteer.BusinessLogic.Implementation.Account.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Role { get; set; }
    }
}
